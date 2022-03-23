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

        }
    }
}
