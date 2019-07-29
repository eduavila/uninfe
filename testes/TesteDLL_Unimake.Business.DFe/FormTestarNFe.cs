using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Unimake.Business.DFe.Security;
using Unimake.Business.DFe.Servicos;
using Unimake.Business.DFe.Servicos.NFe;
using Unimake.Business.DFe.Xml.NFe;
using Unimake.Security.Platform;

namespace TesteDLL_Unimake.Business.DFe
{
    public partial class FormTestarNFe: Form
    {
        #region Private Fields

        private readonly X509Certificate2 CertificadoSelecionado;
        private readonly UFBrasil CUF = UFBrasil.AM;

        private readonly TipoAmbiente TpAmb = TipoAmbiente.Homologacao;

        #endregion Private Fields

        #region Private Properties

        private string UF => ((int)CUF).ToString();

        #endregion Private Properties

        #region Private Methods

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                var xml = new ConsStatServ
                {
                    Versao = "4.00",
                    CUF = CUF,
                    TpAmb = TpAmb
                };

                var configuracao = new Configuracao
                {
                    CertificadoDigital = CertificadoSelecionado
                };

                var statusServico = new StatusServico(xml, configuracao);
                statusServico.Executar();
                MessageBox.Show(statusServico.RetornoWSString);
                //TODO: Bruno - Tem que ver porque o XMotivo está com acentuação destorcida
                MessageBox.Show(statusServico.Result.XMotivo);
            }
            catch(Exception ex)
            {
                CatchException(ex);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                var xml = new ConsSitNFe
                {
                    Versao = "4.00",
                    TpAmb = TpAmb,
                    ChNFe = ((int)CUF).ToString() + "170701761135000132550010000186931903758906"
                };

                var configuracao = new Configuracao
                {
                    CertificadoDigital = CertificadoSelecionado
                };

                var consultaProtocolo = new ConsultaProtocolo(xml, configuracao);
                consultaProtocolo.Executar();
                MessageBox.Show(consultaProtocolo.RetornoWSString);
                //TODO: Bruno - Tem que ver porque o XMotivo está com acentuação destorcida
                MessageBox.Show(consultaProtocolo.Result.XMotivo);
            }
            catch(Exception ex)
            {
                CatchException(ex);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                var xml = new InutNFe
                {
                    Versao = "4.00",
                    InfInut = new InutNFeInfInut
                    {
                        Ano = "19",
                        CNPJ = "06117473000150",
                        CUF = CUF,
                        Mod = ModeloDFe.NFe,
                        NNFIni = 1,
                        NNFFin = 2,
                        Serie = 1,
                        TpAmb = TpAmb,
                        XJust = "Justificativa da inutilizacao"
                    }
                };

                var configuracao = new Configuracao
                {
                    CertificadoDigital = CertificadoSelecionado
                };

                var inutilizacao = new Inutilizacao(xml, configuracao);
                inutilizacao.Executar();
                MessageBox.Show(inutilizacao.RetornoWSString);
                MessageBox.Show(inutilizacao.Result.infInut.XMotivo);
            }
            catch(Exception ex)
            {
                CatchException(ex);
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                var xml = new ConsCad
                {
                    Versao = "2.00",
                    InfCons = new InfCons()
                    {
                        CNPJ = "06117473000150",
                        UF = CUF
                    }
                };

                var configuracao = new Configuracao
                {
                    CertificadoDigital = CertificadoSelecionado
                };

                var consultaCad = new ConsultaCadastro(xml, configuracao);
                consultaCad.Executar();
                MessageBox.Show(consultaCad.RetornoWSString);
                MessageBox.Show(consultaCad.Result.InfCons.XMotivo);
            }
            catch(Exception ex)
            {
                CatchException(ex);
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            try
            {
                var xml = new EnvEvento
                {
                    Versao = "1.00",
                    IdLote = "000000000000001",
                    Evento = new[] {
                        new Evento
                        {
                            Versao = "1.00",
                            InfEvento = new InfEvento(new DetEventoCanc {
                                NProt = "123456789012345",
                                Versao = "1.00",
                                XJust = "Justificativa para cancelamento da NFe"
                            })
                            {
                                COrgao = UFBrasil.MT,
                                ChNFe = "51170701761135000132550010000186931903758906",
                                CNPJ = "06117473000150",
                                DhEvento = DateTime.Now,
                                TpEvento = TipoEventoNFe.Cancelamento,
                                NSeqEvento = 1,
                                VerEvento = "1.00",
                                TpAmb = TipoAmbiente.Homologacao,
                            }
                        }
                    }
                };

                var configuracao = new Configuracao
                {
                    CertificadoDigital = CertificadoSelecionado
                };

                var recepcaoEvento = new RecepcaoEvento(xml, configuracao);
                recepcaoEvento.Executar();
                MessageBox.Show(recepcaoEvento.RetornoWSString);
                //MessageBox.Show(recepcaoEvento.Result.XMotivo);
            }
            catch(Exception ex)
            {
                CatchException(ex);
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            try
            {
                var xml = new EnviNFe
                {
                    Versao = "4.00",
                    IdLote = "000000000000001",
                    IndSinc = SimNao.Sim,
                    NFe = new[] {
                        new NFe
                        {
                            InfNFe = new[] {
                                new InfNFe
                                {
                                    Versao = "4.00",

                                    Ide = new Ide
                                    {
                                        CUF = CUF,
                                        NatOp = "VENDA PRODUC.DO ESTABELEC",
                                        Mod = ModeloDFe.NFe,
                                        Serie = 1,
                                        NNF = 46320,
                                        DhEmi = DateTime.Now,
                                        DhSaiEnt = DateTime.Now,
                                        TpNF = TipoOperacao.Saida,
                                        IdDest = DestinoOperacao.OperacaoInterestadual,
                                        CMunFG = 4118402,
                                        TpImp = FormatoImpressaoDANFE.NormalPaisagem,
                                        TpEmis = TipoEmissao.Normal,
                                        TpAmb = TpAmb,
                                        FinNFe = FinalidadeNFe.Normal,
                                        IndFinal = SimNao.Sim,
                                        IndPres = IndicadorPresenca.OperacaoOutros,
                                        ProcEmi = ProcessoEmissao.AplicativoContribuinte,
                                        VerProc = "TESTE 1.00"
                                    },
                                    Emit = new Emit
                                    {
                                        CNPJ = "06117473000150",
                                        XNome = "UNIMAKE SOLUCOES CORPORATIVAS LTDA",
                                        XFant = "UNIMAKE - PARANAVAI",
                                        EnderEmit = new EnderEmit
                                        {
                                            XLgr = "RUA ANTONIO FELIPE",
                                            Nro = "1500",
                                            XBairro = "CENTRO",
                                            CMun = 4118402,
                                            XMun = "PARANAVAI",
                                            UF = CUF,
                                            CEP = "87704030",
                                            Fone = "04431414900"
                                        },
                                        IE = "9032000301",
                                        IM = "14018",
                                        CNAE = "6202300",
                                        CRT = CRT.SimplesNacional
                                    },
                                    Dest = new Dest
                                    {
                                        CNPJ = "11106441000199",
                                        XNome = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL",
                                        EnderDest = new EnderDest
                                        {
                                            XLgr = "RUA ACAIPE",
                                            Nro = "50",
                                            XBairro = "BURGO PAULISTA",
                                            CMun = 3550308,
                                            XMun = "SAO PAULO",
                                            UF = UFBrasil.SP,
                                            CEP = "03682040",
                                            Fone = "01120461080"
                                        },
                                        IndIEDest = IndicadorIEDestinatario.ContribuinteICMS,
                                        IE = "146939365114",
                                        Email = "teste@teste.com.br"
                                    },
                                    Retirada = new Retirada
                                    {
                                        CNPJ = "00000000000000",
                                        XNome = "TESTE RETIRADA",
                                        XLgr = "RUA TESTE",
                                        Nro = "10",
                                        XCpl = "TESTE COMPLEMENTO",
                                        XBairro = "TESTE BAIRRO",
                                        CMun = 3550308,
                                        XMun = "SAO PAULO",
                                        UF = UFBrasil.SP
                                    },
                                    Entrega = new Entrega
                                    {
                                        CNPJ = "00000000000000",
                                        XNome = "TESTE ENTREGA",
                                        XLgr = "RUA TESTE",
                                        Nro = "10",
                                        XCpl = "TESTE COMPLEMENTO",
                                        XBairro = "TESTE BAIRRO",
                                        CMun = 3550308,
                                        XMun = "SAO PAULO",
                                        UF = UFBrasil.SP
                                    },
                                    AutXML = new[] {
                                        new AutXML
                                        {
                                            CNPJ = "00000000000000"
                                        },
                                        new AutXML
                                        {
                                            CPF = "00000000000"
                                        }
                                    },
                                    Det = new[] {
                                        new Det
                                        {
                                            NItem = 1,
                                            Prod = new Prod
                                            {
                                                CProd = "00001",
                                                CEAN = "SEM GTIN",
                                                XProd = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL",
                                                NCM = "84714900",
                                                CFOP = "6101",
                                                UCom = "UN",
                                                QCom = 1.00,
                                                VUnCom = 1.00,
                                                VProd = 1.00,
                                                UTrib = "UN",
                                                QTrib = 1.00,
                                                VUnTrib = 1.00,
                                                IndTot = SimNao.Sim,
                                                XPed = "123456",
                                                NItemPed = 1
                                            },
                                            Imposto = new Imposto
                                            {
                                                ICMS = new[] {
                                                    new ICMS
                                                    {
                                                        ICMS40 = new ICMS40
                                                        {
                                                            CST = "40",
                                                            Orig = OrigemMercadoria.Nacional
                                                        }
                                                    }
                                                },
                                            }
                                        },
                                        new Det
                                        {
                                            NItem = 2,
                                            Prod = new Prod
                                            {
                                                CProd = "00002",
                                                CEAN = "SEM GTIN",
                                                XProd = "PRODUTO TESTE 2",
                                                NCM = "84714900",
                                                CFOP = "6101",
                                                UCom = "UN",
                                                QCom = 1.00,
                                                VUnCom = 1.123400,
                                                VProd = 1.00,
                                                UTrib = "UN",
                                                QTrib = 1.00,
                                                VUnTrib = 1.00,
                                                IndTot = SimNao.Sim,
                                                VOutro = 1.12,
                                                XPed = "123456",
                                                NItemPed = 2,
                                                Rastro = new[] {
                                                    new  Rastro
                                                    {
                                                        NLote = "0000000001",
                                                        QLote = 10.201,
                                                        DFab = DateTime.Now,
                                                        DVal = DateTime.Now
                                                    },
                                                    new  Rastro
                                                    {
                                                        NLote = "0000000002",
                                                        QLote = 10.201,
                                                        DFab = DateTime.Now,
                                                        DVal = DateTime.Now
                                                    }
                                                }
                                            },
                                            Imposto = new Imposto
                                            {
                                                ICMS = new[] {
                                                    new ICMS
                                                    {
                                                        ICMS00 = new ICMS00
                                                        {
                                                            Orig = OrigemMercadoria.Nacional,
                                                            VBC = 10,
                                                            PICMS = 99.1230
                                                        }
                                                    }
                                                },
                                            }
                                        }
                                    },
                                    Total = new Total
                                    {
                                        ICMSTot = new ICMSTot
                                        {
                                            VBC = 0,
                                            VICMS = 0,
                                            VICMSDeson = 0,
                                            VFCP = 0,
                                            VBCST = 0,
                                            VST = 0,
                                            VFCPST = 0,
                                            VFCPSTRet = 0,
                                            VProd = 0,
                                            VFrete = 0,
                                            VSeg = 0,
                                            VDesc = 0,
                                            VII = 0,
                                            VIPI = 0,
                                            VIPIDevol = 0,
                                            VPIS = 0,
                                            VCOFINS = 0,
                                            VOutro = 0,
                                            VNF = 0
                                        }
                                    },
                                    Transp = new Transp
                                    {
                                        ModFrete = ModalidadeFrete.ContratacaoFretePorContaDestinatário_FOB
                                    },
                                    Cobr = new Cobr()
                                    {
                                        Dup = new[]
                                        {
                                            new Dup
                                            {
                                                DVenc = DateTime.Now,
                                                NDup = "001",
                                                VDup = 10
                                            }
                                        },
                                        Fat = new Fat
                                        {
                                            NFat = "1/1",
                                            VDesc = 0,
                                            VOrig = 1,
                                            VLiq = 1
                                        }
                                    },
                                    Pag = new Pag
                                    {
                                        DetPag = new[]
                                        {
                                             new DetPag
                                             {
                                                 IndPag = IndicadorPagamento.PagamentoPrazo,
                                                 VPag = 10,
                                                 TPag = MeioPagamento.Dinheiro
                                             }
                                        }
                                    },
                                    InfAdic = new InfAdic
                                    {
                                        InfCpl = "Teste do infadic",
                                        ObsCont = new[]
                                        {
                                            new ObsCont
                                            {
                                                XCampo="campo teste",
                                                XTexto="texto teste"
                                            }
                                        }
                                    },
                                    InfRespTec = new InfRespTec
                                    {
                                        CNPJ = "00000000000000",
                                        XContato = "nome do contato",
                                        Email = "email@email.com.br",
                                        Fone = "04431414900"
                                    }
                                }
                            }
                        }
                    }
                };

                var configuracao = new Configuracao
                {
                    CertificadoDigital = CertificadoSelecionado
                };

                var recepcaoEvento = new Autorizacao(xml, configuracao);
                recepcaoEvento.Executar();
                MessageBox.Show(recepcaoEvento.RetornoWSString);
                MessageBox.Show(recepcaoEvento.Result.XMotivo);
            }
            catch(Exception ex)
            {
                CatchException(ex);
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            try
            {
                var xml = new ConsReciNFe
                {
                    Versao = "4.00",
                    TpAmb = TpAmb,
                    NRec = UF + "3456789012345"
                };

                var configuracao = new Configuracao
                {
                    CertificadoDigital = CertificadoSelecionado
                };

                var retAutorizacao = new RetAutorizacao(xml, configuracao);
                retAutorizacao.Executar();
                MessageBox.Show(retAutorizacao.RetornoWSString);
                MessageBox.Show(retAutorizacao.Result.XMotivo);
            }
            catch(Exception ex)
            {
                CatchException(ex);
            }
        }

        private void CatchException(Exception ex)
        {
            var message = new StringBuilder();

            do
            {
                message.AppendLine($"{ex.Message}\r\n");
                ex = ex.InnerException;
            } while(ex != null);

            MessageBox.Show(message.ToString(), "ERRO!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion Private Methods
        #region Public Constructors

        public FormTestarNFe()
        {
            InitializeComponent();

            var cert = new CertificadoDigital();
            CertificadoSelecionado = cert.Selecionar();
        }

        #endregion Public Constructors

        private void Button8_Click(object sender, EventArgs e)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(@"C:\Users\Wandrey\Downloads\NFe Paraguai\FE_v150.xml");
            
            AssinaturaDigital assinaturaDigital = new AssinaturaDigital();

            assinaturaDigital.Assinar(xml, "rDE", "DE", CertificadoSelecionado, AlgorithmType.Sha256, true, "", "Id");
        }
    }
}