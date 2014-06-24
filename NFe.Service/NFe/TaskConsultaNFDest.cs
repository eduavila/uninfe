﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Xml;

using NFe.Components;
using NFe.Settings;
using NFe.Certificado;
using NFe.Exceptions;

namespace NFe.Service
{
    public class TaskConsultaNFDest : TaskAbst
    {
        #region Classe com os dados do XML do registro de consulta da nfe do destinatario
        private DadosConsultaNFeDest oDadosConsultaNFeDest = new DadosConsultaNFeDest();
        #endregion

        #region Execute
        public override void Execute()
        {
            int emp = Functions.FindEmpresaByThread();
            Servico = Servicos.ConsultaNFDest;

            try
            {
                oDadosConsultaNFeDest = new DadosConsultaNFeDest();
                //Ler o XML para pegar parâmetros de envio
                //LerXML oLer = new LerXML();
                /*oLer.*/
                EnvConsultaNFeDest(emp, NomeArquivoXML);

                if (vXmlNfeDadosMsgEhXML)
                {
                    int cUF = Empresa.Configuracoes[emp].UFCod;

                    //cUF = 43;

                    //Definir o objeto do WebService
                    WebServiceProxy wsProxy = ConfiguracaoApp.DefinirWS(
                        Servico,
                        emp,
                        cUF,
                        oDadosConsultaNFeDest.tpAmb,
                        1, string.Empty);

                    //Criar objetos das classes dos serviços dos webservices do SEFAZ
                    object oConsNFDestEvento;
                    //if (NFe.Components.Propriedade.TipoAmbiente.taHomologacao == oDadosConsultaNFeDest.tpAmb)
                    //    oConsNFDestEvento = wsProxy.CriarObjeto("NfeConsultaDest");
                    //else
                    oConsNFDestEvento = wsProxy.CriarObjeto("NFeConsultaDest");
                    object oCabecMsg = wsProxy.CriarObjeto(NomeClasseCabecWS(cUF, Servico));

                    //Atribuir conteúdo para duas propriedades da classe nfeCabecMsg
                    wsProxy.SetProp(oCabecMsg, "cUF", cUF.ToString());
                    //                  try
                    //                  {
                    //                      wsProxy.SetProp(oCabecMsg, "indComp", "0");
                    //                  }
                    //                  catch { }
                    wsProxy.SetProp(oCabecMsg, "versaoDados", NFe.ConvertTxt.versoes.VersaoXMLEnvConsultaNFeDest);

                    //Criar objeto da classe de assinatura digital
                    //AssinaturaDigital oAD = new AssinaturaDigital();

                    //Assinar o XML
                    //oAD.Assinar(NomeArquivoXML, emp);

                    //Invocar o método que envia o XML para o SEFAZ
                    oInvocarObj.Invocar(wsProxy,
                                        oConsNFDestEvento,
                                        "nfeConsultaNFDest",
                                        oCabecMsg,
                                        this,
                                        Propriedade.ExtEnvio.ConsNFeDest_XML.Replace(".xml", ""),
                                        Propriedade.ExtRetorno.retConsNFeDest_XML.Replace(".xml", ""));

                    //Ler o retorno
                    this.LerRetornoConsultaNFeDest(emp);
                }
                else
                {
                    // Gerar o XML de eventos a partir do TXT gerado pelo ERP
                    oGerarXML.EnvioConsultaNFeDest(Functions.ExtrairNomeArq(NomeArquivoXML, Propriedade.ExtEnvio.ConsNFeDest_TXT) + Propriedade.ExtEnvio.ConsNFeDest_XML, /*oLer.*/oDadosConsultaNFeDest);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    var ExtRet = vXmlNfeDadosMsgEhXML ? Propriedade.ExtEnvio.ConsNFeDest_XML : Propriedade.ExtEnvio.ConsNFeDest_TXT;
                    //Gravar o arquivo de erro de retorno para o ERP, caso ocorra
                    TFunctions.GravarArqErroServico(NomeArquivoXML, ExtRet, Propriedade.ExtRetorno.retConsNFeDest_ERR, ex);
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

        #region EnvConsultaNFeDest
        private void EnvConsultaNFeDest(int emp, string arquivoXML)
        {
            if (Path.GetExtension(arquivoXML).ToLower() == ".txt")
            {
                /// tpAmb|2
                /// CNPJ|10290739000139 
                /// indNFe|0
                /// indEmi|0
                /// ultNSU|00000001
                List<string> cLinhas = Functions.LerArquivo(arquivoXML);
                foreach (string cTexto in cLinhas)
                {
                    string[] dados = cTexto.Split('|');
                    if (dados.GetLength(0) <= 1) continue;

                    switch (dados[0].ToLower())
                    {
                        case "tpamb":
                            this.oDadosConsultaNFeDest.tpAmb = Convert.ToInt32("0" + dados[1].Trim());
                            break;
                        case "cnpj":
                            this.oDadosConsultaNFeDest.CNPJ = dados[1].Trim();
                            break;
                        case "indnfe":
                            this.oDadosConsultaNFeDest.indNFe = Convert.ToInt32("0" + dados[1].Trim());
                            break;
                        case "indemi":
                            this.oDadosConsultaNFeDest.indEmi = Convert.ToInt32("0" + dados[1].Trim());
                            break;
                        case "ultnsu":
                            this.oDadosConsultaNFeDest.ultNSU = dados[1].Trim();
                            break;
                    }
                }
            }
            else
            {
                //<?xml version="1.0" encoding="UTF-8"?>
                //<consNFeDest versao="1.00" xmlns="http://www.portalfiscal.inf.br/nfe">
                //      <tpAmb>2</tpAmb>
                //      <xServ>CONSULTAR NFE DEST</xServ>
                //      <CNPJ>10290739000139</CNPJ>
                //      <indNFe>0</indNFe>
                //      <indEmi>0</indEmi>
                //      <ultNSU>000000000000000</ultNSU>
                //</consNFeDest>

                XmlDocument doc = new XmlDocument();
                doc.Load(arquivoXML);

                XmlNodeList envEventoList = doc.GetElementsByTagName("consNFeDest");

                foreach (XmlNode envEventoNode in envEventoList)
                {
                    XmlElement envEventoElemento = (XmlElement)envEventoNode;

                    this.oDadosConsultaNFeDest.tpAmb = Convert.ToInt32("0" + envEventoElemento.GetElementsByTagName("tpAmb")[0].InnerText);
                    this.oDadosConsultaNFeDest.CNPJ = envEventoElemento.GetElementsByTagName("CNPJ")[0].InnerText;
                    this.oDadosConsultaNFeDest.indNFe = Convert.ToInt32("0" + envEventoElemento.GetElementsByTagName("indNFe")[0].InnerText);
                    this.oDadosConsultaNFeDest.indEmi = Convert.ToInt32("0" + envEventoElemento.GetElementsByTagName("indEmi")[0].InnerText);
                    this.oDadosConsultaNFeDest.ultNSU = envEventoElemento.GetElementsByTagName("ultNSU")[0].InnerText;
                }
            }
        }
        #endregion

        #region LerRetornoConsultaNFeDest
        private void LerRetornoConsultaNFeDest(int emp)
        {
            /*
<retConsNFeDest versao="1.01" xmlns="http://www.portalfiscal.inf.br/nfe">
  <tpAmb>2</tpAmb>
  <verAplic>RS20120430083814</verAplic>
  <cStat>137</cStat>
  <xMotivo>Nenhum documento localizado para o destinatário</xMotivo>
  <dhResp>2012-07-28T23:29:41</dhResp>
</retConsNFeDest>
             * 
             * 
<retConsNFeDest versao="1.01" xmlns="http://www.portalfiscal.inf.br/nfe">
  <tpAmb>2</tpAmb>
  <verAplic>RS20120430083814</verAplic>
  <cStat>138</cStat>
  <xMotivo>Documento localizado para o destinatário.</xMotivo>
  <dhResp>2012-07-29T18:49:03</dhResp>
  <indCont>0</indCont>
  <ultNSU>100000</ultNSU>
  <ret>
    <resNFe NSU="1234567">
      <chNFe>43120479999975000104550000000884201000884208</chNFe>
      <CNPJ>79999975000104</CNPJ>
      <xNome>NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL</xNome>
      <IE>123456789</IE>
      <dEmi>2012-05-29</dEmi>
      <tpNF>1</tpNF>
      <vNF>1000.00</vNF>
      <digVal>1eyXp3r2WjqQrATl+kaxz8Pe0bk=</digVal>
      <dhRecbto>2012-05-29T09:30:47</dhRecbto>
      <cSitNFe>1</cSitNFe>
      <cSitConf>0</cSitConf>
    </resNFe>
  </ret>
  <ret>
    <resNFe NSU="1235555">
        <chNFe>43120479999975000104550000000885201000885201</chNFe>
        <CNPJ>79999975000104</CNPJ>
        <xNome>NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL</xNome>
        <IE>123456789</IE>
        <dEmi>2012-05-30</dEmi>
        <tpNF>1</tpNF>
        <vNF>2000.00</vNF>
        <digVal>1eyXp3r2WjqQrATl+kaxz8Pe0bk=</digVal>
        <dhRecbto>2012-05-30T07:00:00</dhRecbto>
        <cSitNFe>1</cSitNFe>
        <cSitConf>0</cSitConf>
    </resNFe>
  </ret>
  <ret>
    <resCCe NSU="1234571">
      <chNFe>43120479999975000104550000000884201000884208</chNFe>
      <dhEvento>22012-05-29T013:10:07-03:00</dhEvento>
      <tpEvento>110110</tpEvento>
      <nSeqEvento>1</nSeqEvento>
      <descEvento>Carta de Correção</descEvento>
      <xCorrecao>A unidade de comercializacao do item 1 deve ser PC</xCorrecao>
      <tpNF>1</tpNF>
      <dhRecbto>22012-05-29T015:10:07</dhRecbto>
    </resCCe>
  </ret>
  <ret>
    <resCanc NSU="1234768">
      <chNFe>43120479999975000104550000000884201000884208</chNFe>
      <CNPJ>79999975000104</CNPJ>
      <xNome>NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL</xNome>
      <IE>123456789</IE>
      <dEmi>2012-05-29</dEmi>
      <tpNF>1</tpNF>
      <vNF>1000.00</vNF>
      <digVal>1eyXp3r2WjqQrATl+kaxz8Pe0bk=</digVal>
      <dhRecbto>2012-05-30T08:30:00</dhRecbto>
      <cSitNFe>3</cSitNFe>
      <cSitConf>0</cSitConf>
    </resCanc>
  </ret>
</retConsNFeDest>

             */
        }
        #endregion
    }
}
