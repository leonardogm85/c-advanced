namespace C.Advanced.Module12.Question03
{
    /// <summary>
    /// <para>
    ///     Na ciência da computação, existem alguns exemplo clássicos de problemas relacionados à
    ///     programação concorrente. Um deles é o problema do "leitor e escritor".
    /// </para>
    /// <para>
    ///     Neste problema, existe uma série de leitores e escritores que agem sobre um mesmo conjunto
    ///     de dados. Para evitar problemas de concorrência, os leitores podem ler os dados ao mesmo
    ///     tempo, enquanto cada escritor deve ter acesso exclusivo aos dados no momento da escrita
    ///     (não pode haver outro escritor escrevendo ou outro leitor lendo durante este processo). Um
    ///     cenário prático deste problema é o acesso a informações em uma tabela de um banco de
    ///     dados.
    /// </para>
    /// <para>
    ///     Crie uma aplicação que reproduz este cenário, onde múltiplas threads (leitoras e escritoras)
    ///     ficam executando em loop simultaneamente, lendo e escrevendo dados de acordo com as
    ///     regras mencionadas acima.
    /// </para>
    /// <para>
    ///     Para saber mais sobre este problema, você pode consultar os seguintes links (na internet
    ///     existe bastante material a respeito também):
    /// </para>
    /// <list type="bullet">
    ///     <item>
    ///         http://ces33.blogspot.com.br/2009/05/o-problema-do-leitores-e-escritores-com.html
    ///     </item>
    ///     <item>
    ///         http://en.wikipedia.org/wiki/Readers-writers_problem
    ///     </item>
    /// </list>
    /// </summary>
    internal static class Answer
    {
        internal static void Run()
        {
            var countReaders = 10;
            var countWriters = 5;

            var mutexWriter = new SemaphoreSlim(1);

            var readers = new Reader[countReaders];
            var writers = new Writer[countWriters];

            for (var index = 0; index < readers.Length; index++)
            {
                readers[index] = new Reader(
                    index,
                    mutexWriter);
            }

            for (var index = 0; index < writers.Length; index++)
            {
                writers[index] = new Writer(
                    index,
                    mutexWriter);
            }
        }
    }

    internal class Reader
    {
        private static readonly object _sync = new();
        private static int _activeReaders;

        private readonly SemaphoreSlim _mutexWriter;

        public Reader(int id, SemaphoreSlim mutexWriter)
        {
            Id = id;
            _mutexWriter = mutexWriter;
            new Thread(Init).Start();
        }

        public int Id { get; private set; }

        private void Init()
        {
            Thread.CurrentThread.Name = $"Reader {Id}";

            while (true)
            {
                lock (_sync)
                {
                    _activeReaders++;

                    if (_activeReaders == 1)
                    {
                        _mutexWriter.Wait();
                    }
                }

                Read();

                lock (_sync)
                {
                    _activeReaders--;

                    if (_activeReaders == 0)
                    {
                        _mutexWriter.Release();
                    }
                }

                UseData();
            }
        }

        private void Read()
        {
            Console.WriteLine(
                "Reader {0} reading.",
                Id);
            Thread.Sleep(Random.Shared.Next(2000));
        }

        private void UseData()
        {
            Console.WriteLine(
                "Reader {0} using read data.",
                Id);
            Thread.Sleep(Random.Shared.Next(2000));
        }
    }

    internal class Writer
    {
        private readonly SemaphoreSlim _mutexWriter;

        public Writer(int id, SemaphoreSlim mutexWriter)
        {
            Id = id;
            _mutexWriter = mutexWriter;
            new Thread(Init).Start();
        }

        public int Id { get; private set; }

        private void Init()
        {
            Thread.CurrentThread.Name = $"Writer {Id}";

            while (true)
            {
                Decide();

                try
                {
                    _mutexWriter.Wait();
                    Write();
                }
                finally
                {
                    _mutexWriter.Release();
                }
            }
        }

        private void Decide()
        {
            Console.WriteLine(
                "Writer {0} deciding what to write.",
                Id);
            Thread.Sleep(Random.Shared.Next(2000));
        }

        private void Write()
        {
            Console.WriteLine(
                "Writer {0} writing.",
                Id);
            Thread.Sleep(Random.Shared.Next(2000));
        }
    }
}
