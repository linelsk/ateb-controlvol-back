using biz.ateb.Entities;

namespace api.flexiform.rarp.Models.Usuarios
{
    public class UsuarioDto
    {
        public string UsuarioId { get; set; }

        public string Password { get; set; }

        public string Correo { get; set; }

        public string Nombre { get; set; }

        public long PerfilId { get; set; }

        public bool Activo { get; set; }

        public bool PrimeraVez { get; set; }

        public DateOnly UpdatePassword { get; set; }

        public virtual ICollection<HistoryPass> HistoryPasses { get; set; } = new List<HistoryPass>();

        public virtual Perfile Perfil { get; set; }

    }
}
