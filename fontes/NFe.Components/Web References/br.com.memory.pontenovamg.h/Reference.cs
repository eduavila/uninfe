﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace NFe.Components.br.com.memory.pontenovamg.h {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="loterpswsdlBinding", Namespace="urn:loterpswsdl")]
    public partial class loterpswsdl : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback tm_lote_rps_servicetestarLoteRPSOperationCompleted;
        
        private System.Threading.SendOrPostCallback tm_lote_rps_serviceimportarLoteRPSOperationCompleted;
        
        private System.Threading.SendOrPostCallback tm_lote_rps_serviceconsultarLoteRPSOperationCompleted;
        
        private System.Threading.SendOrPostCallback tm_lote_rps_serviceconsultarRPSOperationCompleted;
        
        private System.Threading.SendOrPostCallback tm_lote_rps_servicecancelarNFSEOperationCompleted;
        
        private System.Threading.SendOrPostCallback tm_lote_rps_serviceconsultarNFSEOperationCompleted;
        
        private System.Threading.SendOrPostCallback tm_lote_rps_serviceservicoRPSOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public loterpswsdl() {
            this.Url = global::NFe.Components.Properties.Settings.Default.NFe_Components_br_com_memory_pontenovamg_h_loterpswsdl;
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
        public event tm_lote_rps_servicetestarLoteRPSCompletedEventHandler tm_lote_rps_servicetestarLoteRPSCompleted;
        
        /// <remarks/>
        public event tm_lote_rps_serviceimportarLoteRPSCompletedEventHandler tm_lote_rps_serviceimportarLoteRPSCompleted;
        
        /// <remarks/>
        public event tm_lote_rps_serviceconsultarLoteRPSCompletedEventHandler tm_lote_rps_serviceconsultarLoteRPSCompleted;
        
        /// <remarks/>
        public event tm_lote_rps_serviceconsultarRPSCompletedEventHandler tm_lote_rps_serviceconsultarRPSCompleted;
        
        /// <remarks/>
        public event tm_lote_rps_servicecancelarNFSECompletedEventHandler tm_lote_rps_servicecancelarNFSECompleted;
        
        /// <remarks/>
        public event tm_lote_rps_serviceconsultarNFSECompletedEventHandler tm_lote_rps_serviceconsultarNFSECompleted;
        
        /// <remarks/>
        public event tm_lote_rps_serviceservicoRPSCompletedEventHandler tm_lote_rps_serviceservicoRPSCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:loterpswsdl#tm_lote_rps_service.testarLoteRPS", RequestNamespace="urn:loterpswsdl", ResponseNamespace="urn:loterpswsdl")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public string tm_lote_rps_servicetestarLoteRPS(string xml, string codMunicipio, string cnpjPrestador, string hashValidador) {
            object[] results = this.Invoke("tm_lote_rps_servicetestarLoteRPS", new object[] {
                        xml,
                        codMunicipio,
                        cnpjPrestador,
                        hashValidador});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void tm_lote_rps_servicetestarLoteRPSAsync(string xml, string codMunicipio, string cnpjPrestador, string hashValidador) {
            this.tm_lote_rps_servicetestarLoteRPSAsync(xml, codMunicipio, cnpjPrestador, hashValidador, null);
        }
        
        /// <remarks/>
        public void tm_lote_rps_servicetestarLoteRPSAsync(string xml, string codMunicipio, string cnpjPrestador, string hashValidador, object userState) {
            if ((this.tm_lote_rps_servicetestarLoteRPSOperationCompleted == null)) {
                this.tm_lote_rps_servicetestarLoteRPSOperationCompleted = new System.Threading.SendOrPostCallback(this.Ontm_lote_rps_servicetestarLoteRPSOperationCompleted);
            }
            this.InvokeAsync("tm_lote_rps_servicetestarLoteRPS", new object[] {
                        xml,
                        codMunicipio,
                        cnpjPrestador,
                        hashValidador}, this.tm_lote_rps_servicetestarLoteRPSOperationCompleted, userState);
        }
        
        private void Ontm_lote_rps_servicetestarLoteRPSOperationCompleted(object arg) {
            if ((this.tm_lote_rps_servicetestarLoteRPSCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.tm_lote_rps_servicetestarLoteRPSCompleted(this, new tm_lote_rps_servicetestarLoteRPSCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:loterpswsdl#tm_lote_rps_service.importarLoteRPS", RequestNamespace="urn:loterpswsdl", ResponseNamespace="urn:loterpswsdl")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public string tm_lote_rps_serviceimportarLoteRPS(string xml, string codMunicipio, string cnpjPrestador, string hashValidador) {
            object[] results = this.Invoke("tm_lote_rps_serviceimportarLoteRPS", new object[] {
                        xml,
                        codMunicipio,
                        cnpjPrestador,
                        hashValidador});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void tm_lote_rps_serviceimportarLoteRPSAsync(string xml, string codMunicipio, string cnpjPrestador, string hashValidador) {
            this.tm_lote_rps_serviceimportarLoteRPSAsync(xml, codMunicipio, cnpjPrestador, hashValidador, null);
        }
        
        /// <remarks/>
        public void tm_lote_rps_serviceimportarLoteRPSAsync(string xml, string codMunicipio, string cnpjPrestador, string hashValidador, object userState) {
            if ((this.tm_lote_rps_serviceimportarLoteRPSOperationCompleted == null)) {
                this.tm_lote_rps_serviceimportarLoteRPSOperationCompleted = new System.Threading.SendOrPostCallback(this.Ontm_lote_rps_serviceimportarLoteRPSOperationCompleted);
            }
            this.InvokeAsync("tm_lote_rps_serviceimportarLoteRPS", new object[] {
                        xml,
                        codMunicipio,
                        cnpjPrestador,
                        hashValidador}, this.tm_lote_rps_serviceimportarLoteRPSOperationCompleted, userState);
        }
        
        private void Ontm_lote_rps_serviceimportarLoteRPSOperationCompleted(object arg) {
            if ((this.tm_lote_rps_serviceimportarLoteRPSCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.tm_lote_rps_serviceimportarLoteRPSCompleted(this, new tm_lote_rps_serviceimportarLoteRPSCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:loterpswsdl#tm_lote_rps_service.consultarLoteRPS", RequestNamespace="urn:loterpswsdl", ResponseNamespace="urn:loterpswsdl")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public string tm_lote_rps_serviceconsultarLoteRPS(string protocolo, string codMunicipio, string cnpjPrestador, string hashValidador) {
            object[] results = this.Invoke("tm_lote_rps_serviceconsultarLoteRPS", new object[] {
                        protocolo,
                        codMunicipio,
                        cnpjPrestador,
                        hashValidador});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void tm_lote_rps_serviceconsultarLoteRPSAsync(string protocolo, string codMunicipio, string cnpjPrestador, string hashValidador) {
            this.tm_lote_rps_serviceconsultarLoteRPSAsync(protocolo, codMunicipio, cnpjPrestador, hashValidador, null);
        }
        
        /// <remarks/>
        public void tm_lote_rps_serviceconsultarLoteRPSAsync(string protocolo, string codMunicipio, string cnpjPrestador, string hashValidador, object userState) {
            if ((this.tm_lote_rps_serviceconsultarLoteRPSOperationCompleted == null)) {
                this.tm_lote_rps_serviceconsultarLoteRPSOperationCompleted = new System.Threading.SendOrPostCallback(this.Ontm_lote_rps_serviceconsultarLoteRPSOperationCompleted);
            }
            this.InvokeAsync("tm_lote_rps_serviceconsultarLoteRPS", new object[] {
                        protocolo,
                        codMunicipio,
                        cnpjPrestador,
                        hashValidador}, this.tm_lote_rps_serviceconsultarLoteRPSOperationCompleted, userState);
        }
        
        private void Ontm_lote_rps_serviceconsultarLoteRPSOperationCompleted(object arg) {
            if ((this.tm_lote_rps_serviceconsultarLoteRPSCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.tm_lote_rps_serviceconsultarLoteRPSCompleted(this, new tm_lote_rps_serviceconsultarLoteRPSCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:loterpswsdl#tm_lote_rps_service.consultarRPS", RequestNamespace="urn:loterpswsdl", ResponseNamespace="urn:loterpswsdl")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public string tm_lote_rps_serviceconsultarRPS(string numeroRPS, string codMunicipio, string cnpjPrestador, string hashValidador) {
            object[] results = this.Invoke("tm_lote_rps_serviceconsultarRPS", new object[] {
                        numeroRPS,
                        codMunicipio,
                        cnpjPrestador,
                        hashValidador});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void tm_lote_rps_serviceconsultarRPSAsync(string numeroRPS, string codMunicipio, string cnpjPrestador, string hashValidador) {
            this.tm_lote_rps_serviceconsultarRPSAsync(numeroRPS, codMunicipio, cnpjPrestador, hashValidador, null);
        }
        
        /// <remarks/>
        public void tm_lote_rps_serviceconsultarRPSAsync(string numeroRPS, string codMunicipio, string cnpjPrestador, string hashValidador, object userState) {
            if ((this.tm_lote_rps_serviceconsultarRPSOperationCompleted == null)) {
                this.tm_lote_rps_serviceconsultarRPSOperationCompleted = new System.Threading.SendOrPostCallback(this.Ontm_lote_rps_serviceconsultarRPSOperationCompleted);
            }
            this.InvokeAsync("tm_lote_rps_serviceconsultarRPS", new object[] {
                        numeroRPS,
                        codMunicipio,
                        cnpjPrestador,
                        hashValidador}, this.tm_lote_rps_serviceconsultarRPSOperationCompleted, userState);
        }
        
        private void Ontm_lote_rps_serviceconsultarRPSOperationCompleted(object arg) {
            if ((this.tm_lote_rps_serviceconsultarRPSCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.tm_lote_rps_serviceconsultarRPSCompleted(this, new tm_lote_rps_serviceconsultarRPSCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:loterpswsdl#tm_lote_rps_service.cancelarNFSE", RequestNamespace="urn:loterpswsdl", ResponseNamespace="urn:loterpswsdl")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public string tm_lote_rps_servicecancelarNFSE(string numeroNFSE, string codMunicipio, string cnpjPrestador, string hashValidador) {
            object[] results = this.Invoke("tm_lote_rps_servicecancelarNFSE", new object[] {
                        numeroNFSE,
                        codMunicipio,
                        cnpjPrestador,
                        hashValidador});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void tm_lote_rps_servicecancelarNFSEAsync(string numeroNFSE, string codMunicipio, string cnpjPrestador, string hashValidador) {
            this.tm_lote_rps_servicecancelarNFSEAsync(numeroNFSE, codMunicipio, cnpjPrestador, hashValidador, null);
        }
        
        /// <remarks/>
        public void tm_lote_rps_servicecancelarNFSEAsync(string numeroNFSE, string codMunicipio, string cnpjPrestador, string hashValidador, object userState) {
            if ((this.tm_lote_rps_servicecancelarNFSEOperationCompleted == null)) {
                this.tm_lote_rps_servicecancelarNFSEOperationCompleted = new System.Threading.SendOrPostCallback(this.Ontm_lote_rps_servicecancelarNFSEOperationCompleted);
            }
            this.InvokeAsync("tm_lote_rps_servicecancelarNFSE", new object[] {
                        numeroNFSE,
                        codMunicipio,
                        cnpjPrestador,
                        hashValidador}, this.tm_lote_rps_servicecancelarNFSEOperationCompleted, userState);
        }
        
        private void Ontm_lote_rps_servicecancelarNFSEOperationCompleted(object arg) {
            if ((this.tm_lote_rps_servicecancelarNFSECompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.tm_lote_rps_servicecancelarNFSECompleted(this, new tm_lote_rps_servicecancelarNFSECompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:loterpswsdl#tm_lote_rps_service.consultarNFSE", RequestNamespace="urn:loterpswsdl", ResponseNamespace="urn:loterpswsdl")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public string tm_lote_rps_serviceconsultarNFSE(string numeroNFSE, string codMunicipio, string cnpjPrestador, string hashValidador) {
            object[] results = this.Invoke("tm_lote_rps_serviceconsultarNFSE", new object[] {
                        numeroNFSE,
                        codMunicipio,
                        cnpjPrestador,
                        hashValidador});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void tm_lote_rps_serviceconsultarNFSEAsync(string numeroNFSE, string codMunicipio, string cnpjPrestador, string hashValidador) {
            this.tm_lote_rps_serviceconsultarNFSEAsync(numeroNFSE, codMunicipio, cnpjPrestador, hashValidador, null);
        }
        
        /// <remarks/>
        public void tm_lote_rps_serviceconsultarNFSEAsync(string numeroNFSE, string codMunicipio, string cnpjPrestador, string hashValidador, object userState) {
            if ((this.tm_lote_rps_serviceconsultarNFSEOperationCompleted == null)) {
                this.tm_lote_rps_serviceconsultarNFSEOperationCompleted = new System.Threading.SendOrPostCallback(this.Ontm_lote_rps_serviceconsultarNFSEOperationCompleted);
            }
            this.InvokeAsync("tm_lote_rps_serviceconsultarNFSE", new object[] {
                        numeroNFSE,
                        codMunicipio,
                        cnpjPrestador,
                        hashValidador}, this.tm_lote_rps_serviceconsultarNFSEOperationCompleted, userState);
        }
        
        private void Ontm_lote_rps_serviceconsultarNFSEOperationCompleted(object arg) {
            if ((this.tm_lote_rps_serviceconsultarNFSECompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.tm_lote_rps_serviceconsultarNFSECompleted(this, new tm_lote_rps_serviceconsultarNFSECompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:loterpswsdl#tm_lote_rps_service.servicoRPS", RequestNamespace="urn:loterpswsdl", ResponseNamespace="urn:loterpswsdl")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public string tm_lote_rps_serviceservicoRPS(string nomeServico, string parametros, string codMunicipio, string cnpjPrestador, string hashValidador) {
            object[] results = this.Invoke("tm_lote_rps_serviceservicoRPS", new object[] {
                        nomeServico,
                        parametros,
                        codMunicipio,
                        cnpjPrestador,
                        hashValidador});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void tm_lote_rps_serviceservicoRPSAsync(string nomeServico, string parametros, string codMunicipio, string cnpjPrestador, string hashValidador) {
            this.tm_lote_rps_serviceservicoRPSAsync(nomeServico, parametros, codMunicipio, cnpjPrestador, hashValidador, null);
        }
        
        /// <remarks/>
        public void tm_lote_rps_serviceservicoRPSAsync(string nomeServico, string parametros, string codMunicipio, string cnpjPrestador, string hashValidador, object userState) {
            if ((this.tm_lote_rps_serviceservicoRPSOperationCompleted == null)) {
                this.tm_lote_rps_serviceservicoRPSOperationCompleted = new System.Threading.SendOrPostCallback(this.Ontm_lote_rps_serviceservicoRPSOperationCompleted);
            }
            this.InvokeAsync("tm_lote_rps_serviceservicoRPS", new object[] {
                        nomeServico,
                        parametros,
                        codMunicipio,
                        cnpjPrestador,
                        hashValidador}, this.tm_lote_rps_serviceservicoRPSOperationCompleted, userState);
        }
        
        private void Ontm_lote_rps_serviceservicoRPSOperationCompleted(object arg) {
            if ((this.tm_lote_rps_serviceservicoRPSCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.tm_lote_rps_serviceservicoRPSCompleted(this, new tm_lote_rps_serviceservicoRPSCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void tm_lote_rps_servicetestarLoteRPSCompletedEventHandler(object sender, tm_lote_rps_servicetestarLoteRPSCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class tm_lote_rps_servicetestarLoteRPSCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal tm_lote_rps_servicetestarLoteRPSCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void tm_lote_rps_serviceimportarLoteRPSCompletedEventHandler(object sender, tm_lote_rps_serviceimportarLoteRPSCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class tm_lote_rps_serviceimportarLoteRPSCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal tm_lote_rps_serviceimportarLoteRPSCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void tm_lote_rps_serviceconsultarLoteRPSCompletedEventHandler(object sender, tm_lote_rps_serviceconsultarLoteRPSCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class tm_lote_rps_serviceconsultarLoteRPSCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal tm_lote_rps_serviceconsultarLoteRPSCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void tm_lote_rps_serviceconsultarRPSCompletedEventHandler(object sender, tm_lote_rps_serviceconsultarRPSCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class tm_lote_rps_serviceconsultarRPSCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal tm_lote_rps_serviceconsultarRPSCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void tm_lote_rps_servicecancelarNFSECompletedEventHandler(object sender, tm_lote_rps_servicecancelarNFSECompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class tm_lote_rps_servicecancelarNFSECompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal tm_lote_rps_servicecancelarNFSECompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void tm_lote_rps_serviceconsultarNFSECompletedEventHandler(object sender, tm_lote_rps_serviceconsultarNFSECompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class tm_lote_rps_serviceconsultarNFSECompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal tm_lote_rps_serviceconsultarNFSECompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void tm_lote_rps_serviceservicoRPSCompletedEventHandler(object sender, tm_lote_rps_serviceservicoRPSCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class tm_lote_rps_serviceservicoRPSCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal tm_lote_rps_serviceservicoRPSCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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