using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaBackDotnet.Data;
using PruebaBackDotnet.Models.Entities;

namespace PruebaBackDotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartamentosController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public DepartamentosController(AplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene la lista de todos los departamentos disponibles.
        /// </summary>
        /// <returns>Lista de departamentos.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Departamento>>> ObtenerDepartamentos()
        {
            var departamentos = await _context.Departamentos.ToListAsync();
            return Ok(departamentos);
        }
    }
}
