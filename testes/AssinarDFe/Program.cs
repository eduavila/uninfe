using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Xml;
using Unimake.Business.DFe.Security;
using Unimake.Security.Platform;

namespace AssinarDFe
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length >= 1)
            {
                string arqXML = string.Empty;
                string tagAssinatura = string.Empty;
                string tagAtributoId = string.Empty;
                string serialNumberCertificado = string.Empty;
                AlgorithmType algorithmType = AlgorithmType.Sha256;
                string idAttributeName = string.Empty;
                string pfx = string.Empty;
                string pfxSenha = string.Empty;

                foreach (var param in args)
                {
                    string conteudoParam = param.Substring(param.IndexOf("=", 0) + 1);

                    if (param.ToLower().Substring(0, 4).Equals("/arq"))
                    {
                        arqXML = conteudoParam;
                    }
                    else if (param.ToLower().Substring(0, 6).Equals("/tagid"))
                    {
                        tagAtributoId = conteudoParam;
                    }
                    else if (param.ToLower().Substring(0, 4).Equals("/tag"))
                    {
                        tagAssinatura = conteudoParam;
                    }
                    else if (param.ToLower().Substring(0, 3).Equals("/at"))
                    {
                        if (conteudoParam.ToLower() == "sha256")
                        {
                            algorithmType = AlgorithmType.Sha256;
                        }
                        else
                        {
                            algorithmType = AlgorithmType.Sha1;
                        }
                    }
                    else if (param.ToLower().Substring(0, 7).Equals("/nomeid"))
                    {
                        idAttributeName = conteudoParam;
                    }
                    else if (param.ToLower().Substring(0, 3).Equals("/sn"))
                    {
                        serialNumberCertificado = conteudoParam;
                    }
                    else if (param.ToLower().Substring(0, 9).Equals("/pfxsenha"))
                    {
                        pfxSenha = conteudoParam;
                    }
                    else if (param.ToLower().Substring(0, 4).Equals("/pfx"))
                    {
                        pfx = conteudoParam;
                    }
                }

                try
                {
                    X509Certificate2 x509Cert = null;
                    var cert = new CertificadoDigital();

                    if (!string.IsNullOrWhiteSpace(serialNumberCertificado))
                    {
                        x509Cert = cert.BuscarCertificadoDigital(serialNumberCertificado);
                    }
                    else if (!string.IsNullOrWhiteSpace(pfx))
                    {
                        x509Cert = cert.CarregarCertificadoDigitalA1(@pfx, pfxSenha);
                    }

                    XmlDocument xml = new XmlDocument();
                    xml.Load(@arqXML);

                    AssinaturaDigital assinaturaDigital = new AssinaturaDigital();
                    assinaturaDigital.Assinar(xml, tagAssinatura, tagAtributoId, x509Cert, algorithmType, true, "", idAttributeName);

                    string xmlAssinado = arqXML + ".ass";

                    StreamWriter SW_2 = File.CreateText(xmlAssinado);
                    SW_2.Write(xml.OuterXml);
                    SW_2.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                Application.Run(new AssinadorXML());
            }
        }
    }
}
