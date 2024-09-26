namespace C.Advanced.Module02.DelegatesAndEvents.Question02;

/// <summary>
/// <para>
///     Crie uma classe chamada Clock, que irá representar um cronômetro. Esta classe possui o
///     método Start(), que inicia um loop infinito. A cada 1 segundo passado, este método chama
///     métodos de callback registrados informando quantos segundos se passaram desde o início da
///     contagem(a contagem inicia quando Start() é chamado).
/// </para>
/// <para>
///     Para implementar este comportamento, você precisa de um delegate, que você chamará de
///     SecondsHandler. Ele referencia métodos que recebem um parâmetro do tipo long (número
///     de segundos) e retornam void.
/// </para>
/// <para>
///     Crie um método chamado OnSecond() na sua aplicação, que é chamado via delegate pelo
///     objeto Clock a cada segundo. Imprima na tela o valor do segundo fornecido.
/// </para>
/// <para>
///     Dica: Utilize a chamada Thread.Sleep(1000) para fazer a aplicação pausar por 1 segundo. A
///     classe Thread pertence ao namespace System.Threading.
/// </para>
/// </summary>
internal static class Answer
{
    internal static void Run()
    {
        Console.WriteLine("Clock:");

        Clock clock = new Clock();

        clock.RegisterCallback(OnSecond);

        clock.Start();
    }

    private static void OnSecond(long seconds)
    {
        Console.WriteLine(seconds);
    }
}

internal class Clock
{
    private SecondsHandler? callback;

    public void Start()
    {
        long seconds = 0;

        while (true)
        {
            Thread.Sleep(1000);

            seconds++;

            if (callback == null)
            {
                break;
            }

            callback(seconds);
        }
    }

    public void RegisterCallback(SecondsHandler handler)
    {
        callback += handler;
    }
}

internal delegate void SecondsHandler(long seconds);
