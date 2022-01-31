namespace C.Avancado.Modulo01.Exercicio1
{
    /// <summary>
    /// <para>
    ///     Crie uma estrutura Vector para representar o conceito de um vetor na matemática. O vetor
    ///     deve armazenar as coordenadas X e Y na forma de inteiros. O código base da estrutura é este:
    /// </para>
    /// <code>
    ///     public struct Vector
    ///     {
    ///         private int x;
    ///         private int y;
    ///         public Vector(int x, int y)
    ///         : this()
    ///         {
    ///             this.x = x;
    ///             this.y = y;
    ///         }
    ///     }
    /// </code>
    /// <para>
    ///     Nesta estrutura, sobrescreva os operadores "+" e "*". O operador "+" deve permitir a soma de
    ///     dois vetores 'v1' e 'v2', que resulta em um novo vetor 'vr'. A soma é feita através da soma das
    ///     coordenadas, desta forma:
    /// </para>
    /// <para>
    ///     vr = (v1x + v2x, v1y + v2y)
    /// </para>
    /// <para>
    ///     Já o operador "*" deve permitir a multiplicação de um vetor 'v' por um número inteiro 'm'. O
    ///     resultado é um vetor 'vr' cujas coordenadas são multiplicadas por este número, desta forma:
    /// </para>
    /// <para>
    ///     vr = (vx * m, vy * m)
    /// </para>
    /// <para>
    ///     Para testar seu código, utilize a sequência de execução abaixo, que define dois vetores, soma
    ///     ambos e multiplica o vetor resultante por 3:
    /// </para>
    /// <code>
    ///     Vector v1 = new Vector(2, 3);
    ///     Vector v2 = new Vector(4, 5);
    ///     Vector v3 = v1 + v2;
    ///     Vector v4 = v3 * 3;
    /// </code>
    /// <para>
    ///     O resultado final é um vetor cujas coordenadas são (18, 24).
    /// </para>
    /// </summary>
    public static class Resposta
    {
        public static void Executar()
        {
            Vector v1 = new Vector(2, 3);
            Vector v2 = new Vector(4, 5);

            Vector v3 = v1 + v2;

            Vector v4 = v3 * 3;

            Console.WriteLine(v4);
        }
    }

    public struct Vector
    {
        private int x;
        private int y;

        public Vector(int x, int y)
            : this()
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"({x}, {y})";
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(
                v1.x + v2.x,
                v1.y + v2.y);
        }

        public static Vector operator *(Vector v, int m)
        {
            return new Vector(
                v.x * m,
                v.y * m);
        }
    }
}
