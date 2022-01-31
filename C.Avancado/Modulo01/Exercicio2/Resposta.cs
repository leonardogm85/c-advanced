namespace C.Avancado.Modulo01.Exercicio2
{
    /// <summary>
    /// <para>
    ///     Tomando como base a mesma estrutura criada no Exercício 1, crie um casting customizado
    ///     que permita converter, de forma implícita, um Vetor em um objeto string. Esta string deve
    ///     representar o vetor no formato(x, y).
    /// </para>
    /// <para>
    ///     Crie também um indexador que permita extrair ou atribuir valores às coordenadas x e y do
    ///     vetor. Os caracteres 'X' ou 'Y' devem ser usados como indexadores. Veja um exemplo:
    /// </para>
    /// <code>
    ///     Vector v = new Vector();
    ///     v['X'] = 5;
    ///     v['Y'] = 7;
    ///     int x = v['X'];
    ///     int y = v['Y'];
    /// </code>
    /// </summary>
    public static class Resposta
    {
        public static void Executar()
        {
            Vector v = new Vector();

            v['X'] = 5;
            v['Y'] = 7;

            int x = v['X'];
            int y = v['Y'];

            Console.WriteLine($"({x}, {y})");

            string s = v;

            Console.WriteLine(v);
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

        public int this[char coordenada]
        {
            get
            {
                switch (coordenada)
                {
                    case 'X':
                        return x;
                    case 'Y':
                        return y;
                    default:
                        throw new ArgumentException("Coordenada inválida.");
                }
            }
            set
            {
                switch (coordenada)
                {
                    case 'X':
                        x = value;
                        break;
                    case 'Y':
                        y = value;
                        break;
                    default:
                        throw new ArgumentException("Coordenada inválida.");
                }
            }
        }

        public override string ToString()
        {
            return $"({x}, {y})";
        }

        public static implicit operator string(Vector v)
        {
            return v.ToString();
        }
    }
}
