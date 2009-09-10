﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UniNFeLibrary.Enums;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Threading;

namespace UniNFeLibrary
{
    public class Auxiliar
    {
        #region ExtrairNomeArq()
        /// <summary>
        /// Extrai somente o nome do arquivo de uma string; para ser utilizado na situação desejada. Veja os exemplos na documentação do código.
        /// </summary>
        /// <param name="pPastaArq">String contendo o caminho e nome do arquivo que é para ser extraido o nome.</param>
        /// <param name="pFinalArq">String contendo o final do nome do arquivo até onde é para ser extraído.</param>
        /// <returns>Retorna somente o nome do arquivo de acordo com os parâmetros passados - veja exemplos.</returns>
        /// <example>
        /// MessageBox.Show(this.ExtrairNomeArq("C:\\TESTE\\NFE\\ENVIO\\ArqSituacao-ped-sta.xml", "-ped-sta.xml"));
        /// //Será demonstrado no message a string "ArqSituacao"
        /// 
        /// MessageBox.Show(this.ExtrairNomeArq("C:\\TESTE\\NFE\\ENVIO\\ArqSituacao-ped-sta.xml", ".xml"));
        /// //Será demonstrado no message a string "ArqSituacao-ped-sta"
        /// </example>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>19/06/2008</date>
        public string ExtrairNomeArq(string pPastaArq, string pFinalArq)
        {
            FileInfo fi = new FileInfo(pPastaArq);
            string ret = fi.Name;
            ret = ret.Substring(0, ret.Length - pFinalArq.Length);
            return ret;

            #region bkp Código Antigo
            /*
             **** DEPOIS DE AVALIADO O CÓDIGO ACIMA. ESTE AQUI PODE SER APAGADO ****
            //Achar o posição inicial do nome do arquivo
            //procura por pastas, tira elas para ficar somente o nome do arquivo
            Int32 nAchou = 0;
            Int32 nPosI = 0;
            for (Int32 nCont = 0; nCont < pPastaArq.Length; nCont++)
            {
                nAchou = pPastaArq.IndexOf("\\", nCont);
                if (nAchou >= 0)
                {
                    nCont = nAchou;
                    nPosI = nAchou + 1;
                }
                else
                {
                    break;
                }
            }

            //Achar a posição final do nome do arquivo
            Int32 nPosF = pPastaArq.ToUpper().IndexOf(pFinalArq.ToUpper());

            //Extrair o nome do arquivo
            string cRetorna = pPastaArq.Substring(nPosI, nPosF - nPosI);

            return cRetorna; 
             */
            #endregion
        }
        #endregion

        #region MoverArquivo()
        /// <summary>
        /// Move arquivos da nota fiscal eletrônica para suas respectivas pastas
        /// </summary>
        /// <param name="Arquivo">Nome do arquivo a ser movido</param>
        /// <param name="PastaXMLEnviado">Pasta de XML´s enviados para onde será movido o arquivo</param>
        /// <param name="SubPastaXMLEnviado">SubPasta de XML´s enviados para onde será movido o arquivo</param>
        /// <param name="PastaBackup">Pasta para Backup dos XML´s enviados</param>
        /// <param name="Emissao">Data de emissão da Nota Fiscal ou Data Atual do envio do XML para separação dos XML´s em subpastas por Ano e Mês</param>
        /// <date>16/07/2008</date>
        /// <by>Wandrey Mundin Ferreira</by>
        public void MoverArquivo(string Arquivo, PastaEnviados SubPastaXMLEnviado, DateTime Emissao)
        {
            try
            {
                //Definir o arquivo que vai ser deletado ou movido para outra pasta
                FileInfo oArquivo = new FileInfo(Arquivo);

                //Criar a pasta EmProcessamento
                if (!Directory.Exists(ConfiguracaoApp.vPastaXMLEnviado + "\\" + PastaEnviados.EmProcessamento.ToString()))
                {
                    System.IO.Directory.CreateDirectory(ConfiguracaoApp.vPastaXMLEnviado + "\\" + PastaEnviados.EmProcessamento.ToString());
                }

                //Criar a Pasta Autorizado
                if (!Directory.Exists(ConfiguracaoApp.vPastaXMLEnviado + "\\" + PastaEnviados.Autorizados.ToString()))
                {
                    System.IO.Directory.CreateDirectory(ConfiguracaoApp.vPastaXMLEnviado + "\\" + PastaEnviados.Autorizados.ToString());
                }

                //Criar a Pasta Denegado
                if (!Directory.Exists(ConfiguracaoApp.vPastaXMLEnviado + "\\" + PastaEnviados.Denegados.ToString()))
                {
                    System.IO.Directory.CreateDirectory(ConfiguracaoApp.vPastaXMLEnviado + "\\" + PastaEnviados.Denegados.ToString());
                }

                //Criar Pasta do Mês para gravar arquivos enviados autorizados ou denegados
                string strNomePastaEnviado = string.Empty;
                string strDestinoArquivo = string.Empty;
                switch (SubPastaXMLEnviado)
                {
                    case PastaEnviados.EmProcessamento:
                        strNomePastaEnviado = ConfiguracaoApp.vPastaXMLEnviado + "\\" + PastaEnviados.EmProcessamento.ToString();
                        strDestinoArquivo = strNomePastaEnviado + "\\" + this.ExtrairNomeArq(Arquivo, ".xml") + ".xml";
                        break;

                    case PastaEnviados.Autorizados:
                        strNomePastaEnviado = ConfiguracaoApp.vPastaXMLEnviado + "\\" + PastaEnviados.Autorizados.ToString() + "\\" + ConfiguracaoApp.DiretorioSalvarComo.ToString(Emissao);
                        strDestinoArquivo = strNomePastaEnviado + "\\" + this.ExtrairNomeArq(Arquivo, ".xml") + ".xml";
                        goto default;

                    case PastaEnviados.Denegados:
                        strNomePastaEnviado = ConfiguracaoApp.vPastaXMLEnviado + "\\" + PastaEnviados.Denegados.ToString() + "\\" + ConfiguracaoApp.DiretorioSalvarComo.ToString(Emissao);
                        strDestinoArquivo = strNomePastaEnviado + "\\" + this.ExtrairNomeArq(Arquivo, "-nfe.xml") + "-den.xml";
                        goto default;

                    default:
                        if (!Directory.Exists(strNomePastaEnviado))
                        {
                            System.IO.Directory.CreateDirectory(strNomePastaEnviado);
                        }
                        break;
                }

                //Se conseguiu criar a pasta ele move o arquivo, caso contrário
                if (Directory.Exists(strNomePastaEnviado) == true)
                {
                    //Mover o arquivo da nota fiscal para a pasta dos enviados
                    if (File.Exists(strDestinoArquivo))
                    {
                        FileInfo oArqDestino = new FileInfo(strDestinoArquivo);
                        oArqDestino.Delete();
                    }
                    oArquivo.MoveTo(strDestinoArquivo);

                    if (SubPastaXMLEnviado == PastaEnviados.Autorizados || SubPastaXMLEnviado == PastaEnviados.Denegados)
                    {
                        //Fazer um backup do XML que foi copiado para a pasta de enviados
                        //para uma outra pasta para termos uma maior segurança no arquivamento
                        //Normalmente esta pasta é em um outro computador ou HD
                        if (ConfiguracaoApp.cPastaBackup.Trim() != "")
                        {
                            //Criar Pasta do Mês para gravar arquivos enviados
                            string strNomePastaBackup = string.Empty;
                            switch (SubPastaXMLEnviado)
                            {
                                case PastaEnviados.Autorizados:
                                    strNomePastaBackup = ConfiguracaoApp.cPastaBackup + "\\" + PastaEnviados.Autorizados + "\\" + ConfiguracaoApp.DiretorioSalvarComo.ToString(Emissao);
                                    goto default;

                                case PastaEnviados.Denegados:
                                    strNomePastaBackup = ConfiguracaoApp.cPastaBackup + "\\" + PastaEnviados.Denegados + "\\" + ConfiguracaoApp.DiretorioSalvarComo.ToString(Emissao);
                                    goto default;

                                default:
                                    if (Directory.Exists(strNomePastaBackup) == false)
                                    {
                                        System.IO.Directory.CreateDirectory(strNomePastaBackup);
                                    }
                                    break;
                            }

                            //Se conseguiu criar a pasta ele move o arquivo, caso contrário
                            if (Directory.Exists(strNomePastaBackup) == true)
                            {
                                //Mover o arquivo da nota fiscal para a pasta de backup
                                string strNomeArquivoBkp = strNomePastaBackup + "\\" + this.ExtrairNomeArq(Arquivo, ".xml") + ".xml";
                                if (File.Exists(strNomeArquivoBkp))
                                {
                                    FileInfo oArqDestinoBkp = new FileInfo(strNomeArquivoBkp);
                                    oArqDestinoBkp.Delete();
                                }
                                FileInfo oArquivoBkp = new FileInfo(strDestinoArquivo);

                                oArquivoBkp.CopyTo(strNomeArquivoBkp, true);
                            }
                            else
                            {
                                throw new Exception("Pasta de backup informada nas configurações não existe. (Pasta: " + strNomePastaBackup + ")");
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("Pasta para arquivamento dos XML´s enviados não existe. (Pasta: " + strNomePastaEnviado + ")");
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region MoverArquivo() - Sobrecarga
        /// <summary>
        /// Move arquivos da nota fiscal eletrônica para suas respectivas pastas
        /// </summary>
        /// <param name="Arquivo">Nome do arquivo a ser movido</param>
        /// <param name="PastaXMLEnviado">Pasta de XML´s enviados para onde será movido o arquivo</param>
        /// <param name="SubPastaXMLEnviado">SubPasta de XML´s enviados para onde será movido o arquivo</param>
        /// <date>05/08/2009</date>
        /// <by>Wandrey Mundin Ferreira</by>
        public void MoverArquivo(string Arquivo, PastaEnviados SubPastaXMLEnviado)
        {
            try
            {
                this.MoverArquivo(Arquivo, SubPastaXMLEnviado, DateTime.Now);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region DeletarArquivo()
        /// <summary>
        /// Excluir arquivos do HD
        /// </summary>
        /// <param name="Arquivo">Nome do arquivo a ser excluido.</param>
        /// <date>05/08/2009</date>
        /// <by>Wandrey Mundin Ferreira</by>
        public void DeletarArquivo(string Arquivo)
        {
            //TODO: Criar vários try/catch neste método para evitar erros

            //Definir o arquivo que vai ser deletado ou movido para outra pasta
            //FileInfo oArquivo = new FileInfo(Arquivo);

            if (File.Exists(Arquivo))
            {
                FileInfo oArquivo = new FileInfo(Arquivo);  // << movido para cá >>
                oArquivo.Delete();
            }
        }
        #endregion

        #region DeletarArqXMLErro()
        /// <summary>
        /// Deleta o XML da pata temporária dos arquivos com erro se o mesmo existir.
        /// </summary>
        public void DeletarArqXMLErro(string Arquivo)
        {
            try
            {
                this.DeletarArquivo(Arquivo);
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

        #region XmlToString()
        /// <summary>
        /// Método responsável por ler o conteúdo de um XML e retornar em uma string
        /// </summary>
        /// <param name="parNomeArquivo">Caminho e nome do arquivo XML que é para pegar o conteúdo e retornar na string.</param>
        /// <returns>Retorna uma string com o conteúdo do arquivo XML</returns>
        /// <example>
        /// string ConteudoXML;
        /// ConteudoXML = THIS.XmltoString( @"c:\arquivo.xml" );
        /// MessageBox.Show( ConteudoXML );
        /// </example>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>04/06/2008</date>
        public string XmlToString(string parNomeArquivo)
        {
            string conteudo_xml = string.Empty;

            StreamReader SR = null;
            try
            {
                SR = File.OpenText(parNomeArquivo);
                conteudo_xml = SR.ReadToEnd();
            }
            catch (IOException ex)
            {
                throw (ex);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                SR.Close();
            }

            return conteudo_xml;
        }
        #endregion

        #region MoveArqErro()
        /// <summary>
        /// Move arquivos com a extensão informada e que está com erro para uma pasta de xml´s/arquivos com erro configurados no UniNFe.
        /// </summary>
        /// <param name="cArquivo">Nome do arquivo a ser movido para a pasta de XML´s com erro</param>
        /// <param name="ExtensaoArq">Extensão do arquivo que vai ser movido. Ex: .xml</param>
        /// <example>this.MoveArqErro(this.vXmlNfeDadosMsg, ".xml")</example>
        public void MoveArqErro(string Arquivo, string ExtensaoArq)
        {
            if (File.Exists(Arquivo))
            {
                FileInfo oArquivo = new FileInfo(Arquivo);

                if (Directory.Exists(ConfiguracaoApp.vPastaXMLErro) == true)
                {
                    //Mover o arquivo da nota fiscal para a pasta do XML com erro
                    string vNomeArquivo = ConfiguracaoApp.vPastaXMLErro + "\\" + this.ExtrairNomeArq(Arquivo, ExtensaoArq) + ExtensaoArq;
                    if (File.Exists(vNomeArquivo))
                    {
                        FileInfo oArqDestino = new FileInfo(vNomeArquivo);
                        oArqDestino.Delete();
                    }

                    oArquivo.MoveTo(vNomeArquivo);
                }
                else
                {
                    oArquivo.Delete();
                }
            }
        }
        #endregion

        #region MoveArqErro
        /// <summary>
        /// Move arquivos XML com erro para uma pasta de xml´s com erro configurados no UniNFe.
        /// </summary>
        /// <param name="cArquivo">Nome do arquivo a ser movido para a pasta de XML´s com erro</param>
        /// <example>this.MoveArqErro(this.vXmlNfeDadosMsg)</example>
        public void MoveArqErro(string Arquivo)
        {
            this.MoveArqErro(Arquivo, ".xml");
        }
        #endregion

        #region GravarArqErroServico()
        /// <summary>
        /// Grava um arquivo texto com um erro ocorrido na invocação dos WebServices ou na execusão de alguma
        /// rotina de validação, etc. Este arquivo é gravado para que o sistema ERP tenha condições de interagir
        /// com o usuário.
        /// </summary>
        /// <param name="Arquivo">Nome do arquivo XML que foi enviado para os WebServices</param>
        /// <param name="PastaXMLRetorno">Pasta de retorno para gravar o XML de erro</param>
        /// <param name="FinalArqEnvio">Final do nome do arquivo de solicitação do serviço</param>
        /// <param name="FinalArqErro">Final do nome do arquivo que é para ser gravado o erro</param>
        /// <param name="Erro">Texto do erro ocorrido a ser gravado no arquivo</param>
        /// <param name="PastaXMLErro">Pasta para mover o XML com erro</param>
        /// <by>Wandrey Mundin Ferreira</by>
        public void GravarArqErroServico(string Arquivo, string FinalArqEnvio, string FinalArqErro, string Erro)
        {
            //Qualquer erro ocorrido o aplicativo vai mover o XML com falha da pasta de envio
            //para a pasta de XML´s com erros. Futuramente ele é excluido quando outro igual
            //for gerado corretamente.
            this.MoveArqErro(Arquivo);

            //Grava arquivo de ERRO para o ERP
            string cArqErro = ConfiguracaoApp.vPastaXMLRetorno + "\\" +
                              this.ExtrairNomeArq(Arquivo, FinalArqEnvio) +
                              FinalArqErro;

            File.WriteAllText(cArqErro, Erro, Encoding.Default);
        }
        #endregion

        #region GravarArqErroERP
        /// <summary>
        /// grava um arquivo de erro ao ERP
        /// </summary>
        /// <param name="Arquivo"></param>
        /// <param name="Erro"></param>
        public void GravarArqErroERP(string Arquivo, string Erro)
        {
            if (!string.IsNullOrEmpty(Arquivo))
            {
                //Grava arquivo de ERRO para o ERP
                string cArqErro = ConfiguracaoApp.vPastaXMLRetorno + "\\" + Path.GetFileName(Arquivo);
                File.WriteAllText(cArqErro, Erro, Encoding.Default);
            }
        }
        #endregion

        #region ValidarArqXML()
        /// <summary>
        /// Valida o arquivo XML 
        /// </summary>
        /// <param name="Arquivo">Nome do arquivo XML a ser validado</param>
        /// <returns>
        /// Se retornar uma string em branco, significa que o XML foi 
        /// validado com sucesso, ou seja, não tem nenhum erro. Se o retorno
        /// tiver algo, algum erro ocorreu na validação.
        /// </returns>
        /// <example>
        /// string cResultadoValidacao = this.ValidarArqXML();
        /// 
        /// if (cResultadoValidacao == "")
        /// {
        ///     MessageBox.Show( "Arquivo validado com sucesso" );
        /// }
        /// else
        /// {
        ///     MessageBox.Show( cResultadoValidacao );
        /// }
        /// </example>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>31/07/2008</date>         
        public string ValidarArqXML(string Arquivo)
        {
            string cRetorna = "";

            // Validar Arquivo XML
            ValidarXMLs oValidador = new ValidarXMLs();
            oValidador.TipoArquivoXML(Arquivo);

            if (oValidador.nRetornoTipoArq >= 1 && oValidador.nRetornoTipoArq <= 11)
            {
                oValidador.Validar(Arquivo, oValidador.cArquivoSchema);
                if (oValidador.Retorno != 0)
                {
                    cRetorna = "XML INCONSISTENTE!\r\n\r\n" + oValidador.RetornoString;
                }
            }
            else
            {
                cRetorna = "XML INCONSISTENTE!\r\n\r\n" + oValidador.cRetornoTipoArq;
            }

            return cRetorna;
        }
        #endregion

        #region ValidarAssinarXML()
        /// <summary>
        /// Efetua a validação de qualquer XML, NFE, Cancelamento, Inutilização, etc..., e retorna se está ok ou não
        /// </summary>
        /// <param name="Arquivo">Nome do arquivo XML a ser validado e assinado</param>
        /// <param name="PastaValidar">Nome da pasta onde fica os arquivos a serem validados</param>
        /// <param name="PastaXMLErro">Nome da pasta onde é para gravar os XML´s validados que apresentaram erro.</param>
        /// <param name="PastaXMLRetorno">Nome da pasta de retorno onde será gravado o XML com o status da validação</param>
        /// <param name="Certificado">Certificado digital a ser utilizado na validação</param>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>28/05/2009</date>
        public void ValidarAssinarXML(string Arquivo)
        {
            Boolean Assinou = true;
            ValidarXMLs oValidador = new ValidarXMLs();
            oValidador.TipoArquivoXML(Arquivo);

            //Assinar o XML se tiver tag para assinar
            if (oValidador.TagAssinar != string.Empty)
            {
                AssinaturaDigital oAD = new AssinaturaDigital();

                try
                {
                    oAD.Assinar(Arquivo, oValidador.TagAssinar, ConfiguracaoApp.oCertificado);

                    if (oAD.intResultado != 0)
                    {
                        Assinou = false;
                    }
                }
                catch (Exception ex)
                {
                    Assinou = false;
                    this.GravarXMLRetornoValidacao(Arquivo, "2", "Ocorreu um erro ao assinar o XML: " + ex.Message);
                    this.MoveArqErro(Arquivo);
                }
            }


            if (Assinou)
            {
                // Validar o Arquivo XML
                if (oValidador.nRetornoTipoArq >= 1 && oValidador.nRetornoTipoArq <= 11)
                {
                    oValidador.Validar(Arquivo, oValidador.cArquivoSchema);
                    if (oValidador.Retorno != 0)
                    {
                        this.GravarXMLRetornoValidacao(Arquivo, "3", "Ocorreu um erro ao validar o XML: " + oValidador.RetornoString);
                        this.MoveArqErro(Arquivo);
                    }
                    else
                    {
                        if (!Directory.Exists(ConfiguracaoApp.PastaValidar + "\\Validado"))
                        {
                            Directory.CreateDirectory(ConfiguracaoApp.PastaValidar + "\\Validado");
                        }

                        string ArquivoNovo = ConfiguracaoApp.PastaValidar + "\\Validado\\" + this.ExtrairNomeArq(Arquivo, ".xml") + ".xml";

                        if (File.Exists(ArquivoNovo))
                        {
                            FileInfo oArqNovo = new FileInfo(ArquivoNovo);
                            oArqNovo.Delete();
                        }

                        FileInfo oArquivo = new FileInfo(Arquivo);
                        oArquivo.MoveTo(ArquivoNovo);

                        this.GravarXMLRetornoValidacao(Arquivo, "1", "XML assinado e validado com sucesso.");
                    }
                }
                else
                {
                    this.GravarXMLRetornoValidacao(Arquivo, "4", "Ocorreu um erro ao validar o XML: " + oValidador.cRetornoTipoArq);
                    this.MoveArqErro(Arquivo);
                }
            }
        }
        #endregion

        #region GravarXMLRetornoValidacao()
        /// <summary>
        /// Na tentativa de somente validar ou assinar o XML se encontrar um erro vai ser retornado um XML para o ERP
        /// </summary>
        /// <param name="Arquivo">Nome do arquivo XML validado</param>
        /// <param name="PastaXMLRetorno">Pasta de retorno para ser gravado o XML</param>
        /// <param name="cStat">Status da validação</param>
        /// <param name="xMotivo">Status descritivo da validação</param>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>28/05/2009</date>
        private void GravarXMLRetornoValidacao(string Arquivo, string cStat, string xMotivo)
        {
            //Definir o nome do arquivo de retorno
            string ArquivoRetorno = this.ExtrairNomeArq(Arquivo, ".xml") + "-ret.xml";

            XmlWriterSettings oSettings = new XmlWriterSettings();
            UTF8Encoding c = new UTF8Encoding(false);

            //Para começar, vamos criar um XmlWriterSettings para configurar nosso XML
            oSettings.Encoding = c;
            oSettings.Indent = true;
            oSettings.IndentChars = "";
            oSettings.NewLineOnAttributes = false;
            oSettings.OmitXmlDeclaration = false;

            //Agora vamos criar um XML Writer
            XmlWriter oXmlGravar = XmlWriter.Create(ConfiguracaoApp.vPastaXMLRetorno + "\\" + ArquivoRetorno);

            //Agora vamos gravar os dados
            oXmlGravar.WriteStartDocument();
            oXmlGravar.WriteStartElement("Validacao");
            oXmlGravar.WriteElementString("cStat", cStat);
            oXmlGravar.WriteElementString("xMotivo", xMotivo);
            oXmlGravar.WriteEndElement(); //nfe_configuracoes
            oXmlGravar.WriteEndDocument();
            oXmlGravar.Flush();
            oXmlGravar.Close();
        }
        #endregion

        #region LerTag()
        /// <summary>
        /// Busca o nome de uma determinada TAG em um Elemento do XML para ver se existe, se existir retorna seu conteúdo.
        /// </summary>
        /// <param name="Elemento">Elemento a ser pesquisado o Nome da TAG</param>
        /// <param name="NomeTag">Nome da Tag</param>
        /// <returns>Conteúdo da tag</returns>
        /// <date>05/08/2009</date>
        /// <by>Wandrey Mundin Ferreira</by>
        public string LerTag(XmlElement Elemento, string NomeTag)
        {
            string Retorno = string.Empty;

            if (Elemento.GetElementsByTagName(NomeTag).Count != 0)
            {
                Retorno = Elemento.GetElementsByTagName(NomeTag)[0].InnerText;
                Retorno += ";";
            }

            return Retorno;
        }
        #endregion

        #region VerStatusServico() e ConsultaCadastro()

        /// <summary>
        /// Verifica e retorna o Status do Servido da NFE. Para isso este método gera o arquivo XML necessário
        /// para obter o status do serviõ e faz a leitura do XML de retorno, disponibilizando uma string com a mensagem
        /// obtida.
        /// </summary>
        /// <returns>Retorna uma string com a mensagem obtida do webservice de status do serviço da NFe</returns>
        /// <example>string vPastaArq = this.CriaArqXMLStatusServico();</example>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>17/06/2008</date>
        public string VerStatusServico(string XmlNfeDadosMsg)
        {
            string ArqXMLRetorno = ConfiguracaoApp.vPastaXMLRetorno + "\\" +
                      this.ExtrairNomeArq(XmlNfeDadosMsg, ExtXml.PedSta) +
                      "-sta.xml";

            string ArqERRRetorno = ConfiguracaoApp.vPastaXMLRetorno + "\\" +
                      this.ExtrairNomeArq(XmlNfeDadosMsg, ExtXml.PedSta) +
                      "-ped-sta.err";

            string result = (string)EnviaArquivoERecebeResposta(ArqXMLRetorno, ArqERRRetorno, ProcessaStatusServico);

            this.DeletarArquivo(ArqERRRetorno);
            this.DeletarArquivo(ArqXMLRetorno);

            return result;
        }


        /// <summary>
        /// Função Callback que analisa a resposta do Status do Servido
        /// </summary>
        /// <param name="elem"></param>
        /// <by>Marcos Diez</by>
        /// <date>30/8/2009</date>
        /// <returns></returns>
        private static string ProcessaStatusServico(XmlTextReader elem)
        {
            while (elem.Read())
            {
                if (elem.NodeType == XmlNodeType.Element)
                {
                    if (elem.Name == "xMotivo")
                    {
                        elem.Read();
                        return elem.Value;
                    }
                }
            }
            return "Erro na leitura do XML";
        }

        /// <summary>
        /// Função Callback que analisa a resposta do Status do Servido
        /// </summary>
        /// <param name="elem"></param>
        /// <by>Marcos Diez</by>
        /// <date>30/8/2009</date>
        /// <returns></returns>
        /*public string VerConsultaCadastro(string XmlNfeDadosMsg)
        {
            string ArqXMLRetorno = ConfiguracaoApp.vPastaXMLRetorno + "\\" +
                       this.ExtrairNomeArq(XmlNfeDadosMsg, "-cons-cad.xml") +
                       "-ret-cons-cad.xml";

            string ArqERRRetorno = ConfiguracaoApp.vPastaXMLRetorno + "\\" +
                      this.ExtrairNomeArq(XmlNfeDadosMsg, "-cons-cad.xml") +
                      "-ret-cons-cad.err";

            var saida = (string)EnviaArquivoERecebeResposta(ArqXMLRetorno, ArqERRRetorno, ProcessaConsultaCadastro);
            return saida;
        }
        */

        public object VerConsultaCadastroClass(string XmlNfeDadosMsg)
        {
            string ArqXMLRetorno = ConfiguracaoApp.vPastaXMLRetorno + "\\" +
                       this.ExtrairNomeArq(XmlNfeDadosMsg, ExtXml.ConsCad) +
                       "-ret-cons-cad.xml";

            string ArqERRRetorno = ConfiguracaoApp.vPastaXMLRetorno + "\\" +
                      this.ExtrairNomeArq(XmlNfeDadosMsg, ExtXml.ConsCad) +
                      "-ret-cons-cad.err";

            object vRetorno = EnviaArquivoERecebeResposta(ArqXMLRetorno, ArqERRRetorno, ProcessaConsultaCadastroClass);

            this.DeletarArquivo(ArqERRRetorno);
            this.DeletarArquivo(ArqXMLRetorno);

            return vRetorno;
        }


        /// <summary>
        /// Função Callback que analisa a resposta da Consulta de Cadastro
        /// </summary>
        /// <param name="elem"></param>
        /// <by>Marcos Diez</by>
        /// <date>30/8/2009</date>
        /// <returns></returns>
        /*
        private static string ProcessaConsultaCadastro(XmlTextReader elem)
        {
            string ie = "";
            string cnpj = "";
            string xNome = "";
            string cpf = "";
            string situacao = "";

            // "infCad" , 
            bool deuErro = true;

            while (elem.Read())
            {
                if (elem.NodeType == XmlNodeType.Element)
                {
                    switch (elem.Name)
                    {
                        case "cStat":
                            elem.Read();
                            if (elem.Value == "111" || elem.Value == "112")
                            {
                                deuErro = false;
                            }
                            break;
                        case "xMotivo":
                            elem.Read();
                            if (deuErro)
                            {
                                return elem.Value;
                            }
                            break;
                        case "IE":
                            elem.Read();
                            ie = elem.Value;
                            break;
                        case "CPF":
                            elem.Read();
                            cpf = elem.Value;
                            break;
                        case "CNPJ":
                            elem.Read();
                            cnpj = elem.Value;
                            break;
                        case "xNome":
                            elem.Read();
                            xNome = elem.Value;
                            break;
                        case "cSit":
                            elem.Read();
                            if (elem.Value == "0")
                                situacao = "não habilitado";
                            else if (elem.Value == "1")
                                situacao = "habilitado";
                            break;
                        default:
                            break;
                    }
                }
            }
            var forma = "{0}\r\nCPF:\t{1}\r\nCNPJ:\t{2}\r\nIE:\t{3}\r\nSituação:\t{4}";
            var output = String.Format(forma, xNome, cpf, cnpj, ie, situacao);
            return output;
        }
        */

        private static DateTime getDateTime(string value)
        {
            if (string.IsNullOrEmpty(value))
                return DateTime.MinValue;

            int _ano = Convert.ToInt16(value.Substring(0, 4));
            int _mes = Convert.ToInt16(value.Substring(5, 2));
            int _dia = Convert.ToInt16(value.Substring(8, 2));
            if (value.Contains("T") && value.Contains(":"))
            {
                int _hora = Convert.ToInt16(value.Substring(11, 2));
                int _min = Convert.ToInt16(value.Substring(14, 2));
                int _seg = Convert.ToInt16(value.Substring(17, 2));
                return new DateTime(_ano, _mes, _dia, _hora, _min, _seg);
            }
            return new DateTime(_ano, _mes, _dia);
        }

        private static RetConsCad ProcessaConsultaCadastroClass(XmlTextReader elem)
        {
            RetConsCad vRetorno = new RetConsCad();

            while (elem.Read())
            {
                if (elem.NodeType == XmlNodeType.Element)
                {
                    switch (elem.Name)
                    {
                        case "infCad":
                            elem.Read();
                            vRetorno.infCad.Add(new infCad());
                            break;

                        case "cStat":
                            elem.Read();
                            vRetorno.cStat = Convert.ToInt32("0" + elem.Value);
                            break;
                        case "xMotivo":
                            elem.Read();
                            vRetorno.xMotivo = elem.Value;
                            break;
                        case "UF":
                            elem.Read();
                            if (vRetorno.infCad.Count > 0)
                                vRetorno.infCad[vRetorno.infCad.Count - 1].UF = elem.Value;
                            else
                                vRetorno.UF = elem.Value;
                            break;
                        case "IE":
                            elem.Read();
                            if (vRetorno.infCad.Count > 0)
                                vRetorno.infCad[vRetorno.infCad.Count - 1].IE = elem.Value;
                            else
                                vRetorno.IE = elem.Value;
                            break;
                        case "CNPJ":
                            elem.Read();
                            if (vRetorno.infCad.Count > 0)
                                vRetorno.infCad[vRetorno.infCad.Count - 1].CNPJ = elem.Value;
                            else
                                vRetorno.CNPJ = elem.Value;
                            break;
                        case "CPF":
                            elem.Read();
                            if (vRetorno.infCad.Count > 0)
                                vRetorno.infCad[vRetorno.infCad.Count - 1].CNPJ = elem.Value;
                            else
                                vRetorno.CPF = elem.Value;
                            break;
                        case "dhCons":
                            elem.Read();
                            vRetorno.dhCons = getDateTime(elem.Value);
                            break;
                        case "cUF":
                            elem.Read();
                            vRetorno.cUF = elem.Value;
                            break;

                        case "xNome":
                            elem.Read();
                            vRetorno.infCad[vRetorno.infCad.Count - 1].xNome = elem.Value;
                            break;
                        case "xFant":
                            elem.Read();
                            vRetorno.infCad[vRetorno.infCad.Count - 1].xFant = elem.Value;
                            break;
                        case "xLgr":
                            elem.Read();
                            vRetorno.infCad[vRetorno.infCad.Count - 1].xLgr = elem.Value;
                            break;
                        case "nro":
                            elem.Read();
                            vRetorno.infCad[vRetorno.infCad.Count - 1].nro = elem.Value;
                            break;
                        case "xCpl":
                            elem.Read();
                            vRetorno.infCad[vRetorno.infCad.Count - 1].xCpl = elem.Value;
                            break;
                        case "xBairro":
                            elem.Read();
                            vRetorno.infCad[vRetorno.infCad.Count - 1].xBairro = elem.Value;
                            break;
                        case "xMun":
                            elem.Read();
                            vRetorno.infCad[vRetorno.infCad.Count - 1].xMun = elem.Value;
                            break;
                        case "cMun":
                            elem.Read();
                            vRetorno.infCad[vRetorno.infCad.Count - 1].cMun = Convert.ToInt32("0" + elem.Value);
                            break;
                        case "CEP":
                            elem.Read();
                            vRetorno.infCad[vRetorno.infCad.Count - 1].CEP = Convert.ToInt32("0" + elem.Value);
                            break;
                        case "IEAtual":
                            elem.Read();
                            vRetorno.infCad[vRetorno.infCad.Count - 1].IEAtual = elem.Value;
                            break;
                        case "IEUnica":
                            elem.Read();
                            vRetorno.infCad[vRetorno.infCad.Count - 1].IEUnica = elem.Value;
                            break;
                        case "dBaixa":
                            elem.Read();
                            vRetorno.infCad[vRetorno.infCad.Count - 1].dBaixa = getDateTime(elem.Value);
                            break;
                        case "dUltSit":
                            elem.Read();
                            vRetorno.infCad[vRetorno.infCad.Count - 1].dUltSit = getDateTime(elem.Value);
                            break;
                        case "dIniAtiv":
                            elem.Read();
                            vRetorno.infCad[vRetorno.infCad.Count - 1].dIniAtiv = getDateTime(elem.Value);
                            break;
                        case "CNAE":
                            elem.Read();
                            vRetorno.infCad[vRetorno.infCad.Count - 1].CNAE = Convert.ToInt32("0" + elem.Value);
                            break;
                        case "xRegApur":
                            elem.Read();
                            vRetorno.infCad[vRetorno.infCad.Count - 1].xRegApur = elem.Value;
                            break;
                        case "cSit":
                            elem.Read();
                            if (elem.Value == "0")
                                vRetorno.infCad[vRetorno.infCad.Count - 1].cSit = "Contribuinte não habilitado";
                            else if (elem.Value == "1")
                                vRetorno.infCad[vRetorno.infCad.Count - 1].cSit = "Contribuinte habilitado";
                            break;
                    }
                }
            }
            //if (infcad != null)
              //  vRetorno.infCad.Add(infcad);
            return vRetorno;
        }

        /// <summary>
        /// Escopo do CalBack de analise de resposta da EnviarArquivoEReceberResposta
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        delegate object Processa(XmlTextReader elem);

        /// <summary>
        /// Envia um arquivo para o webservice da NFE e recebe a resposta. 
        /// </summary>
        /// <returns>Retorna uma string com a mensagem obtida do webservice de status do serviço da NFe</returns>
        /// <example>string vPastaArq = this.CriaArqXMLStatusServico();</example>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>17/06/2009</date>
        private object EnviaArquivoERecebeResposta(string ArqXMLRetorno, string ArqERRRetorno, Processa processa)
        {
            object vStatus = "Ocorreu uma falha ao tentar obter a situação do serviço. Aguarde um momento e tente novamente.";
            DateTime startTime;
            DateTime stopTime;
            TimeSpan elapsedTime;

            long elapsedMillieconds;
            startTime = DateTime.Now;

            while (true)
            {
                stopTime = DateTime.Now;
                elapsedTime = stopTime.Subtract(startTime);
                elapsedMillieconds = (int)elapsedTime.TotalMilliseconds;

                if (elapsedMillieconds >= 120000) //120.000 ms que corresponde á 120 segundos que corresponde a 2 minutos
                {
                    break;
                }

                if (File.Exists(ArqXMLRetorno))
                {
                    try
                    {
                        //Verificar se consegue abrir o arquivo em modo exclusivo, se conseguir ele dá sequencia
                        using (FileStream fs = File.Open(ArqXMLRetorno, FileMode.Open, FileAccess.ReadWrite, FileShare.Write))
                        {
                            //Conseguiu abrir o arquivo, significa que está perfeitamente gerado
                            //assim vou iniciar o processo de envio do XML
                            fs.Close();

                            //Ler o status do serviço no XML retornado pelo WebService
                            XmlTextReader oLerXml = new XmlTextReader(ArqXMLRetorno);

                            try
                            {
                                vStatus = processa(oLerXml);
                            }
                            catch
                            {
                                //Se não conseguir ler o arquivo vai somente retornar ao loop para tentar novamente, pois 
                                //pode ser que o arquivo esteja em uso ainda.
                            }
                            oLerXml.Close();

                            //Detetar o arquivo de retorno
                            try
                            {
                                FileInfo oArquivoDel = new FileInfo(ArqXMLRetorno);
                                oArquivoDel.Delete();
                                break;
                            }
                            catch
                            {
                                //Somente deixa fazer o loop novamente e tentar deletar
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                else if (File.Exists(ArqERRRetorno))
                {
                    //Retornou um arquivo com a extensão .ERR, ou seja, deu um erro,
                    //futuramente tem que retornar esta mensagem para a MessageBox do usuário.

                    //Detetar o arquivo de retorno
                    try
                    {
                        vStatus = System.IO.File.ReadAllText(ArqERRRetorno, Encoding.Default);
                        System.IO.File.Delete(ArqERRRetorno);
                        break;
                    }
                    catch
                    {
                        //Somente deixa fazer o loop novamente e tentar deletar
                    }
                }
                Thread.Sleep(5000);
            }
            //Retornar o status do serviço
            return vStatus;
        }
        #endregion

        #region GerarChaveNFe
        /// <summary>
        /// MontaChave
        /// Cria a chave de acesso da NFe
        /// </summary>
        /// <param name="ArqXMLPedido"></param>
        public void GerarChaveNFe(string ArqPedido, Boolean xml)
        {
            // XML - pedido
            // Filename: XXXXXXXX-gerar-chave.xml
            // -------------------------------------------------------------------
            // <?xml version="1.0" encoding="UTF-8"?>
            // <gerarChave>
            //      <UF>35</UF>                 //se não informado, assume a da configuracao
            //      <nNF>1000</nNF>
            //      <cNF>0</cNF>                //se não informado, eu gero
            //      <serie>1</serie>
            //      <AAMM>0912</AAMM>
            //      <CNPJ>55801377000131</CNPJ>
            // </gerarChave>
            //
            // XML - resposta
            // Filename: XXXXXXXX-ret-gerar-chave.xml
            // -------------------------------------------------------------------
            // <?xml version="1.0" encoding="UTF-8"?>
            // <retGerarChave>
            //      <chaveNFe>350912</chaveNFe>
            // </retGerarChave>
            //

            // TXT - pedido
            // Filename: XXXXXXXX-gerar-chave.txt
            // -------------------------------------------------------------------
            // UF|35
            // nNF|1000
            // cNF|0
            // serie|1
            // AAMM|0912
            // CNPJ|55801377000131
            //
            // TXT - resposta
            // Filename: XXXXXXXX-ret-gerar-chave.txt
            // -------------------------------------------------------------------
            // 35091255801377000131550010000000010000176506
            //
            // -------------------------------------------------------------------
            // ERR - resposta
            // Filename: XXXXXXXX-gerar-chave.err

            string ArqXMLRetorno = ConfiguracaoApp.vPastaXMLRetorno + "\\" + (xml ? this.ExtrairNomeArq(ArqPedido, ExtXml.GerarChaveNFe_XML) + "-ret-gerar-chave.xml" : this.ExtrairNomeArq(ArqPedido, ExtXml.GerarChaveNFe_TXT) + "-ret-gerar-chave.txt");
            string ArqERRRetorno = ConfiguracaoApp.vPastaXMLRetorno + "\\" + (xml ? this.ExtrairNomeArq(ArqPedido, ExtXml.GerarChaveNFe_XML) + "-gerar-chave.err" : this.ExtrairNomeArq(ArqPedido, ExtXml.GerarChaveNFe_TXT) + "-gerar-chave.err");

            this.DeletarArquivo(ArqXMLRetorno);
            this.DeletarArquivo(ArqERRRetorno);
            this.DeletarArquivo(ConfiguracaoApp.vPastaXMLErro + "\\" + ArqPedido);

            try
            {
                if (!File.Exists(ArqPedido))
                {
                    throw new Exception("Arquivo " + ArqPedido + " não encontrado");
                }
                UnitxtTOxmlClass oUniTxtToXml = new UnitxtTOxmlClass();

                using (FileStream fs = File.Open(ArqPedido, FileMode.Open, FileAccess.ReadWrite, FileShare.Write))
                {
                    fs.Close();

                    int serie = 0;
                    int nNF = 0;
                    int cNF = 0;
                    int cUF = ConfiguracaoApp.UFCod;
                    string cAAMM = "0000";
                    string cChave = "";
                    string cCNPJ = "";
                    string cError = "";

                    if (xml)
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(ArqPedido);

                        XmlNodeList mChaveList = doc.GetElementsByTagName("gerarChave");

                        foreach (XmlNode mChaveNode in mChaveList)
                        {
                            XmlElement mChaveElemento = (XmlElement)mChaveNode;

                            if (mChaveElemento.GetElementsByTagName("UF").Count != 0)
                                cUF = Convert.ToInt32("0" + mChaveElemento.GetElementsByTagName("UF")[0].InnerText);

                            if (mChaveElemento.GetElementsByTagName("nNF").Count != 0)
                                nNF = Convert.ToInt32("0" + mChaveElemento.GetElementsByTagName("nNF")[0].InnerText);

                            if (mChaveElemento.GetElementsByTagName("cNF").Count != 0)
                                cNF = Convert.ToInt32("0" + mChaveElemento.GetElementsByTagName("cNF")[0].InnerText);

                            if (mChaveElemento.GetElementsByTagName("serie").Count != 0)
                                serie = Convert.ToInt32("0" + mChaveElemento.GetElementsByTagName("serie")[0].InnerText);

                            if (mChaveElemento.GetElementsByTagName("AAMM").Count != 0)
                                cAAMM = mChaveElemento.GetElementsByTagName("AAMM")[0].InnerText;

                            if (mChaveElemento.GetElementsByTagName("CNPJ").Count != 0)
                                cCNPJ = mChaveElemento.GetElementsByTagName("CNPJ")[0].InnerText;
                        }
                    }
                    else
                    {
                        TextReader txt = new StreamReader(ArqPedido);
                        string cLinhaTXT = txt.ReadLine();
                        string[] dados;
                        while (cLinhaTXT != null)
                        {
                            dados = cLinhaTXT.Split('|');
                            dados[0] = dados[0].ToUpper();
                            if (dados.GetLength(0) == 1)
                                cError += "Segmento [" + dados[0] + "] inválido";
                            else
                                switch (dados[0])
                                {
                                    case "UF":
                                        cUF = Convert.ToInt32("0" + dados[1]);
                                        break;
                                    case "NNF":
                                        nNF = Convert.ToInt32("0" + dados[1]);
                                        break;
                                    case "CNF":
                                        cNF = Convert.ToInt32("0" + dados[1]);
                                        break;
                                    case "SERIE":
                                        serie = Convert.ToInt32("0" + dados[1]);
                                        break;
                                    case "AAMM":
                                        cAAMM = dados[1];
                                        break;
                                    case "CNPJ":
                                        cCNPJ = dados[1];
                                        break;
                                }
                            cLinhaTXT = txt.ReadLine();
                        }
                        txt.Close();
                    }
                    if (nNF == 0)
                        cError = "Número da nota fiscal deve ser informado" + Environment.NewLine;

                    if (string.IsNullOrEmpty(cAAMM))
                        cError += "Ano e mês da emissão deve ser informado" + Environment.NewLine;

                    if (string.IsNullOrEmpty(cCNPJ))
                        cError += "CNPJ deve ser informado" + Environment.NewLine;

                    //System.Windows.Forms.MessageBox.Show(cAAMM);

                    if (cAAMM.Substring(0, 2) == "00")
                        cError += "Ano da emissão inválido" + Environment.NewLine;

                    //System.Windows.Forms.MessageBox.Show(cChave + "\n\r" + cUF.ToString() + "\n\r" + nNF.ToString() + "\n\r" + cNF.ToString() + "\n\r" + serie.ToString() + "\n\r" + cCNPJ + "\n\r" + cAAMM);

                    if (Convert.ToInt32(cAAMM.Substring(2, 2)) <= 0 || Convert.ToInt32(cAAMM.Substring(2, 2)) > 12)
                        cError += "Mês da emissão inválido" + Environment.NewLine;

                    if (cError != "")
                        throw new Exception(cError);

                    //System.Windows.Forms.MessageBox.Show(cChave + "\n\r" + cUF.ToString() + "\n\r" + nNF.ToString() + "\n\r" + cNF.ToString() + "\n\r" + serie.ToString() + "\n\r" + cCNPJ + "\n\r" + cAAMM);

                    Int64 iTmp = Convert.ToInt64("0" + cCNPJ);
                    cChave = cUF.ToString("00") + cAAMM + iTmp.ToString("00000000000000") + "55";

                    //System.Windows.Forms.MessageBox.Show(cChave);

                    if (cNF == 0)
                    {
                        ///
                        /// gera codigo aleatorio
                        /// 
                        //System.Windows.Forms.MessageBox.Show("go cNF");
                        cNF = oUniTxtToXml.GerarCodigoNumerico(nNF);

                        //System.Windows.Forms.MessageBox.Show(cNF.ToString());
                    }
                    ///
                    /// calcula do digito verificador
                    /// 
                    string ccChave = cChave + serie.ToString("000") + nNF.ToString("000000000") + cNF.ToString("000000000");
                    int cDV = oUniTxtToXml.GerarDigito(ccChave);
                    ///
                    /// monta a chave da NFe
                    /// 
                    cChave += serie.ToString("000") + nNF.ToString("000000000") + cNF.ToString("000000000") + cDV.ToString("0");

                    //System.Windows.Forms.MessageBox.Show(cChave + "\n\r" + cUF.ToString() + "\n\r" + nNF.ToString() + "\n\r" + cNF.ToString() + "\n\r" + serie.ToString() + "\n\r" + cCNPJ + "\n\r" + cAAMM);

                    ///
                    /// grava o XML/TXT de resposta
                    /// 
                    string vMsgRetorno = (xml ? "<?xml version=\"1.0\" encoding=\"UTF-8\"?><retGerarChave><chaveNFe>" + cChave + "</chaveNFe></retGerarChave>" : cChave);
                    File.WriteAllText(ArqXMLRetorno, vMsgRetorno, Encoding.Default);
                    ///
                    /// exclui o XML/TXT de pedido
                    /// 
                    this.DeletarArquivo(ArqPedido);
                }
            }
            catch (Exception ex)
            {
                this.MoveArqErro(ArqPedido);
                File.WriteAllText(ArqERRRetorno, "Arquivo " + ArqERRRetorno + Environment.NewLine + ex.Message, Encoding.Default);
            }
        }
        #endregion

        #region MemoryStream
        /// <summary>
        /// Método responsável por converter uma String contendo a estrutura de um XML em uma Stream para
        /// ser lida pela XMLDocument
        /// </summary>
        /// <returns>String convertida em Stream</returns>
        /// <remarks>Conteúdo do método foi fornecido pelo Marcelo da desenvolvedores.net</remarks>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>20/04/2009</date>
        public static MemoryStream StringXmlToStream(string strXml)
        {
            byte[] byteArray = new byte[strXml.Length];
            System.Text.ASCIIEncoding encoding = new
            System.Text.ASCIIEncoding();
            byteArray = encoding.GetBytes(strXml);
            MemoryStream memoryStream = new MemoryStream(byteArray);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;
        }
        #endregion

        #region FileInUse()
        /// <summary>
        /// detectar se o arquivo está em uso
        /// </summary>
        /// <param name="file">caminho do arquivo</param>
        /// <returns>true se estiver em uso</returns>
        /// <by>http://desenvolvedores.net/marcelo</by>
        public static bool FileInUse(string file)
        {
            bool ret = false;

            try
            {
                using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
                {
                    fs.Close();//fechar o arquivo para nao dar erro em outras aplicações
                }
                return ret;
            }
            catch (IOException ex)
            {
                ret = true;
            }
            return ret;
        }
        #endregion
    }
    
    public class infCad
    {
        public string IE { get; set; }
        public string CNPJ { get; set; }
        public string CPF { get; set; }
        public string UF { get; set; }
        public string xNome { get; set; }
        public string xFant { get; set; }
        public string xLgr { get; set; }
        public string nro { get; set; }
        public string xCpl { get; set; }
        public string xBairro { get; set; }
        public string xMun { get; set; }
        public int cMun { get; set; }
        public int CEP { get; set; }
        public string IEAtual { get; set; }
        public string IEUnica { get; set; }
        public DateTime dBaixa { get; set; }
        public DateTime dUltSit { get; set; }
        public DateTime dIniAtiv { get; set; }
        public int CNAE { get; set; }
        public string xRegApur { get; set; }
        public string cSit { get; set; }
    }

    public class RetConsCad
    {
        public int cStat { get; set; }
        public string xMotivo { get; set; }
        public string UF { get; set; }
        public string IE { get; set; }
        public string CNPJ {get; set; }
        public string CPF {get; set; }
        public DateTime dhCons { get; set; }
        public string cUF { get; set; }
        public List<infCad> infCad { get; set; }

        public RetConsCad()
        {
            infCad = new List<infCad>();
        }
    }
}