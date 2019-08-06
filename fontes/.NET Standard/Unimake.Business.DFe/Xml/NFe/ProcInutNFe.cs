using System;
using System.Xml;
using System.Xml.Serialization;

namespace Unimake.Business.DFe.Xml.NFe
{
    [Serializable()]
    [XmlRoot("procInutNFe", Namespace = "http://www.portalfiscal.inf.br/nfe", IsNullable = false)]
    public class ProcInutNFe : XMLBase
    {
        [XmlAttribute(AttributeName = "versao", DataType = "token")]
        public string Versao { get; set; }

        [XmlElement("inutNFe")]
        public InutNFe InutNFe { get; set; }

        [XmlElement("retInutNFe")]
        public RetInutNFe RetInutNFe { get; set; }

        /// <summary>
        /// Nome do arquivo de distribuição
        /// </summary>
        [XmlIgnore]
        public string NomeArquivoDistribuicao => RetInutNFe.infInut.Id + "-procinutnfe.xml";

        public override XmlDocument GerarXML()
        {
            XmlDocument xmlDocument = base.GerarXML();

            return xmlDocument;
        }
    }
}
