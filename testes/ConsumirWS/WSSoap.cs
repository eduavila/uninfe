using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumirWS
{
    class WSSoap
    {
        private string _EnderecoWeb = "";

        public string EnderecoWeb
        {
            get { return _EnderecoWeb; }
            set { _EnderecoWeb = value; }
        }

        private string _ActionWeb = "";

        public string ActionWeb
        {
            get { return _ActionWeb; }
            set { _ActionWeb = value; }
        }

        private string _SoapWeb = "soap12";

        public string SoapWeb
        {
            get { return _SoapWeb; }
            set { _SoapWeb = value; }
        }

        private string _TagRetorno = "nfeResultMsg";

        public string TagRetorno
        {
            get { return _TagRetorno; }
            set { _TagRetorno = value; }
        }
    }
}
