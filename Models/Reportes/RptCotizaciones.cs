using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Reportes
{
    public class RptCotizaciones
    {
        public int Codigo { get; set; }
        public decimal Cantidad { get; set; }
        //public decimal PrecioUnitario { get; set; }
        //public decimal PrecioTotal { get; set; }
        public decimal Precio { get; set; }
        public decimal PorcentajeCotizacion { get; set; }
        public string Nomenclatura { get; set; }
        public string Descripcion { get; set; }
        public string Cliente { get; set; }
        public DateTime FechaCotizacion { get; set; }
        public decimal TotalCotizacion { get; set; }
        public int Folio { get; set; }
        public string Vendedor { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public string UnidadMedida { get; set; }
        public string PrecioFormato
        {
            get
            {
                return string.Format("{0:C}", Precio);
            }
        }
    }
}
