using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI_Udemy;
using PeliculasAPI_Udemy.DTOs;
using PeliculasAPI_Udemy.Entidades;
using PeliculasAPI_Udemy.Helper;
using PeliculasAPI_Udemy.Migrations;
using PeliculasAPI_Udemy.Services;

namespace PeliculasAPI_Udemy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileStorage fileStorage;
        private readonly string contenedor = "actores";

        public ActoresController( ApplicationDbContext context, IMapper mapper, IFileStorage fileStorage )
        {
            this._context = context;
            this._mapper = mapper;
            this.fileStorage = fileStorage;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActorDTO>>> Get([FromQuery] PaginacionDTO paginacion)
        {
            var queryable = _context.Actores.AsQueryable();
            await HttpContext.InsertarParametrosPaginacion(queryable, paginacion.RecordsPerPage);
            List<Actor> actores = await queryable.Paginar(paginacion).ToListAsync();
            
            return Ok(_mapper.Map<List<ActorDTO>>(actores));
        }

        [HttpGet("{id:int}", Name = "ObtenerActor")]
        public async Task<ActionResult<ActorDTO>> Get(int id)
        {
            Actor actor = await _context.Actores.FirstOrDefaultAsync(x => x.Id == id);

            if (actor == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ActorDTO>(actor));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ActorCreacionDTO actorCreacionDTO)
        {
            Actor actor = _mapper.Map<Actor>(actorCreacionDTO);

            if(actorCreacionDTO.Foto != null)
            {
                using(var memoryStream = new MemoryStream())
                {
                    await actorCreacionDTO.Foto.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(actorCreacionDTO.Foto.FileName);
                    actor.Foto = await fileStorage.SaveFile(contenido, extension, contenedor, actorCreacionDTO.Foto.ContentType);
                }
            }
            
            _context.Add(actor);
            await _context.SaveChangesAsync();
            ActorDTO actorDTO = _mapper.Map<ActorDTO>(actor);
            return new CreatedAtRouteResult("ObtenerActor", new { id = actor.Id }, actorDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] ActorCreacionDTO actorCreacionDTO)
        {
            Actor ActorDB = await _context.Actores.FirstOrDefaultAsync(x => x.Id == id);
            if(ActorDB == null) return NotFound();

            Actor actor = _mapper.Map(actorCreacionDTO, ActorDB);

            if (actorCreacionDTO.Foto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await actorCreacionDTO.Foto.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(actorCreacionDTO.Foto.FileName);
                    ActorDB.Foto = await fileStorage.EditFile(contenido, extension, contenedor, ActorDB.Foto, actorCreacionDTO.Foto.ContentType);
                }
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<ActorPatchDTO> document)
        {
            if(document == null) return BadRequest();

            Actor actorDB = await _context.Actores.FirstOrDefaultAsync(x => x.Id == id);

            if (actorDB == null) return NotFound();

            var actorPatch = _mapper.Map<ActorPatchDTO>(actorDB);


            document.ApplyTo(actorPatch, ModelState);

            if (!TryValidateModel(actorPatch)) return BadRequest(ModelState);

            var actor = _mapper.Map(actorPatch, actorDB);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            Actor actor = await _context.Actores.FirstOrDefaultAsync(x => x.Id == id);

            if (actor == null)
            {
                return NotFound();
            }

            _context.Remove(actor);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
