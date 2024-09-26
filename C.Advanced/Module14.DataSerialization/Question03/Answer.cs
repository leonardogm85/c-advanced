using System.Xml.Serialization;

namespace C.Advanced.Module14.DataSerialization.Question03;

/// <summary>
/// <para>
///     Crie uma aplicação que solicita ao usuário um diretório existente no sistema de arquivos e
///     armazena o seu nome em um objeto da classe Itens:
/// </para>
/// <code>
///     public class Itens
///     {
///         public string NomeDiretorio { get; set; }
///         public List<![CDATA[<Item>]]> Lista { get; set; }
///         public Itens()
///         {
///             Lista = new List<![CDATA[<Item>]]>();
///         }
///     }
/// </code>
/// <para>
///     Depois, obtenha a lista de arquivos e diretórios que estão localizados dentro do diretório
///     fornecido pelo usuário e armazene as informações do nome e tamanho em bytes (apenas para
///     arquivos) em objetos da classe Item:
/// </para>
/// <code>
///     public class Item
///     {
///         public string Nome { get; set; }
///         public bool IsArquivo { get; set; }
///         public long Tamanho { get; set; }
///     } 
/// </code>
/// <para>
///     Os objetos da classe Item devem ser adicionados à property Lista do objeto Item.
/// </para>
/// <para>
///     Por fim, serialize o objeto da classe Itens para o formato XML, de forma que a estrutura do
///     arquivo seja a seguinte:
/// </para>
/// <code>
/// <![CDATA[
///     <?xml version="1.0"?>
///     <ArquivosEDiretorios
///             xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
///             xmlns:xsd="http://www.w3.org/2001/XMLSchema"
///             NomeDiretorio="C:\temp">
///         <Item IsArquivo="true">
///             <Nome>Arquivo-1.txt</Nome>
///             <Tamanho>205</Tamanho>
///         </Item>
///         <Item IsArquivo="true">
///             <Nome>Arquivo-2.txt</Nome>
///             <Tamanho>1081</Tamanho>
///         </Item>
///         <Item IsArquivo = "false">
///             <Nome>Diretório-1</Nome>
///         </Item>
///         <Item IsArquivo="false">
///             <Nome>Diretório-2</Nome>
///         </Item>
///     </ArquivoEDiretorios>
/// ]]>
/// </code>
/// <para>
///     Dica: Você vai precisar usar os atributos [XmlRoot], [XmlElement] e [XmlAttribute] para
///     poder customizar a geração do arquivo XML.
/// </para>
/// </summary>
internal static class Answer
{
    internal static void Run()
    {
        Console.Write("Enter a directory name: ");
        string directoryName = Read();
        Console.WriteLine();

        Serialize(directoryName);
        Console.WriteLine("Serialization complete.");
    }

    private static string Read()
    {
        string? value = Console.ReadLine();

        if (value == null)
        {
            throw new ArgumentNullException("Could not convert the entered value to a valid input.");
        }

        if (!Directory.Exists(value))
        {
            throw new DirectoryNotFoundException("The directory not found.");
        }

        return value;
    }

    private static List<Item> GetFilesAndDirectories(string directoryName)
    {
        List<Item> list = new List<Item>();

        DirectoryInfo directoryInfo = new DirectoryInfo(directoryName);

        foreach (FileInfo file in directoryInfo.GetFiles())
        {
            Item item = new Item
            {
                Name = file.Name,
                IsFile = true,
                Length = file.Length
            };
            list.Add(item);
        }

        foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
        {
            Item item = new Item
            {
                Name = directory.Name,
                IsFile = false
            };
            list.Add(item);
        }

        return list;
    }

    private static void Serialize(string directoryName)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(directoryName);

        List<Item> list = GetFilesAndDirectories(directoryInfo.FullName);

        Items items = new Items
        {
            DirectoryName = directoryInfo.FullName,
            List = list
        };

        XmlSerializer xml = new XmlSerializer(typeof(Items));

        using (FileStream stream = File.OpenWrite($"{directoryInfo.Name}.xml"))
        {
            xml.Serialize(stream, items);
        }
    }
}

[XmlRoot(ElementName = "FilesAndDirectories")]
public class Items
{
    [XmlAttribute]
    public string DirectoryName { get; set; } = null!;

    [XmlElement(ElementName = "Item")]
    public List<Item> List { get; set; } = null!;
}

[XmlRoot]
public class Item
{
    [XmlElement]
    public string Name { get; set; } = null!;

    [XmlAttribute]
    public bool IsFile { get; set; }

    [XmlElement]
    public long? Length { get; set; }
}
