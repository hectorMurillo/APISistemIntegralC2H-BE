using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Cotizaciones
{
    public class CotizacionModel
    {
        public int FolioCotizacion { get; set; }
        public int CodCliente { get; set; }
        public int CodObra { get; set; }
        public int codVendedor { get; set; }
        //public float Cantidad { get; set; }
        //public string Estatus { get; set; }
        //public int CodProducto { get; set; }

        public string OtroCliente { get; set; }
        public string OtraObra { get; set; }



        public List<ProductoCotizadoModel> productos { get; set; }
        public bool TieneDescuento { get; set; }
        public decimal PorcentajeDescuento { get; set; }
        //public string Observacion { get; set; }
        //public bool TieneFibra { get; set; }
        //public bool TieneImper { get; set; }
        //public bool Bombeado { get; set; }
        //public Decimal PrecioOriginal { get; set; }
        //public Decimal PrecioDescuento { get; set; }

    }
}
