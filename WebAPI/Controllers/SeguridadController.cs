using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("Seguridad")]
    [EnableCors("MyPolicy")]
    [ApiController]
    [Authorize]
    public class SeguridadController : ControllerBase
    {
        private readonly BD_KOA_SERVICEContext _context;
        private IConfiguration _configuration;
        public SeguridadController(BD_KOA_SERVICEContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("ValidarUsuario")]
        public async Task<IActionResult> ValidarUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null)
                return BadRequest();

            var result = await _context.Usuarios.Where(o => o.NombreUsuario == usuario.NombreUsuario && o.Clave == usuario.Clave).FirstOrDefaultAsync();
            if (result != null)
            {
                var Token = BuildToken(usuario);
                usuario.Token = Token;
                return Ok(usuario);
            }
            else
            {
                return NotFound();
            }
        }
        private string BuildToken(Usuario usuario)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Auth:Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,usuario.NombreUsuario)
            };
            var token = new JwtSecurityToken(_configuration["Auth:Jwt:Issuer"],
                                             _configuration["Auth:Jwt:Audience"],
                                             claims,
                                             expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Auth:Jwt:TokenExpirationInMinutes"])),
                                             signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
