using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Pedidos
{
    public class PedidoReagendarModel
    {
        public int FolioPedido { get; set; }
        public string HoraSalida { get; set; }
        public string FechaSalida { get; set; }
        public string Motivo { get; set; }
    }
}
