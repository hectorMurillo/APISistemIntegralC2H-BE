using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AgenteVentas
{
    public class PrecioProductoModel
    {
        public int CodProductoXCliente { get; set; }
        public int CodAgente { get; set; }
        public int CodProducto { get; set; }
        public int CodCliente { get; set; }
        public decimal Precio { get; set; }
    }
}
