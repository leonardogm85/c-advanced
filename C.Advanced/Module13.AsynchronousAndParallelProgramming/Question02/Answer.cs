namespace C.Advanced.Module13.AsynchronousAndParallelProgramming.Question02;

/// <summary>
/// <para>
///     Crie a mesma aplicação do Exercício 1, mas utilize um objeto Task para realizar a contagem ao
///     invés de um delegate assíncrono.
/// </para>
/// </summary>
internal static class Answer
{
    private static string directory = string.Empty;
    private static bool isCompleted = false;

    internal static void Run()
    {
        Console.Write("Enter a diretory name: ");
        directory = Read();

        Task.Factory
            .StartNew(CountNumberOfFilesAndDirectories)
            .ContinueWith(ShowNumberOfFilesAndDirectories);

        Task.Factory.StartNew(ShowCountProgress).Wait();
    }

    private static string Read()
    {
        string? value = Console.ReadLine();

        if (value == null)
        {
            throw new ArgumentNullException("Could not convert the entered value to a valid input.");
        }

        return value;
    }

    private static void ShowCountProgress()
    {
        while (!isCompleted)
        {
            Console.WriteLine("Counting number of files and directories ...");
            Thread.Sleep(1000);
        }
    }

    private static Result CountNumberOfFilesAndDirectories()
    {
        if (string.IsNullOrWhiteSpace(directory))
        {
            throw new ArgumentException("A valid directory must be provided.");
        }

        Counter counter = new Counter();

        return counter.CountNumberOfFilesAndDirectories(directory);
    }

    private static void ShowNumberOfFilesAndDirectories(Task<Result> task)
    {
        isCompleted = true;

        if (task.Exception == null || task.Exception.InnerException == null)
        {
            Console.WriteLine("Number of directories: {0}", task.Result.NumberOfDirectories);
            Console.WriteLine("Number of files: {0}", task.Result.NumberOfFiles);
        }
        else
        {
            Console.WriteLine(task.Exception.InnerException.Message);
        }
    }
}

internal class Counter
{
    public Result CountNumberOfFilesAndDirectories(string directoryName)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(directoryName);

        if (!directoryInfo.Exists)
        {
            throw new ArgumentException($"The {directoryName} directory does not exist.");
        }

        Result result = new Result();

        IncrementNumberOfFilesAndDirectories(result, directoryInfo);

        return result;
    }

    private void IncrementNumberOfFilesAndDirectories(Result result, DirectoryInfo directory)
    {
        try
        {
            result.IncrementNumberOfFiles(directory.GetFiles().Length);

            foreach (DirectoryInfo subdirectory in directory.GetDirectories())
            {
                result.IncrementNumberOfDirectories(1);

                IncrementNumberOfFilesAndDirectories(result, subdirectory);
            }
        }
        catch (UnauthorizedAccessException)
        {
        }
    }
}

internal class Result
{
    public int NumberOfDirectories { get; private set; }
    public int NumberOfFiles { get; private set; }

    public void IncrementNumberOfDirectories(int number)
    {
        NumberOfDirectories += number;
    }

    public void IncrementNumberOfFiles(int number)
    {
        NumberOfFiles += number;
    }
}
