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

namespace Cursos.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsuariosController : ControllerBase
    {
        CursosCTX ctx;
        public UsuariosController(CursosCTX _ctx)
        {
            ctx = _ctx;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Usuarios usuario)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            if(await ctx.Usuarios.Where(x => x.Usuario == usuario.Usuario).AnyAsync()){
                return BadRequest(ErrorHelper.Response(400, $"El usuario {usuario.Usuario} ya existe."));
            }

            HashedPassword password = HashHelper.Hash(usuario.Clave);

            usuario.Clave = password.Password;
            usuario.Sal = password.Salt;
            ctx.Usuarios.Add(usuario);
            await ctx.SaveChangesAsync();

            return Ok(new UsuarioVM {
                IdUsuario = usuario.IdUsuario,
                Name = usuario.Usuario
            });

        }
    }

    public class UsuarioVM
    {
        public int IdUsuario {get; set;}
        public string Name {get; set;}
    }
}