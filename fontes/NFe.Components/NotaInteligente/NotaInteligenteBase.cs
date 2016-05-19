using NFe.Components.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Services.Protocols;
using NFe.Components.com.notainteligente.claudiomg.h;

namespace NFe.Components.NotaInteligente
{
    public abstract class NotaInteligenteBase : EmiteNFSeBase
    {
        #region locais/ protegidos
        object notaInteligenteService;
        private int CodigoMun = 0;
        private string ProxyUser = null;
        private string ProxyPass = null;
        private string ProxyServer = null;


        protected object NotaInteligenteService
        {
            get
            {
                if (notaInteligenteService == null)
                {
                    if (tpAmb == TipoAmbiente.taHomologacao)
                    {
                        switch (CodigoMun)
                        {
                            case 3116605: // Claudio-MG
                                notaInteligenteService = new com.notainteligente.claudiomg.h.service();
                                break;

                            default:
                                break;
                        }
                    }

                    else
                    {
                        switch (CodigoMun)
                        {
                            case 3116605: // Claudio-MG
                                notaInteligenteService = new com.notainteligente.claudiomg.p.service();
                                break;

                            default:
                                break;
                        }
                    }

                    //AddClientCertificates();
                    AddProxyUser();
                }
                return notaInteligenteService;
            }
        }

        private void AddClientCertificates()
        {
            X509CertificateCollection certificates = null;
            Type t = NotaInteligenteService.GetType();
            PropertyInfo pi = t.GetProperty("ClientCertificates");
            certificates = pi.GetValue(NotaInteligenteService, null) as X509CertificateCollection;
            certificates.Add(Certificate);
        }

        private void AddProxyUser()
        {
            if (!String.IsNullOrEmpty(ProxyUser))
            {
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(ProxyUser, ProxyPass, ProxyServer);
                System.Net.WebRequest.DefaultWebProxy.Credentials = credentials;

                WebServiceProxy wsp = new WebServiceProxy(Certificate as X509Certificate2);

                wsp.SetProp(notaInteligenteService, "Proxy", Proxy.DefinirProxy(ProxyServer, ProxyUser, ProxyPass, 8080));
            }
        }
        #endregion

        #region propriedades
        public X509Certificate Certificate { get; private set; }        
        #endregion

        #region Construtores
        public NotaInteligenteBase(TipoAmbiente tpAmb, string pastaRetorno, X509Certificate certificate, int codMun, string proxyUser, string proxyPass, string proxyServer)
            : base(tpAmb, pastaRetorno)
        {
            Certificate = certificate;
            CodigoMun = codMun;
            ProxyUser = proxyUser;
            ProxyPass = proxyPass;
            ProxyServer = proxyServer;

        }
        #endregion

        #region Métodos
        public override void EmiteNF(string file)
        {
            service qq = new service();
            ServicePointManager.ServerCertificateValidationCallback = MyCertHandler;
            qq.Proxy = WebRequest.DefaultWebProxy;
            qq.Proxy.Credentials = new NetworkCredential(ProxyUser, ProxyPass);
            qq.Credentials = new NetworkCredential(ProxyPass, ProxyPass);
            qq.ClientCertificates.Add(Certificate);

            qq.RecepcionarLoteRps(ReaderXML(file));

            //AddClientCertificates();
            //string strResult = Invoke("RecepcionarLoteRps", new[] { ReaderXML(file) });
            //GerarRetorno(file, strResult,   Propriedade.Extensao(Propriedade.TipoEnvio.EnvLoteRps).EnvioXML, Propriedade.Extensao(Propriedade.TipoEnvio.EnvLoteRps).RetornoXML);
        }

        public override void CancelarNfse(string file)
        {
            string strResult = Invoke("CancelarNfse", new[] { ReaderXML(file) });
            GerarRetorno(file, strResult,   Propriedade.Extensao(Propriedade.TipoEnvio.PedCanNFSe).EnvioXML, 
                                            Propriedade.Extensao(Propriedade.TipoEnvio.PedCanNFSe).RetornoXML);
        }

        public override void ConsultarLoteRps(string file)
        {
            string strResult = Invoke("ConsultarLoteRps", new[] { ReaderXML(file) });
            GerarRetorno(file, strResult,   Propriedade.Extensao(Propriedade.TipoEnvio.PedLoteRps).EnvioXML, 
                                            Propriedade.Extensao(Propriedade.TipoEnvio.PedLoteRps).RetornoXML);
        }

        public override void ConsultarSituacaoLoteRps(string file)
        {
            throw new Exceptions.ServicoInexistenteException();
        }

        public override void ConsultarNfse(string file)
        {
            throw new Exceptions.ServicoInexistenteException();
        }

        public override void ConsultarNfsePorRps(string file)
        {
            throw new Exceptions.ServicoInexistenteException();
        }
        #endregion

        #region invoke
        string Invoke(string methodName, params object[] _params)
        {
            object result = "";
            ServicePointManager.Expect100Continue = false;
            Type t = NotaInteligenteService.GetType();
            MethodInfo mi = t.GetMethod(methodName);
            result = mi.Invoke(NotaInteligenteService, _params);
            return result.ToString();
        }
        #endregion

        #region ReaderXML
        private string ReaderXML(string file)
        {
            string result = "";

            using (StreamReader reader = new StreamReader(file))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    result += line;
                }
            }
            return result;
        }
        #endregion

        /// <summary>
        /// Indentificamos falha no certificado o do servidor, entao temos que ignorar os erros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns></returns>
        private bool MyCertHandler(object sender, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
    
}
