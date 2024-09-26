namespace C.Advanced.Module02.DelegatesAndEvents.Question01;

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
internal static class Answer
{
    internal static void Run()
    {
        List<int> list = new List<int>();

        for (int i = 0; i <= 10; i++)
        {
            list.Add(i);
        }

        List<int> newList = list;
        PrintList("All numbers", newList);

        newList = FilterList(list, FilterGreaterThan5);
        PrintList("Only numbers greater than 5", newList);

        newList = FilterList(list, FilterOdd);
        PrintList("Only odd numbers", newList);
    }

    private static bool FilterGreaterThan5(int item)
    {
        if (item > 5)
        {
            return true;
        }

        return false;
    }

    private static bool FilterOdd(int item)
    {
        if (item % 2 == 0)
        {
            return false;
        }

        return true;
    }

    private static List<int> FilterList(List<int> list, Filter filter)
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

    private static void PrintList(string description, List<int> list)
    {
        string content = string.Join(", ", list);
        Console.WriteLine("{0}: {1}", description, content);
    }

    private delegate bool Filter(int number);
}
