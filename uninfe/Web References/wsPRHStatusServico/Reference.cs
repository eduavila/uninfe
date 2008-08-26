﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1434
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 2.0.50727.1434.
// 
#pragma warning disable 1591

namespace uninfe.wsPRHStatusServico {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1434")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="NfeStatusServicoSoap", Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NfeStatusServico")]
    public partial class NfeStatusServico : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback nfeStatusServicoNFOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public NfeStatusServico() {
            this.Url = global::uninfe.Properties.Settings.Default.uninfe_wsPRHStatusServico_NfeStatusServico;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event nfeStatusServicoNFCompletedEventHandler nfeStatusServicoNFCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.portalfiscal.inf.br/nfe/wsdl/NfeStatusServico/nfeStatusServicoNF", RequestNamespace="http://www.portalfiscal.inf.br/nfe/wsdl/NfeStatusServico", ResponseNamespace="http://www.portalfiscal.inf.br/nfe/wsdl/NfeStatusServico", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string nfeStatusServicoNF(string nfeCabecMsg, string nfeDadosMsg) {
            object[] results = this.Invoke("nfeStatusServicoNF", new object[] {
                        nfeCabecMsg,
                        nfeDadosMsg});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void nfeStatusServicoNFAsync(string nfeCabecMsg, string nfeDadosMsg) {
            this.nfeStatusServicoNFAsync(nfeCabecMsg, nfeDadosMsg, null);
        }
        
        /// <remarks/>
        public void nfeStatusServicoNFAsync(string nfeCabecMsg, string nfeDadosMsg, object userState) {
            if ((this.nfeStatusServicoNFOperationCompleted == null)) {
                this.nfeStatusServicoNFOperationCompleted = new System.Threading.SendOrPostCallback(this.OnnfeStatusServicoNFOperationCompleted);
            }
            this.InvokeAsync("nfeStatusServicoNF", new object[] {
                        nfeCabecMsg,
                        nfeDadosMsg}, this.nfeStatusServicoNFOperationCompleted, userState);
        }
        
        private void OnnfeStatusServicoNFOperationCompleted(object arg) {
            if ((this.nfeStatusServicoNFCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.nfeStatusServicoNFCompleted(this, new nfeStatusServicoNFCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1434")]
    public delegate void nfeStatusServicoNFCompletedEventHandler(object sender, nfeStatusServicoNFCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1434")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class nfeStatusServicoNFCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal nfeStatusServicoNFCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591