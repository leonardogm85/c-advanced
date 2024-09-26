using System.Reflection;

namespace C.Advanced.Module05.ReflectionsAndAttributes.Question01;

/// <summary>
/// <para>
///     Crie uma aplicação que faça o seguinte:
/// </para>
/// <list type="number">
///     <item>
///         Solicite ao usuário um nome de uma classe.
///     </item>
///     <item>
///         Liste os métodos dessa classe que não são sejam estáticos, públicos, e que tenham 0
///         parâmetros ou 1 ou mais parâmetros do tipo string.
///     </item>
///     <item>
///         Peça para que o usuário escolha um método da lista para executar.
///     </item>
///     <item>
///         Solicite ao usuário o valor para cada um dos parâmetros a serem fornecidos ao método,
///         caso existam.
///     </item>
///     <item>
///         Crie um objeto da classe escolhida (chamando o construtor padrão, sem parâmetros).
///     </item>
///     <item>
///         Chame o método escolhido pelo usuário com os valores de parâmetros fornecidos pelo
///         usuário no objeto recém-criado.
///     </item>
///     <item>
///         Mostre na tela as informações impressas pelo método.
///     </item>
/// </list>
/// <para>
///     Para testar o funcionamento da aplicação, crie uma classe qualquer com alguns métodos que
///     atendem os requisitos anteriormente descritos e faça chamadas a estes métodos.
/// </para>
/// <para>
///     Dica: Métodos como GetMethods() e GetParameters() podem ser utilizados para obter as
///     informações a respeito de métodos e parâmetros. Consulte a documentação do C# para obter
///     mais detalhes.
/// </para>
/// </summary>
internal static class Answer
{
    internal static void Run()
    {
        Console.Write("Enter a full class name: ");
        Type selectedType = GetSelectedType();
        Console.WriteLine();

        Console.WriteLine("List of methods from the class:");
        List<MethodInfo> methodsFromSelectedType = GetMethodsFromSelectedType(selectedType);
        int index = 0;
        foreach (MethodInfo method in methodsFromSelectedType)
        {
            Console.WriteLine("{0}. {1};", ++index, method.Name);
        }
        Console.WriteLine();

        Console.Write("Choose a method from the class: ");
        MethodInfo selectedMethod = GetSelectedMethod(methodsFromSelectedType);
        Console.WriteLine();

        string[] parameterFromSelectedMethod = GetParameterFromSelectedMethod(selectedMethod);

        Console.Write("Run the method: ");
        object? createdInstance = Activator.CreateInstance(selectedType);
        selectedMethod.Invoke(createdInstance, parameterFromSelectedMethod);
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

    private static Type GetSelectedType()
    {
        Type? type = Type.GetType(Read());

        if (type == null)
        {
            throw new ArgumentNullException("Could not find a class by the given name.");
        }

        ConstructorInfo? constructor = type.GetConstructor(Type.EmptyTypes);

        if (constructor == null)
        {
            throw new ArgumentNullException("Could not find a default constructor in the given class.");
        }

        return type;
    }

    private static List<MethodInfo> GetMethodsFromSelectedType(Type selectedType)
    {
        List<MethodInfo> listedMethods = new List<MethodInfo>();

        MethodInfo[] methodsFromSelectedType = selectedType.GetMethods();

        foreach (MethodInfo method in methodsFromSelectedType)
        {
            if (method.IsStatic || !method.IsPublic)
            {
                continue;
            }

            ParameterInfo[] parameters = method.GetParameters();

            bool methodWithStringParameters = true;

            foreach (ParameterInfo parameter in parameters)
            {
                if (parameter.ParameterType == typeof(string))
                {
                    continue;
                }

                methodWithStringParameters = false;

                break;
            }

            if (methodWithStringParameters)
            {
                listedMethods.Add(method);
            }
        }

        if (listedMethods.Count == 0)
        {
            throw new ArgumentException("Could not find the method in the given class.");
        }

        return listedMethods;
    }

    private static MethodInfo GetSelectedMethod(List<MethodInfo> methods)
    {
        if (int.TryParse(Read(), out int index))
        {
            if (methods.Count >= index)
            {
                return methods[index - 1];
            }

            throw new IndexOutOfRangeException("Could not find the method by chosen index.");
        }

        throw new FormatException("Could not convert the entered value to a valid input.");
    }

    private static string[] GetParameterFromSelectedMethod(MethodInfo method)
    {
        ParameterInfo[] parameters = method.GetParameters();

        string[] selectedParameters = new string[parameters.Length];

        if (parameters.Length == 0)
        {
            return selectedParameters;
        }

        Console.WriteLine("Enter the values for the method parameters:");

        foreach (ParameterInfo parameter in parameters)
        {
            Console.Write("Parameter value ({0}): ", parameter.Name);
            selectedParameters[parameter.Position] = Read();
        }

        Console.WriteLine();

        return selectedParameters;
    }
}

public class ClassTest
{
    public void Method1()
    {
        Console.WriteLine("Method1()");
    }

    public void Method2(string parameter1)
    {
        Console.WriteLine("Method2({0})", parameter1);
    }

    public void Method3(string parameter1, string parameter2)
    {
        Console.WriteLine("Method3({0}, {1})", parameter1, parameter2);
    }

    public void Method4(int parameter1)
    {
        Console.WriteLine("Method4(int)");
    }

    public void Method5(string parameter1, int parameter2)
    {
        Console.WriteLine("Method5(string, int)");
    }

    public static void Method6()
    {
        Console.WriteLine("Method6()");
    }

    internal void Method7()
    {
        Console.WriteLine("Method7()");
    }
}
