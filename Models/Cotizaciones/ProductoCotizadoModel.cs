using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Cotizaciones
{
    public class ProductoCotizadoModel
    {
        public float Cantidad { get; set; }
        public int CodProducto { get; set; }
        public bool TieneDescuento { get; set; }
        public decimal PorcentajeDescuento { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioTotal { get; set; }
    }
}
