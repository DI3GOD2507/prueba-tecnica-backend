using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // Necesario para ILogger
using PruebaBackDotnet.Data;
using PruebaBackDotnet.DTOs;
using PruebaBackDotnet.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaBackDotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UsuariosController> _logger; // Inyectar ILogger

        // Inyectar ILogger en el constructor
        public UsuariosController(AplicationDbContext context, IMapper mapper, ILogger<UsuariosController> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Obtiene la lista completa de usuarios.
        /// </summary>
        /// <response code="200">Retorna la lista de usuarios.</response>
        /// <response code="500">Si ocurre un error inesperado en el servidor.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsuarios()
        {
            try
            {
                var usuarios = await _context.Usuarios
                    .Include(u => u.Departamento)
                    .Include(u => u.Cargo)
                    .AsNoTracking()
                    .ToListAsync();

                var usuariosDto = _mapper.Map<List<UserDto>>(usuarios);
                return Ok(usuariosDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener la lista de usuarios.");
                return StatusCode(500, "Ocurrió un error interno en el servidor.");
            }
        }

        /// <summary>
        /// Obtiene un usuario específico por su ID.
        /// </summary>
        /// <param name="id">El ID (Guid) del usuario.</param>
        /// <response code="200">Retorna el usuario encontrado.</response>
        /// <response code="404">Si el usuario con el ID especificado no se encuentra.</response>
        /// <response code="500">Si ocurre un error inesperado en el servidor.</response>
        [HttpGet("{id}", Name = "GetUsuarioById")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<UserDto>> GetUsuarioById(Guid id)
        {
            try
            {
                var usuario = await _context.Usuarios
                    .Include(u => u.Departamento)
                    .Include(u => u.Cargo)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (usuario == null)
                {
                    _logger.LogWarning("Intento de obtener usuario con ID {UserId} no encontrado.", id);
                    return NotFound($"Usuario con ID {id} no encontrado.");
                }

                var usuarioDto = _mapper.Map<UserDto>(usuario);
                return Ok(usuarioDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener el usuario con ID {UserId}.", id);
                return StatusCode(500, "Ocurrió un error interno en el servidor.");
            }
        }

        /// <summary>
        /// Crea un nuevo usuario.
        /// </summary>
        /// <param name="crearUsuarioDto">DTO del usuario. Debe incluir IdDepartamento e IdCargo válidos.</param>
        /// <response code="201">Usuario creado exitosamente. Retorna el usuario creado.</response>
        /// <response code="400">Si los datos proporcionados son inválidos o incompletos.</response>
        /// <response code="500">Si ocurre un error inesperado durante la creación.</response>
        [HttpPost]
        [ProducesResponseType(typeof(UserDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<UserDto>> CrearUsuario([FromBody] UserDto crearUsuarioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (crearUsuarioDto == null) return BadRequest("Datos del usuario no proporcionados.");

            try
            {
                var departamentoExiste = await _context.Departamentos.AnyAsync(d => d.Id == crearUsuarioDto.IdDepartamento);
                var cargoExiste = await _context.Cargos.AnyAsync(c => c.Id == crearUsuarioDto.IdCargo);

                if (!departamentoExiste || !cargoExiste)
                {
                    _logger.LogWarning("Intento de crear usuario con Departamento ID {DepartamentoId} o Cargo ID {CargoId} inválidos.",
                        crearUsuarioDto.IdDepartamento, crearUsuarioDto.IdCargo);
                    return BadRequest("El Departamento o Cargo especificado no existe.");
                }

                var nuevoUsuario = _mapper.Map<User>(crearUsuarioDto);

                _context.Usuarios.Add(nuevoUsuario);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Usuario {Username} creado con ID {UserId}.", nuevoUsuario.Usuario, nuevoUsuario.Id);

                var usuarioCreadoConRelaciones = await _context.Usuarios
                    .Include(u => u.Departamento)
                    .Include(u => u.Cargo)
                    .FirstOrDefaultAsync(u => u.Id == nuevoUsuario.Id);

                if (usuarioCreadoConRelaciones == null)
                {
                    _logger.LogError("Error crítico: No se pudo encontrar el usuario con ID {UserId} inmediatamente después de crearlo.", nuevoUsuario.Id);
                    return StatusCode(500, "Error interno al recuperar el usuario después de la creación.");
                }

                var usuarioDtoRespuesta = _mapper.Map<UserDto>(usuarioCreadoConRelaciones);
                return CreatedAtAction(nameof(GetUsuarioById), new { id = nuevoUsuario.Id }, usuarioDtoRespuesta);
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error de base de datos al crear el usuario {Username}.", crearUsuarioDto.Usuario);
                return StatusCode(500, "Error al guardar los datos en la base de datos.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al crear el usuario {Username}.", crearUsuarioDto.Usuario);
                return StatusCode(500, "Ocurrió un error interno en el servidor durante la creación.");
            }
        }

        /// <summary>
        /// Actualiza un usuario existente.
        /// </summary>
        /// <param name="id">ID del usuario a actualizar (URL).</param>
        /// <param name="usuarioDto">DTO con datos actualizados. Debe incluir Id, IdDepartamento e IdCargo válidos.</param>
        /// <response code="204">Usuario actualizado exitosamente.</response>
        /// <response code="400">Si los datos proporcionados son inválidos o los IDs no coinciden.</response>
        /// <response code="404">Si el usuario con el ID especificado no se encuentra.</response>
        /// <response code="500">Si ocurre un error inesperado durante la actualización.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ActualizarUsuario(Guid id, [FromBody] UserDto usuarioDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (usuarioDto == null || id != usuarioDto.Id)
                return BadRequest("Datos inválidos o IDs no coinciden.");

            try
            {
                var usuarioExistente = await _context.Usuarios.FindAsync(id);
                if (usuarioExistente == null)
                {
                    _logger.LogWarning("Intento de actualizar usuario con ID {UserId} no encontrado.", id);
                    return NotFound($"Usuario con ID {id} no encontrado.");
                }

                var departamentoExiste = await _context.Departamentos.AnyAsync(d => d.Id == usuarioDto.IdDepartamento);
                var cargoExiste = await _context.Cargos.AnyAsync(c => c.Id == usuarioDto.IdCargo);
                if (!departamentoExiste || !cargoExiste)
                {
                    _logger.LogWarning("Intento de actualizar usuario ID {UserId} con Departamento ID {DepartamentoId} o Cargo ID {CargoId} inválidos.",
                       id, usuarioDto.IdDepartamento, usuarioDto.IdCargo);
                    return BadRequest("El nuevo Departamento o Cargo especificado no existe.");
                }

                _mapper.Map(usuarioDto, usuarioExistente);

                await _context.SaveChangesAsync();
                _logger.LogInformation("Usuario con ID {UserId} actualizado.", id);

                return NoContent(); // HTTP 204
            }
            catch (DbUpdateConcurrencyException concEx)
            {
                _logger.LogWarning(concEx, "Conflicto de concurrencia al actualizar usuario con ID {UserId}.", id);
                if (!await _context.Usuarios.AnyAsync(e => e.Id == id)) return NotFound($"Usuario con ID {id} no encontrado (posiblemente eliminado).");
                else return Conflict("El registro fue modificado por otro usuario. Por favor, recargue e intente de nuevo.");
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error de base de datos al actualizar el usuario con ID {UserId}.", id);
                return StatusCode(500, "Error al guardar los datos en la base de datos.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al actualizar el usuario con ID {UserId}.", id);
                return StatusCode(500, "Ocurrió un error interno en el servidor durante la actualización.");
            }
        }

        /// <summary>
        /// Elimina un usuario por su ID.
        /// </summary>
        /// <param name="id">ID (Guid) del usuario a eliminar.</param>
        /// <response code="204">Usuario eliminado exitosamente.</response>
        /// <response code="404">Si el usuario con el ID especificado no se encuentra.</response>
        /// <response code="500">Si ocurre un error inesperado durante la eliminación.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> EliminarUsuario(Guid id)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);
                if (usuario == null)
                {
                    _logger.LogWarning("Intento de eliminar usuario con ID {UserId} no encontrado.", id);
                    return NotFound($"Usuario con ID {id} no encontrado.");
                }

                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Usuario con ID {UserId} eliminado.", id);

                return NoContent(); // HTTP 204
            }
            catch (DbUpdateException dbEx) 
            {
                _logger.LogError(dbEx, "Error de base de datos al eliminar el usuario con ID {UserId}.", id);
                return StatusCode(500, "Error al eliminar los datos en la base de datos. Verifique si el usuario tiene registros asociados.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al eliminar el usuario con ID {UserId}.", id);
                return StatusCode(500, "Ocurrió un error interno en el servidor durante la eliminación.");
            }
        }
    }
}