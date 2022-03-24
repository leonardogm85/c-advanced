using Microsoft.Extensions.Configuration;
using System.Data.Common;

namespace C.Avancado.Modulo08.Exercicio1
{
    /// <summary>
    /// <para>
    ///     Crie uma aplicação capaz de manipular músicas em uma tabela do banco de dados. Esta
    ///     aplicação deve ter uma classe Musica, definida da seguinte forma:
    /// </para>
    /// <code>
    ///     class Musica
    ///     {
    ///         public int Id { get; set; }
    ///         public string Titulo { get; set; }
    ///         public string Cantor { get; set; }
    ///         public string Album { get; set; }
    ///         public int? Ano { get; set; }
    ///         public Genero Genero { get; set; }
    ///     }
    /// </code>
    /// <para>
    ///     O gênero da música é um enum, que pode, por exemplo, ser definido assim:
    /// </para>
    /// <code>
    ///     enum Genero
    ///     {
    ///         Rock, Pop, Jazz, Reggae, Blues
    ///     }
    /// </code>
    /// <para>
    ///     Crie uma tabela no banco de dados que espelha a classe música, onde as músicas serão
    ///     armazenadas.
    /// </para>
    /// <para>
    ///     A fim de encapsular o acesso a dados, toda a interação com o banco de dados deve ser feita
    ///     através da classe MusicaDao, que deverá ter operações para:
    /// </para>
    /// <list type="bullet">
    ///     <item>Inserir uma música</item>
    ///     <item>Atualizar uma música</item>
    ///     <item>Excluir uma música</item>
    ///     <item>Carregar uma música pelo seu id</item>
    ///     <item>Contar as músicas cadastradas</item>
    /// </list>
    /// <para>
    ///     Crie chamadas para serem disparadas a partir do método Main() para manipular algumas
    ///     músicas no banco de dados. Assim será possível testar se os métodos definidos em
    ///     MusicaDao estão funcionando adequadamente.
    /// </para>
    /// <para>
    ///     Dica: A resolução deste exercício acompanha um arquivo criar-tabela.sql, com o script de
    ///     criação da tabela Musica utilizada na resolução proposta. Você pode utilizá-lo caso deseje criar
    ///     uma tabela igual. O script é para o banco de dados SQL Server.
    /// </para>
    /// </summary>
    public static class Resposta
    {
        public static void Executar()
        {
            MusicaDao dao = DaoFactory.CreateDao<MusicaDao>();

            Inserir(dao);
            Listar(dao);
            Atualizar(dao);
            Carregar(dao);
            Excluir(dao);
            Contar(dao);
        }

        public static void Inserir(MusicaDao dao)
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

            dao.Inserir(m1);
            dao.Inserir(m2);

            Console.WriteLine("Músicas inseridas.");

            Console.WriteLine();
        }

        public static void Atualizar(MusicaDao dao)
        {
            Musica musica = dao.Carregar(2)!;

            musica.DefinirTitulo("Título Atualizado");
            musica.DefinirCantor("Cantor Atualizado");
            musica.DefinirAlbum("Album Atualizado");
            musica.DefinirAno(2020);
            musica.DefinirGenero(Genero.Blues);

            dao.Atualizar(musica);

            Console.WriteLine("Música atualizada.");

            Console.WriteLine();
        }

        public static void Excluir(MusicaDao dao)
        {
            dao.Excluir(1);

            Console.WriteLine("Música excluída");

            Console.WriteLine();
        }

        public static void Carregar(MusicaDao dao)
        {
            Musica musica = dao.Carregar(2)!;

            Console.WriteLine("Música carregada:");

            Console.WriteLine(musica);

            Console.WriteLine();
        }

        public static void Listar(MusicaDao dao)
        {
            List<Musica> musicas = dao.Listar();

            Console.WriteLine("Músicas Listadas:");

            musicas.ForEach(Console.WriteLine);

            Console.WriteLine();
        }

        public static void Contar(MusicaDao dao)
        {
            int quantidade = dao.Contar();

            Console.WriteLine("Quantidade de músicas cadastradas:");

            Console.WriteLine(quantidade);
        }
    }

    public class Musica
    {
        public Musica(int id, string titulo, string cantor, string album, int ano, Genero genero)
        {
            Id = id;
            Titulo = titulo;
            Cantor = cantor;
            Album = album;
            Ano = ano;
            Genero = genero;
        }

        public int Id { get; private set; }
        public string Titulo { get; private set; }
        public string Cantor { get; private set; }
        public string Album { get; private set; }
        public int Ano { get; private set; }
        public Genero Genero { get; private set; }

        public void DefinirTitulo(string novoTitulo) => Titulo = novoTitulo;
        public void DefinirCantor(string novoCantor) => Cantor = novoCantor;
        public void DefinirAlbum(string novoAlbum) => Album = novoAlbum;
        public void DefinirAno(int novoAno) => Ano = novoAno;
        public void DefinirGenero(Genero novoGenero) => Genero = novoGenero;

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

    public class MusicaDao : Dao
    {
        public void Inserir(Musica musica)
        {
            using DbConnection connection = CreateConnection();

            using DbCommand command = CreateCommand(
                connection,
                "INSERT INTO Musica (Titulo, Cantor, Album, Ano, Genero) VALUES (@Titulo, @Cantor, @Album, @Ano, @Genero)");

            CreateParameter(command, "Titulo", musica.Titulo);
            CreateParameter(command, "Cantor", musica.Cantor);
            CreateParameter(command, "Album", musica.Album);
            CreateParameter(command, "Ano", musica.Ano);
            CreateParameter(command, "Genero", musica.Genero);

            command.ExecuteNonQuery();
        }

        public void Atualizar(Musica musica)
        {
            using DbConnection connection = CreateConnection();

            using DbCommand command = CreateCommand(
                connection,
                "UPDATE Musica SET Titulo = @Titulo, Cantor = @Cantor, Album = @Album, Ano = @Ano, Genero = @Genero WHERE Id = @Id");

            CreateParameter(command, "Id", musica.Id);
            CreateParameter(command, "Titulo", musica.Titulo);
            CreateParameter(command, "Cantor", musica.Cantor);
            CreateParameter(command, "Album", musica.Album);
            CreateParameter(command, "Ano", musica.Ano);
            CreateParameter(command, "Genero", musica.Genero);

            command.ExecuteNonQuery();
        }

        public void Excluir(int id)
        {
            using DbConnection connection = CreateConnection();

            using DbCommand command = CreateCommand(
                connection,
                "DELETE FROM Musica WHERE Id = @Id");

            CreateParameter(command, "Id", id);

            command.ExecuteNonQuery();
        }

        public Musica? Carregar(int id)
        {
            using DbConnection connection = CreateConnection();

            using DbCommand command = CreateCommand(
                connection,
                "SELECT Id, Titulo, Cantor, Album, Ano, Genero FROM Musica WHERE Id = @Id");

            CreateParameter(command, "Id", id);

            using DbDataReader reader = command.ExecuteReader();

            if (!reader.Read())
            {
                return null;
            }

            return new Musica(
                (int)reader["Id"],
                (string)reader["Titulo"],
                (string)reader["Cantor"],
                (string)reader["Album"],
                (int)reader["Ano"],
                Enum.Parse<Genero>((string)reader["Genero"]));
        }

        public List<Musica> Listar()
        {
            using DbConnection connection = CreateConnection();

            using DbCommand command = CreateCommand(
                connection,
                "SELECT Id, Titulo, Cantor, Album, Ano, Genero FROM Musica");

            List<Musica> musicas = new List<Musica>();

            using DbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Musica musica = new Musica(
                    (int)reader["Id"],
                    (string)reader["Titulo"],
                    (string)reader["Cantor"],
                    (string)reader["Album"],
                    (int)reader["Ano"],
                    Enum.Parse<Genero>((string)reader["Genero"]));
                musicas.Add(musica);
            }

            return musicas;
        }

        public int Contar()
        {
            using DbConnection connection = CreateConnection();

            using DbCommand command = CreateCommand(
                connection,
                "SELECT COUNT(*) FROM Musica");

            return (int)command.ExecuteScalar()!;
        }
    }

    public static class DaoFactory
    {
        public static T CreateDao<T>() where T : Dao, new() => new T();
    }

    public abstract class Dao
    {
        protected static DbProviderFactory ProviderFactory;
        protected static string ConnectionString;

        static Dao()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration
                .GetConnectionString("DefaultConnection");
            string providerInvariantName = configuration
                .GetValue<string>("AppSettings:ProviderInvariantName");
            string factoryTypeAssemblyQualifiedName = configuration
                .GetValue<string>("AppSettings:FactoryTypeAssemblyQualifiedName");

            DbProviderFactories.RegisterFactory(
                providerInvariantName,
                factoryTypeAssemblyQualifiedName);

            ConnectionString = connectionString;
            ProviderFactory = DbProviderFactories.GetFactory(providerInvariantName);
        }

        protected DbConnection CreateConnection()
        {
            DbConnection connection = ProviderFactory.CreateConnection()!;
            connection.ConnectionString = ConnectionString;
            connection.Open();
            return connection;
        }

        protected DbCommand CreateCommand(DbConnection connection, string commandText)
        {
            DbCommand command = ProviderFactory.CreateCommand()!;
            command.Connection = connection;
            command.CommandText = commandText;
            return command;
        }

        protected DbParameter CreateParameter(DbCommand command, string name, object value)
        {
            DbParameter parameter = ProviderFactory.CreateParameter()!;
            parameter.ParameterName = name;
            parameter.Value = value;
            command.Parameters.Add(parameter);
            return parameter;
        }
    }
}
