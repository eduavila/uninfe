﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Xml;
using NFe.Components;
using NFe.Settings;
using NFe.Certificado;

namespace NFe.Service
{
    public class TaskEventosCTe : TaskAbst
    {
        public TaskEventosCTe()
        {
            Servico = Servicos.RecepcaoEventoCTe;
        }

        #region Classe com os dados do XML do registro de eventos
        private DadosenvEvento dadosEnvEvento;
        private int tpEmis = 0;
        #endregion

        #region Execute
        public override void Execute()
        {
            int emp = Empresas.FindEmpresaByThread();

            try
            {
                dadosEnvEvento = new DadosenvEvento();
                //Ler o XML para pegar parâmetros de envio
                EnvEvento(emp, NomeArquivoXML);

                int currentEvento = Convert.ToInt32(dadosEnvEvento.eventos[0].tpEvento);

                foreach (Evento item in dadosEnvEvento.eventos)
                {
                    tpEmis = Convert.ToInt32(item.chNFe.Substring(34, 1)); //vai pegar o ambiente da Chave da Nfe autorizada p/ corrigir caso emitida em modo SCAN - Renan
                    if (currentEvento != Convert.ToInt32(item.tpEvento))
                        throw new Exception(string.Format("Não é possivel mesclar tipos de eventos dentro de um mesmo xml/txt de eventos. O tipo de evento neste xml/txt é {0}", currentEvento));
                }

                //Pegar o estado da chave, pois na cOrgao pode vir o estado 91 - Wandreuy 22/08/2012
                int cOrgao = dadosEnvEvento.eventos[0].cOrgao;
                int ufParaWS = cOrgao;

                //Se o cOrgao for igual a 91 tenho que mudar a ufParaWS para que na hora de buscar o WSDL para conectar ao serviço, ele consiga encontrar. Wandrey 23/01/2013
                if (cOrgao == 91)
                    ufParaWS = Convert.ToInt32(dadosEnvEvento.eventos[0].chNFe.Substring(0, 2));

                //Definir o objeto do WebService
                WebServiceProxy wsProxy = 
                    ConfiguracaoApp.DefinirWS(  Servico, 
                                                emp, 
                                                ufParaWS, 
                                                dadosEnvEvento.eventos[0].tpAmb, 
                                                tpEmis, 
                                                string.Empty);
                object oRecepcaoEvento = wsProxy.CriarObjeto(wsProxy.NomeClasseWS);//NomeClasseWS(Servico, ufParaWS));
                object oCabecMsg = wsProxy.CriarObjeto(NomeClasseCabecWS(cOrgao, Servico));

                wsProxy.SetProp(oCabecMsg, "cUF", cOrgao.ToString());
                wsProxy.SetProp(oCabecMsg, "versaoDados", NFe.ConvertTxt.versoes.VersaoXMLCTeEvento);

                //Criar objeto da classe de assinatura digital
                AssinaturaDigital oAD = new AssinaturaDigital();

                //Assinar o XML
                oAD.Assinar(NomeArquivoXML, emp, cOrgao);

                oInvocarObj.Invocar(wsProxy, 
                                    oRecepcaoEvento, 
                                    wsProxy.NomeMetodoWS[0],//NomeMetodoWS(Servico, ufParaWS), 
                                    oCabecMsg, 
                                    this, 
                                    Propriedade.ExtEnvio.PedEve.Replace(".xml", ""), 
                                    Propriedade.ExtRetorno.Eve.Replace(".xml", ""));

                //Ler o retorno
                LerRetornoEvento(emp);
            }
            catch (Exception ex)
            {
                try
                {
                    //Gravar o arquivo de erro de retorno para o ERP, caso ocorra
                    TFunctions.GravarArqErroServico(NomeArquivoXML, Propriedade.ExtEnvio.PedEve, Propriedade.ExtRetorno.Eve_ERR, ex);
                }
                catch
                {
                    //Se falhou algo na hora de gravar o retorno .ERR (de erro) para o ERP, infelizmente não posso fazer mais nada.
                    //Wandrey 09/03/2010
                }
            }
            finally
            {
                try
                {
                    Functions.DeletarArquivo(NomeArquivoXML);
                }
                catch
                {
                    //Se falhou algo na hora de deletar o XML de evento, infelizmente
                    //não posso fazer mais nada, o UniNFe vai tentar mandar o arquivo novamente para o webservice, pois ainda não foi excluido.
                    //Wandrey 09/03/2010
                }
            }
        }
        #endregion

        #region EnvEvento()
        /// <summary>
        /// Fazer a leitura dos dados do XML
        /// </summary>
        /// <param name="emp">Código da Empresa</param>
        /// <param name="arquivoXML">Arquivo XML</param>
        private void EnvEvento(int emp, string arquivoXML)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(arquivoXML);

            XmlNodeList envEventoList = doc.GetElementsByTagName("infEvento");

            foreach (XmlNode envEventoNode in envEventoList)
            {
                XmlElement envEventoElemento = (XmlElement)envEventoNode;

                dadosEnvEvento.eventos.Add(new Evento());
                dadosEnvEvento.eventos[this.dadosEnvEvento.eventos.Count - 1].tpEvento = envEventoElemento.GetElementsByTagName("tpEvento")[0].InnerText;
                dadosEnvEvento.eventos[this.dadosEnvEvento.eventos.Count - 1].tpAmb = Convert.ToInt32("0" + envEventoElemento.GetElementsByTagName("tpAmb")[0].InnerText);
                dadosEnvEvento.eventos[this.dadosEnvEvento.eventos.Count - 1].cOrgao = Convert.ToInt32("0" + envEventoElemento.GetElementsByTagName("cOrgao")[0].InnerText);
                dadosEnvEvento.eventos[this.dadosEnvEvento.eventos.Count - 1].chNFe = envEventoElemento.GetElementsByTagName("chCTe")[0].InnerText;
                dadosEnvEvento.eventos[this.dadosEnvEvento.eventos.Count - 1].nSeqEvento = Convert.ToInt32("0" + envEventoElemento.GetElementsByTagName("nSeqEvento")[0].InnerText);
            }
        }
        #endregion

        #region LerRetornoEvento
        private void LerRetornoEvento(int emp)
        {
            //<<<UTF8 -> tem acentuacao no retorno
            TextReader txt = new StreamReader(NomeArquivoXML, Encoding.Default);
            XmlDocument docEventoOriginal = new XmlDocument();
            docEventoOriginal.Load(Functions.StringXmlToStreamUTF8(txt.ReadToEnd()));
            txt.Close();

            /*
            vStrXmlRetorno = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                "<retEventoCTe xmlns=\"http://www.portalfiscal.inf.br/cte\" versao=\"2.00\">" +
                "  <infEvento Id=\"ID342130000096132\">" +
                "    <tpAmb>2</tpAmb>" +
                "    <verAplic>RS20130820221405</verAplic>" +
                "    <cOrgao>42</cOrgao>" +
                "    <cStat>135</cStat>" +
                "    <xMotivo>Evento registrado e vinculado a CT-e</xMotivo>" +
                "    <chCTe>42131175892067000187570040000001091211932160</chCTe>" +
                "    <tpEvento>110111</tpEvento>" +
                "    <xEvento>Cancelamento</xEvento>" +
                "    <nSeqEvento>1</nSeqEvento>" +
                "    <dhRegEvento>2013-11-13T15:27:12</dhRegEvento>" +
                "    <nProt>342130000096132</nProt>" +
                "</infEvento>" +
                "</retEventoCTe>";
            */

            MemoryStream msXml = Functions.StringXmlToStreamUTF8(vStrXmlRetorno);
            XmlDocument doc = new XmlDocument();
            doc.Load(msXml);

            XmlNodeList retEnvRetornoList = doc.GetElementsByTagName("retEventoCTe");

            foreach (XmlNode retConsSitNode in retEnvRetornoList)
            {
                XmlElement retConsSitElemento = (XmlElement)retConsSitNode;

                XmlNodeList envEventosList = doc.GetElementsByTagName("infEvento");
                for (int i = 0; i < envEventosList.Count; ++i)
                {
                    XmlElement eleRetorno = envEventosList.Item(i) as XmlElement;

                    string cStatCons = eleRetorno.GetElementsByTagName("cStat")[0].InnerText;

                    if (cStatCons == "134" || cStatCons == "135" || cStatCons == "136")
                    {
                        string chCTe = eleRetorno.GetElementsByTagName("chCTe")[0].InnerText;
                        Int32 nSeqEvento = Convert.ToInt32("0" + eleRetorno.GetElementsByTagName("nSeqEvento")[0].InnerText);
                        string tpEvento = eleRetorno.GetElementsByTagName("tpEvento")[0].InnerText;
                        string Id = NFe.Components.NFeStrConstants.ID + tpEvento + chCTe + nSeqEvento.ToString("00");
                        ///
                        ///procura no Xml de envio pelo Id retornado
                        ///nao sei se a Sefaz retorna na ordem em que foi enviado, então é melhor pesquisar
                        foreach (XmlNode env in docEventoOriginal.GetElementsByTagName("infEvento"))
                        {
                            string Idd = env.Attributes.GetNamedItem("Id").Value;
                            if (Idd == Id)
                            {
                                DateTime dhRegEvento = Functions.GetDateTime(eleRetorno.GetElementsByTagName("dhRegEvento")[0].InnerText);

                                //Gerar o arquivo XML de distribuição do evento, retornando o nome completo do arquivo gravado
                                oGerarXML.XmlDistEventoCTe(emp, chCTe, nSeqEvento, Convert.ToInt32(tpEvento), env.ParentNode.OuterXml, eleRetorno.OuterXml, dhRegEvento);

                                switch ((NFe.ConvertTxt.tpEventos)Convert.ToInt32(tpEvento))
                                {
                                    case ConvertTxt.tpEventos.tpEvCancelamentoNFe:
                                    case ConvertTxt.tpEventos.tpEvCCe:
                                        try
                                        {
                                            NFe.Service.TFunctions.ExecutaUniDanfe(oGerarXML.NomeArqGerado, DateTime.Today, Empresas.Configuracoes[emp]);
                                        }
                                        catch (Exception ex)
                                        {
                                            Auxiliar.WriteLog("TaskEventosCTe: " + ex.Message);
                                        }
                                        break;
                                }

                                break;
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}