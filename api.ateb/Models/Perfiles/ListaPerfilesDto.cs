namespace api.ateb.Models.Perfiles
{
    public class ListaPerfilesDto
    {
        public long PerfilId { get; set; }

        public string Perfil { get; set; }

        public string Descripcion { get; set; }
        public List<string> ListaEmpresas { get; set; }
        public List<string> ListaAcciones { get; set; }
    }
}
