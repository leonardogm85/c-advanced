using System.Text.Json;
using System.Xml.Serialization;

namespace C.Advanced.Module14.DataSerialization.Question01;

/// <summary>
/// <para>
///     Crie uma aplicação que solicite ao usuário a digitação de 5 números e armazene-os em uma
///     lista. Na sequência, o usuário deverá escolher o nome para um arquivo onde os números
///     digitados serão serializados, e também escolher o formato do arquivo (binário ou XML). A
///     aplicação deverá então gravar os números digitados no arquivo no formato especificado.
/// </para>
/// </summary>
internal static class Answer
{
    internal static void Run()
    {
        int[] numbers = new int[5];
        Console.WriteLine("Enter 5 numbers to store in a file:");
        for (int i = 0; i < 5; i++)
        {
            Console.Write("Number {0}: ", i + 1);
            numbers[i] = ReadTheNumber();
        }
        Console.WriteLine();

        Console.Write("Enter the file name: ");
        string fileName = ReadTheFileName();
        Console.WriteLine();

        WriteTheFileFormats();
        Console.Write("Enter the file format: ");
        FileFormat fileFormat = ReadTheFileFormat();
        Console.WriteLine();

        Serialize(numbers, fileName, fileFormat);
        Console.WriteLine("Serialization complete.");
    }

    private static int ReadTheNumber()
    {
        if (int.TryParse(Console.ReadLine(), out int value))
        {
            return value;
        }

        throw new FormatException("Could not convert the entered value to a valid number.");
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
        if (Enum.TryParse(Console.ReadLine(), out FileFormat value))
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

    private static void Serialize(int[] numbers, string fileName, FileFormat fileFormat)
    {
        using (FileStream stream = File.OpenWrite($"{fileName}.{fileFormat.ToString().ToLower()}"))
        {
            switch (fileFormat)
            {
                case FileFormat.Json:
                    JsonSerializer.Serialize(stream, numbers);
                    break;
                case FileFormat.Xml:
                    new XmlSerializer(typeof(int[])).Serialize(stream, numbers);
                    break;
            }
        }
    }
}

internal enum FileFormat
{
    Json,
    Xml
}
