using System.Reflection;

namespace C.Avancado.Modulo05.Exercicio1
{
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
    public static class Resposta
    {
        public static void Executar()
        {
            Console.Write("Informe o nome de uma classe: ");
            Type tipoInformado = ObterTipo();
            Console.WriteLine();

            Console.WriteLine("Lista de métodos da classe:");
            List<MethodInfo> metodosDoTipo = ObterMetodosDoTipo(tipoInformado);
            int indice = 0;
            foreach (MethodInfo metodo in metodosDoTipo)
            {
                Console.WriteLine("{0}. {1};", ++indice, metodo.Name);
            }
            Console.WriteLine();

            Console.Write("Escolha um método da classe: ");
            MethodInfo metodoEscolhido = ObterMedotoEscolhido(metodosDoTipo);
            Console.WriteLine();

            string[] parametrosDoMetodo = ObterParametrosDoMetodo(metodoEscolhido);

            Console.Write("Execução do método: ");
            object? objetoDaClasse = Activator.CreateInstance(tipoInformado);
            metodoEscolhido.Invoke(objetoDaClasse, parametrosDoMetodo);
        }

        private static string LerEntrada()
        {
            string? valor = Console.ReadLine();

            if (valor == null)
            {
                throw new ArgumentNullException("Não foi possível converter o valor informado para uma entrada válida.");
            }

            return valor;
        }

        private static Type ObterTipo()
        {
            Type? tipo = Type.GetType(LerEntrada());

            if (tipo == null)
            {
                throw new ArgumentNullException("Não foi possível encontrar a classe pelo nome informado.");
            }

            ConstructorInfo? contrutor = tipo.GetConstructor(Type.EmptyTypes);

            if (contrutor == null)
            {
                throw new ArgumentNullException("Não foi possível encontrar um construtor padrão na classe informada.");
            }

            return tipo;
        }

        private static List<MethodInfo> ObterMetodosDoTipo(Type tipo)
        {
            List<MethodInfo> metodosListado = new List<MethodInfo>();

            MethodInfo[] metodosDoTipo = tipo.GetMethods();

            foreach (MethodInfo metodo in metodosDoTipo)
            {
                if (metodo.IsStatic || !metodo.IsPublic)
                {
                    continue;
                }

                ParameterInfo[] parametros = metodo.GetParameters();

                bool metodoComParametrosStrings = true;

                foreach (ParameterInfo parametro in parametros)
                {
                    if (parametro.ParameterType == typeof(string))
                    {
                        continue;
                    }

                    metodoComParametrosStrings = false;

                    break;
                }

                if (metodoComParametrosStrings)
                {
                    metodosListado.Add(metodo);
                }
            }

            if (metodosListado.Count == 0)
            {
                throw new ArgumentException("Não foi possível encontrar o método na classe informada.");
            }

            return metodosListado;
        }

        private static MethodInfo ObterMedotoEscolhido(List<MethodInfo> metodos)
        {
            if (int.TryParse(LerEntrada(), out int indice))
            {
                if (metodos.Count >= indice)
                {
                    return metodos[indice - 1];
                }

                throw new IndexOutOfRangeException("Não foi possível encontrar o método pelo índice escolhido.");
            }

            throw new FormatException("Não foi possível converter o valor digitado para um número válido.");
        }

        private static string[] ObterParametrosDoMetodo(MethodInfo metodo)
        {
            ParameterInfo[] parametros = metodo.GetParameters();

            string[] parametrosInformado = new string[parametros.Length];

            if (parametros.Length == 0)
            {
                return parametrosInformado;
            }

            Console.WriteLine("Informe os valores para os parâmetros do método:");

            foreach (ParameterInfo parametro in parametros)
            {
                Console.Write("Valor do parâmetro ({0}): ", parametro.Name);
                parametrosInformado[parametro.Position] = LerEntrada();
            }

            Console.WriteLine();

            return parametrosInformado;
        }
    }

    public class ClasseTeste
    {
        public void Metodo1()
        {
            Console.WriteLine("Metodo1()");
        }

        public void Metodo2(string parametro1)
        {
            Console.WriteLine("Metodo2({0})", parametro1);
        }

        public void Metodo3(string parametro1, string parametro2)
        {
            Console.WriteLine("Metodo3({0}, {1})", parametro1, parametro2);
        }

        public void Metodo4(int parametro1)
        {
            Console.WriteLine("Metodo4(int)");
        }

        public void Metodo5(string parametro1, int parametro2)
        {
            Console.WriteLine("Metodo5(string, int)");
        }

        public static void Metodo6()
        {
            Console.WriteLine("Metodo6()");
        }

        internal void Metodo7()
        {
            Console.WriteLine("Metodo7()");
        }
    }
}
