using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace C.Advanced.Module09.AdoNetEntityFramework.Question01;

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
internal static class Answer
{
    internal static void Run()
    {
        using (SongContext context = new SongContext())
        {
            Insert(context);
            List(context);
            Update(context);
            Load(context);
            Delete(context);
            Count(context);
        }
    }

    private static void Insert(SongContext context)
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

        context.Songs.Add(m1);
        context.Songs.Add(m2);

        context.SaveChanges();

        Console.WriteLine("Inserted songs.");

        Console.WriteLine();
    }

    private static void Update(SongContext context)
    {
        Song song = context.Songs.First(song => song.Id == 2);

        song.Title = "Updated title";
        song.Singer = "Updated singer";
        song.Album = "Updated album";
        song.Year = 2020;
        song.Genre = Genre.Blues;

        context.SaveChanges();

        Console.WriteLine("Updated song.");

        Console.WriteLine();
    }

    private static void Delete(SongContext context)
    {
        if (context.Songs.Any(song => song.Id == 1))
        {
            Song song = context.Songs.First(song => song.Id == 1);

            context.Songs.Remove(song);

            context.SaveChanges();
        }

        Console.WriteLine("Deleted song.");

        Console.WriteLine();
    }

    private static void Load(SongContext context)
    {
        Song song = context.Songs.First(song => song.Id == 2);

        Console.WriteLine("Loaded song:");

        Console.WriteLine(song);

        Console.WriteLine();
    }

    private static void List(SongContext context)
    {
        List<Song> songs = context.Songs.ToList();

        Console.WriteLine("Listed songs:");

        songs.ForEach(Console.WriteLine);

        Console.WriteLine();
    }

    private static void Count(SongContext context)
    {
        int count = context.Songs.Count();

        Console.WriteLine("Number of registered songs:");

        Console.WriteLine(count);
    }
}

internal class SongContext : DbContext
{
    public DbSet<Song> Songs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = configuration
            .GetConnectionString("DefaultConnection")!;

        optionsBuilder.UseSqlServer(connectionString);
    }
}

internal class Song
{
    protected Song()
    {
    }

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
    public string Title { get; set; } = null!;
    public string Singer { get; set; } = null!;
    public string Album { get; set; } = null!;
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
