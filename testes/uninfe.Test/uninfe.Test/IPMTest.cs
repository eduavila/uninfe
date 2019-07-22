using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFe.Components;
using NFSe.Components;
using System.IO;

namespace uninfe.Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class IPMTest: BaseTest
    {
        //definir o usuário e senha de testes
        //Código da cidade na receita federal (TOM), pesquisei o código em http://www.ekwbrasil.com.br/municipio.php3.
        IPM nf = new IPM("xxx", "zzz", 74837);

        /// <summary>
        /// Gera um arquivo xml e retorna o caminho do arquivo
        /// </summary>
        /// <returns></returns>
        private string GenerateXML()
        {

            string result = Environment.GetEnvironmentVariable("temp") + @"\xmlExemplo.xml";

            using(StreamWriter writer = new StreamWriter(result))
            {
                #region xml
                writer.Write(@"
<?xml version='1.0' encoding='iso-8859-1'?>
<nfse>
  <nf>
    <valor_total>100,00</valor_total>
    <valor_desconto>0,00</valor_desconto>
    <valor_ir>0,00</valor_ir>
    <valor_inss>0,00</valor_inss>
    <valor_contribuicao_social>0,00</valor_contribuicao_social>
    <valor_rps>0,00</valor_rps>
    <valor_pis>0,00</valor_pis>
    <valor_cofins>0,00</valor_cofins>
    <observacao></observacao>
  </nf>
  <prestador>
    <cpfcnpj>21948242000181</cpfcnpj>
    <cidade>8003</cidade>
  </prestador>
  <tomador>
    <tipo>F</tipo>
    <cpfcnpj>00622793942</cpfcnpj>
    <ie></ie>
    <nome_razao_social>RODRIGO MARCOLA</nome_razao_social>
    <sobrenome_nome_fantasia></sobrenome_nome_fantasia>
    <logradouro>Rua Jaco Finardi, 799</logradouro>
    <email>email@dominio.com.br</email>
    <complemento></complemento>
    <ponto_referencia></ponto_referencia>
    <bairro>Canta Galo</bairro>
    <cidade>8291</cidade>
    <cep>89160000</cep>
    <ddd_fone_comercial></ddd_fone_comercial>
    <fone_comercial></fone_comercial>
    <ddd_fone_residencial></ddd_fone_residencial>
    <fone_residencial></fone_residencial>
    <ddd_fax></ddd_fax>
    <fone_fax></fone_fax>
  </tomador>
  <itens>
    <lista>
      <codigo_local_prestacao_servico>8003</codigo_local_prestacao_servico>
      <codigo_item_lista_servico>706</codigo_item_lista_servico>
      <descritivo>Teste para emissão de NFS-e</descritivo>
      <aliquota_item_lista_servico>3,00</aliquota_item_lista_servico>
      <situacao_tributaria>00</situacao_tributaria>
      <valor_tributavel>100</valor_tributavel>
      <valor_deducao>0,00</valor_deducao>
      <valor_issrf>0,00</valor_issrf>
      <tributa_municipio_prestador>S</tributa_municipio_prestador>
      <unidade_codigo/>
      <unidade_quantidade/>
      <unidade_valor_unitario/>                    
    </lista>
  </itens>
  <produtos>
  </produtos>
</nfse>");
                #endregion

                writer.Flush();
                writer.Close();
            }

            return result;
        }

        [TestMethod]
        public void EmitirNF()
        {
            nf.EmitirNF(GenerateXML(), TpAmb.Homologacao);
        }

        [TestMethod]
        public void CancelarNF()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void ConsultarLote()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void ConsultarNF()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void ConsultarSituacaoLote()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void RecepcionarLote()
        {
            throw new NotImplementedException();
        }
    }
}