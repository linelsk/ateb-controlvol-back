using biz.ateb.Entities;

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
        public List<string> listaPlantas{ get; set; } = new List<string>();

        public List<string> listaProveedores { get; set; } = new List<string>();
    }
}
