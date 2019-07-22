using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web.Services.Description;
using System.IO;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Globalization;
using Microsoft.CSharp;
using System.Reflection;
using System.Xml.Serialization;

namespace UniNFeProxy
{
    public class DynamicProxy
    {
        //Location of the WSDL file
        private Uri _uri = null;
        //Services exposed by the WSDL file
        private ServiceDescription _serviceDesc = null;
        //Name of the web service
        private String _serviceName;
        ////Web methods available on a particular service
        //private MethodInfo[] _webMethods; 

        //Proxy assembly to the web service
        private Assembly _proxyAssembly;

        #region Constructor

        public DynamicProxy(Uri uri)
        {
            _uri = uri;
            ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
        }

        #endregion

        #region Public Methods

        public bool CheckValidationResult(object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            if ((sslPolicyErrors == SslPolicyErrors.None) ||
            ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateChainErrors) == SslPolicyErrors.RemoteCertificateChainErrors) ||
            ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateNameMismatch) == SslPolicyErrors.RemoteCertificateNameMismatch) ||
            ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateNotAvailable) == SslPolicyErrors.RemoteCertificateNotAvailable))
                return true;

            return false;
        }

        //<summary>
        //Create the proxy assembly for the web service and get a list of public methods on that proxy assembly
        //</summary>
        //<returns>A MethodInfo array with information about all the public methods of the proxy class.</returns>
        public MethodInfo[] GetWebMethods()
        {
            if (_serviceDesc == null) _serviceDesc = GetServiceDescription();
            if (_serviceDesc == null)
                throw new InvalidOperationException("Unable to generate the proxy assembly.");
            _serviceName = _serviceDesc.Services[0].Name;

            if (_proxyAssembly == null)
            {
                _proxyAssembly = GenerateProxyAssembly();
                if (_proxyAssembly == null)
                    throw new InvalidOperationException("Unable to generate the proxy assembly.");
            }
            //'Use reflection to find the methods on the service requested by the user
            Type service = _proxyAssembly.GetType(_serviceName);
            return service.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.IgnoreCase |
            BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.Public);
        }

        //<summary>
        //Given the name of a web service and a method on that service get the list of parameters
        //</summary>
        //<param name="methodName">The web method to call.</param>
        //<returns>A ParameterInfo array with information about all the parameters</returns>
        //<remarks></remarks>
        public ParameterInfo[] GetParameters(string methodName)
        {
            if (_proxyAssembly == null) return null;

            Type serviceType = _proxyAssembly.GetType(_serviceName);
            return serviceType.GetMethod(methodName).GetParameters();
        }

        //<summary>
        //Instantiate the proxy class and call one of its methods.
        //</summary>
        //<param name="methodName">The name of the web method to call</param>
        //<param name="params">An array of objects that represent the parameters to the web method</param>
        //<returns>Whatever the web method returns</returns>
        //<remarks></remarks>
        public object Invoke(string methodName, object[] parms)
        {
            if (_proxyAssembly == null) return null;

            Type assemblyType = _proxyAssembly.GetType(_serviceName);
            MethodInfo methodInfo = assemblyType.GetMethod(methodName);
            Object instance = Activator.CreateInstance(assemblyType);
            if (instance == null)
                throw new InvalidOperationException("Unable to create an instance of the proxy class");
            return methodInfo.Invoke(instance, parms);
        }

        //<summary>
        //Calls GetType on one of the types in the proxy assembly
        //</summary>
        //<param name="typeName">Type name of the object that you want to get the type for</param>
        //<returns>The type from the proxy assembly</returns>
        //<remarks>The type is from the proxy assembly so it will only have the public interface to the type</remarks>
        public Type GetRemoteType(string typeName)
        {
            if (_proxyAssembly == null) return null;
            return _proxyAssembly.GetType(typeName);
        }

        #endregion

        #region Private Methods

        //<summary>
        //Gets the services exposed by the URI.
        //</summary>
        //<returns>A ServiceDescription object populated with information about the services in the WSDL.</returns>
        private ServiceDescription GetServiceDescription()
        {
            if (_uri == null) return null;

            WebRequest webReq = WebRequest.Create(_uri);
            Stream reqStrm = webReq.GetResponse().GetResponseStream();
            return ServiceDescription.Read(reqStrm);
        }

        //<summary>
        //Dynamically generate a proxy assembly for the web service described in the WSDL.
        //</summary>
        //<returns>An assembly that is a proxy class for the web service defined in the WSDL</returns>
        //<remarks>This method uses the ServiceDescription so if it does not exist and can not be created thorw an exception.</remarks>
        private Assembly GenerateProxyAssembly()
        {
            //Check the pre-requesites
            if (_serviceDesc == null)
            {
                _serviceDesc = GetServiceDescription();
                if (_serviceDesc == null)
                {
                    throw new InvalidOperationException("Unable to create the ServiceDescription for the specified URI.");
                }
            }

            //Import the information about the service
            ServiceDescriptionImporter servImport = new ServiceDescriptionImporter();
            servImport.AddServiceDescription(_serviceDesc, String.Empty, String.Empty);
            servImport.ProtocolName = "Soap";
            servImport.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties;

            //Set up to generate the code

            CodeNamespace ns = new CodeNamespace();
            CodeCompileUnit ccu = new CodeCompileUnit();
            ccu.Namespaces.Add(ns);
            ServiceDescriptionImportWarnings warnings = servImport.Import(ns, ccu);
            //If we didn't find any methods there will be nothing to call later so just stop now.
            if ((warnings == ServiceDescriptionImportWarnings.NoCodeGenerated) ||
            (warnings == ServiceDescriptionImportWarnings.NoMethodsGenerated)) return null;

            CSharpCodeProvider prov = new CSharpCodeProvider();

            //Generate the code
            StringWriter sw = new StringWriter(CultureInfo.CurrentCulture);
            prov.GenerateCodeFromNamespace(ns, sw, null);

            //Create the assembly
            CompilerParameters param = new CompilerParameters(new String[] { "System.dll", "System.Xml.dll", "System.Web.Services.dll", "System.Data.dll" });
            param.GenerateExecutable = false;
            param.GenerateInMemory = false;
            param.TreatWarningsAsErrors = false;
            param.WarningLevel = 4;
            CompilerResults results = new CompilerResults(null);
            results = prov.CompileAssemblyFromSource(param, sw.ToString());
            return results.CompiledAssembly;
        }

        #endregion
    }
}
