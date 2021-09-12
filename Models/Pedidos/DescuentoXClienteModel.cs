using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Pedidos
{
    public class DescuentoXClienteModel
    {
        public int IdCatDescuentoPedidoXCliente { get; set; }
        public int CodCliente { get; set; }
        public string Cliente { get; set; }
        public decimal PorcentajeDescuento { get; set; }
    }
}
