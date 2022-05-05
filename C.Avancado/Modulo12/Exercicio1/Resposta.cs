namespace C.Avancado.Modulo12.Exercicio1
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
    public static class Resposta
    {
        public static void Executar()
        {
            List<string> lista = new List<string>();

            CountdownEvent countdown = new CountdownEvent(5);

            for (int indice = 0; indice < 5; indice++)
            {
                Thread thread = new Thread(() => Inserir(lista, countdown));
                thread.Name = $"Thread #{indice}";
                thread.Start();
            }

            countdown.Wait();

            foreach (string item in lista)
            {
                Console.WriteLine(item);
            }
        }

        private static void Inserir(List<string> lista, CountdownEvent countdown)
        {
            Random random = new Random();

            for (int index = 0; index < 10; index++)
            {
                lock (lista)
                {
                    lista.Add($"Number #{random.Next():00} - {Thread.CurrentThread.Name}");
                }
            }

            countdown.Signal();
        }
    }
}
