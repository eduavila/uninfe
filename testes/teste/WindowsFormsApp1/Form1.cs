using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unimake.Security.Platform;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var cert = new CertificadoDigital();
            CertificadoSelecionado = cert.Selecionar();
        }

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
            catch (Exception ex)
            {
                CatchException(ex);
            }
        }
    }
}
