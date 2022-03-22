namespace C.Avancado.Modulo06.Exercicio2
{
    /// <summary>
    /// <para>
    ///     Crie uma classe Pessoa da seguinte forma:
    /// </para>
    /// <code>
    ///     class Pessoa 
    ///     { 
    ///         public string Nome { get; set; }
    ///         public Pessoa Pai { get; set; }
    ///         public Pessoa Mae { get; set; }
    ///     }
    /// </code>
    /// <para>
    ///     As properties Pai e Mae representam o pai e a mãe desta pessoa, respectivamente. Caso seus
    ///     pais não forem definidos, estas properties possuem o valor null.
    /// </para>
    /// <para>
    ///     Depois crie uma lista de objetos do tipo Pessoa para representar a árvore genealógica abaixo:
    /// </para>
    /// <list type="bullet">
    ///     <item>Daniel: Pai (André) e Mãe (Luciana)</item>
    ///     <item>André: Pai (Ângelo) e Mãe (Mariana)</item>
    ///     <item>Luciana: Pai (Luis) e Mãe (Rafaela)</item>
    ///     <item>Ângelo: Pai (null) e Mãe (null)</item>
    ///     <item>Mariana: Pai (null) e Mãe (null)</item>
    ///     <item>Luis: Pai (null) e Mãe (null)</item>
    ///     <item>Rafaela: Pai (null) e Mãe (null)</item>
    /// </list>
    /// <para>
    ///     Note que a seta sempre aponta para o pai ou para a mãe. Por exemplo, os pais de Daniel são
    ///     André e Luciana.
    /// </para>
    /// <para>
    ///     Crie uma expressão LINQ que retorne o nome das pessoas juntamente com os nomes dos avós
    ///     paternos e maternos. Caso o nome dos avós não seja definido, um "-" deve ser exibido no
    ///     lugar.
    /// </para>
    /// </summary>
    public static class Resposta
    {
        public static void Executar()
        {
            var lista = from pessoa in CriarPessoas()
                        select new
                        {
                            Nome = pessoa.Nome,

                            AvoPaterno1 = pessoa.Pai?.Pai?.Nome ?? "-",
                            AvoPaterno2 = pessoa.Pai?.Mae?.Nome ?? "-",

                            AvoMaterno1 = pessoa.Mae?.Pai?.Nome ?? "-",
                            AvoMaterno2 = pessoa.Mae?.Mae?.Nome ?? "-"
                        };
            lista.ToList().ForEach(Console.WriteLine);
        }

        public static List<Pessoa> CriarPessoas()
        {
            Pessoa angelo = new Pessoa("Ângelo");
            Pessoa mariana = new Pessoa("Mariana");

            Pessoa luis = new Pessoa("Luis");
            Pessoa rafaela = new Pessoa("Rafaela");

            Pessoa andre = new Pessoa("André", angelo, mariana);
            Pessoa luciana = new Pessoa("Luciana", luis, rafaela);

            Pessoa daniel = new Pessoa("Daniel", andre, luciana);

            return new List<Pessoa>
            {
                daniel,
                andre,
                luciana,
                angelo,
                mariana,
                luis,
                rafaela
            };
        }
    }

    public class Pessoa
    {
        public Pessoa(string nome, Pessoa? pai = null, Pessoa? mae = null)
        {
            Nome = nome;
            Pai = pai;
            Mae = mae;
        }

        public string Nome { get; private set; }
        public Pessoa? Pai { get; private set; }
        public Pessoa? Mae { get; private set; }
    }
}
