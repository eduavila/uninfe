using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace NFe.Components.NotaInteligente
{
    public class NotaInteligente : NotaInteligenteBase
    {
        #region Construtores
        public NotaInteligente(TipoAmbiente tpAmb, string pastaRetorno, X509Certificate certificate, int codMun, string proxyUser, string proxyPass, string proxyServer)
            : base(tpAmb, pastaRetorno, certificate, codMun, proxyUser, proxyPass, proxyServer)
        { }
        #endregion
    }
}
