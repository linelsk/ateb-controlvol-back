namespace api.ateb.Models.Usuarios
{
    public class CrearUsuarioDto
    {
        public string UsuarioId { get; set; }

        public string Password { get; set; }

        public string Correo { get; set; }

        public string Nombre { get; set; }

        public long PerfilId { get; set; }

        public bool Activo { get; set; }
    }
}
