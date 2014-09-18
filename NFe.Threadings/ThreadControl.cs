﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;
using NFe.Settings;
using NFe.Components;
using NFe.Service;

namespace NFe.Threadings
{
    #region Semaphore
    /// <summary>
    /// controla a posição em que thread está no momento
    /// </summary>
    public enum Semaphore
    {
        /// <summary>
        /// adicionada. Podemos executar
        /// </summary>
        Added,

        /// <summary>
        /// Está sendo processada
        /// </summary>
        Started,

        /// <summary>
        /// finalizou a execução
        /// </summary>
        Finished,

        /// <summary>
        /// Terminou, pode ser removida da lista
        /// </summary>
        Released
    }
    #endregion

    #region #21040

    /// <summary>
    /// Método que será executado ao processar o timer
    /// </summary>
    /// <param name="item"></param>
    delegate void ProcessarHandler(ThreadItem item);

    class BufferItem : IDisposable
    {
        #region propriedades
        /// <summary>
        /// Quando uma empresa usa um certificado do tipo A3, é executado por este Buffer
        /// </summary>
        public Queue<ThreadItem> Buffer { get; private set; }

        /// <summary>
        /// Este timer é executado sempre que o buffer possuir itens que deverão ser enviados
        /// </summary>
        System.Timers.Timer timer = null;

        #endregion

        #region construtores
        /// <summary>
        /// Inicia uma nova instancia e define o método de processamento
        /// </summary>
        /// <param name="processar">método que será chamado para processar o item</param>
        public BufferItem(ProcessarHandler processar)
        {
            Buffer = new Queue<ThreadItem>();

            if (timer == null)
            {
                //-------------------------------------------------------------------------
                // Inicia o timer com 500 milissegundos. Desta forma a execução será rápida
                // Como faz um "Stop" logo ao iniciar o método "Elapsed", não tem problema
                // de chamar com este tempo.
                //-------------------------------------------------------------------------
                timer = new System.Timers.Timer(500);

                timer.Elapsed += (s, e) =>
                {
                    lock (Buffer)
                    {
                        timer.Stop();

                        try
                        {
                            //-------------------------------------------------------------------------
                            // Recupera o threaditem e executa o mesmo
                            //-------------------------------------------------------------------------
                            while (Buffer.Count > 0)
                            {
                                ThreadItem item = Buffer.Dequeue();
                                if (item == null) continue;
                                Auxiliar.WriteLog("O arquivo " + item.FileInfo.FullName + " iniciou o processamento pelo Buffer");
                                processar.Invoke(item);
                                Thread.Sleep(0);
                                Auxiliar.WriteLog("O arquivo " + item.FileInfo.FullName + " finalizou o processamento pelo Buffer");
                                if (Disposed) return;//cai fora ... foi descarregada
                            }
                        }
                        catch (Exception ex)
                        {
                            Auxiliar.WriteLog("ExceptionBuffer: " + ex.ToString());
                        }
                        finally
                        {
                            //-------------------------------------------------------------------------
                            // Sair ... mas continuar executando de tempos em tempos
                            //-------------------------------------------------------------------------
                            timer.Start();
                        }
                    }
                };
            }

            timer.Start();

        }
        #endregion

        #region IDisposable members
        /// <summary>
        /// Retorna true se este objeto foi descarregado
        /// </summary>	
        public bool Disposed { get; private set; }

        ~BufferItem()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //faça algo
                GC.SuppressFinalize(this);
            }

            Disposed = true;
            timer.Stop();
            Buffer.Clear();
        }

        public virtual void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
    #endregion

    #region ThreadItem
    /// <summary>
    /// classe de item da thread
    /// </summary>
    public class ThreadItem : IDisposable
    {
        #region #21040
        static Dictionary<int, BufferItem> _buffer = null;
        /// <summary>
        /// Quando uma empresa usa um certificado do tipo A3, é executado por este Buffer
        /// É utilizado apenas os primeiros 8 dígitos do CNPJ como chave.
        /// </summary>
        static IDictionary<int, BufferItem> Buffer
        {
            get
            {
                if (_buffer == null)
                    _buffer = new Dictionary<int, BufferItem>();

                return _buffer;

            }
        }
        #endregion

        #region delegates
        public delegate void ThreadStartHandler(ThreadItem item);
        public delegate void ThreadEndedHandler(ThreadItem item);
        public delegate void ThreadReleasedHandler(ThreadItem item);
        #endregion

        #region Eventos
        /// <summary>
        /// acontece quando a thread começou a leitura do arquivo
        /// </summary>
        public static event ThreadStartHandler OnStarted;
        /// <summary>
        /// acontece quando a thread finalizou a leitura do arquivo 
        /// </summary>
        public static event ThreadEndedHandler OnEnded;

        /// <summary>
        /// acontece quando a thread removeu o arquivo do buffer
        /// </summary>
        public static event ThreadReleasedHandler OnReleased;

        #endregion

        public ThreadItem(System.IO.FileInfo fi, int empresa)
        {
            FileInfo = fi;
            Empresa = empresa;

            #region #21040
            if (_buffer == null)
            {
                //criar um buffer para cada empresa que o certificado é A3
                foreach (Empresa emp in Empresas.Configuracoes)
                {
                    if (emp.X509Certificado.IsA3())
                    {
                        //-------------------------------------------------------------------------
                        // Usar o subject como chave, pois pode se configurar o mesmo certificado
                        // para empresas diferentes.
                        //-------------------------------------------------------------------------
                        int key = emp.X509Certificado.Subject.GetHashCode();
                        AddToBuffer(key);
                    }
                }
            }
            #endregion
        }

        /// <summary>
        /// Este método adiciona o certificado a lista de buffer de certificados
        /// </summary>
        /// <param name="key">numero de chave do certificado</param>
        private void AddToBuffer(int key)
        {
            if (!Buffer.ContainsKey(key)) Buffer[key] = new BufferItem(Processar);
        }

        public System.IO.FileInfo FileInfo { get; private set; }
        public int Empresa { get; private set; }

        /*<#8084>
         * Com a morte da classe ThreadControl, este método passou a ser responsável 
         * pela execução dos eventos que antes eram feitos pela ThreadControl
         * 
         */
        /// <summary>
        /// Método responsável por executar os eventos de forma síncrona em uma thread separada
        /// </summary>
        public void Run()
        {
            #region #21040
            if (Empresas.Configuracoes.Count != 0)
            {
                Empresa empresa = Empresas.Configuracoes[Empresa];
                /*
                 * Se o certificado for A3, então vai para o Buffer controlado, pois deverá ser executado um de cada vez
                 * 
                 */
                if (empresa.X509Certificado.IsA3())
                {
                    //-------------------------------------------------------------------------
                    // Usar o subject como chave, pois pode se configurar o mesmo certificado
                    // para empresas diferentes.
                    //-------------------------------------------------------------------------
                    int key = empresa.X509Certificado.Subject.GetHashCode();
                    AddToBuffer(key);
                    BufferItem bItem = Buffer[key];
                    bItem.Buffer.Enqueue(this);
                    return;
                }
            }
            #endregion

            Processar(this);
        }

        private void Processar(ThreadItem item)
        {
            if (String.IsNullOrEmpty(Thread.CurrentThread.Name))
                Thread.CurrentThread.Name = item.Empresa.ToString();

            try
            {
                //avisa que vai iniciar
                if (OnStarted != null) OnStarted(item);

                //avisa que vai finalizar
                if (OnEnded != null) OnEnded(item);
            }
            catch (Exception ex)
            {
                Auxiliar.WriteLog("Ocorreu um erro na execução da thread que está sendo executada.\r\nThreadControl.cs (1)\r\n" + ex.Message, true);
            }
            finally
            {
                try
                {
                    //remove o item                   
                    //avisa que removeu o item
                    if (OnReleased != null) OnReleased(item);
                }
                catch (Exception ex)
                {
                    Auxiliar.WriteLog("Ocorreu um erro ao tentar remover o item da Thread que está sendo executada.\r\nThreadControl.cs (2)\r\n" + ex.Message, true);
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        #region IDisposable members
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                //need to do something?
            }

#if DEBUG
            Debug.WriteLine(String.Format("ThreadItem Dipose(disposing: {0});", disposing));
#endif
        }

        ~ThreadItem()
        {
#if DEBUG
            Debug.WriteLine("ThreadItem ~Destructor");
#endif

            Dispose(false);
        }
        #endregion
        //</#8084>
    }
    #endregion

    /*<#8084>
     * A classe ThreadControl deixou de existir por não ser mais utilizada dentro do aplicação, 
     * foi substituída pelo método ThreadItem.Run()_
     *</#8084>
     */

    #region  ThreadService
    /// <summary>
    /// Classe responsável por executar as thread´s base de verificação dos serviços a serem executados
    /// </summary>
    public static class ThreadService
    {
        public static List<Thread> Threads = new List<Thread>();

        public static void Stop()
        {
            for (int i = 0; i < Threads.Count; i++)
            {
                Thread t = Threads[i];
                t.Abort();
            }
            Threads.Clear();

            for (int i = 0; i < MonitoraPasta.fsw.Count; i++)
            {
                MonitoraPasta.fsw[i].StopWatch = true;
            }
        }

        public static void Start()
        {
            Empresas.CarregaConfiguracao();

            #region Ticket #110
            Empresas.CreateLockFile(true);
            #endregion

            //Executar o monitoramento de pastas das empresas cadastradas
            MonitoraPasta e = new MonitoraPasta();

            Threads.Clear();

            //Executa a thread que faz a limpeza dos arquivos temporários
            Thread t = new Thread(new Processar().LimpezaTemporario);
            t.IsBackground = true;
            t.Start();
            Threads.Add(t);

            if (Propriedade.TipoExecucao == TipoExecucao.teAll ||
                Propriedade.TipoExecucao == TipoExecucao.teNFe)
            {
                //Executa a thread que faz a verificação das notas em processamento
                Thread t2 = new Thread(new Processar().EmProcessamento);
                t2.IsBackground = true;
                t2.Start();
                Threads.Add(t2);

                //Executar a thread que faz a consulta do recibo das notas fiscais enviadas
                Processar srv = new Processar();
                Thread t3 = new Thread(srv.GerarXMLPedRec);
                t3.IsBackground = true;
                t3.Start(new NFe.Service.TaskGerarXMLPedRec());
                Threads.Add(t3);
            }
        }
    }
    #endregion
}
