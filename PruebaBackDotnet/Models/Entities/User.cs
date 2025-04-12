﻿namespace PruebaBackDotnet.Models.Entities
{
    public class User
    {

        public Guid Id { get; set; }
        public string Usuario { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }

        public Guid IdDepartamento { get; set; }
        public Departamento Departamento { get; set; }

        public Guid IdCargo { get; set; }
        public Cargo Cargo { get; set; }
    }
}
