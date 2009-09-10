﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using UniNFeLibrary;
using UniNFeLibrary.Enums;

namespace uninfe
{
    public class GerarXML : absGerarXML
    {
        #region IniciarLoteNfe()
        /// <summary>
        /// Inicia a string do XML do Lote de notas fiscais
        /// </summary>
        /// <param name="intNumeroLote">Número do lote que será enviado</param>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>15/04/2009</date>
        protected override void IniciarLoteNfe(Int32 intNumeroLote)
        {
            string cVersaoDados = "1.10";

            strXMLLoteNfe = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
            strXMLLoteNfe += "<enviNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" xmlns:ds=\"http://www.w3.org/2000/09/xmldsig#\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" versao=\"" + cVersaoDados + "\">";
            strXMLLoteNfe += "<idLote>" + intNumeroLote.ToString("000000000000000") + "</idLote>";
        }

        #endregion

        #region InserirNFeLote()
        /// <summary>
        /// Insere o XML de Nota Fiscal passado por parâmetro na string do XML de Lote de NFe
        /// </summary>
        /// <param name="strArquivoNfe">Nome do arquivo XML de nota fiscal eletrônica a ser inserido no lote</param>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>15/04/2009</date>
        protected override void InserirNFeLote(string strArquivoNfe)
        {
            try
            {
                string vNfeDadosMsg = this.oAux.XmlToString(strArquivoNfe);

                //Separar somente o conteúdo a partir da tag <NFe> até </NFe>
                Int32 nPosI = vNfeDadosMsg.IndexOf("<NFe");
                Int32 nPosF = vNfeDadosMsg.Length - nPosI;
                strXMLLoteNfe += vNfeDadosMsg.Substring(nPosI, nPosF);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region EncerrarLoteNfe()
        /// <summary>
        /// Encerra a string do XML de lote de notas fiscais eletrônicas
        /// </summary>
        /// <param name="intNumeroLote">Número do lote que será enviado</param>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>15/04/2009</date>
        protected override void EncerrarLoteNfe(Int32 intNumeroLote)
        {
            strXMLLoteNfe += "</enviNFe>";

            try
            {
                this.GerarXMLLote(intNumeroLote);
            }
            catch (IOException ex)
            {
                throw (ex);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region CabecMsg()
        /// <summary>
        /// Gera uma string com o XML do cabeçalho dos dados a serem enviados para os serviços da NFe
        /// </summary>
        /// <param name="pVersaoDados">
        /// Versão do arquivo XML que será enviado para os WebServices. Esta versão varia de serviço para
        /// serviço e deve ser consultada no manual de integração da NFE
        /// </param>
        /// <returns>
        /// Retorna uma string com o XML do cabeçalho dos dados a serem enviados para os serviços da NFe
        /// </returns>
        /// <example>
        /// vCabecMSG = GerarXMLCabecMsg("1.07");
        /// MessageBox.Show( vCabecMSG );
        /// 
        /// //O conteúdo que será demonstrado no MessageBox é:
        /// //
        /// //  <?xml version="1.0" encoding="UTF-8" ?>
        /// //  <cabecMsg xmlns="http://www.portalfiscal.inf.br/nfe" versao="1.02">
        /// //     <versaoDados>1.07</versaoDados>
        /// //  </cabecMsg>
        /// </example>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>04/06/2008</date>
        public override string CabecMsg(string VersaoDados)
        {
            string vCabecMsg = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><cabecMsg xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"" + ConfiguracaoApp.VersaoXMLCabecMsg + "\"><versaoDados>" + VersaoDados + "</versaoDados></cabecMsg>";

            return vCabecMsg;
        }
        #endregion

        #region StatusServico()
        /// <summary>
        /// Criar um arquivo XML com a estrutura necessária para consultar o status do serviço
        /// </summary>
        /// <returns>Retorna o caminho e nome do arquivo criado</returns>
        /// <example>
        /// string vPastaArq = this.CriaArqXMLStatusServico();
        /// </example>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>17/06/2008</date>
        public override string StatusServico(int tpEmis)
        {
            return this.StatusServico(tpEmis, ConfiguracaoApp.UFCod);
        }
        #endregion

        #region StatusServico() - Sobrecarga
        public override string StatusServico(int tpEmis, int cUF)
        {
            //TODO: CONFIG
            string vDadosMsg = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>"
                + "<consStatServ xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" versao=\"1.07\" xmlns=\"" + ConfiguracaoApp.nsURI + "\">"
                + "<tpAmb>" + ConfiguracaoApp.tpAmb.ToString() + "</tpAmb>"
                + "<cUF>" + cUF.ToString() + "</cUF>"
                + "<tpEmis>" + tpEmis.ToString() + "</tpEmis>"  //danasa 9-2009
                + "<xServ>STATUS</xServ>"
                + "</consStatServ>";

            string _arquivo_saida = ConfiguracaoApp.vPastaXMLEnvio + "\\" +
                                    DateTime.Now.ToString("yyyyMMddThhmmss") +
                                    ExtXml.PedSta;// "-ped-sta.xml";

            StreamWriter SW = File.CreateText(_arquivo_saida);
            SW.Write(vDadosMsg);
            SW.Close();

            return _arquivo_saida;
        } 
        #endregion

        #region ConsultaCadastro()

        /// <summary>
        /// Cria um arquivo XML com a estrutura necessária para consultar um cadastro
        /// Voce deve preencher o estado e mais um dos tres itens: CPNJ, IE ou CPF
        /// </summary>
        /// <param name="uf">Sigla do UF do cadastro a ser consultado. Tem que ter duas letras. SU para suframa.</param>
        /// <param name="cnpj"></param>
        /// <param name="ie"></param>
        /// <param name="cpf"></param>
        /// <returns>Retorna o caminho e nome do arquivo criado</returns>
        /// <by>Marcos Diez</by>
        /// <date>29/08/2009</date>
        public string ConsultaCadastro(string uf, string cnpj, string ie, string cpf)
        {
            string header = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><ConsCad xmlns=\"" +
                ConfiguracaoApp.nsURI +
                "\" versao=\"1.01\"><infCons><xServ>CONS-CAD</xServ>";
            string footer = "</infCons></ConsCad>";

            cnpj = OnlyNumbers(cnpj);
            ie = OnlyNumbers(ie);
            cpf = OnlyNumbers(cpf);

            StringBuilder saida = new StringBuilder();
            saida.Append(header);
            saida.AppendFormat("<UF>{0}</UF>", uf);
            if (cnpj != null && cnpj != "")
            {
                saida.AppendFormat("<CNPJ>{0}</CNPJ>", cnpj);
            }
            if (ie != null && ie != "")
            {
                saida.AppendFormat("<IE>{0}</IE>", ie);
            }
            if (cpf != null && cpf != "")
            {
                saida.AppendFormat("<CPF>{0}</CPF>", cpf);
            }
            saida.Append(footer);

            string _arquivo_saida = ConfiguracaoApp.vPastaXMLEnvio + "\\" + 
                                    DateTime.Now.ToString("yyyyMMddThhmmss") + 
                                    ExtXml.ConsCad;// "-cons-cad.xml";

            StreamWriter SW = File.CreateText(_arquivo_saida);
            //var output = saida.ToString();
            SW.Write(saida.ToString());//output);
            SW.Close();

            return _arquivo_saida;
        }

        /// <summary>
        /// retorna uma string contendo apenas os digitos da entrada
        /// </summary>
        /// <by>Marcos Diez</by>
        /// <date>29/08/2009</date>
        private static string OnlyNumbers(string entrada)
        {
            if (entrada == null) return null;
            StringBuilder saida = new StringBuilder(entrada.Length);
            foreach (char c in entrada)
            {
                if (char.IsDigit(c))
                {
                    saida.Append(c);
                }
            }
            return saida.ToString();
        }

#endregion  

        #region GravarRetornoEmTXT()
        //TODO: Documentar este método
        protected override void TXTRetorno(string pFinalArqEnvio, string pFinalArqRetorno, string ConteudoXMLRetorno)
        {
            string ConteudoRetorno = string.Empty;

            MemoryStream msXml = Auxiliar.StringXmlToStream(ConteudoXMLRetorno);

            try
            {
                switch (Servico)
                {
                    case Servicos.EnviarLoteNfe:
                        XmlDocument docRec = new XmlDocument();
                        docRec.Load(msXml);

                        XmlNodeList retEnviNFeList = docRec.GetElementsByTagName("retEnviNFe");
                        XmlElement retEnviNFeElemento = (XmlElement)retEnviNFeList.Item(0);

                        ConteudoRetorno += oAux.LerTag(retEnviNFeElemento, "cStat");
                        ConteudoRetorno += oAux.LerTag(retEnviNFeElemento, "xMotivo");

                        XmlNodeList infRecList = retEnviNFeElemento.GetElementsByTagName("infRec");
                        XmlElement infRecElemento = (XmlElement)infRecList.Item(0);

                        ConteudoRetorno += oAux.LerTag(infRecElemento, "nRec");
                        ConteudoRetorno += oAux.LerTag(infRecElemento, "dhRecbto");
                        ConteudoRetorno += oAux.LerTag(infRecElemento, "tMed");

                        goto default;

                    case Servicos.PedidoSituacaoLoteNFe:
                        XmlDocument docProRec = new XmlDocument();
                        docProRec.Load(msXml);

                        XmlNodeList retConsReciNFeList = docProRec.GetElementsByTagName("retConsReciNFe");
                        XmlElement retConsReciNFeElemento = (XmlElement)retConsReciNFeList.Item(0);

                        ConteudoRetorno += oAux.LerTag(retConsReciNFeElemento, "nRec");
                        ConteudoRetorno += oAux.LerTag(retConsReciNFeElemento, "cStat");
                        ConteudoRetorno += oAux.LerTag(retConsReciNFeElemento, "xMotivo");
                        ConteudoRetorno += "\r\n";

                        XmlNodeList protNFeList = retConsReciNFeElemento.GetElementsByTagName("protNFe");
                        XmlElement protNFeElemento = (XmlElement)protNFeList.Item(0);
                        XmlNodeList infProtList = protNFeElemento.GetElementsByTagName("infProt");

                        foreach (XmlNode infProtNode in infProtList)
                        {
                            XmlElement infProtElemento = (XmlElement)infProtNode;
                            string chNFe = oAux.LerTag(infProtElemento, "chNFe");

                            ConteudoRetorno += chNFe.Substring(6, 14) + ";";
                            ConteudoRetorno += chNFe.Substring(25, 9) + ";";
                            ConteudoRetorno += chNFe;
                            ConteudoRetorno += oAux.LerTag(infProtElemento, "dhRecbto");
                            ConteudoRetorno += oAux.LerTag(infProtElemento, "nProt");
                            ConteudoRetorno += oAux.LerTag(infProtElemento, "digVal");
                            ConteudoRetorno += oAux.LerTag(infProtElemento, "cStat");
                            ConteudoRetorno += oAux.LerTag(infProtElemento, "xMotivo");
                            ConteudoRetorno += "\r\n";
                        }

                        goto default;

                    case Servicos.CancelarNFe:
                        break;
                    case Servicos.InutilizarNumerosNFe:
                        break;
                    case Servicos.PedidoConsultaSituacaoNFe:
                        break;
                    case Servicos.PedidoConsultaStatusServicoNFe:
                        break;
                    case Servicos.ConsultaCadastroContribuinte:
                        break;
                    case Servicos.ConsultaInformacoesUniNFe:
                        break;

                    default: //Gravar o TXT de retorno para o ERP
                        string TXTRetorno = string.Empty;
                        TXTRetorno = oAux.ExtrairNomeArq(this.NomeXMLDadosMsg, pFinalArqEnvio) + pFinalArqRetorno;
                        TXTRetorno = ConfiguracaoApp.vPastaXMLRetorno + "\\" + oAux.ExtrairNomeArq(TXTRetorno, ".xml") + ".txt";

                        File.WriteAllText(TXTRetorno, ConteudoRetorno, Encoding.Default);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region XMLDistInut()
        /// <summary>
        /// Criar o arquivo XML de distribuição das Inutilizações de Números de NFe´s com o protocolo de autorização anexado
        /// </summary>
        /// <param name="strArqInut">Nome arquivo XML de Inutilização</param>
        /// <param name="strProtNfe">String contendo a parte do XML do protocolo a ser anexado</param>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>21/04/2009</date>
        public override void XmlDistInut(string strArqInut, string strRetInutNFe)
        {
            StreamWriter swProc = null;

            try
            {
                //Separar as tag´s da NFe que interessa <NFe> até </NFe>
                XmlDocument doc = new XmlDocument();

                doc.Load(strArqInut);

                XmlNodeList InutNFeList = doc.GetElementsByTagName("inutNFe");
                XmlNode InutNFeNode = InutNFeList[0];
                string strInutNFe = InutNFeNode.OuterXml;

                //Montar o XML -procCancNFe.xml
                string strXmlProcInutNfe = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" +
                    "<procInutNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"1.07\">" +
                    strInutNFe +
                    strRetInutNFe +
                    "</procInutNFe>";

                //Montar o nome do arquivo -proc-NFe.xml
                string strNomeArqProcInutNFe =  ConfiguracaoApp.vPastaXMLEnviado + "\\" + 
                                                PastaEnviados.EmProcessamento + "\\" + 
                                                oAux.ExtrairNomeArq(strArqInut, ExtXml.PedInu/*"-ped-inu.xml"*/) +
                                                ExtXml.ProcInutNFe;// "-procInutNFe.xml";

                //Gravar o XML em uma linha só (sem quebrar as tag's linha a linha) ou dá erro na hora de validar o XML pelos Schemas. Wandrey 13/05/2009
                swProc = File.CreateText(strNomeArqProcInutNFe);
                swProc.Write(strXmlProcInutNfe);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (swProc != null)
                {
                    swProc.Close();
                }
            }

        }
        #endregion

        #region XMLDistCanc()
        /// <summary>
        /// Criar o arquivo XML de distribuição dos Cancelamentos com o protocolo de autorização anexado
        /// </summary>
        /// <param name="strArqCanc">Nome arquivo XML de Cancelamento</param>
        /// <param name="strProtNfe">String contendo a parte do XML do protocolo a ser anexado</param>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>21/04/2009</date>
        public override void XmlDistCanc(string strArqCanc, string strRetCancNFe)
        {
            StreamWriter swProc = null;

            try
            {
                //Separar as tag´s da NFe que interessa <NFe> até </NFe>
                XmlDocument doc = new XmlDocument();

                doc.Load(strArqCanc);

                XmlNodeList CancNFeList = doc.GetElementsByTagName("cancNFe");
                XmlNode CancNFeNode = CancNFeList[0];
                string strCancNFe = CancNFeNode.OuterXml;

                //Montar o XML -procCancNFe.xml
                string strXmlProcCancNfe = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" +
                    "<procCancNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"1.07\">" +
                    strCancNFe +
                    strRetCancNFe +
                    "</procCancNFe>";

                //Montar o nome do arquivo -proc-NFe.xml
                string strNomeArqProcCancNFe = ConfiguracaoApp.vPastaXMLEnviado + "\\" + 
                                                PastaEnviados.EmProcessamento + "\\" + 
                                                oAux.ExtrairNomeArq(strArqCanc, ExtXml.PedCan/*"-ped-can.xml"*/) +
                                                ExtXml.ProcCancNFe;// "-procCancNFe.xml";

                //Gravar o XML em uma linha só (sem quebrar as tag's linha a linha) ou dá erro na hora de validar o XML pelos Schemas. Wandrey 13/05/2009
                swProc = File.CreateText(strNomeArqProcCancNFe);
                swProc.Write(strXmlProcCancNfe);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (swProc != null)
                {
                    swProc.Close();
                }
            }
        }
        #endregion

        #region XmlPedRec()
        /// <summary>
        /// Gera o XML de pedido de analise do recibo do lote
        /// </summary>
        /// <param name="strRecibo">Número do recibo a ser consultado o lote</param>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>21/04/2009</date>
        public override void XmlPedRec(string strRecibo)
        {
            string strXml = string.Empty;
            string strNomeArqPedRec = ConfiguracaoApp.vPastaXMLEnvio + "\\" + strRecibo + ExtXml.PedRec;// "-ped-rec.xml";
            if (!File.Exists(strNomeArqPedRec))
            {
                //TODO: CONFIG
                strXml += "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                    "<consReciNFe xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" versao=\"1.10\" xmlns=\"http://www.portalfiscal.inf.br/nfe\">" +
                    "<tpAmb>" + ConfiguracaoApp.tpAmb.ToString() + "</tpAmb>" +
                    "<nRec>" + strRecibo + "</nRec>" +
                    "</consReciNFe>";

                //Gravar o XML
                MemoryStream oMemoryStream = Auxiliar.StringXmlToStream(strXml);
                XmlDocument docProc = new XmlDocument();
                docProc.Load(oMemoryStream);
                docProc.Save(strNomeArqPedRec);
            }
        }
        #endregion

        #region XMLDistNFe()
        /// <summary>
        /// Criar o arquivo XML de distribuição das NFE com o protocolo de autorização anexado
        /// </summary>
        /// <param name="strArqNFe">Nome arquivo XML da NFe</param>
        /// <param name="strProtNfe">String contendo a parte do XML do protocolo a ser anexado</param>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>20/04/2009</date>
        public override void XmlDistNFe(string strArqNFe, string strProtNfe)
        {
            StreamWriter swProc = null;

            try
            {
                //Separar as tag´s da NFe que interessa <NFe> até </NFe>
                XmlDocument doc = new XmlDocument();

                doc.Load(strArqNFe);

                XmlNodeList NFeList = doc.GetElementsByTagName("NFe");
                XmlNode NFeNode = NFeList[0];
                string strNFe = NFeNode.OuterXml;

                //Montar a string contendo o XML -proc-NFe.xml
                string strXmlProcNfe = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" +
                    "<nfeProc xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"1.10\">" +
                    strNFe +
                    strProtNfe +
                    "</nfeProc>";

                //Montar o nome do arquivo -proc-NFe.xml
                string strNomeArqProcNFe = ConfiguracaoApp.vPastaXMLEnviado + "\\" + 
                                            PastaEnviados.EmProcessamento + "\\" + 
                                            oAux.ExtrairNomeArq(strArqNFe, ExtXml.Nfe/*"-nfe.xml"*/) +
                                            ExtXml.ProcNFe;// "-procNFe.xml";

                //Gravar o XML em uma linha só (sem quebrar as tag´s linha a linha) ou dá erro na hora de validar o XML pelos Schemas. Wandrey 13/05/2009
                swProc = File.CreateText(strNomeArqProcNFe);
                swProc.Write(strXmlProcNfe);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (swProc != null)
                {
                    swProc.Close();
                }
            }
        }
        #endregion

        #region LerXMLNFe()
        protected override absLerXML.DadosNFeClass LerXMLNFe(string Arquivo)
        {
            LerXML oLerXML = new LerXML();
            oLerXML.Nfe(Arquivo);

            return oLerXML.oDadosNfe;
        }
        #endregion

        #region LerXMLRecibo()
        protected override absLerXML.DadosRecClass LerXMLRecibo(string Arquivo)
        {
            LerXML oLerXML = new LerXML();
            oLerXML.Recibo(Arquivo);

            return oLerXML.oDadosRec;
        }
        #endregion   
    }
}