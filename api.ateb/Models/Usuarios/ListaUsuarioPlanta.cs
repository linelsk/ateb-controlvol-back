namespace api.ateb.Models.Usuarios
{
    public class ListaUsuarioPlanta
    {
        public string UsuarioId { get; set; }

        public string Password { get; set; }

        public string Correo { get; set; }

        public string Nombre { get; set; }

        public long PerfilId { get; set; }

        public bool Activo { get; set; }

        public bool PrimeraVez { get; set; }

        public DateOnly UpdatePassword { get; set; }

        public List<string> ListaPlantas { get; set; }
        public string PerfilDescripcion { get; set; }

    }
}
