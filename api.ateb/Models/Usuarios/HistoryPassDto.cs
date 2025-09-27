namespace api.ateb.Models.Usuarios
{
    public class HistoryPassDto
    {
        public string UsuarioId { get; set; }

        public long Consecutivo { get; set; }

        public string HashPassword { get; set; }

        public DateTime Fecha { get; set; }
    }
}
