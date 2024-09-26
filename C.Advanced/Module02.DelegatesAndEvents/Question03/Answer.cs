namespace C.Advanced.Module02.DelegatesAndEvents.Question03;

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
internal static class Answer
{
    internal static void Run()
    {
        Console.WriteLine("Clock:");

        Clock clock = new Clock();

        clock.OnSecondElapsed += OnSecond;

        clock.Start();
    }

    private static void OnSecond(object sender, SecondElapsedEventArgs args)
    {
        Console.WriteLine(args.SecondElapsed);
    }
}

internal class Clock
{
    public event SecondsHandler? OnSecondElapsed;

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

internal class SecondElapsedEventArgs : EventArgs
{
    public long SecondElapsed { get; private set; }

    public SecondElapsedEventArgs(long secondElapsed)
    {
        SecondElapsed = secondElapsed;
    }
}

internal delegate void SecondsHandler(object sender, SecondElapsedEventArgs args);
