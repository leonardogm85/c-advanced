using System.Reflection;

namespace C.Avancado.Modulo05.Exercicio2
{
    /// <summary>
    /// <para>
    ///     O atributo [Obsolete], representado pela classe System.ObsoleteAttribute, é utilizado em
    ///     situações onde tipos e elementos não devem mais ser utilizados, apesar de existirem na API.
    /// </para>
    /// <para>
    ///     Crie uma aplicação que lista todas as classes do assembly System.dll que definem o atributo
    ///     [Obsolete].
    /// </para>
    /// <para>
    ///     Na sequência, mostre também na tela o nome de todos os métodos de classes do assembly
    ///     System.dll que definem o atributo[Obsolete], juntamente com o nome das classes que os
    ///     definem.
    /// </para>
    /// <para>
    ///     Dica: Para obter uma referência ao assembly System.dll, use a chamada
    ///     Assembly.GetAssembly() passando como parâmetro o objeto Type de algum tipo que faz
    ///     parte deste assembly(por exemplo, o tipo object).
    /// </para>
    /// </summary>
    public static class Resposta
    {
        public static void Executar()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(object))!;

            Type[] types = assembly.GetTypes();

            Console.WriteLine("Lista de todas as classes do assembly System.dll que definem o atributo [Obsolete]:");

            foreach (Type type in types)
            {
                if (!type.IsClass)
                {
                    continue;
                }

                ObsoleteAttribute? obsolete = type.GetCustomAttribute<ObsoleteAttribute>();

                if (obsolete == null)
                {
                    continue;
                }

                Console.WriteLine("Classe: {0}", type.FullName);
            }

            Console.WriteLine();

            Console.WriteLine("Lista de todas os métodos de classes do assembly System.dll que definem o atributo [Obsolete]:");

            foreach (Type type in types)
            {
                if (!type.IsClass)
                {
                    continue;
                }

                MethodInfo[] methods = type.GetMethods();

                foreach (MethodInfo method in methods)
                {
                    ObsoleteAttribute? obsolete = method.GetCustomAttribute<ObsoleteAttribute>();

                    if (obsolete == null)
                    {
                        continue;
                    }

                    Console.WriteLine("Classe: {0}, Método: {1}", type.FullName, method.Name);
                }
            }
        }
    }
}
