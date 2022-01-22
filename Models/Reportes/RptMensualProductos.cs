using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Reportes
{
    public class RptMensualProductos
    {
        public int codProducto { get; set; }
        public int codVendedor { get; set; }
        public decimal cantidad { get; set; }
        public string descripcion { get; set; }
        public string nomenclatura { get; set; }
        public string agente { get; set; }
    }
}
