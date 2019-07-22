using System;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Xml;
using Unimake.Security.Platform;
using Unimake.Businness.DFe;

namespace TesteUnimakeDFe
{
    public partial class Form1 : Form
    {
        private X509Certificate2 CertificadoSelecionado;

        public Form1()
        {
            InitializeComponent();

            CertificadoDigital cert = new CertificadoDigital();
            CertificadoSelecionado = cert.Selecionar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                XmlDocument doc = new XmlDocument
                {
                    PreserveWhitespace = false
                };
                doc.Load(@"D:\projetos\uninfe\exemplos\NFe e NFCe 4.00\NFe\20100222T222310-ped-sta.xml");

                Consumir(doc);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void Consumir(XmlDocument doc)
        {
            Configuracao configuracao = new Configuracao
            {
                CertificadoDigital = CertificadoSelecionado,
                Servico = new TipoServico(doc).Definir()
            };

            switch (configuracao.Servico)
            {
                case Servicos.NFeStatusServico:
                    var statusServico = new Unimake.DFe.Servicos.NFe.StatusServico(doc, configuracao);
                    statusServico.Executar();
                    MessageBox.Show(statusServico.RetornoWSString);
                    break;

                case Servicos.NFeConsultaProtocolo:
                    var consultaProtocolo = new Unimake.DFe.Servicos.NFe.ConsultaProtocolo(doc, configuracao);
                    consultaProtocolo.Executar();
                    MessageBox.Show(consultaProtocolo.RetornoWSString);
                    break;

                case Servicos.NFeInutilizacao:
                    var inutilizacao = new Unimake.DFe.Servicos.NFe.Inutilizacao(doc, configuracao);
                    inutilizacao.Executar();
                    MessageBox.Show(inutilizacao.RetornoWSString);
                    break;

                case Servicos.NFeConsultaCadastro:
                    var consultaCad = new Unimake.DFe.Servicos.NFe.ConsultaCadastro(doc, configuracao);
                    consultaCad.Executar();
                    MessageBox.Show(consultaCad.RetornoWSString);
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                XmlDocument doc = new XmlDocument
                {
                    PreserveWhitespace = false
                };
                doc.Load(@"D:\projetos\uninfe\exemplos\NFe e NFCe 4.00\NFe\41190206117473000150550010000557241005753008-ped-sit.xml");

                Consumir(doc);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro: " + ex.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                XmlDocument doc = new XmlDocument
                {
                    PreserveWhitespace = false
                };
                doc.Load(@"D:\projetos\uninfe\exemplos\NFe e NFCe 4.00\NFe\41080676472349000430550010000001041671821888-ped-inu.xml");

                Consumir(doc);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {

                XmlDocument doc = new XmlDocument
                {
                    PreserveWhitespace = false
                };
                doc.Load(@"D:\projetos\uninfe\exemplos\NFe e NFCe 4.00\NFe\05065047000157-cons-cad.xml");

                Consumir(doc);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro: " + ex.Message);
            }
        }
    }
}
