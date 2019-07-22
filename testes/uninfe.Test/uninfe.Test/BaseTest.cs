using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace uninfe.Test
{
    /// <summary>
    /// Classe para testes dentro do UniNFe.
    /// <para>Esta classe inicializa as configurações do UniNFe e prepara para que possa ser utilizada de forma correta dentro dos testes</para>
    /// </summary>
    [TestClass]
    public abstract class BaseTest
    {
        #region Propriedades
        public TestContext TestContext { get; set; }
        #endregion

        #region init e construtores
        /// <summary>
        /// Inicializa as configurações do UniNFe
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
        }
        #endregion

        #region dispose
        ~BaseTest()
        {
            Dispose(false);
        }

        /// <summary>
        /// Descarrega todos os objetos utilizados na classe
        /// </summary>
        [ClassCleanup()]
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Descarrega todos os objetos utilizados na classe
        /// </summary>
        /// <param name="disposing">se true, para executar ações de limpeza adicionais</param>
        public void Dispose(bool disposing)
        {
            if(disposing)
            {
            }
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Escreve no contexto do teste a string passada em format
        /// </summary>
        /// <param name="format">string a ser escrita</param>
        /// <param name="args">argumentos para formatação da string</param>
        public void WriteLine(string format, params object[] args)
        {
            if(TestContext != null)
                TestContext.WriteLine(format, args);
            else
                System.Diagnostics.Debug.WriteLine(format, args);
        }
        #endregion
    }
}