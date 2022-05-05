namespace C.Avancado.Modulo12.Exercicio2
{
    /// <summary>
    /// <para>
    ///     Implemente uma fila de execução de tarefas. Esta fila é uma thread que fica aguardando
    ///     tarefas. Quando uma ou mais tarefas chegam, elas são executadas na ordem que chegaram.
    /// </para>
    /// <para>
    ///     Uma tarefa é representada por um delegate Action. Crie uma classe QueueTask para
    ///     representar a fila de tarefas e um método Enqueue() que permite enfileirar uma tarefa para
    ///     execução.
    /// </para>
    /// <para>
    ///     Utilize um AutoResetEvent para gerenciar a fila, de forma que a thread da fila de tarefas deve
    ///     ficar bloqueada aguardando até que uma tarefa esteja disponível.
    /// </para>
    /// <para>
    ///     Escreva um código que enfileire algumas tarefas para testar o uso da fila.
    /// </para>
    /// </summary>
    public static class Resposta
    {
        public static void Executar()
        {
            TaskQueue taskQueue = new TaskQueue();

            for (int index = 0; index < 100; index++)
            {
                int task = index;

                Console.WriteLine($"Enfileirando a tarefa: {task}");
                taskQueue.Enqueue(() => Console.WriteLine($"Tarefa: {task}"));
                Thread.Sleep(200);
            }

            taskQueue.Close();

            Console.WriteLine("Aguardando o processamento das tarefas pendentes");
        }
    }

    public class TaskQueue
    {
        private readonly AutoResetEvent _event = new AutoResetEvent(false);
        private readonly Queue<Action> _queue = new Queue<Action>();
        private readonly Thread _thread;

        public TaskQueue()
        {
            _thread = new Thread(Run);
            _thread.Start();
        }

        public bool Break { get; private set; }

        public void Enqueue(Action task)
        {
            lock (_queue)
            {
                _queue.Enqueue(task);
            }

            _event.Set();
        }

        public void Close()
        {
            Enqueue(() => Break = true);
        }

        private void Run()
        {
            while (true)
            {
                if (_queue.Count == 0)
                {
                    _event.WaitOne();
                }

                Action action = _queue.Dequeue();
                action();

                if (Break)
                {
                    Console.WriteLine("Todas as tarefas foram processadas");

                    break;
                }

                Thread.Sleep(500);
            }
        }
    }
}
