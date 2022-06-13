using Microsoft.Extensions.Configuration;
using System.Data.Common;

namespace C.Advanced.Module08.Question01
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
    internal static class Answer
    {
        internal static void Run()
        {
            SongDao dao = DaoFactory.CreateDao<SongDao>();

            Insert(dao);
            List(dao);
            Update(dao);
            Load(dao);
            Delete(dao);
            Count(dao);
        }

        private static void Insert(SongDao dao)
        {
            Song m1 = new Song(
                0,
                "Title 1",
                "Singer 1",
                "Album 1",
                2021,
                Genre.Rock);
            Song m2 = new Song(
                0,
                "Title 2",
                "Singer 2",
                "Album 2",
                2022,
                Genre.Pop);

            dao.Insert(m1);
            dao.Insert(m2);

            Console.WriteLine("Inserted songs.");

            Console.WriteLine();
        }

        private static void Update(SongDao dao)
        {
            Song song = dao.Load(2)!;

            song.Title = "Updated title";
            song.Singer = "Updated singer";
            song.Album = "Updated album";
            song.Year = 2020;
            song.Genre = Genre.Blues;

            dao.Update(song);

            Console.WriteLine("Updated song.");

            Console.WriteLine();
        }

        private static void Delete(SongDao dao)
        {
            dao.Delete(1);

            Console.WriteLine("deleted song");

            Console.WriteLine();
        }

        private static void Load(SongDao dao)
        {
            Song song = dao.Load(2)!;

            Console.WriteLine("Loaded song:");

            Console.WriteLine(song);

            Console.WriteLine();
        }

        private static void List(SongDao dao)
        {
            List<Song> songs = dao.List();

            Console.WriteLine("Listed songs:");

            songs.ForEach(Console.WriteLine);

            Console.WriteLine();
        }

        private static void Count(SongDao dao)
        {
            int count = dao.Count();

            Console.WriteLine("Number of registered songs:");

            Console.WriteLine(count);
        }
    }

    internal class SongDao : Dao
    {
        public void Insert(Song song)
        {
            using DbConnection connection = CreateConnection();

            using DbCommand command = CreateCommand(
                connection,
                "INSERT INTO Songs (Title, Singer, Album, Year, Genre) VALUES (@Title, @Singer, @Album, @Year, @Genre)");

            CreateParameter(command, "Title", song.Title);
            CreateParameter(command, "Singer", song.Singer);
            CreateParameter(command, "Album", song.Album);
            CreateParameter(command, "Year", song.Year);
            CreateParameter(command, "Genre", song.Genre);

            command.ExecuteNonQuery();
        }

        public void Update(Song song)
        {
            using DbConnection connection = CreateConnection();

            using DbCommand command = CreateCommand(
                connection,
                "UPDATE Songs SET Title = @Title, Singer = @Singer, Album = @Album, Year = @Year, Genre = @Genre WHERE Id = @Id");

            CreateParameter(command, "Id", song.Id);
            CreateParameter(command, "Title", song.Title);
            CreateParameter(command, "Singer", song.Singer);
            CreateParameter(command, "Album", song.Album);
            CreateParameter(command, "Year", song.Year);
            CreateParameter(command, "Genre", song.Genre);

            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using DbConnection connection = CreateConnection();

            using DbCommand command = CreateCommand(
                connection,
                "DELETE FROM Songs WHERE Id = @Id");

            CreateParameter(command, "Id", id);

            command.ExecuteNonQuery();
        }

        public Song? Load(int id)
        {
            using DbConnection connection = CreateConnection();

            using DbCommand command = CreateCommand(
                connection,
                "SELECT Id, Title, Singer, Album, Year, Genre FROM Songs WHERE Id = @Id");

            CreateParameter(command, "Id", id);

            using DbDataReader reader = command.ExecuteReader();

            if (!reader.Read())
            {
                return null;
            }

            return new Song(
                (int)reader["Id"],
                (string)reader["Title"],
                (string)reader["Singer"],
                (string)reader["Album"],
                (int)reader["Year"],
                (Genre)reader["Genre"]);
        }

        public List<Song> List()
        {
            using DbConnection connection = CreateConnection();

            using DbCommand command = CreateCommand(
                connection,
                "SELECT Id, Title, Singer, Album, Year, Genre FROM Songs");

            List<Song> songs = new List<Song>();

            using DbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Song song = new Song(
                    (int)reader["Id"],
                    (string)reader["Title"],
                    (string)reader["Singer"],
                    (string)reader["Album"],
                    (int)reader["Year"],
                    (Genre)reader["Genre"]);
                songs.Add(song);
            }

            return songs;
        }

        public int Count()
        {
            using DbConnection connection = CreateConnection();

            using DbCommand command = CreateCommand(
                connection,
                "SELECT COUNT(*) FROM Songs");

            return (int)command.ExecuteScalar()!;
        }
    }

    internal static class DaoFactory
    {
        public static T CreateDao<T>() where T : Dao, new() => new T();
    }

    internal abstract class Dao
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

    internal class Song
    {
        public Song(int id, string title, string singer, string album, int year, Genre genre)
        {
            Id = id;
            Title = title;
            Singer = singer;
            Album = album;
            Year = year;
            Genre = genre;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Singer { get; set; }
        public string Album { get; set; }
        public int Year { get; set; }
        public Genre Genre { get; set; }

        public override string ToString()
        {
            return string.Format(
                "{0}. {1}",
                Id,
                Title);
        }
    }

    internal enum Genre
    {
        Rock,
        Pop,
        Jazz,
        Reggae,
        Blues
    }
}
