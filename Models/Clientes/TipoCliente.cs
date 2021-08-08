using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Clientes
{
    public class TipoCliente
    {
        public int CodTipoCliente { get; set; }
        public string Descripcion { get; set; }
        public string Observacion { get; set; }
        public string Estatus { get; set; }
    }
}
