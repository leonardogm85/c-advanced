namespace C.Advanced.Module06.LinqLanguageIntegratedQuery.Question01;

/// <summary>
/// <para>
///     Crie uma classe ItemMercado, cujos objetos correspondem a itens que você precisa comprar
///     no supermercado. A classe deve possuir as properties Nome (string), Tipo (Tipo) e Preco
///     (double). Tipo deve ser um enum, que pode ter os valores: Comida, Bebida, Higiene e
///     Limpeza.
/// </para>
/// <para>
///     Na sequência, crie uma lista de objetos do tipo ItemMercado, de acordo com a tabela abaixo:
/// </para>
/// <list type="bullet">
///     <item>Nome: Arroz; Tipo: Comida; Preço: R$ 3,90</item>
///     <item>Nome: Azeite; Tipo: Comida; Preço: R$ 2,50</item>
///     <item>Nome: Macarrão; Tipo: Comida; Preço: R$ 3,90</item>
///     <item>Nome: Cerveja; Tipo: Bebida; Preço: R$ 22,90</item>
///     <item>Nome: Refrigerante; Tipo: Bebida; Preço: R$ 5,50</item>
///     <item>Nome: Shampoo; Tipo: Higiene; Preço: R$ 7,00</item>
///     <item>Nome: Sabonete; Tipo: Higiene; Preço: R$ 2,40</item>
///     <item>Nome: Cotonete; Tipo: Higiene; Preço: R$ 5,70</item>
///     <item>Nome: Sabão em pó; Tipo: Limpeza; Preço: R$ 8,20</item>
///     <item>Nome: Detergente; Tipo: Limpeza; Preço: R$ 2,60</item>
///     <item>Nome: Amaciante; Tipo: Limpeza; Preço: R$ 6,40</item>
/// </list>
/// <para>
///     Com base nos dados desta lista, crie uma série de expressões LINQ para retornar algumas
///     informações:
/// </para>
/// <list type="number">
/// <item>Retorne uma lista de itens do tipo Higiene ordenados por ordem decrescente de preço.</item>
/// <item>
///     Retorne uma lista de itens cujo preço seja maior ou igual a R$ 5,00. A ordenação deve ser
///     feita por ordem crescente de preço.
/// </item>
/// <item>
///     Retorne uma lista de itens cujo tipo seja Comida ou Bebida. A ordenação deve ser feita por
///     nome em ordem alfabética.
/// </item>
/// <item>Retorne cada um dos tipos associado com a quantidade de itens de cada tipo.</item>
/// <item>
///     Retorne cada um dos tipos associado com o preço máximo, preço mínimo e média de
///     preço de cada tipo.
/// </item>
/// </list>
/// </summary>
internal static class Answer
{
    internal static void Run()
    {
        List<MarketItem> items = CreateItems();
        Expression1(items);
        Expression2(items);
        Expression3(items);
        Expression4(items);
        Expression5(items);
    }

    private static List<MarketItem> CreateItems()
    {
        return new List<MarketItem>
        {
            new MarketItem("Rice", Type.Food, 3.9),
            new MarketItem("Olive oil", Type.Food, 2.5),
            new MarketItem("Noodle", Type.Food, 3.9),
            new MarketItem("Beer", Type.Drink, 22.9),
            new MarketItem("Soda", Type.Drink, 5.5),
            new MarketItem("Shampoo", Type.Hygiene, 7),
            new MarketItem("Soap", Type.Hygiene, 2.4),
            new MarketItem("Cotton swab", Type.Hygiene, 5.7),
            new MarketItem("Washing powder", Type.Cleaning, 8.2),
            new MarketItem("Detergent", Type.Cleaning, 2.6),
            new MarketItem("Fabric softener", Type.Cleaning, 6.4)
        };
    }

    private static void Expression1(List<MarketItem> items)
    {
        Console.WriteLine("1) Returns a list of hygiene type items sorted in descending order of price.");
        var list = from item in items
                   where item.Type == Type.Hygiene
                   orderby item.Price descending
                   select item;
        list.ToList().ForEach(Console.WriteLine);
        Console.WriteLine();
    }

    private static void Expression2(List<MarketItem> items)
    {
        Console.WriteLine("2) Returns a list of items whose price is greater than or equal to $ 5.00. Sorting must be done in ascending order of price.");
        var list = from item in items
                   where item.Price >= 5
                   orderby item.Price ascending
                   select item;
        list.ToList().ForEach(Console.WriteLine);
        Console.WriteLine();
    }

    private static void Expression3(List<MarketItem> items)
    {
        Console.WriteLine("3) Returns a list of items whose type is food or drink. Sorting must be done by name in alphabetical order.");
        var list = from item in items
                   where item.Type == Type.Food || item.Type == Type.Drink
                   orderby item.Name ascending
                   select item;
        list.ToList().ForEach(Console.WriteLine);
        Console.WriteLine();
    }

    private static void Expression4(List<MarketItem> items)
    {
        Console.WriteLine("4) Returns each of the types associated with the amount of items of each type.");
        var list = from item in items
                   group item by item.Type into @group
                   select new
                   {
                       Type = @group.Key,
                       Count = @group.Count()
                   };
        list.ToList().ForEach(Console.WriteLine);
        Console.WriteLine();
    }

    private static void Expression5(List<MarketItem> items)
    {
        Console.WriteLine("5) Returns each of the types associated with the maximum price, minimum price and average price of each type.");
        var list = from item in items
                   group item by item.Type into @group
                   select new
                   {
                       Type = @group.Key,
                       Max = @group.Max(g => g.Price),
                       Min = @group.Min(g => g.Price),
                       Average = @group.Average(g => g.Price)
                   };
        list.ToList().ForEach(Console.WriteLine);
        Console.WriteLine();
    }
}

internal class MarketItem
{
    public MarketItem(string name, Type type, double price)
    {
        Name = name;
        Type = type;
        Price = price;
    }

    public string Name { get; private set; }
    public Type Type { get; private set; }
    public double Price { get; private set; }

    public override string ToString()
    {
        return $"{Name,15} - {Type,-8}: {Price:c2}";
    }
}

internal enum Type
{
    Food,
    Drink,
    Hygiene,
    Cleaning
}
