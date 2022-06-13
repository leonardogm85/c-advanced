using System.Xml.Linq;

namespace C.Advanced.Module07.Question02
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
    internal static class Answer
    {
        internal static void Run()
        {
            XElement xml = CreateXml();
            Expression1(xml);
            Expression2(xml);
        }

        private static void Expression1(XElement xml)
        {
            Console.WriteLine("1) List of all singers' names in alphabetical order.");
            var names = from singer in xml.Elements("Singer")
                        let name = singer.Element("Name")!.Value
                        orderby name ascending
                        select name;
            names.ToList().ForEach(Console.WriteLine);
            Console.WriteLine();
        }

        private static void Expression2(XElement xml)
        {
            Console.WriteLine("2) List of objects of an anonymous class with the name and birth date of all singers who were born in the month of May.");
            var singers = from singer in xml.Elements("Singer")
                          let name = singer.Element("Name")!.Value
                          let birthDate = DateTime.Parse(singer.Element("BirthDate")!.Value)
                          where birthDate.Month == 5
                          select new
                          {
                              Name = name,
                              BirthDate = birthDate
                          };
            singers.ToList().ForEach(singer =>
            {
                Console.WriteLine(
                    "Name: {0,-14} and birth date: {1:d}",
                    singer.Name,
                    singer.BirthDate);
            });
            Console.WriteLine();
        }

        private static XElement CreateXml()
        {
            return new XElement("Singers",
                new XElement("Singer",
                    new XElement("Name", "Elvis Presley"),
                    new XElement("BirthDate", "1935-08-01")
                ),
                new XElement("Singer",
                    new XElement("Name", "John Lennon"),
                    new XElement("BirthDate", "1940-10-09")
                ),
                new XElement("Singer",
                    new XElement("Name", "Stevie Wonder"),
                    new XElement("BirthDate", "1950-05-13")
                ),
                new XElement("Singer",
                    new XElement("Name", "James Brown"),
                    new XElement("BirthDate", "1933-05-03")
                ),
                new XElement("Singer",
                    new XElement("Name", "Bob Dylan"),
                    new XElement("BirthDate", "1941-05-24")
                )
            );
        }
    }
}
