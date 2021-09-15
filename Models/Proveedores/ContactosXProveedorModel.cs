using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Proveedores
{
    public class ContactosXProveedorModel
    {
        public int IdContacto { get; set; }

        public int IdProveedor { get; set; }

        public string Nombre { get; set; }

        public string Telefono { get; set; }

        public string TelefonoMovil { get; set; }

        public string Correo { get; set; }

        public Boolean CBPrincipal { get; set; }

        public bool Activado { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}
