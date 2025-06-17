using System.Text.Json;
using MiMangaBot.Domain;
using MiMangaBot.Domain.Filters;

namespace MiMangaBot.Infrastructure;

public class MangaRepository
{
    private readonly List<Manga> _mangas;
    private readonly string _filePath;

    public MangaRepository(IConfiguration configuration)
    {
        _filePath = configuration.GetValue<string>("dataBank") ?? "javerage.library.data.json";
        _mangas = LoadData();
    }

    private string GetCurrentFilePath()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var currentFilePath = Path.Combine(currentDirectory, _filePath);
        return currentFilePath;
    }

    private List<Manga> LoadData()
    {
        var currentFilePath = GetCurrentFilePath();
        if (File.Exists(currentFilePath))
        {
            var jsonData = File.ReadAllText(currentFilePath);
            return JsonSerializer.Deserialize<List<Manga>>(jsonData) ?? new List<Manga>();
        }
        return new List<Manga>();
    }

    public IEnumerable<Manga> GetAllMangas()
    {
        return _mangas;
    }

    public Manga? GetMangaById(string id)
    {
        return _mangas.Find(m => m.Id == id);
    }

    public void AddManga(Manga manga)
    {
        manga.Id = Guid.NewGuid().ToString(); // asigna un nuevo ID al agregar
        _mangas.Add(manga);
        SaveData();
    }

    public void UpdateManga(string id, Manga updatedManga)
    {
        var index = _mangas.FindIndex(m => m.Id == id);
        if (index != -1)
        {
            updatedManga.Id = id; // conserva el mismo ID
            _mangas[index] = updatedManga;
            SaveData();
        }
    }

    public void DeleteManga(string id)
    {
        var manga = GetMangaById(id);
        if (manga != null)
        {
            _mangas.Remove(manga);
            SaveData();
        }
    }

    public IEnumerable<Manga> SearchMangas(MangaFilter filter)
    {
        return _mangas.Where(filter.BuildFilter());
    }

    private void SaveData()
    {
        File.WriteAllText(
            GetCurrentFilePath(),
            JsonSerializer.Serialize(_mangas, new JsonSerializerOptions { WriteIndented = true })
        );
    }
}