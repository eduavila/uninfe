using System.Windows.Forms;

namespace AssinarDFe
{
    public partial class AssinadorXML : Form
    {
        public AssinadorXML()
        {
            InitializeComponent();

            tbMensagem.Text = "Parâmetros necessários para que a assinatura seja realizada:\r\n\r\n" +
                "Arquivo xml a ser assinado\r\n" +
                "/Arq=\"D:\\projetos\\uninfe\\testes\\AssinarDFe\\bin\\Debug\\FE_1500.xml\"\r\n\r\n" +
                "Tag do XML a ser assinada\r\n" +
                "/Tag=rDE\r\n\r\n" +
                "Tag que contem o atributo ID para assinatura\r\n" +
                "/TagId=DE\r\n\r\n" +
                "Tipo de algorítimo a ser utilizado na assinatura (Sha256 ou Sha1)\r\n" +
                "/AT=Sha256\r\n\r\n" +
                "Nome do atributo que tem o Id do documento fiscal eletrônico\r\n" +
                "/NomeID=Id\r\n\r\n" +
                "Serial Number do certificado instalado no repositório do windows\r\n" +
                "/SN=1de4d00ad86eg45d\r\n\r\n" +
                "Se prefeir pode utilizar o certificado A1 via arquivo, basta passar o caminho do .PFX e senha de instalação\r\n\r\n" +
                "/pfx=\"C:\\Certificado Digital\\CertEmpresa.pfx\"\r\n\r\n" +
                "/pfxsenha=12345678\r\n\r\n" +
                "Exemplo:\r\n\r\n" +
                "AssinarDfe.exe /arq=\"D:\\projetos\\uninfe\\testes\\AssinarDFe\\bin\\Debug\\FE_1500.xml\" /tag=rDE /tagID=DE /AT=Sha256 /NomeId=Id /SN=1de4d00ad86eg45d\r\n\r\n" +
                "ou\r\n\r\n" +
                "AssinarDfe.exe /arq=\"D:\\projetos\\uninfe\\testes\\AssinarDFe\\bin\\Debug\\FE_1500.xml\" /tag=rDE /tagID=DE /AT=Sha256 /NomeId=Id /pfx=\"C:\\Certificado Digital\\CertEmpresa.pfx\" /pfxsenha=12345678";
                }
    }
}
