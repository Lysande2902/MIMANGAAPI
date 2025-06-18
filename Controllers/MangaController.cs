using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiMangaBot.Domain;
using MiMangaBot.Domain.Filters; // Asegúrate de tener esta directiva using para MangaFilter
using MiMangaBot.Infrastructure;
using MiMangaBot.Services;

namespace MiMangaBot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MangaController : ControllerBase
    {
        private readonly MangaServices _mangaServices;

        public MangaController(MangaServices mangaServices)
        {
            _mangaServices = mangaServices;
        }

        [HttpGet]
        public ActionResult<List<Manga>> GetAllMangas()
        {
            return Ok(_mangaServices.GetAllMangas());
        }

        // Nuevo endpoint para obtener mangas filtrados
        [HttpGet("filter")]
        public ActionResult<IEnumerable<Manga>> SearchMangas([FromQuery] MangaFilter filters)
        {
            try
            {
                var filteredMangas = _mangaServices.SearchMangas(filters);
                return Ok(filteredMangas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Manga> GetMangaById([FromRoute] int id)
        {
            var manga = _mangaServices.GetMangaById(id);
            if (manga == null)
            {
                return NotFound();
            }
            return Ok(manga);
        }

        [HttpPost]
        public ActionResult AddManga([FromBody] Manga manga)
        {
            _mangaServices.AddManga(manga);
            return CreatedAtAction(nameof(GetMangaById), new { id = manga.Id }, manga);
        }
        [HttpPut("{id}")]
        public ActionResult UpdateManga(int id, [FromBody] Manga updatedManga)
        {
            _mangaServices.UpdateManga(id, updatedManga);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteManga(int id)
        {
            _mangaServices.DeleteManga(id);
            return NoContent();
        }

    }
}
