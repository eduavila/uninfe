using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Xml;

namespace ConsumirWS
{
    public partial class Form1 : Form
    {
        private X509Certificate2 certificado;

        public Form1()
        {
            InitializeComponent();

            ServicePointManager.Expect100Continue = false;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
            certificado = SelecionarCertificado();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Ex. de como executar (Ex. Sefaz/SP)
            WSSoap oSoap = new WSSoap();
            oSoap.EnderecoWeb = "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfestatusservico4.asmx";
            oSoap.ActionWeb = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4";

            string xmlConsultaStatus = @"teste-ped-sta.xml";

            ConsumirWS qq = new ConsumirWS();
            qq.InvocarSoap(xmlConsultaStatus, oSoap, certificado);
        }

        public X509Certificate2 SelecionarCertificado()
        {
            X509Certificate2 certificado = null;

            X509Certificate2 oX509Cert = new X509Certificate2();
            X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
            X509Certificate2Collection collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
            X509Certificate2Collection collection2 = (X509Certificate2Collection)collection.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, false);
            X509Certificate2Collection scollection = X509Certificate2UI.SelectFromCollection(collection2, "Certificado(s) Digital(is) disponível(is)", "Selecione o certificado digital para uso no aplicativo", X509SelectionFlag.SingleSelection);

            if (scollection.Count == 0)
            {
                string msgResultado = "Nenhum certificado digital foi selecionado ou o certificado selecionado está com problemas.";
                MessageBox.Show(msgResultado, "Advertência", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                oX509Cert = scollection[0];
                certificado = oX509Cert;
            }

            return certificado;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string xmlConsultaStatus = @"teste-ped-sta.xml";

            XmlDocument doc = new XmlDocument();
            doc.Load(xmlConsultaStatus);

            SaoPaulo2.NFeStatusServico4 aa = new SaoPaulo2.NFeStatusServico4();
            aa.ClientCertificates.Add(certificado);
            XmlNode retorno = aa.nfeStatusServicoNF(doc);

            MessageBox.Show(retorno.OuterXml);
        }
    }
}