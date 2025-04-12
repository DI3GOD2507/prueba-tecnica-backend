using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaBackDotnet.Data;
using PruebaBackDotnet.Models.Entities;

namespace PruebaBackDotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CargosController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public CargosController(AplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene la lista de todos los cargos disponibles.
        /// </summary>
        /// <returns>Lista de cargos.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cargo>>> ObtenerCargos()
        {
            var cargos = await _context.Cargos.ToListAsync();
            return Ok(cargos);
        }
    }
}
