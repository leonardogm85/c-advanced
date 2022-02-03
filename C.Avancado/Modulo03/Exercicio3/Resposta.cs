namespace C.Avancado.Modulo03.Exercicio3
{
    /// <summary>
    /// <para>
    ///     Crie um método chamado Count() que recebe dois parâmetros: uma lista do tipo int e um
    ///     delegate do tipo Predicate<int>. A tarefa deste método é retornar a quantidade de
    ///     elementos presentes na lista que atendam determinada condição.
    /// </para>
    /// <para>
    ///     Dica: Cada um dos elementos da lista deve ser submetido à execução do método referenciado
    ///     pelo Predicate<int>. Caso o retorno do método seja verdadeiro, o elemento é considerado
    ///     na contagem; caso contrário, não é considerado.
    /// </para>
    /// <para>
    ///     Para exercitar, crie uma lista com alguns números e chame o método Count() para contar
    ///     quantos elementos nesta lista são maiores ou iguais a 5. Use uma expressão lambda para
    ///     definir esta regra.
    /// </para>
    /// </summary>
    public static class Resposta
    {
        public static void Executar()
        {
            List<int> list = new List<int>
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9
            };

            int count = Count(list, item => item >= 5);

            Console.WriteLine(count);
        }

        public static int Count(List<int> list, Predicate<int> predicate)
        {
            int count = 0;

            list.ForEach(item =>
            {
                if (predicate(item))
                {
                    count++;
                }
            });

            return count;
        }
    }
}
