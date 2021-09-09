using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Cuentas
{
    public class BancoXCuenta
    {
        public int IdCuenta { get; set; }

        public int IdBanco { get; set; }

        public int IdProveedor { get; set; }

        public string NumeroCuenta { get; set; }


        public string CLABE { get; set; }

        public Boolean CBPrincipal { get; set; }

        public int Clave { get; set; }

        public string Nombre { get; set; }

        public string RazonSocial { get; set; }
    }
}
