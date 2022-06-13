namespace C.Advanced.Module06.Question02
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
    internal static class Answer
    {
        internal static void Run()
        {
            Console.WriteLine("Returns a list of persons along with the names of paternal and maternal grandparents.");
            var list = from person in CreatePersons()
                       select new
                       {
                           Name = person.Name,

                           PaternalGrandfather = person.Father?.Father?.Name ?? "-",
                           PaternalGrandmother = person.Father?.Mother?.Name ?? "-",

                           MaternalGrandfather = person.Mother?.Father?.Name ?? "-",
                           MaternalGrandmother = person.Mother?.Mother?.Name ?? "-"
                       };
            list.ToList().ForEach(person =>
            {
                Console.WriteLine(
                    "Name: {0,-7}, paternal grandparents {1,-7} and {2,-7}, maternal grandparents {3,-7} and {4,-7}.",
                    person.Name,
                    person.PaternalGrandfather,
                    person.PaternalGrandmother,
                    person.MaternalGrandfather,
                    person.MaternalGrandmother);
            });
        }

        private static List<Person> CreatePersons()
        {
            Person angelo = new Person("Ângelo");
            Person mariana = new Person("Mariana");

            Person luis = new Person("Luis");
            Person rafaela = new Person("Rafaela");

            Person andre = new Person("André", angelo, mariana);
            Person luciana = new Person("Luciana", luis, rafaela);

            Person daniel = new Person("Daniel", andre, luciana);

            return new List<Person>
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

    internal class Person
    {
        public Person(string name, Person? father = null, Person? mother = null)
        {
            Name = name;
            Father = father;
            Mother = mother;
        }

        public string Name { get; private set; }
        public Person? Father { get; private set; }
        public Person? Mother { get; private set; }
    }
}
