using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaBackDotnet.Data;
using PruebaBackDotnet.DTOs;
using PruebaBackDotnet.Models.Entities;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly AplicationDbContext _context;
    private readonly IMapper _mapper;

    public UsuariosController(AplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsuarios()
    {
        var usuarios = await _context.Usuarios
            .Include(u => u.Departamento)
            .Include(u => u.Cargo)
            .ToListAsync();

        return _mapper.Map<List<UserDto>>(usuarios);
    }


    [HttpPost]
    public async Task<ActionResult<UserDto>> CrearUsuario([FromBody] UserDto crearUsuarioDto)
    {
        var usuario = _mapper.Map<User>(crearUsuarioDto);

        // Validar que existen el Departamento y Cargo
        var departamentoExiste = await _context.Departamentos.AnyAsync(d => d.Id == crearUsuarioDto.IdDepartamento);
        var cargoExiste = await _context.Cargos.AnyAsync(c => c.Id == crearUsuarioDto.IdCargo);

        if (!departamentoExiste || !cargoExiste)
        {
            return BadRequest("El Departamento o Cargo no existen.");
        }

        usuario.Id = Guid.NewGuid(); // Asegurarse de generar nuevo ID
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUsuarios), new { id = usuario.Id }, _mapper.Map<UserDto>(usuario));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarUsuario(Guid id, [FromBody] UserDto usuarioDto)
    {
        if (id != usuarioDto.Id) return BadRequest("IDs no coinciden.");

        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null) return NotFound();

        // Mapear propiedades nuevas
        _mapper.Map(usuarioDto, usuario);

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarUsuario(Guid id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null) return NotFound();

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
