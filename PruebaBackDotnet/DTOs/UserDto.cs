// DTOs/UserDto.cs
namespace PruebaBackDotnet.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }  // Para PUT, ignorado en POST
        public string Usuario { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public Guid IdDepartamento { get; set; }
        public Guid IdCargo { get; set; }

        // Estos solo los devuelves en GET
        public DepartamentoDto? Departamento { get; set; }
        public CargoDto? Cargo { get; set; }
    }
}
