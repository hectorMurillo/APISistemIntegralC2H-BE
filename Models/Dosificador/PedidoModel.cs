using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dosificador
{
    public class PedidoModel
    {
        public int IdCatPedidos { get; set; }
		public int IdCatFormula { get; set; }
		public int FolioPedido { get; set; }
		public int CodCliente { get; set; }
		public int CodVendedor { get; set; }
		public int CodObra { get; set; }
		public decimal Cantidad { get; set; }
		public string HoraSalida { get; set; }
		public bool TieneNotaRemision { get; set; }
		public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public string Vendedor { get; set; }

    }
}
