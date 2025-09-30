using biz.ateb.Entities;

namespace api.ateb.Models.Proveedores
{
    public class ListaProveedoresDto
    {
        public string NoProveedor { get; set; }

        public string RfcP { get; set; }

        public string RazonSocialP { get; set; }

        public string NumPermiso { get; set; }

        public bool? Activo { get; set; }

    }
}
