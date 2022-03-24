using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace C.Avancado.Modulo09.Exercicio1
{
    /// <summary>
    /// <para>
    ///     No Exercício 1 do módulo de ADO.NET, você criou uma tabela no banco de dados para
    ///     armazenar músicas.
    /// </para>
    /// <para>
    ///     Aproveite que esta tabela já existe e crie uma aplicação semelhante à criada naquele exercício,
    ///     mas que faz a manipulação de dados através do uso do ADO.NET Entity Framework.
    /// </para>
    /// <para>
    ///     Você pode utilizar o modelo "database first" se já tiver a tabela criada no banco de dados e
    ///     quiser aproveitá-la. Caso contrário, você pode criar a entidade Musica primeiro e usar o
    ///     modelo "model first". A classe MusicaDao não é necessária neste cenário, uma vez que o
    ///     próprio Entity Framework já fica responsável por encapsular o acesso ao banco de dados.
    /// </para>
    /// </summary>
    public static class Resposta
    {
        public static void Executar()
        {
            using (MusicaContext context = new MusicaContext())
            {
                Inserir(context);
                Listar(context);
                Atualizar(context);
                Carregar(context);
                Excluir(context);
                Contar(context);
            }
        }

        public static void Inserir(MusicaContext context)
        {
            Musica m1 = new Musica(
                0,
                "Título 1",
                "Cantor 1",
                "Album 1",
                2021,
                Genero.Rock);
            Musica m2 = new Musica(
                0,
                "Título 2",
                "Cantor 2",
                "Album 2",
                2022,
                Genero.Pop);

            context.Musicas.Add(m1);
            context.Musicas.Add(m2);

            context.SaveChanges();

            Console.WriteLine("Músicas inseridas.");

            Console.WriteLine();
        }

        public static void Atualizar(MusicaContext context)
        {
            Musica musica = context.Musicas.First(musica => musica.Id == 2);

            musica.Titulo = "Título Atualizado";
            musica.Cantor = "Cantor Atualizado";
            musica.Album = "Album Atualizado";
            musica.Ano = 2020;
            musica.Genero = Genero.Blues;

            context.SaveChanges();

            Console.WriteLine("Música atualizada.");

            Console.WriteLine();
        }

        public static void Excluir(MusicaContext context)
        {
            Musica musica = context.Musicas.First(musica => musica.Id == 1);

            context.Musicas.Remove(musica);

            context.SaveChanges();

            Console.WriteLine("Música excluída");

            Console.WriteLine();
        }

        public static void Carregar(MusicaContext context)
        {
            Musica musica = context.Musicas.First(musica => musica.Id == 2);

            Console.WriteLine("Música carregada:");

            Console.WriteLine(musica);

            Console.WriteLine();
        }

        public static void Listar(MusicaContext context)
        {
            List<Musica> musicas = context.Musicas.ToList();

            Console.WriteLine("Músicas Listadas:");

            musicas.ForEach(Console.WriteLine);

            Console.WriteLine();
        }

        public static void Contar(MusicaContext context)
        {
            int quantidade = context.Musicas.Count();

            Console.WriteLine("Quantidade de músicas cadastradas:");

            Console.WriteLine(quantidade);
        }
    }

    public class MusicaContext : DbContext
    {
        public DbSet<Musica> Musicas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration
                .GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    public class Musica
    {
        public Musica()
        {
        }

        public Musica(int id, string titulo, string cantor, string album, int ano, Genero genero)
        {
            Id = id;
            Titulo = titulo;
            Cantor = cantor;
            Album = album;
            Ano = ano;
            Genero = genero;
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Cantor { get; set; }
        public string Album { get; set; }
        public int Ano { get; set; }
        public Genero Genero { get; set; }

        public override string ToString()
        {
            return string.Format(
                "{0}. {1}",
                Id,
                Titulo);
        }
    }

    public enum Genero
    {
        Rock,
        Pop,
        Jazz,
        Reggae,
        Blues
    }
}
