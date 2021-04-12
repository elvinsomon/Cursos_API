using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Cursos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
using Cursos.Helper;
using Microsoft.AspNetCore.Authorization;

namespace Cursos.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Authorize]
    public class EstudiantesController : ControllerBase
    {
        private readonly CursosCTX ctx;

        public EstudiantesController(CursosCTX ctx)
        {
            this.ctx = ctx;
        }

        [HttpGet]
        public async Task<IEnumerable<Estudiante>> Get()
        {
            return await ctx.Estudiante.ToArrayAsync();
        }

        [HttpGet("{Id}", Name="GetEstudiante")]
        public async Task<IActionResult> Get(int Id, string codigo)
        {
            var estudiante = await ctx.Estudiante.FindAsync(Id);
            
            if (estudiante == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(estudiante);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Estudiante Estudiante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(); //400
            }
            else
            {
                if( await ctx.Estudiante.Where(x => x.Codigo == Estudiante.Codigo).AnyAsync() )
                {
                    return BadRequest(ErrorHelper.Response(400, $"El codigo {Estudiante.Codigo} ya existe."));
                }
                Estudiante.IdEstudiante = 0;
                ctx.Estudiante.Add(Estudiante);
                await ctx.SaveChangesAsync();
                return Created($"/Estudiante/{Estudiante.IdEstudiante}", Estudiante);

                //Created At Action
                //return CreatedAtAction(nameof(Get), new{id = Estudiante.IdEstudiante, Codigo=Estudiante.Codigo}, Estudiante);

                //Created At Route
                //return CreatedAtRoute("GetEstudiante", new { id = Estudiante.IdEstudiante, codigo = Estudiante.Codigo }, Estudiante);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Estudiante estudiante)
        {
            if(estudiante.IdEstudiante == 0)
            {
                estudiante.IdEstudiante = id;
            }
            if(estudiante.IdEstudiante != id)
            {
                return BadRequest();
            }


            if(!await ctx.Estudiante.Where(x => x.IdEstudiante == id).AsNoTracking().AnyAsync())
            {
                return NotFound();
            }

            
            if(await ctx.Estudiante.Where(x => x.Codigo == estudiante.Codigo && x.IdEstudiante != estudiante.IdEstudiante).AnyAsync())
            {
                return BadRequest(ErrorHelper.Response(400, $"El codigo {estudiante.Codigo} ya existe."));
            }
            ctx.Entry(estudiante).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("CambiarCodigo/{id}")]
        public async Task<IActionResult> CambiarCodigo(int id, [FromQuery] string codigo)
        {
            var Estudiante = await ctx.Estudiante.FindAsync(id);

            if (string.IsNullOrEmpty(codigo))
            {
                return BadRequest(ErrorHelper.Response(400, "El codigo no puede ser vacio."));
            }

            if(Estudiante == null)
            {
                return NotFound();
            }

            if(await ctx.Estudiante.Where(x => x.Codigo == codigo && x.IdEstudiante != id).AnyAsync())
            {
                return BadRequest(ErrorHelper.Response(400, $"El codigo {codigo} ya existe."));
            }

            Estudiante.Codigo = codigo;
            await ctx.SaveChangesAsync();
            return StatusCode(201, Estudiante);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Estudiante = await ctx.Estudiante.FindAsync(id);

            if(Estudiante == null)
            {
                return NotFound();
            }

            ctx.Estudiante.Remove(Estudiante);
            await ctx.SaveChangesAsync();
            return NoContent();
        }
    }
}