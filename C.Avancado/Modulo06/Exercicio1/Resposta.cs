namespace C.Avancado.Modulo06.Exercicio1
{
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
    public static class Resposta
    {
        public static void Executar()
        {
            List<ItemMercado> itens = CriarItens();
            Expressao1(itens);
            Expressao2(itens);
            Expressao3(itens);
            Expressao4(itens);
            Expressao5(itens);
        }

        public static void Expressao5(List<ItemMercado> itens)
        {
            Console.WriteLine("5) Retorne cada um dos tipos associado com o preço máximo, preço mínimo e média de preço de cada tipo.");
            var lista = from item in itens
                        group item by item.Tipo into grupo
                        select new
                        {
                            Tipo = grupo.Key,
                            Maximo = grupo.Max(g => g.Preco),
                            Minimo = grupo.Min(g => g.Preco),
                            Media = grupo.Average(g => g.Preco)
                        };
            lista.ToList().ForEach(Console.WriteLine);
            Console.WriteLine();
        }

        public static void Expressao4(List<ItemMercado> itens)
        {
            Console.WriteLine("4) Retorne cada um dos tipos associado com a quantidade de itens de cada tipo.");
            var lista = from item in itens
                        group item by item.Tipo into grupo
                        select new
                        {
                            Tipo = grupo.Key,
                            Quantidade = grupo.Count()
                        };
            lista.ToList().ForEach(Console.WriteLine);
            Console.WriteLine();
        }

        public static void Expressao3(List<ItemMercado> itens)
        {
            Console.WriteLine("3) Retorne uma lista de itens cujo tipo seja Comida ou Bebida. A ordenação deve ser feita por nome em ordem alfabética.");
            var lista = from item in itens
                        where item.Tipo == Tipo.Comida || item.Tipo == Tipo.Bebida
                        orderby item.Nome ascending
                        select item;
            lista.ToList().ForEach(Console.WriteLine);
            Console.WriteLine();
        }

        public static void Expressao2(List<ItemMercado> itens)
        {
            Console.WriteLine("2) Retorne uma lista de itens cujo preço seja maior ou igual a R$ 5,00. A ordenação deve ser feita por ordem crescente de preço.");
            var lista = from item in itens
                        where item.Preco >= 5
                        orderby item.Preco ascending
                        select item;
            lista.ToList().ForEach(Console.WriteLine);
            Console.WriteLine();
        }

        public static void Expressao1(List<ItemMercado> itens)
        {
            Console.WriteLine("1) Retorne uma lista de itens do tipo Higiene ordenados por ordem decrescente de preço.");
            var lista = from item in itens
                        where item.Tipo == Tipo.Higiene
                        orderby item.Preco descending
                        select item;
            lista.ToList().ForEach(Console.WriteLine);
            Console.WriteLine();
        }

        public static List<ItemMercado> CriarItens()
        {
            return new List<ItemMercado>
            {
                new ItemMercado("Arroz", Tipo.Comida, 3.9),
                new ItemMercado("Azeite", Tipo.Comida, 2.5),
                new ItemMercado("Macarrão", Tipo.Comida, 3.9),
                new ItemMercado("Cerveja", Tipo.Bebida, 22.9),
                new ItemMercado("Refrigerante", Tipo.Bebida, 5.5),
                new ItemMercado("Shampoo", Tipo.Higiene, 7),
                new ItemMercado("Sabonete", Tipo.Higiene, 2.4),
                new ItemMercado("Cotonete", Tipo.Higiene, 5.7),
                new ItemMercado("Sabão em pó", Tipo.Limpeza, 8.2),
                new ItemMercado("Detergente", Tipo.Limpeza, 2.6),
                new ItemMercado("Amaciante", Tipo.Limpeza, 6.4)
            };
        }
    }

    public class ItemMercado
    {
        public ItemMercado(string nome, Tipo tipo, double preco)
        {
            Nome = nome;
            Tipo = tipo;
            Preco = preco;
        }

        public string Nome { get; private set; }
        public Tipo Tipo { get; private set; }
        public double Preco { get; private set; }

        public override string ToString()
        {
            return $"{Nome,15} - {Tipo,-7}: {Preco:c2}";
        }
    }

    public enum Tipo
    {
        Comida,
        Bebida,
        Higiene,
        Limpeza
    }
}
