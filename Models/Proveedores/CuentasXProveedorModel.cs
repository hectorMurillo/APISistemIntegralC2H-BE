using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Proveedores
{
    public class CuentasXProveedorModel
    {
        public int IdCuenta { get; set; }

        public int IdBanco { get; set; }

        public int CodReferencia { get; set; }

        public string NumeroCuenta { get; set; }

        public string Tipo { get; set; }

        public string CLABE { get; set; }

        public Boolean CBPrincipal { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}
