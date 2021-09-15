using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Cuentas
{
    public class CuentaModel
    {
        public int IdCuenta { get; set; }

        public int IdBanco { get; set; }

        public int IdProveedor { get; set; }

        public string NumeroCuenta { get; set; }


        public string CLABE { get; set; }

        public Boolean Activado { get; set; }

        public Boolean CBPrincipal { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}
