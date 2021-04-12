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
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace Cursos.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class LoginController : Controller
    {
        private readonly CursosCTX ctx;
        private readonly IConfiguration config;

        public LoginController(CursosCTX _ctx, IConfiguration _configuration)
        {
            ctx = _ctx;
            config = _configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(LoginVM login)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            Usuarios usuario = await ctx.Usuarios.Where(x => x.Usuario == login.Usuario).FirstOrDefaultAsync();
            if(usuario == null)
            {
                return NotFound(ErrorHelper.Response(404, "Usuario no encontrado."));
            }

            if (HashHelper.CheckHash(login.Clave, usuario.Clave, usuario.Sal))
            {
                var secretKey = config.GetValue<string>("SecretKey");
                var key = Encoding.ASCII.GetBytes(secretKey);

                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, login.Usuario));

                var tokenDescriptor = new SecurityTokenDescriptor{
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddHours(4),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);

                string bearerToken = tokenHandler.WriteToken(createdToken);
                return Ok(bearerToken);
            }
            else
            {
                return Forbid();
            }

        }

        [HttpGet]
        public IActionResult Get()
        {
            var user = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);
            return Ok(user == null ? "" : user.Value);
        }
        
    }

    public class LoginVM
    {
        [Required(ErrorMessage = "El usuario es obligatorio.")]
        public string Usuario {get; set;}

        [Required(ErrorMessage = "La clave es obligatoria.")]
        public string Clave {get; set;}
    }
}