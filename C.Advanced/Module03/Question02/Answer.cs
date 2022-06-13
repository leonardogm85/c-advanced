namespace C.Advanced.Module03.Question02
{
    /// <summary>
    /// <para>
    ///     Crie uma classe Pessoa, definida desta forma:
    /// </para>
    /// <code>
    ///     class Pessoa 
    ///     { 
    ///         public double Peso { get; set; }
    ///         public double Altura { get; set; }
    ///         public double CalcularImc(Calculo calculo)
    ///         {
    ///             return calculo(Peso, Altura);
    ///         }
    ///     }
    /// </code>
    /// <para>
    ///     O tipo Calculo é um delegate, que deve ser criado por você. Como é possível perceber na
    ///     definição da classe, os parâmetros passados para ele são o peso e a altura, e o retorno é o
    ///     valor do IMC.
    /// </para>
    /// <para>
    ///     Crie um objeto do tipo Pessoa, com um determinado peso e altura. Depois chame o método
    ///     CalcularImc() para descobrir o IMC dessa pessoa. Utilize uma expressão lambda como
    ///     parâmetro para o método, a fim de realizar o cálculo.
    /// </para>
    /// <para>
    ///     O cálculo do IMC pode ser feito através desta fórmula:
    /// </para>
    /// <para>
    ///     IMC = peso / (altura * altura)
    /// </para>
    /// </summary>
    internal static class Answer
    {
        internal static void Run()
        {
            Person person = new Person();

            person.Weight = 75;
            person.Height = 1.75;

            double imc = person.CalculateImc((weight, height) => weight / Math.Pow(height, 2));

            Console.WriteLine("Weight: {0}", person.Weight);
            Console.WriteLine("Height: {0}", person.Height);
            Console.WriteLine("IMC = weight / (height * height): {0}", imc);
        }
    }

    internal class Person
    {
        public double Weight { get; set; }
        public double Height { get; set; }

        public double CalculateImc(Calculate calculate)
        {
            return calculate(Weight, Height);
        }
    }

    internal delegate double Calculate(double weight, double height);
}
