using MiMangaBot.Domain;

namespace MiMangaBot.Domain.Filters;

public class MangaFilter
{
    public string? Titulo { get; set; }
    public string? Autor { get; set; }
    public string? Genero { get; set; }
    public int? Anio_Publicacion { get; set; }
    public int? MinVolumenes { get; set; }
    public int? MaxVolumenes { get; set; }
    public bool? Sigue_En_Emision { get; set; }

    public Func<Manga, bool> BuildFilter()
    {
        Func<Manga, bool> filter = m => true;

        if (!string.IsNullOrEmpty(Titulo))
        {
            filter = filter.And(m => m.Titulo.Contains(Titulo, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(Autor))
        {
            filter = filter.And(m => m.Autor.Contains(Autor, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(Genero))
        {
            filter = filter.And(m => m.Genero.Contains(Genero, StringComparison.OrdinalIgnoreCase));
        }

        if (Anio_Publicacion.HasValue)
        {
            filter = filter.And(m => m.Anio_Publicacion == Anio_Publicacion.Value);
        }

        if (MinVolumenes.HasValue)
        {
            filter = filter.And(m => m.Volumenes >= MinVolumenes.Value);
        }

        if (MaxVolumenes.HasValue)
        {
            filter = filter.And(m => m.Volumenes <= MaxVolumenes.Value);
        }

        if (Sigue_En_Emision.HasValue)
        {
            filter = filter.And(m => m.Sigue_En_Emision == Sigue_En_Emision.Value);
        }

        return filter;
    }
}
