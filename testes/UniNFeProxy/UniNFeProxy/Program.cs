using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Security.Cryptography.X509Certificates;

namespace UniNFeProxy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("A-1) " + DateTime.Now.ToString());
            teste oTeste = new teste();
            Console.WriteLine("A-2) " + DateTime.Now.ToString());
            Console.ReadKey();
        }
    }

    class teste
    {
        public teste()
        {
            #region Definir a URI do WSDL
            Uri oUri = new Uri("https://homologacao.nfe.fazenda.sp.gov.br/nfeweb/services/NfeCancelamento2.asmx?WSDL");
            //Uri oUri = new Uri("https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeStatus2?WSDL");       
            #endregion

            #region Definir o Certificado digital a ser utilizado
            string _xnome = "CN=SANDRA GRIPP NOVAES FERNANDES:10648018000158, OU=Autenticado por AR Sescap PR, OU=RFB e-CNPJ A1, OU=Secretaria da Receita Federal do Brasil - RFB, L=Paranavai, S=PR, O=ICP-Brasil, C=BR";

            X509Certificate2 _X509Cert = new X509Certificate2();
            X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
            X509Certificate2Collection collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindBySubjectDistinguishedName, _xnome, false);

            if (collection1.Count == 0)
            {
                Console.WriteLine("Foi detectado problemas com o certificado digital. (Código do Erro: 2)");
                Console.ReadKey();
            }
            else
            {
                // certificado ok
                _X509Cert = collection1[0];
            }
            #endregion

            #region Comentário
            Console.WriteLine("B-1) " + DateTime.Now.ToString());            
            #endregion

            try
            {
                #region Criar as classes dos webservices
                WebServiceProxy webServiceProxy = new WebServiceProxy(oUri, _X509Cert);
                #endregion

                #region Comentário
                Console.WriteLine("B-2) " + DateTime.Now.ToString());
                Console.WriteLine("C-1) " + DateTime.Now.ToString());
                #endregion

                #region Criar objetos das classes dos webservices
                object oStatusServico = webServiceProxy.CriarObjeto("NfeCancelamento2");
                object oCabecMsg = webServiceProxy.CriarObjeto("nfeCabecMsg");
                #endregion

                #region Comentário
                Console.WriteLine("C-2) " + DateTime.Now.ToString());
                Console.WriteLine("D-1) " + DateTime.Now.ToString());
                #endregion

                #region Atualizar algumas propriedades das classes dos webservices
                webServiceProxy.SetProp(oCabecMsg, "cUF", "43");
                webServiceProxy.SetProp(oCabecMsg, "versaoDados", "2.00");
                webServiceProxy.SetProp(oStatusServico, "nfeCabecMsgValue", oCabecMsg);
                #endregion

                #region Defini o XML a ser enviado para o webservice
                //string XmlNfeDadosMsg = @"C:\testeNFE\envio\20100222T222310-ped-sta.xml";
                string XmlNfeDadosMsg = @"C:\testeNFE\envio\353100304678683000191550010000344924352121508-ped-can.xml";
                XmlDocument docXML = new XmlDocument();
                docXML.Load(XmlNfeDadosMsg);
                #endregion

                #region Enviar o XML para o webservice
                XmlNode Retorno = (XmlNode)webServiceProxy.InvokeXML(oStatusServico, "nfeCancelamentoNF2", new object[] { docXML });
                #endregion

                #region Demonstrar o XML retornado
                Console.WriteLine(Retorno.OuterXml);
                #endregion

                #region Comentário
                Console.WriteLine("D-2) " + DateTime.Now.ToString());
                #endregion

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            #region Pausa
            Console.ReadKey();
            #endregion
        }
    }
}
