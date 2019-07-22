using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography.X509Certificates;

namespace uninfe.Test
{
    struct Assinatura
    {
        public string InscricaoMunicipal;
        public string SerieRPS;
        public string NumeroRPS;
        public DateTime DataEmissao;
        public string TributacaoRPS;
        public string Status;
        public string ISSRetido;
        public string ValorServicos;
        public string ValorDeducoes;
        public string CodigoServicoPrestado;
        public string Indicador;
        public string CpfCnpj;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(InscricaoMunicipal.PadLeft(8, '0'));
            sb.Append(SerieRPS.PadRight(5));
            sb.Append(NumeroRPS.PadLeft(12, '0'));
            sb.Append(DataEmissao.ToString("yyyyMMdd"));
            sb.Append(TributacaoRPS);
            sb.Append(Status);
            sb.Append(ISSRetido);
            sb.Append(ValorServicos.PadLeft(15, '0'));
            sb.Append(ValorDeducoes.PadLeft(15, '0'));
            sb.Append(CodigoServicoPrestado.PadLeft(5, '0')); ;
            sb.Append(Indicador);
            sb.Append(CpfCnpj.PadLeft(14, '0'));

            return sb.ToString();
        }
    }

    [TestClass]
    public class RSASHA1Test: BaseTest
    {
        [TestMethod]
        public void Encrypt()
        {
            Assinatura assinatura = new Assinatura
            {
                InscricaoMunicipal = "31000",
                SerieRPS = "A",
                NumeroRPS = "2",
                DataEmissao = new DateTime(2011, 01, 10),
                TributacaoRPS = "T",
                Status = "N",
                ISSRetido = "N",
                ValorServicos = "20500",
                ValorDeducoes = "5000",
                CodigoServicoPrestado = "2658",
                Indicador = "1",
                CpfCnpj = "12345678909"
            };

            //assinatura válida retirada de um arquivo assinado
            string valida = "lQllFyvbUo6C68CnFrFosa2jPbjmplQ6x3Q59+vYeTXAwACzXfD71aziAnP3NtEP/UiRRAyOQZOO73N+u2g8sanXJ4jhIOMXkP6yeK9JwTkZ/UoeJQUS7j7iyGw0IOq6o6sb0sV0BxZiclI/EMDvZ5H2zZrEEF9AstZkyEoEoJ8=";
            X509Certificate2 cert = new X509Certificate2();

            string criptografada = NFe.Components.Criptografia.SignWithRSASHA1(cert, assinatura.ToString());
            Assert.AreEqual<string>(valida, criptografada);
        }
    }
}