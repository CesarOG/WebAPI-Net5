using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("Producto")]
    [EnableCors("MyPolicy")]
    [Authorize]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly BD_KOA_SERVICEContext _context;

        public ProductosController(BD_KOA_SERVICEContext context)
        {
            _context = context;
        }

        // GET: api/Productos
        [HttpGet]
        [Route("ListarProductos")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return await _context.Productos.ToListAsync();
        }

        // GET: api/Productos/5
        [HttpGet]
        [Route("ObtenerProducto/{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        // PUT: api/Productos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("ActualizarProducto")]
        public async Task<IActionResult> PutProducto(Producto producto)
        {
            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(producto.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Productos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("RegistarProducto")]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            var result = await _context.SaveChangesAsync();
            if (result == 1)
                return Ok(result);
            else
                return NotFound();
        }

        // DELETE: api/Productos/5
        [HttpDelete]
        [Route("EliminarProducto/{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            string response;
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
                response = "1";
            }
            else
            {
                response = "0";
            }

            return Ok(response);
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }
    }
}
