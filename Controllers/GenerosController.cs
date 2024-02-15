using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI_Udemy.Entidades;
using PeliculasAPI_Udemy.DTOs;

namespace PeliculasAPI_Udemy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenerosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper mapper;

        public GenerosController(ApplicationDbContext contex, IMapper mapper)
        {
            this._context = contex;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GeneroDTO>>> Get()
        {
            List<Genero> Generos = await _context.Generos.ToListAsync();

            return Ok(mapper.Map<List<GeneroDTO>>(Generos));
        }

        [HttpGet("{id:int}", Name = "ObtenerGenero")]
        public async Task<ActionResult<GeneroDTO>> Get(int id)
        {
            Genero Genero = await _context.Generos.FirstOrDefaultAsync(x => x.Id == id);

            if (Genero == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<GeneroDTO>(Genero));
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GeneroCreacionDTO generoCreacionDTO)
        {
            Genero genero = mapper.Map<Genero>(generoCreacionDTO);
            _context.Add(genero);
            //await _context.SaveChangesAsync();

            GeneroDTO generoDTO = mapper.Map<GeneroDTO>(genero);
            return new CreatedAtRouteResult("ObtenerGenero", new { id = generoDTO.Id }, generoDTO);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] GeneroCreacionDTO generoCreacionDTO)
        {
            Genero genero = mapper.Map<Genero>(generoCreacionDTO);
            genero.Id = id;
            _context.Entry(genero).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            Genero genero = await _context.Generos.FirstOrDefaultAsync(x => x.Id == id);

            if (genero == null)
            {
                return NotFound();
            }

            _context.Remove(genero);
            await _context.SaveChangesAsync();
            return NoContent();
        }   
    }
}
