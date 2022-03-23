using System.Xml.Linq;

namespace C.Avancado.Modulo07.Exercicio2
{
    /// <summary>
    /// <para>
    ///     Crie um arquivo XML com o conteúdo abaixo, que representa uma lista de cantores famosos
    ///     juntamente com sua data de nascimento:
    /// </para>
    /// <code>
    ///     <![CDATA[
    ///     <Cantores>
    ///         <Cantor>
    ///             <Nome>Elvis Presley</Nome>
    ///             <DataNascimento>1935-08-01</DataNascimento>
    ///         </Cantor>
    ///         <Cantor>
    ///             <Nome>John Lennon</Nome>
    ///             <DataNascimento>1940-10-09</DataNascimento>
    ///         </Cantor>
    ///         <Cantor>
    ///             <Nome>Stevie Wonder</Nome>
    ///             <DataNascimento>1950-05-13</DataNascimento>
    ///         </Cantor>
    ///         <Cantor>
    ///             <Nome>James Brown</Nome>
    ///             <DataNascimento>1933-05-03</DataNascimento>
    ///         </Cantor>
    ///         <Cantor>
    ///             <Nome>Bob Dylan</Nome>
    ///             <DataNascimento>1941-05-24</DataNascimento>
    ///         </Cantor>
    ///     </Cantores>
    ///     ]]>
    /// </code>
    /// <para>
    ///     Agora crie expressões LINQ para extrair dados deste documento XML. Os dados que devem
    ///     ser retornados são:
    /// </para>
    /// <list type="number">
    ///     <item>
    ///         Lista de todos os nomes dos cantores, ordenados alfabeticamente.
    ///     </item>
    ///     <item>
    ///         Lista de objetos de uma classe anônima, mostrando o nome e a data de nascimento de
    ///         todos os cantores que nasceram no mês de maio.
    ///     </item>
    /// </list>
    /// <para>
    ///     Dica 1: Use o método DateTime.Parse() para converter a data de nascimento do XML, que
    ///     está na forma de uma string, em um objeto do tipo DateTime. A property Month pode ser
    ///     usada neste objeto para identificar o mês representado pela data.
    /// </para>
    /// <para>
    ///     Dica 2: O uso do let na expressão LINQ pode auxiliar, reduzindo a quantidade de código a ser
    ///     escrito.
    /// </para>
    /// </summary>
    public static class Resposta
    {
        public static void Executar()
        {
            XElement xml = CriarXml();
            Expressao1(xml);
            Expressao2(xml);
        }

        public static void Expressao1(XElement xml)
        {
            Console.WriteLine("1) Lista de todos os nomes dos cantores, ordenados alfabeticamente.");
            var nomes = from cantor in xml.Elements("Cantor")
                        let nome = cantor.Element("Nome")!.Value
                        orderby nome ascending
                        select nome;
            nomes.ToList().ForEach(Console.WriteLine);
            Console.WriteLine();
        }

        public static void Expressao2(XElement xml)
        {
            Console.WriteLine("2) Lista de objetos de uma classe anônima, mostrando o nome e a data de nascimento de todos os cantores que nasceram no mês de maio.");
            var cantores = from cantor in xml.Elements("Cantor")
                           let nome = cantor.Element("Nome")!.Value
                           let dataNascimento = DateTime.Parse(cantor.Element("DataNascimento")!.Value)
                           where dataNascimento.Month == 5
                           select new
                           {
                               Nome = nome,
                               DataNascimento = dataNascimento
                           };
            cantores.ToList().ForEach(Console.WriteLine);
            Console.WriteLine();
        }

        public static XElement CriarXml()
        {
            return new XElement("Cantores",
                new XElement("Cantor",
                    new XElement("Nome", "Elvis Presley"),
                    new XElement("DataNascimento", "1935-08-01")
                ),
                new XElement("Cantor",
                    new XElement("Nome", "John Lennon"),
                    new XElement("DataNascimento", "1940-10-09")
                ),
                new XElement("Cantor",
                    new XElement("Nome", "Stevie Wonder"),
                    new XElement("DataNascimento", "1950-05-13")
                ),
                new XElement("Cantor",
                    new XElement("Nome", "James Brown"),
                    new XElement("DataNascimento", "1933-05-03")
                ),
                new XElement("Cantor",
                    new XElement("Nome", "Bob Dylan"),
                    new XElement("DataNascimento", "1941-05-24")
                )
            );
        }
    }
}
