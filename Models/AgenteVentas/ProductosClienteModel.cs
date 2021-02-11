using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AgenteVentas
{
    public class ProductosClienteModel
    {
        public int Codigo { get; set; }
		public int CodCliente { get; set; }
		public int CodAgente { get; set; }
		public int CodProducto { get; set; }
		public decimal Precio { get; set; }
		public DateTime Fecha { get; set; }

	}
}
