using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("Roles")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly BD_KOA_SERVICEContext _context;

        public RolController(BD_KOA_SERVICEContext context)
        {
            _context = context;
        }

        // GET: api/Rol
        [HttpGet]
        [Route("ListarRoles")]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(new { data = await _context.Roles.ToListAsync() });
        }

        // GET: api/Rol/5
        [HttpGet]
        [Route("ObtenerRol/{id}")]
        public async Task<IActionResult> GetRol(Guid id)
        {
            var rol = await _context.Roles.FindAsync(id);

            if (rol == null)
            {
                return NotFound();
            }

            return Ok(new { data = rol });
        }

        // PUT: api/Rol/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("ActualizarRol")]
        public async Task<IActionResult> PutRol(Rol rol)
        {
            _context.Entry(rol).State = EntityState.Modified;

            try
            {
                var result = await _context.SaveChangesAsync();
                if (result == 1)
                    return Ok(new { mensaje = "Rol actualizado correctamente." });
                else
                    return NotFound(new { mensaje = "Ocurrio un error al actualizar el rol" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolExists(rol.Id_Rol))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Rol
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("RegistrarRol")]
        public async Task<IActionResult> PostRol(Rol rol)
        {
            rol.Estado = true;
            rol.FechaCreacion = DateTime.Now;
            _context.Roles.Add(rol);
            var result = await _context.SaveChangesAsync();

            if (result == 1)
                return Ok(new { mensaje = "Rol registrado correctamente." });
            else
                return NotFound(new { mensaje = "Ocurrio un error al registrar el rol" });
        }

        // DELETE: api/Rol/5
        [HttpDelete]
        [Route("EliminarRol/{id}")]
        public async Task<IActionResult> DeleteRol(Guid id)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(rol);
            var result = await _context.SaveChangesAsync();

            if (result == 1)
                return Ok(new { mensaje = "Rol eliminado correctamente." });
            else
                return NotFound(new { mensaje = "Ocurrio un error al eliminar el rol" });
        }

        private bool RolExists(Guid id)
        {
            return _context.Roles.Any(e => e.Id_Rol == id);
        }
    }
}
