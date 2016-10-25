using NFe.ConvertCFe.Data.Recepcao;
using NFe.Validate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace NFe.ConvertCFe.Conversao
{
    public class ConverterNFCe
    {
        private FileStream FileStream;

        private XmlDocument Document;

        private envCFe ObjEnvio = new envCFe();

        private string InputFile;

        private string OutputFile;

        public string XMLOutput = "";

        public ConverterNFCe(string file, string outputfile = null)
        {
            if (outputfile == null)
                outputfile = file;

            InputFile = file;
            OutputFile = outputfile;

            FileStream = new FileStream(InputFile, FileMode.Open, FileAccess.ReadWrite);

            Document = new XmlDocument();
            Document.Load(FileStream);
        }

        public void ConverterSAT()
        {
            Random r = new Random();

            ObjEnvio.versao = "0.06"; //TODO: SAT por enquanto vai fixo
            ObjEnvio.nserieSAT = "900002188"; //TODO: SAT ver se tem no aparelho sat

            ObjEnvio.cUF = GetValueXML("ide", "cUF");
            ObjEnvio.dhEnvio = GetValueXML("ide", "dhEmi").Substring(0, 19).Replace("T", "").Replace(":", "").Replace("-", "");
            ObjEnvio.nSeg = "assinaturadigitaldonumerodesegurança"; //TODO: SAT ver de onde vai pegar isso
            ObjEnvio.tpAmb = GetValueXML("ide", "tpAmb");
            ObjEnvio.LoteCFe = new List<envCFeCFe>();
            ObjEnvio.LoteCFe.Add(GerarLoteCFe());
            ObjEnvio.idLote = r.Next(11111, 99999).ToString() + r.Next(11111, 99999).ToString() + r.Next(11111, 99999).ToString(); //TODO: SAT utilizar sequencia de lote do UniNFe?

            if (File.Exists(OutputFile))
                File.Delete(OutputFile);
           
            XMLOutput = ObjEnvio.Serialize(); 
            XmlDocument xmloutput = new XmlDocument();
            xmloutput.LoadXml(XMLOutput);
            xmloutput.Save(OutputFile);

            //TODO: SAT Já foi adicionado os schema's só, que nos testes unitários não foi carregado as configurações da empresa
            // ValidarCFe();
        }

        /// <summary>
        /// Gerar arquivo de lote a partir da NFCe
        /// </summary>
        /// <returns>Lote da CFe</returns>
        private envCFeCFe GerarLoteCFe()
        {
            string chave = "CFe" + CalcularChaveAcesso();
            return new envCFeCFe
            {
                infCFe = new envCFeCFeInfCFe
                {
                    Id = chave,
                    versao = ObjEnvio.versao,
                    versaoDadosEnt = ObjEnvio.versao,
                    versaoSB = "010100", //TODO: SAT ver de onde vai pegar isso                    
                    ide = new envCFeCFeInfCFeIde
                    {
                        cUF = ObjEnvio.cUF,
                        cNF = GetValueXML("ide", "cNF"),
                        mod = GetValueXML("ide", "mod"),
                        nserieSAT = ObjEnvio.nserieSAT,
                        nCFe = GetValueXML("ide", "nNF"),
                        dEmi = GetValueXML("ide", "dhEmi").Substring(0, 10).Replace("-", ""),
                        hEmi = GetValueXML("ide", "dhEmi").Substring(11, 8).Replace(":", ""),
                        cDV = chave.Substring(chave.Length - 1),
                        tpAmb = GetValueXML("ide", "tpAmb"),
                        CNPJ = "06117473000150", //TODO: SAT ver de onde vai pegar isso
                        signAC = "IUxeuAuMWtSc0VEblDhfoeo9giv1Y1cxCLqoAQqmWBELhTu7JNmJFVyflgPN9lyzjgdqCXbo9ZlhpiGGyuDOa0wbI1NfXrO39JwmyShOKsG08YDupiLGioPMi0/PabBoQ1mDAtzdAEuPblPR1Fhka8UWrHNbMBs7INi1izkMOYqaSGOO2cbifiBQVBb1P3j5/tNDQwloKODB/zsRA5cGoR0DuDs+gCRRxpzZpsCgZuKO/shVu45iwcQ4GZRx1mSP5ifAaDK1QWMej0deCi/GeK8IERAS7PwhWuIsW8+pxjnAixSg+x/55fIYuog/HApvCQBLhLoEjjoJyCcE6QpgaA==", //TODO: ver de onde vai pegar isso
                        assinaturaQRCODE = "MvLQhTadPDGaTyjePyiPoJccKO5CIb8aIpzg41JhBxuqZyrtchebVfijQdlVamlX3cRFaPV1vGQ+CvX7aYVx+cUz9mcaIKn8EVHNH+op2BtWMHE6xo2hUZXizGsghE62QA+/oBVzMFSyDbDlOHyp9AXsUW8X50zEGAwznVkRe/6GT9cArk2V9EElSDcxxs6uXKN7ufEBNuUhRCwG8u4FgPi9H4itYYdi31H8r5MEtG7B02Hcjy39RfP+SdqP6zc3SdSnYJ+qZB+V7DyJuod6G35u3zBlgaKl3os6WBGUlkE6VM8OFphPEprvUeRk76rv0LdUK7v+vEfzaZWtMSkF/Q==", //TODO: ver de onde vai pegar isso
                        numeroCaixa = "999" //TODO: SAT ver de onde vai pegar isso
                    },
                    emit = new envCFeCFeInfCFeEmit
                    {
                        CNPJ = GetValueXML("emit", "CNPJ"),
                        xNome = GetValueXML("emit", "xNome"),
                        xFant = GetValueXML("emit", "xFant"),
                        enderEmit = new envCFeCFeInfCFeEmitEnderEmit
                        {
                            xLgr = GetValueXML("enderEmit", "xLgr"),
                            nro = GetValueXML("enderEmit", "nro"),
                            xCpl = GetValueXML("enderEmit", "xCpl"),
                            xBairro = GetValueXML("enderEmit", "xBairro"),
                            xMun = GetValueXML("enderEmit", "xMun"),
                            CEP = GetValueXML("enderEmit", "CEP")
                        },
                        IE = GetValueXML("emit", "IE"),
                        cRegTrib = GetValueXML("emit", "cRegTrib"),
                        cRegTribISSQN = GetValueXML("emit", "cRegTribISSQN"),
                        IM = GetValueXML("emit", "IM"),
                        indRatISSQN = GetValueXML("emit", "indRatISSQN"),
                    },
                    dest = new envCFeCFeInfCFeDest
                    {
                        Item = GetDocumentoPessoa("dest"),
                        ItemElementName = (GetDocumentoPessoa("dest").Length == 11 ? ItemChoiceType.CPF : ItemChoiceType.CNPJ),
                        xNome = GetValueXML("dest", "xNome")
                    },
                    det = PopularProdutos(),
                    total = new envCFeCFeInfCFeTotal
                    {
                        ICMSTot = new envCFeCFeInfCFeTotalICMSTot
                        {
                            vICMS = GetValueXML("ICMSTot", "vICMS"),
                            vProd = GetValueXML("ICMSTot", "vProd"),
                            vDesc = GetValueXML("ICMSTot", "vDesc"),
                            vPIS = GetValueXML("ICMSTot", "vPIS"),
                            vCOFINS = GetValueXML("ICMSTot", "vCOFINS"),
                            vPISST = GetValueXML("ICMSTot", "vPISST"),
                            vCOFINSST = GetValueXML("ICMSTot", "vCOFINSST"),
                            vOutro = GetValueXML("ICMSTot", "vOutro")
                        },
                        vCFe = GetValueXML("ICMSTot", "vNF")
                    },
                    entrega = new envCFeCFeInfCFeEntrega
                    {
                        xLgr = GetValueXML("enderEmit", "xLgr"),
                        nro = GetValueXML("enderEmit", "nro"),
                        xCpl = GetValueXML("enderEmit", "xCpl"),
                        xBairro = GetValueXML("enderEmit", "xBairro"),
                        xMun = GetValueXML("enderEmit", "xMun"),
                        UF = GetValueXML("enderEmit", "UF")
                    },
                    pgto = new envCFeCFeInfCFePgto
                    {
                        MP = PopularMeioPagamento(),
                        vTroco = "0.00"
                    }
                }
            };
        }

        /// <summary>
        /// Adiciona os itens da nota ao Lote da CFe
        /// </summary>
        /// <returns>Lista de Produtos</returns>
        private List<envCFeCFeInfCFeDet> PopularProdutos()
        {
            List<envCFeCFeInfCFeDet> result = new List<envCFeCFeInfCFeDet>();

            XmlNodeList nodes = Document.GetElementsByTagName("det");
            foreach (XmlNode detNFCe in nodes)
            {
                envCFeCFeInfCFeDet det = new envCFeCFeInfCFeDet();
                det.nItem = detNFCe.Attributes["nItem"].Value;

                foreach (XmlNode itensDet in detNFCe.ChildNodes)
                {
                    switch (itensDet.Name)
                    {
                        case "prod":
                            det.prod = new envCFeCFeInfCFeDetProd
                            {
                                cProd = GetXML(itensDet.ChildNodes, "cProd"),
                                xProd = GetXML(itensDet.ChildNodes, "xProd"),
                                NCM = GetXML(itensDet.ChildNodes, "NCM"),
                                CFOP = GetXML(itensDet.ChildNodes, "CFOP"),
                                uCom = GetXML(itensDet.ChildNodes, "uCom"),
                                qCom = GetXML(itensDet.ChildNodes, "qCom"),
                                vUnCom = GetXML(itensDet.ChildNodes, "vUnCom"),
                                vProd = GetXML(itensDet.ChildNodes, "vProd"),
                                indRegra = "T",
                                vItem = GetXML(itensDet.ChildNodes, "vProd")
                            };
                            break;

                        case "imposto":
                            det.imposto = new envCFeCFeInfCFeDetImposto();

                            foreach (XmlNode n in itensDet.ChildNodes)
                            {
                                switch (n.Name)
                                {
                                    case "ICMS":
                                        det.imposto.Item = ImpostoProduto<envCFeCFeInfCFeDetImpostoICMS>(n.ChildNodes);
                                        break;

                                    case "PIS":
                                        det.imposto.PIS = ImpostoProduto<envCFeCFeInfCFeDetImpostoPIS>(n.ChildNodes);
                                        break;

                                    case "COFINS":
                                        det.imposto.COFINS = ImpostoProduto<envCFeCFeInfCFeDetImpostoCOFINS>(n.ChildNodes);
                                        break;

                                    default:
                                        break;
                                }
                            }
                            break;

                        default:
                            break;
                    }
                }


                result.Add(det);
            }

            return result;
        }

        /// <summary>
        /// Converte a tag <pag> para <MP> utilizada para formas de pagamento
        /// </summary>
        /// <returns>lista de formas de pagamento</returns>
        private List<envCFeCFeInfCFePgtoMP> PopularMeioPagamento()
        {
            List<envCFeCFeInfCFePgtoMP> result = new List<envCFeCFeInfCFePgtoMP>();

            XmlNodeList nodes = Document.GetElementsByTagName("pag");
            foreach (XmlNode pagNFCe in nodes)
            {
                envCFeCFeInfCFePgtoMP mp = new envCFeCFeInfCFePgtoMP
                {
                    cMP = GetXML(pagNFCe.ChildNodes, "tPag"),
                    vMP = GetXML(pagNFCe.ChildNodes, "vPag")
                };

                result.Add(mp);
            }
            return result;
        }

        /// <summary>
        /// Grava valores de impostos nos produtos
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="childs"></param>
        /// <returns></returns>
        private T ImpostoProduto<T>(XmlNodeList childs)
            where T : new()
        {
            T result = new T();

            foreach (XmlNode tag in childs)
            {
                switch (tag.Name)
                {
                    #region ICMSSN102
                    case "ICMSSN102":
                        envCFeCFeInfCFeDetImpostoICMSICMSSN102 ICMSSN102 = new envCFeCFeInfCFeDetImpostoICMSICMSSN102
                        {
                            CSOSN = GetXML(tag.ChildNodes, "CSOSN"),
                            Orig = GetXML(tag.ChildNodes, "orig")
                        };

                        SetProperrty(result, "Item", ICMSSN102);
                        break;
                    #endregion

                    #region ICMSSN900
                    case "ICMSSN900":
                        envCFeCFeInfCFeDetImpostoICMSICMSSN900 ICMSSN900 = new envCFeCFeInfCFeDetImpostoICMSICMSSN900
                        {
                            CSOSN = GetXML(tag.ChildNodes, "CSOSN"),
                            Orig = GetXML(tag.ChildNodes, "orig")
                        };

                        SetProperrty(result, "Item", ICMSSN900);
                        break;
                    #endregion

                    #region PISAliq
                    case "PISAliq":
                        envCFeCFeInfCFeDetImpostoPISPISAliq PISAliq = new envCFeCFeInfCFeDetImpostoPISPISAliq
                        {
                            CST = GetXML(tag.ChildNodes, "CST"),
                            pPIS = GetXML(tag.ChildNodes, "pPIS"),
                            vBC = GetXML(tag.ChildNodes, "vBC"),
                            vPIS = GetXML(tag.ChildNodes, "vPIS")
                        };

                        SetProperrty(result, "Item", PISAliq);
                        break;
                    #endregion

                    #region PISNT
                    case "PISNT":
                        envCFeCFeInfCFeDetImpostoPISPISNT PISPISNT = new envCFeCFeInfCFeDetImpostoPISPISNT
                        {
                            CST = GetXML(tag.ChildNodes, "CST")
                        };

                        SetProperrty(result, "Item", PISPISNT);
                        break;
                    #endregion

                    #region PISOutr
                    case "PISOutr":
                        envCFeCFeInfCFeDetImpostoPISPISOutr PISOutr = new envCFeCFeInfCFeDetImpostoPISPISOutr
                        {
                            CST = GetXML(tag.ChildNodes, "CST"),
                            vPIS = GetXML(tag.ChildNodes, "vPIS")
                        };

                        SetProperrty(result, "Item", PISOutr);
                        break;
                    #endregion

                    #region PISQtde
                    case "PISQtde":
                        envCFeCFeInfCFeDetImpostoPISPISQtde PISQtde = new envCFeCFeInfCFeDetImpostoPISPISQtde
                        {
                            CST = GetXML(tag.ChildNodes, "CST"),
                            qBCProd = GetXML(tag.ChildNodes, "qBCProd"),
                            vAliqProd = GetXML(tag.ChildNodes, "vAliqProd"),
                            vPIS = GetXML(tag.ChildNodes, "vPIS")
                        };

                        SetProperrty(result, "Item", PISQtde);
                        break;
                    #endregion

                    #region PISSN
                    case "PISSN":
                        envCFeCFeInfCFeDetImpostoPISPISSN PISSN = new envCFeCFeInfCFeDetImpostoPISPISSN
                        {
                            CST = GetXML(tag.ChildNodes, "CST")
                        };
                        SetProperrty(result, "Item", PISSN);
                        break;
                    #endregion

                    #region COFINSAliq
                    case "COFINSAliq":
                        envCFeCFeInfCFeDetImpostoCOFINSCOFINSAliq COFINSAliq = new envCFeCFeInfCFeDetImpostoCOFINSCOFINSAliq
                        {
                            CST = GetXML(tag.ChildNodes, "CST"),
                            pCOFINS = GetXML(tag.ChildNodes, "pCOFINS"),
                            vCOFINS = GetXML(tag.ChildNodes, "vCOFINS"),
                            vBC = GetXML(tag.ChildNodes, "vBC")
                        };
                        SetProperrty(result, "Item", COFINSAliq);
                        break;
                    #endregion

                    #region COFINSNT
                    case "COFINSNT":
                        envCFeCFeInfCFeDetImpostoCOFINSCOFINSNT COFINSNT = new envCFeCFeInfCFeDetImpostoCOFINSCOFINSNT
                        {
                            CST = GetXML(tag.ChildNodes, "CST")
                        };
                        SetProperrty(result, "Item", COFINSNT);
                        break;
                    #endregion

                    #region COFINSNT
                    case "COFINSOutr":
                        envCFeCFeInfCFeDetImpostoCOFINSCOFINSOutr COFINSOutr = new envCFeCFeInfCFeDetImpostoCOFINSCOFINSOutr
                        {
                            CST = GetXML(tag.ChildNodes, "CST"),
                            vCOFINS = GetXML(tag.ChildNodes, "vCOFINS")
                        };
                        SetProperrty(result, "Item", COFINSOutr);
                        break;
                    #endregion

                    #region COFINSNT
                    case "COFINSQtde":
                        envCFeCFeInfCFeDetImpostoCOFINSCOFINSQtde COFINSQtde = new envCFeCFeInfCFeDetImpostoCOFINSCOFINSQtde
                        {
                            CST = GetXML(tag.ChildNodes, "CST"),
                            qBCProd = GetXML(tag.ChildNodes, "qBCProd"),
                            vCOFINS = GetXML(tag.ChildNodes, "vCOFINS"),
                            vAliqProd = GetXML(tag.ChildNodes, "vAliqProd")
                        };
                        SetProperrty(result, "Item", COFINSQtde);
                        break;
                    #endregion

                    #region COFINSSN
                    case "COFINSSN":
                        envCFeCFeInfCFeDetImpostoCOFINSCOFINSSN COFINSSN = new envCFeCFeInfCFeDetImpostoCOFINSCOFINSSN
                        {
                            CST = GetXML(tag.ChildNodes, "CST")
                        };
                        SetProperrty(result, "Item", COFINSSN);
                        break;
                    #endregion

                    default:
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// Adiciona valores por referencia nos objetos
        /// </summary>
        /// <param name="result">objeto que será definido o valor</param>
        /// <param name="propertyName">nome da propriedade</param>
        /// <param name="value">valor que será definido</param>
        private void SetProperrty(object result, string propertyName, object value)
        {
            PropertyInfo pi = result.GetType().GetProperty(propertyName);

            if (pi != null && !String.IsNullOrEmpty(value.ToString()))
            {
                pi.SetValue(result, value, null);
            }
        }

        /// <summary>
        /// Busca valor de uma tag a partir de um nó
        /// </summary>
        /// <param name="nodes">nó onde vai ser procurado a tag</param>
        /// <param name="nameTag">nome da tag</param>
        /// <returns></returns>
        private string GetXML(XmlNodeList nodes, string nameTag)
        {
            string value = "";
            foreach (XmlNode n in nodes)
            {
                if (n.NodeType == XmlNodeType.Element)
                {
                    if (n.Name.Equals(nameTag))
                    {
                        value = n.InnerText;
                        break;
                    }
                }
            }

            return value;
        }

        /// <summary>
        /// Localiza o numero do documento da pessoa
        /// </summary>
        /// <param name="parentTag"></param>
        /// <returns></returns>
        private string GetDocumentoPessoa(string parentTag)
        {
            string result = GetValueXML("dest", "CPF");

            if (String.IsNullOrEmpty(result))
                result = GetValueXML("dest", "CNPJ");

            return result;
        }

        /// <summary>
        /// Retorna o valor do documento a partir da raiz
        /// </summary>
        /// <param name="groupTag">nome do nó</param>
        /// <param name="nameTag">nome da tag</param>
        /// <returns></returns>
        private string GetValueXML(string groupTag, string nameTag)
        {
            string value = "";
            XmlNodeList nodes = Document.GetElementsByTagName(groupTag);
            XmlNode node = nodes[0];

            foreach (XmlNode n in node)
            {
                if (n.NodeType == XmlNodeType.Element)
                {
                    if (n.Name.Equals(nameTag))
                    {
                        value = n.InnerText;
                        break;
                    }
                }
            }

            return value;
        }

        /// <summary>
        /// Calcula chave de acesso
        /// Código UF (2) / AAMM da Emissão (4) / CNPJ emitente (14) / mod (2) / N serie SAT (9) / Numero CF-e SAT (6) / Código aleatório (6) / cDV (1)
        /// </summary>
        /// <returns></returns>
        private string CalcularChaveAcesso()
        {
            Random r = new Random();

            StringBuilder result = new StringBuilder();

            result.Append(ObjEnvio.cUF);
            result.Append(ObjEnvio.dhEnvio.Substring(2, 2) + ObjEnvio.dhEnvio.Substring(5, 2));
            result.Append(GetValueXML("emit", "CNPJ"));
            result.Append(GetValueXML("ide", "mod"));
            result.Append(ObjEnvio.nserieSAT);
            result.Append(GetValueXML("ide", "nNF"));
            result.Append(r.Next(111111, 999999).ToString());
            result.Append(Modulus11Digit(result.ToString()));

            return result.ToString();
        }

        /// <summary>
        /// Retorna o dígito verificador do valor informado
        /// </summary>
        /// <param name="value">Valor para calcular o dígito verificador</param>
        /// <returns></returns>
        private int Modulus11Digit(string value)
        {
            int[] weigths = { 2, 3, 4, 5, 6, 7, 8, 9, 2, 3, 4, 5, 6, 7, 8, 9 };

            int sum = 0;
            int idx = 0;

            for (int i = value.Length - 1; i >= 0; i--)
            {
                sum += Convert.ToInt32(value[i].ToString()) * weigths[idx];
                if (idx == 9)
                {
                    idx = 2;
                }
                else
                {
                    idx++;
                }
            }

            int rest = (sum * 10) % 11;
            int result = rest;
            if (result >= 10)
                result = 0;

            return result;
        }

        /// <summary>
        /// Realiza validação do XML a partir dos schemas
        /// </summary>
        private void ValidarCFe()
        {
            ValidarXML validar = new ValidarXML(OutputFile, Convert.ToInt16(ObjEnvio.cUF), true);
            string cResultadoValidacao = validar.ValidarArqXML(OutputFile);
            if (cResultadoValidacao != "")
            {
                throw new Exception(cResultadoValidacao);
            }
        }

    }
}
