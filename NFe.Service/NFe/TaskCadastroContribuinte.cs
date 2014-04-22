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
    public class TaskCadastroContribuinte: TaskAbst
    {
        public TaskCadastroContribuinte()
        {
            Servico = Servicos.ConsultaCadastroContribuinte;
        }

        #region Classe com os Dados do XML da Consulta Cadastro do Contribuinte
        /// <summary>
        /// Esta herança que deve ser utilizada fora da classe para obter os valores das tag´s da consulta do cadastro do contribuinte
        /// </summary>
        private DadosConsCad dadosConsCad;
        #endregion

        #region Execute
        public override void Execute()
        {
            int emp = Functions.FindEmpresaByThread();

            try
            {
                dadosConsCad = new DadosConsCad();
                //Ler o XML para pegar parâmetros de envio
                ConsCad(NomeArquivoXML);

                if(this.vXmlNfeDadosMsgEhXML)  //danasa 12-9-2009
                {
                    //Definir o objeto do WebService
                    WebServiceProxy wsProxy = ConfiguracaoApp.DefinirWS(Servico, emp, dadosConsCad.cUF, dadosConsCad.tpAmb, Propriedade.TipoEmissao.teNormal, dadosConsCad.versao);

                    //Criar objetos das classes dos serviços dos webservices do SEFAZ
                    object oConsCad = wsProxy.CriarObjeto(NomeClasseWS(Servico, dadosConsCad.cUF));
                    object oCabecMsg = wsProxy.CriarObjeto(NomeClasseCabecWS(dadosConsCad.cUF, Servico));

                    //Atribuir conteúdo para duas propriedades da classe nfeCabecMsg
                    wsProxy.SetProp(oCabecMsg, "cUF", dadosConsCad.cUF.ToString());
                    wsProxy.SetProp(oCabecMsg, "versaoDados", ConfiguracaoApp.VersaoXMLConsCad);

                    //Invocar o método que envia o XML para o SEFAZ
                    oInvocarObj.Invocar(wsProxy, oConsCad, NomeMetodoWS(Servico, dadosConsCad.cUF), oCabecMsg, this, "-cons-cad", "-ret-cons-cad");
                }
                else
                {
                    //Gerar o XML da consulta cadastro do contribuinte a partir do TXT gerado pelo ERP
                    oGerarXML.ConsultaCadastro(Path.GetFileNameWithoutExtension(NomeArquivoXML) + ".xml",
                        dadosConsCad.UF,
                        dadosConsCad.CNPJ,
                        dadosConsCad.IE,
                        dadosConsCad.CPF,
                        dadosConsCad.versao);
                }
            }
            catch(Exception ex)
            {
                string ExtRet = string.Empty;

                if(this.vXmlNfeDadosMsgEhXML) //Se for XML
                    ExtRet = Propriedade.ExtEnvio.ConsCad_XML;
                else //Se for TXT
                    ExtRet = Propriedade.ExtEnvio.ConsCad_TXT;

                try
                {
                    //Gravar o arquivo de erro de retorno para o ERP, caso ocorra
                    TFunctions.GravarArqErroServico(NomeArquivoXML, ExtRet, Propriedade.ExtRetorno.ConsCad_ERR, ex);
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
                    //Deletar o arquivo de solicitação do serviço
                    Functions.DeletarArquivo(NomeArquivoXML);
                }
                catch
                {
                    //Se falhou algo na hora de deletar o XML de solicitação do serviço, 
                    //infelizmente não posso fazer mais nada, o UniNFe vai tentar mandar 
                    //o arquivo novamente para o webservice
                    //Wandrey 09/03/2010
                }
            }
        }
        #endregion

        #region ConsCad()
        /// <summary>
        /// Faz a leitura do XML de consulta do cadastro do contribuinte e disponibiliza os valores de algumas tag´s
        /// </summary>
        /// <param name="cArquivoXML">Caminho e nome do arquivo XML da consulta do cadastro do contribuinte a ser lido</param>
        private void ConsCad(string cArquivoXML)
        {
            this.dadosConsCad.CNPJ = string.Empty;
            this.dadosConsCad.IE = string.Empty;
            this.dadosConsCad.UF = string.Empty;
            this.dadosConsCad.versao = ConfiguracaoApp.VersaoXMLConsCad;

            if(Path.GetExtension(cArquivoXML).ToLower() == ".txt")
            {
                List<string> cLinhas = Functions.LerArquivo(cArquivoXML);
                foreach(string cTexto in cLinhas)
                {
                    string[] dados = cTexto.Split('|');
                    switch(dados[0].ToLower())
                    {
                        case "cnpj":
                            this.dadosConsCad.CNPJ = dados[1].Trim();
                            break;
                        case "cpf":
                            this.dadosConsCad.CPF = dados[1].Trim();
                            break;
                        case "ie":
                            this.dadosConsCad.IE = dados[1].Trim();
                            break;
                        case "uf":
                            this.dadosConsCad.UF = dados[1].Trim();
                            break;
                        case "versao":
                            this.dadosConsCad.versao = dados[1].Trim();
                            break;
                    }
                }
            }
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(cArquivoXML);

                XmlNodeList ConsCadList = doc.GetElementsByTagName("ConsCad");
                foreach(XmlNode ConsCadNode in ConsCadList)
                {
                    XmlElement ConsCadElemento = (XmlElement)ConsCadNode;

                    this.dadosConsCad.versao = ConsCadElemento.Attributes["versao"].InnerText;

                    XmlNodeList infConsList = ConsCadElemento.GetElementsByTagName("infCons");

                    foreach(XmlNode infConsNode in infConsList)
                    {
                        XmlElement infConsElemento = (XmlElement)infConsNode;

                        if(infConsElemento.GetElementsByTagName("CNPJ")[0] != null)
                        {
                            this.dadosConsCad.CNPJ = infConsElemento.GetElementsByTagName("CNPJ")[0].InnerText;
                        }
                        if(infConsElemento.GetElementsByTagName("CPF")[0] != null)
                        {
                            this.dadosConsCad.CPF = infConsElemento.GetElementsByTagName("CPF")[0].InnerText;
                        }
                        if(infConsElemento.GetElementsByTagName("UF")[0] != null)
                        {
                            this.dadosConsCad.UF = infConsElemento.GetElementsByTagName("UF")[0].InnerText;
                        }
                        if(infConsElemento.GetElementsByTagName("IE")[0] != null)
                        {
                            this.dadosConsCad.IE = infConsElemento.GetElementsByTagName("IE")[0].InnerText;
                        }
                    }
                }
            }
        }
        #endregion

    }
}
