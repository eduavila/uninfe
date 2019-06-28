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

namespace NFe.Components.HRondonopolisMT {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="nfse_web_serviceSoapBinding", Namespace="Tributario")]
    public partial class nfse_web_service : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback RECEPCIONARLOTERPSOperationCompleted;
        
        private System.Threading.SendOrPostCallback GERARNFSEOperationCompleted;
        
        private System.Threading.SendOrPostCallback RECEPCIONARLOTERPSSINCRONOOperationCompleted;
        
        private System.Threading.SendOrPostCallback CANCELARNFSEOperationCompleted;
        
        private System.Threading.SendOrPostCallback CONSULTARLOTERPSOperationCompleted;
        
        private System.Threading.SendOrPostCallback CONSULTARNFSEFAIXAOperationCompleted;
        
        private System.Threading.SendOrPostCallback CONSULTARNFSEPORRPSOperationCompleted;
        
        private System.Threading.SendOrPostCallback CONSULTARNFSESERVICOPRESTADOOperationCompleted;
        
        private System.Threading.SendOrPostCallback SUBSTITUIRNFSEOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public nfse_web_service() {
            this.Url = global::NFe.Components.Properties.Settings.Default.NFe_Components_HRondonopolisMT_nfse_web_service;
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
        public event RECEPCIONARLOTERPSCompletedEventHandler RECEPCIONARLOTERPSCompleted;
        
        /// <remarks/>
        public event GERARNFSECompletedEventHandler GERARNFSECompleted;
        
        /// <remarks/>
        public event RECEPCIONARLOTERPSSINCRONOCompletedEventHandler RECEPCIONARLOTERPSSINCRONOCompleted;
        
        /// <remarks/>
        public event CANCELARNFSECompletedEventHandler CANCELARNFSECompleted;
        
        /// <remarks/>
        public event CONSULTARLOTERPSCompletedEventHandler CONSULTARLOTERPSCompleted;
        
        /// <remarks/>
        public event CONSULTARNFSEFAIXACompletedEventHandler CONSULTARNFSEFAIXACompleted;
        
        /// <remarks/>
        public event CONSULTARNFSEPORRPSCompletedEventHandler CONSULTARNFSEPORRPSCompleted;
        
        /// <remarks/>
        public event CONSULTARNFSESERVICOPRESTADOCompletedEventHandler CONSULTARNFSESERVICOPRESTADOCompleted;
        
        /// <remarks/>
        public event SUBSTITUIRNFSECompletedEventHandler SUBSTITUIRNFSECompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("Tributarioaction/ANFSE_WEB_SERVICE.RECEPCIONARLOTERPS", RequestElementName="nfse_web_service.RECEPCIONARLOTERPS", RequestNamespace="Tributario", ResponseElementName="nfse_web_service.RECEPCIONARLOTERPSResponse", ResponseNamespace="Tributario", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("Recepcionarloterpsresponse")]
        public output RECEPCIONARLOTERPS(input Recepcionarloterpsrequest) {
            object[] results = this.Invoke("RECEPCIONARLOTERPS", new object[] {
                        Recepcionarloterpsrequest});
            return ((output)(results[0]));
        }
        
        /// <remarks/>
        public void RECEPCIONARLOTERPSAsync(input Recepcionarloterpsrequest) {
            this.RECEPCIONARLOTERPSAsync(Recepcionarloterpsrequest, null);
        }
        
        /// <remarks/>
        public void RECEPCIONARLOTERPSAsync(input Recepcionarloterpsrequest, object userState) {
            if ((this.RECEPCIONARLOTERPSOperationCompleted == null)) {
                this.RECEPCIONARLOTERPSOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRECEPCIONARLOTERPSOperationCompleted);
            }
            this.InvokeAsync("RECEPCIONARLOTERPS", new object[] {
                        Recepcionarloterpsrequest}, this.RECEPCIONARLOTERPSOperationCompleted, userState);
        }
        
        private void OnRECEPCIONARLOTERPSOperationCompleted(object arg) {
            if ((this.RECEPCIONARLOTERPSCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RECEPCIONARLOTERPSCompleted(this, new RECEPCIONARLOTERPSCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("Tributarioaction/ANFSE_WEB_SERVICE.GERARNFSE", RequestElementName="nfse_web_service.GERARNFSE", RequestNamespace="Tributario", ResponseElementName="nfse_web_service.GERARNFSEResponse", ResponseNamespace="Tributario", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("Gerarnfseresponse")]
        public output GERARNFSE(input Gerarnfserequest) {
            object[] results = this.Invoke("GERARNFSE", new object[] {
                        Gerarnfserequest});
            return ((output)(results[0]));
        }
        
        /// <remarks/>
        public void GERARNFSEAsync(input Gerarnfserequest) {
            this.GERARNFSEAsync(Gerarnfserequest, null);
        }
        
        /// <remarks/>
        public void GERARNFSEAsync(input Gerarnfserequest, object userState) {
            if ((this.GERARNFSEOperationCompleted == null)) {
                this.GERARNFSEOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGERARNFSEOperationCompleted);
            }
            this.InvokeAsync("GERARNFSE", new object[] {
                        Gerarnfserequest}, this.GERARNFSEOperationCompleted, userState);
        }
        
        private void OnGERARNFSEOperationCompleted(object arg) {
            if ((this.GERARNFSECompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GERARNFSECompleted(this, new GERARNFSECompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("Tributarioaction/ANFSE_WEB_SERVICE.RECEPCIONARLOTERPSSINCRONO", RequestElementName="nfse_web_service.RECEPCIONARLOTERPSSINCRONO", RequestNamespace="Tributario", ResponseElementName="nfse_web_service.RECEPCIONARLOTERPSSINCRONOResponse", ResponseNamespace="Tributario", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("Recepcionarloterpssincronoresponse")]
        public output RECEPCIONARLOTERPSSINCRONO(input Recepcionarloterpssincronorequest) {
            object[] results = this.Invoke("RECEPCIONARLOTERPSSINCRONO", new object[] {
                        Recepcionarloterpssincronorequest});
            return ((output)(results[0]));
        }
        
        /// <remarks/>
        public void RECEPCIONARLOTERPSSINCRONOAsync(input Recepcionarloterpssincronorequest) {
            this.RECEPCIONARLOTERPSSINCRONOAsync(Recepcionarloterpssincronorequest, null);
        }
        
        /// <remarks/>
        public void RECEPCIONARLOTERPSSINCRONOAsync(input Recepcionarloterpssincronorequest, object userState) {
            if ((this.RECEPCIONARLOTERPSSINCRONOOperationCompleted == null)) {
                this.RECEPCIONARLOTERPSSINCRONOOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRECEPCIONARLOTERPSSINCRONOOperationCompleted);
            }
            this.InvokeAsync("RECEPCIONARLOTERPSSINCRONO", new object[] {
                        Recepcionarloterpssincronorequest}, this.RECEPCIONARLOTERPSSINCRONOOperationCompleted, userState);
        }
        
        private void OnRECEPCIONARLOTERPSSINCRONOOperationCompleted(object arg) {
            if ((this.RECEPCIONARLOTERPSSINCRONOCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RECEPCIONARLOTERPSSINCRONOCompleted(this, new RECEPCIONARLOTERPSSINCRONOCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("Tributarioaction/ANFSE_WEB_SERVICE.CANCELARNFSE", RequestElementName="nfse_web_service.CANCELARNFSE", RequestNamespace="Tributario", ResponseElementName="nfse_web_service.CANCELARNFSEResponse", ResponseNamespace="Tributario", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("Cancelarnfseresponse")]
        public output CANCELARNFSE(input Cancelarnfserequest) {
            object[] results = this.Invoke("CANCELARNFSE", new object[] {
                        Cancelarnfserequest});
            return ((output)(results[0]));
        }
        
        /// <remarks/>
        public void CANCELARNFSEAsync(input Cancelarnfserequest) {
            this.CANCELARNFSEAsync(Cancelarnfserequest, null);
        }
        
        /// <remarks/>
        public void CANCELARNFSEAsync(input Cancelarnfserequest, object userState) {
            if ((this.CANCELARNFSEOperationCompleted == null)) {
                this.CANCELARNFSEOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCANCELARNFSEOperationCompleted);
            }
            this.InvokeAsync("CANCELARNFSE", new object[] {
                        Cancelarnfserequest}, this.CANCELARNFSEOperationCompleted, userState);
        }
        
        private void OnCANCELARNFSEOperationCompleted(object arg) {
            if ((this.CANCELARNFSECompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CANCELARNFSECompleted(this, new CANCELARNFSECompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("Tributarioaction/ANFSE_WEB_SERVICE.CONSULTARLOTERPS", RequestElementName="nfse_web_service.CONSULTARLOTERPS", RequestNamespace="Tributario", ResponseElementName="nfse_web_service.CONSULTARLOTERPSResponse", ResponseNamespace="Tributario", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("Consultarloterpsresponse")]
        public output CONSULTARLOTERPS(input Consultarloterpsrequest) {
            object[] results = this.Invoke("CONSULTARLOTERPS", new object[] {
                        Consultarloterpsrequest});
            return ((output)(results[0]));
        }
        
        /// <remarks/>
        public void CONSULTARLOTERPSAsync(input Consultarloterpsrequest) {
            this.CONSULTARLOTERPSAsync(Consultarloterpsrequest, null);
        }
        
        /// <remarks/>
        public void CONSULTARLOTERPSAsync(input Consultarloterpsrequest, object userState) {
            if ((this.CONSULTARLOTERPSOperationCompleted == null)) {
                this.CONSULTARLOTERPSOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCONSULTARLOTERPSOperationCompleted);
            }
            this.InvokeAsync("CONSULTARLOTERPS", new object[] {
                        Consultarloterpsrequest}, this.CONSULTARLOTERPSOperationCompleted, userState);
        }
        
        private void OnCONSULTARLOTERPSOperationCompleted(object arg) {
            if ((this.CONSULTARLOTERPSCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CONSULTARLOTERPSCompleted(this, new CONSULTARLOTERPSCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("Tributarioaction/ANFSE_WEB_SERVICE.CONSULTARNFSEFAIXA", RequestElementName="nfse_web_service.CONSULTARNFSEFAIXA", RequestNamespace="Tributario", ResponseElementName="nfse_web_service.CONSULTARNFSEFAIXAResponse", ResponseNamespace="Tributario", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("Consultarnfseporfaixaresponse")]
        public output CONSULTARNFSEFAIXA(input Consultarnfseporfaixarequest) {
            object[] results = this.Invoke("CONSULTARNFSEFAIXA", new object[] {
                        Consultarnfseporfaixarequest});
            return ((output)(results[0]));
        }
        
        /// <remarks/>
        public void CONSULTARNFSEFAIXAAsync(input Consultarnfseporfaixarequest) {
            this.CONSULTARNFSEFAIXAAsync(Consultarnfseporfaixarequest, null);
        }
        
        /// <remarks/>
        public void CONSULTARNFSEFAIXAAsync(input Consultarnfseporfaixarequest, object userState) {
            if ((this.CONSULTARNFSEFAIXAOperationCompleted == null)) {
                this.CONSULTARNFSEFAIXAOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCONSULTARNFSEFAIXAOperationCompleted);
            }
            this.InvokeAsync("CONSULTARNFSEFAIXA", new object[] {
                        Consultarnfseporfaixarequest}, this.CONSULTARNFSEFAIXAOperationCompleted, userState);
        }
        
        private void OnCONSULTARNFSEFAIXAOperationCompleted(object arg) {
            if ((this.CONSULTARNFSEFAIXACompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CONSULTARNFSEFAIXACompleted(this, new CONSULTARNFSEFAIXACompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("Tributarioaction/ANFSE_WEB_SERVICE.CONSULTARNFSEPORRPS", RequestElementName="nfse_web_service.CONSULTARNFSEPORRPS", RequestNamespace="Tributario", ResponseElementName="nfse_web_service.CONSULTARNFSEPORRPSResponse", ResponseNamespace="Tributario", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("Consultarnfseporrpsresponse")]
        public output CONSULTARNFSEPORRPS(input Consultarnfseporrpsrequest) {
            object[] results = this.Invoke("CONSULTARNFSEPORRPS", new object[] {
                        Consultarnfseporrpsrequest});
            return ((output)(results[0]));
        }
        
        /// <remarks/>
        public void CONSULTARNFSEPORRPSAsync(input Consultarnfseporrpsrequest) {
            this.CONSULTARNFSEPORRPSAsync(Consultarnfseporrpsrequest, null);
        }
        
        /// <remarks/>
        public void CONSULTARNFSEPORRPSAsync(input Consultarnfseporrpsrequest, object userState) {
            if ((this.CONSULTARNFSEPORRPSOperationCompleted == null)) {
                this.CONSULTARNFSEPORRPSOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCONSULTARNFSEPORRPSOperationCompleted);
            }
            this.InvokeAsync("CONSULTARNFSEPORRPS", new object[] {
                        Consultarnfseporrpsrequest}, this.CONSULTARNFSEPORRPSOperationCompleted, userState);
        }
        
        private void OnCONSULTARNFSEPORRPSOperationCompleted(object arg) {
            if ((this.CONSULTARNFSEPORRPSCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CONSULTARNFSEPORRPSCompleted(this, new CONSULTARNFSEPORRPSCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("Tributarioaction/ANFSE_WEB_SERVICE.CONSULTARNFSESERVICOPRESTADO", RequestElementName="nfse_web_service.CONSULTARNFSESERVICOPRESTADO", RequestNamespace="Tributario", ResponseElementName="nfse_web_service.CONSULTARNFSESERVICOPRESTADOResponse", ResponseNamespace="Tributario", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("Consultarnfseservicoprestadoresponse")]
        public output CONSULTARNFSESERVICOPRESTADO(input Consultarnfseservicoprestadorequest) {
            object[] results = this.Invoke("CONSULTARNFSESERVICOPRESTADO", new object[] {
                        Consultarnfseservicoprestadorequest});
            return ((output)(results[0]));
        }
        
        /// <remarks/>
        public void CONSULTARNFSESERVICOPRESTADOAsync(input Consultarnfseservicoprestadorequest) {
            this.CONSULTARNFSESERVICOPRESTADOAsync(Consultarnfseservicoprestadorequest, null);
        }
        
        /// <remarks/>
        public void CONSULTARNFSESERVICOPRESTADOAsync(input Consultarnfseservicoprestadorequest, object userState) {
            if ((this.CONSULTARNFSESERVICOPRESTADOOperationCompleted == null)) {
                this.CONSULTARNFSESERVICOPRESTADOOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCONSULTARNFSESERVICOPRESTADOOperationCompleted);
            }
            this.InvokeAsync("CONSULTARNFSESERVICOPRESTADO", new object[] {
                        Consultarnfseservicoprestadorequest}, this.CONSULTARNFSESERVICOPRESTADOOperationCompleted, userState);
        }
        
        private void OnCONSULTARNFSESERVICOPRESTADOOperationCompleted(object arg) {
            if ((this.CONSULTARNFSESERVICOPRESTADOCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CONSULTARNFSESERVICOPRESTADOCompleted(this, new CONSULTARNFSESERVICOPRESTADOCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("Tributarioaction/ANFSE_WEB_SERVICE.SUBSTITUIRNFSE", RequestElementName="nfse_web_service.SUBSTITUIRNFSE", RequestNamespace="Tributario", ResponseElementName="nfse_web_service.SUBSTITUIRNFSEResponse", ResponseNamespace="Tributario", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("Substituirnfseresponse")]
        public output SUBSTITUIRNFSE(input Substituirnfserequest) {
            object[] results = this.Invoke("SUBSTITUIRNFSE", new object[] {
                        Substituirnfserequest});
            return ((output)(results[0]));
        }
        
        /// <remarks/>
        public void SUBSTITUIRNFSEAsync(input Substituirnfserequest) {
            this.SUBSTITUIRNFSEAsync(Substituirnfserequest, null);
        }
        
        /// <remarks/>
        public void SUBSTITUIRNFSEAsync(input Substituirnfserequest, object userState) {
            if ((this.SUBSTITUIRNFSEOperationCompleted == null)) {
                this.SUBSTITUIRNFSEOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSUBSTITUIRNFSEOperationCompleted);
            }
            this.InvokeAsync("SUBSTITUIRNFSE", new object[] {
                        Substituirnfserequest}, this.SUBSTITUIRNFSEOperationCompleted, userState);
        }
        
        private void OnSUBSTITUIRNFSEOperationCompleted(object arg) {
            if ((this.SUBSTITUIRNFSECompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SUBSTITUIRNFSECompleted(this, new SUBSTITUIRNFSECompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="Tributario")]
    public partial class input
    {

        [XmlIgnore]
        public string nfseCabecMsg { get; set; }

        System.Xml.XmlCDataSection _nfseCabecMsgCDATA = null;
        
        /// <remarks/>
        [XmlElement("nfseCabecMsg")]
        public System.Xml.XmlCDataSection nfseCabecMsgCDATA
        {
            get
            {

                if (_nfseCabecMsgCDATA == null)
                    _nfseCabecMsgCDATA = new System.Xml.XmlDocument().CreateCDataSection(this.nfseCabecMsg);

                return _nfseCabecMsgCDATA;
            }
            set
            {
                _nfseCabecMsgCDATA = value;
            }
        }
        
        [XmlIgnore]
        public string nfseDadosMsg { get; set; }


        System.Xml.XmlCDataSection _nfseDadosMsgCDATA = null;

        [XmlElement("nfseDadosMsg")]
        public System.Xml.XmlCDataSection nfseDadosMsgCDATA
        {
            get
            {
                if (_nfseDadosMsgCDATA == null)
                    _nfseDadosMsgCDATA = new System.Xml.XmlDocument().CreateCDataSection(this.nfseDadosMsg);

                return _nfseDadosMsgCDATA;
            }
            set
            {
                _nfseDadosMsgCDATA = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="Tributario")]
    public partial class output {
        
        private string outputXMLField;
        
        /// <remarks/>
        public string outputXML {
            get {
                return this.outputXMLField;
            }
            set {
                this.outputXMLField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    public delegate void RECEPCIONARLOTERPSCompletedEventHandler(object sender, RECEPCIONARLOTERPSCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RECEPCIONARLOTERPSCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RECEPCIONARLOTERPSCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public output Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((output)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    public delegate void GERARNFSECompletedEventHandler(object sender, GERARNFSECompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GERARNFSECompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GERARNFSECompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public output Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((output)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    public delegate void RECEPCIONARLOTERPSSINCRONOCompletedEventHandler(object sender, RECEPCIONARLOTERPSSINCRONOCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RECEPCIONARLOTERPSSINCRONOCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RECEPCIONARLOTERPSSINCRONOCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public output Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((output)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    public delegate void CANCELARNFSECompletedEventHandler(object sender, CANCELARNFSECompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CANCELARNFSECompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CANCELARNFSECompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public output Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((output)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    public delegate void CONSULTARLOTERPSCompletedEventHandler(object sender, CONSULTARLOTERPSCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CONSULTARLOTERPSCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CONSULTARLOTERPSCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public output Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((output)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    public delegate void CONSULTARNFSEFAIXACompletedEventHandler(object sender, CONSULTARNFSEFAIXACompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CONSULTARNFSEFAIXACompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CONSULTARNFSEFAIXACompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public output Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((output)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    public delegate void CONSULTARNFSEPORRPSCompletedEventHandler(object sender, CONSULTARNFSEPORRPSCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CONSULTARNFSEPORRPSCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CONSULTARNFSEPORRPSCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public output Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((output)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    public delegate void CONSULTARNFSESERVICOPRESTADOCompletedEventHandler(object sender, CONSULTARNFSESERVICOPRESTADOCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CONSULTARNFSESERVICOPRESTADOCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CONSULTARNFSESERVICOPRESTADOCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public output Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((output)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    public delegate void SUBSTITUIRNFSECompletedEventHandler(object sender, SUBSTITUIRNFSECompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SUBSTITUIRNFSECompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SUBSTITUIRNFSECompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public output Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((output)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591