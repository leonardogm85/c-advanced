namespace C.Avancado.Modulo02.Exercicio3
{
    /// <summary>
    /// <para>
    ///     Implemente a mesma aplicação do Exercício 2, mas utilize um event para notificar os
    ///     interessados a respeito da passagem do tempo.
    /// </para>
    /// <para>
    ///     Use o padrão de eventos definido pela Microsoft, onde o método chamado deve receber dois
    ///     parâmetros: um object, que identifica o gerador do evento; e um objeto da classe
    ///     SecondElapsedEventArgs, que será criada por você e conterá as informações do evento
    ///     (neste caso, o número de segundos passados).
    /// </para>
    /// </summary>
    public static class Resposta
    {
        public static void Executar()
        {
            Clock clock = new Clock();

            clock.OnSecondElapsed += OnSecond;

            clock.Start();
        }

        public static void OnSecond(object sender, SecondElapsedEventArgs args)
        {
            Console.WriteLine(args.SecondElapsed);
        }
    }

    public class Clock
    {
        public event SecondsHandler OnSecondElapsed;

        public void Start()
        {
            long seconds = 0;

            while (true)
            {
                Thread.Sleep(1000);

                seconds++;

                if (OnSecondElapsed == null)
                {
                    break;
                }

                SecondElapsedEventArgs args = new SecondElapsedEventArgs(seconds);

                OnSecondElapsed(this, args);
            }
        }
    }

    public class SecondElapsedEventArgs : EventArgs
    {
        public long SecondElapsed { get; private set; }

        public SecondElapsedEventArgs(long secondElapsed)
        {
            SecondElapsed = secondElapsed;
        }
    }

    public delegate void SecondsHandler(object sender, SecondElapsedEventArgs args);
}
