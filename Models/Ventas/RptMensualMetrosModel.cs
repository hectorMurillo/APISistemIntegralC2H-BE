using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Ventas
{
    public class RptMensualMetrosModel
    {
        public int codVendedor { get; set; }
        public int codCliente { get; set; }
        public int codObra { get; set; }
        public int codProducto { get; set; }
        public string producto { get; set; }
    }
}
