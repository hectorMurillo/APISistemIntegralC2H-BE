using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Pedidos
{
    public class PedidoCierre
    {
        public int IdCatPedidosCierres { get; set; }
        public int FolioPedido { get; set; }
        public decimal Cantidad { get; set; }
        public string Estatus { get; set; }

        public string CantidadFormato
        {
            get
            {
                return Cantidad.ToString() + "m³";
            }
        }

    }
}
