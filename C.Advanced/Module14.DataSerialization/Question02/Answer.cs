using System.Text.Json;
using System.Xml.Serialization;

namespace C.Advanced.Module14.DataSerialization.Question02;

/// <summary>
/// <para>
///     Aproveitando os arquivos gerados como resultado do Exercício 1, crie agora uma aplicação que
///     faça o caminho inverso.
/// </para>
/// <para>
///     Esta aplicação deve solicitar ao usuário o nome de um arquivo para leitura e o formato (binário
///     ou XML). Na sequência, deve ler as informações contidas no arquivo fornecido (a lista de
///     números) e mostrá-las na tela.
/// </para>
/// </summary>
internal static class Answer
{
    internal static void Run()
    {
        Console.Write("Enter the file name: ");
        string fileName = ReadTheFileName();
        Console.WriteLine();

        WriteTheFileFormats();
        Console.Write("Enter the file format: ");
        FileFormat fileFormat = ReadTheFileFormat();
        Console.WriteLine();

        Deserialize(fileName, fileFormat);
        Console.WriteLine("Deserialization complete.");
    }

    private static string ReadTheFileName()
    {
        string? value = Console.ReadLine();

        if (value == null)
        {
            throw new ArgumentNullException("Could not convert the entered value to a valid input.");
        }

        return value;
    }

    private static FileFormat ReadTheFileFormat()
    {
        if (Enum.TryParse(Console.ReadLine(), out FileFormat value) && Enum.IsDefined(value))
        {
            return value;
        }

        throw new FormatException("Could not convert the entered value to a valid number.");
    }

    private static void WriteTheFileFormats()
    {
        Console.WriteLine("File formats:");

        FileFormat[] fileFormats = Enum.GetValues<FileFormat>();

        foreach (FileFormat fileFormat in fileFormats)
        {
            Console.WriteLine("{0}. {1}", (int)fileFormat, fileFormat);
        }
    }

    private static void Deserialize(string fileName, FileFormat fileFormat)
    {
        string path = $"{fileName}.{fileFormat.ToString().ToLower()}";

        if (!File.Exists(path))
        {
            throw new FileNotFoundException("File not found.");
        }

        int[] numbers = new int[0];

        using (FileStream stream = File.OpenRead(path))
        {
            switch (fileFormat)
            {
                case FileFormat.Json:
                    numbers = (int[])JsonSerializer.Deserialize(stream, typeof(int[]))!;
                    break;
                case FileFormat.Xml:
                    numbers = (int[])new XmlSerializer(typeof(int[])).Deserialize(stream)!;
                    break;
            }
        }

        Console.WriteLine("The 5 numbers read from the file:");

        for (int i = 0; i < numbers.Length; i++)
        {
            Console.WriteLine("Number {0}: {1}", i + 1, numbers[i]);
        }
    }
}

internal enum FileFormat
{
    Json,
    Xml
}
