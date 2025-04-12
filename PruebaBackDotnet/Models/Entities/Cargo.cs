namespace PruebaBackDotnet.Models.Entities
{
    public class Cargo
    {

        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public int IdUsuarioCreacion { get; set; }

        public ICollection<User> Usuarios { get; set; }
    }
}
