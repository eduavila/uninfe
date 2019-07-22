using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsumirWS
{
    class ConsumirWS
    {
        string _sErro = string.Empty;

        private CookieContainer cookies = new CookieContainer();
        //sFile é o arquivo XML para enviar
        public bool InvocarSoap(string sFile, object oServico, X509Certificate2 certificado)
        {
            bool lbRetu = false;

            WSSoap loSolp = (WSSoap)oServico;
            try
            {

                Uri urlpost = new Uri(loSolp.EnderecoWeb);
                HttpWebRequest httpPostNFe = (HttpWebRequest)HttpWebRequest.Create(urlpost);

                XmlDocument doc = new XmlDocument();
                doc.Load(sFile);
                
                string sNFeDados = SoapXmlEnvelopar(doc.OuterXml, loSolp);

                string postConsultaComParametros = sNFeDados;

                byte[] buffer2 = Encoding.UTF8.GetBytes(postConsultaComParametros);

                httpPostNFe.CookieContainer = cookies;
                httpPostNFe.Timeout = 60000;
                httpPostNFe.ContentType = "application/soap+xml; charset=utf-8; action=" + loSolp.ActionWeb;
                httpPostNFe.Method = "POST";

                ServicePointManager.Expect100Continue = false;
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(RetornoValidacao);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | (SecurityProtocolType)768 | (SecurityProtocolType)3072;

                //Certifica o objeto/servico -> adiciona o certificado
                httpPostNFe.ClientCertificates.Add(certificado);

                httpPostNFe.ContentLength = buffer2.Length;

                Stream PostData = httpPostNFe.GetRequestStream();
                PostData.Write(buffer2, 0, buffer2.Length);
                PostData.Close();

                HttpWebResponse responsePost = (HttpWebResponse)httpPostNFe.GetResponse();
                Stream istreamPost = responsePost.GetResponseStream();
                StreamReader strRespotaUrlConsultaNFe = new StreamReader(istreamPost, System.Text.Encoding.UTF8);

                var x = strRespotaUrlConsultaNFe.ReadToEnd();

                XmlDocument retornoXml = new XmlDocument();
                retornoXml.LoadXml(x);

                string retorno = retornoXml.GetElementsByTagName("nfeResultMsg")[0].ChildNodes[0].OuterXml;

                System.Windows.Forms.MessageBox.Show(retorno);

                lbRetu = true;
            }
            catch (Exception ex)
            {
                _sErro = ex.ToString();

                System.Windows.Forms.MessageBox.Show(_sErro);
            }

            return lbRetu;
        }

        private static String SoapXmlEnvelopar(string sXml, WSSoap loSoap)
        {
            string sRetu = string.Empty;
            if (sXml.IndexOf("?>") >= 0)
            {
                sXml = sXml.Substring(sXml.IndexOf("?>") + 2);
            }

            sRetu = "<?xml version='1.0' encoding='UTF-8'?>";
            sRetu += "<" + loSoap.SoapWeb + ":Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:" + loSoap.SoapWeb + "='http://www.w3.org/2003/05/soap-envelope'>";
            sRetu += "<" + loSoap.SoapWeb + ":Body>";
            sRetu += "<nfeDadosMsg xmlns= '" + loSoap.ActionWeb + "'>" + sXml + "</nfeDadosMsg>";
            sRetu += "</" + loSoap.SoapWeb + ":Body>";
            sRetu += "</" + loSoap.SoapWeb + ":Envelope>";

            return sRetu;
        }

        public bool RetornoValidacao(object sender,
           X509Certificate certificate,
           X509Chain chain,
           SslPolicyErrors sslPolicyErros)
        {
            return true;
        }
    }
}
