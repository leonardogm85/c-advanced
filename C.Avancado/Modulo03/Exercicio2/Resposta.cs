namespace C.Avancado.Modulo03.Exercicio2
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
    public static class Resposta
    {
        public static void Executar()
        {
            Pessoa pessoa = new Pessoa();

            pessoa.Peso = 75;
            pessoa.Altura = 1.75;

            double imc = pessoa.CalcularImc((peso, altura) => peso / Math.Pow(altura, 2));

            Console.WriteLine(imc);
        }
    }

    public class Pessoa
    {
        public double Peso { get; set; }
        public double Altura { get; set; }

        public double CalcularImc(Calculo calculo)
        {
            return calculo(Peso, Altura);
        }
    }

    public delegate double Calculo(double peso, double Altura);
}
