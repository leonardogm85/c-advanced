namespace C.Advanced.Module12.Question01
{
    /// <summary>
    /// <para>
    ///     Crie uma aplicação onde 5 threads inserem 10 números randômicos em uma lista, a qual é
    ///     compartilhada entre as threads. Quando todas terminarem de executar sua tarefa, imprima os
    ///     números na tela.
    /// </para>
    /// <para>
    ///     É importante evitar que problemas de concorrência no acesso à lista ocorram, de forma que
    ///     apenas uma thread deve poder acessar a lista por vez. Sincronize a menor porção de código
    ///     possível, permitindo que haja concorrência nos locais onde ela pode realmente ocorrer.
    /// </para>
    /// <para>
    ///     Utilize um CountdownEvent para fazer com que a thread principal fique bloqueada aguardando
    ///     todas as 5 threads realizarem suas tarefas antes da impressão dos números na tela.
    /// </para>
    /// </summary>
    internal static class Answer
    {
        internal static void Run()
        {
            List<string> list = new List<string>();

            CountdownEvent countdown = new CountdownEvent(5);

            for (int index = 0; index < 5; index++)
            {
                Thread thread = new Thread(() => Insert(list, countdown));
                thread.Name = $"Thread #{index}";
                thread.Start();
            }

            countdown.Wait();

            foreach (string item in list)
            {
                Console.WriteLine(item);
            }
        }

        private static void Insert(List<string> list, CountdownEvent countdown)
        {
            Random random = new Random();

            for (int index = 0; index < 10; index++)
            {
                lock (list)
                {
                    list.Add($"Number #{random.Next():00} - {Thread.CurrentThread.Name}");
                }
            }

            countdown.Signal();
        }
    }
}
