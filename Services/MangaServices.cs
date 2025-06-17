using System.Collections.Generic;
using System.Linq;
using MiMangaBot.Domain;
using MiMangaBot.Domain.Filters;
using MiMangaBot.Infrastructure;

namespace MiMangaBot.Services;

public class MangaServices
{
    private readonly MangaRepository _mangaRepository;

    public MangaServices(MangaRepository mangaRepository)
    {
        _mangaRepository = mangaRepository;
    }

    public List<Manga> GetAllMangas()
    {
        return _mangaRepository.GetAllMangas().ToList();
    }

    public Manga? GetMangaById(string id)
    {
        return _mangaRepository.GetMangaById(id);
    }

    public void AddManga(Manga manga)
    {
        _mangaRepository.AddManga(manga);
    }

    public void UpdateManga(string id, Manga updatedManga)
    {
        _mangaRepository.UpdateManga(id, updatedManga);
    }

    public void DeleteManga(string id)
    {
        _mangaRepository.DeleteManga(id);
    }

    // Nuevo método para obtener mangas filtrados
    public List<Manga> SearchMangas(MangaFilter filter)
    {
        return _mangaRepository.SearchMangas(filter).ToList();
    }
}
