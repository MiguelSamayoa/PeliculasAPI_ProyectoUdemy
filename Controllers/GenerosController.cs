using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI_ProyectoUdemy.Entidades;

namespace PeliculasAPI_ProyectoUdemy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenerosController: ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GenerosController(ApplicationDbContext contex)
        {
            this._context = contex;
        }
        [HttpGet]
        public async Task<ActionResult<List<Genero>>> Get()
        {
            List<Genero> Generos = await _context.Generos.ToListAsync();

            return Ok(Generos);
        }
    }
}
