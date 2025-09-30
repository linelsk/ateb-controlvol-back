namespace api.ateb.Models.Empresas
{
    public class ListaEmpresasDto
    {
        public string EmpresaId { get; set; }

        public string RazonSocial { get; set; }

        public string Rfc { get; set; }

        public string RfcRepresentanteLegal { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string VersionCtrVol { get; set; }

        public bool Activa { get; set; }
    }
}
