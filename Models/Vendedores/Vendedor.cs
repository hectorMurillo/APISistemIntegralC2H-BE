using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Vendedores
{
    public class Vendedor
    {
        public int codigo { get; set; }
        public string nombre { get; set; }
        public string apellidoP { get; set; }
        public string apellidoM { get; set; }
        public string nombreCompleto { get; set; }
        public string rFC { get; set; }
        public int codigoTipoEmpleado { get; set; }
        public string estatus { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public string celular { get; set; }
        public string tipoEmpleado { get; set; }
    }
}
