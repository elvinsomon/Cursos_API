using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Cursos.Models;
using Microsoft.EntityFrameworkCore;

namespace Cursos.Controllers
{
    [ApiController]
    [Route("[Controller]")]
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

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
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
                Estudiante.IdEstudiante = 0;
                ctx.Estudiante.Add(Estudiante);
                await ctx.SaveChangesAsync();
                return base.Created($"/Estudiante/{Estudiante.IdEstudiante}", Estudiante);
            }
        }

        
    }
}