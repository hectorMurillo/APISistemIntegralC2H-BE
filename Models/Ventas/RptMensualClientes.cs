using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Ventas
{
    public class RptMensualClientes
    {
        public decimal Cantidad { get; set; }
        public string Cliente { get; set; }
        public int CodCliente { get; set; }
        public string Obra { get; set; }
        public string Agente { get; set; }
        public DateTime Fecha { get; set; }
    }
}
