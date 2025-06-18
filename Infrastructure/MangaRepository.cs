using System.Data;
using MiMangaBot.Domain;
using MiMangaBot.Domain.Filters;
using MySqlConnector;

namespace MiMangaBot.Infrastructure
{
    public class MangaRepository
    {
        private readonly string _connectionString =
            "Server=crossover.proxy.rlwy.net;Port=47368;Database=railway;Uid=root;Pwd=lNJhARzfrNWndzwpJdJIhgAfPWjgmuWa;";

        public IEnumerable<Manga> GetAllMangas()
        {
            var mangas = new List<Manga>();
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var command = new MySqlCommand("SELECT * FROM mangas", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                mangas.Add(new Manga
                {
                    Id = reader.GetInt32("Id"),
                    Titulo = reader.GetString("Titulo"),
                    Genero = reader.GetString("Genero"),
                    Anio_Publicacion = reader.GetInt32("Anio_Publicacion"),
                    Autor = reader.GetString("Autor"),
                    Volumenes = reader.GetInt32("Volumenes"),
                    Sigue_En_Emision = reader.GetBoolean("Sigue_En_Emision")
                });
            }

            return mangas;
        }

        public Manga? GetMangaById(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var command = new MySqlCommand("SELECT * FROM mangas WHERE Id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Manga
                {
                    Id = reader.GetInt32("Id"),
                    Titulo = reader.GetString("Titulo"),
                    Genero = reader.GetString("Genero"),
                    Anio_Publicacion = reader.GetInt32("Anio_Publicacion"),
                    Autor = reader.GetString("Autor"),
                    Volumenes = reader.GetInt32("Volumenes"),
                    Sigue_En_Emision = reader.GetBoolean("Sigue_En_Emision")
                };
            }

            return null;
        }

        public void AddManga(Manga manga)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var command = new MySqlCommand(
                @"
                INSERT INTO mangas (Titulo, Genero, Anio_Publicacion, Autor, Volumenes, Sigue_En_Emision)
                VALUES (@Titulo, @Genero, @Anio, @Autor, @Volumenes, @Sigue)",
                connection
            );

            command.Parameters.AddWithValue("@Titulo", manga.Titulo);
            command.Parameters.AddWithValue("@Genero", manga.Genero);
            command.Parameters.AddWithValue("@Anio", manga.Anio_Publicacion);
            command.Parameters.AddWithValue("@Autor", manga.Autor);
            command.Parameters.AddWithValue("@Volumenes", manga.Volumenes);
            command.Parameters.AddWithValue("@Sigue", manga.Sigue_En_Emision);

            command.ExecuteNonQuery();
        }

        public void UpdateManga(int id, Manga manga)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var command = new MySqlCommand(
                @"
                UPDATE mangas SET 
                    Titulo = @Titulo,
                    Genero = @Genero,
                    Anio_Publicacion = @Anio,
                    Autor = @Autor,
                    Volumenes = @Volumenes,
                    Sigue_En_Emision = @Sigue
                WHERE Id = @Id",
                connection
            );

            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Titulo", manga.Titulo);
            command.Parameters.AddWithValue("@Genero", manga.Genero);
            command.Parameters.AddWithValue("@Anio", manga.Anio_Publicacion);
            command.Parameters.AddWithValue("@Autor", manga.Autor);
            command.Parameters.AddWithValue("@Volumenes", manga.Volumenes);
            command.Parameters.AddWithValue("@Sigue", manga.Sigue_En_Emision);

            command.ExecuteNonQuery();
        }

        public void DeleteManga(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var command = new MySqlCommand("DELETE FROM mangas WHERE Id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }

        public IEnumerable<Manga> SearchMangas(MangaFilter filter)
        {
            return GetAllMangas().Where(filter.BuildFilter());
        }
    }
}