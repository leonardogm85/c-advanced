namespace C.Avancado.Modulo02.Exercicio1
{
    /// <summary>
    /// <para>
    ///     Crie um delegate chamado Filter, que pode referenciar métodos que recebem um int como
    ///     parâmetro e retornam um bool.
    /// </para>
    /// <para>
    ///     Depois, crie um método chamado FilterList(), que recebe como parâmetro uma lista de
    ///     números inteiros e um delegate Filter. Este método deve invocar o delegate Filter em cada
    ///     um dos elementos e, no final, retornar uma nova lista. Esta nova lista conterá apenas os
    ///     elementos cujo retorno do delegate for true.
    /// </para>
    /// <para>
    ///     Para testar o método FilterList(), crie uma lista de números de 0 a 10. Depois crie dois
    ///     métodos que poderão ser referenciados pelo delegate. O primeiro método é o
    ///     FilterGreatedThan5(), que deve retornar na nova lista apenas os números maiores do que
    ///     5. O segundo método é o FilterOdd(), que deve retornar na nova lista apenas os números
    ///     ímpares.
    /// </para>
    /// <para>
    ///     Dica: Números ímpares são aqueles cujo resto da divisão por 2 (operador "%") é diferente de
    ///     0.
    /// </para>
    /// </summary>
    public static class Resposta
    {
        public static void Executar()
        {
            List<int> list = new List<int>();

            for (int i = 0; i <= 10; i++)
            {
                list.Add(i);
            }

            List<int> newList;

            newList = FilterList(list, FilterGreatedThan5);
            PrintList(newList);

            newList = FilterList(list, FilterOdd);
            PrintList(newList);
        }

        public static bool FilterGreatedThan5(int item)
        {
            if (item > 5)
            {
                return true;
            }

            return false;
        }

        public static bool FilterOdd(int item)
        {
            if (item % 2 == 0)
            {
                return false;
            }

            return true;
        }

        public static List<int> FilterList(List<int> list, Filter filter)
        {
            List<int> newList = new List<int>();

            foreach (int item in list)
            {
                if (filter(item))
                {
                    newList.Add(item);
                }
            }

            return newList;
        }

        public static void PrintList(List<int> list)
        {
            string text = string.Join(", ", list);
            Console.WriteLine("Lista filtrada: {0}", text);
        }
    }

    public delegate bool Filter(int number);
}
