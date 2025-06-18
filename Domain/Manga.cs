namespace MiMangaBot.Domain;

public class Manga
{
    public int Id { get; set; } // Cambiado de string a int

    public required string Titulo { get; set; }
    public required string Genero { get; set; }
    public required int Anio_Publicacion { get; set; }
    public required string Autor { get; set; }
    public required int Volumenes { get; set; }
    public required bool Sigue_En_Emision { get; set; }
}
