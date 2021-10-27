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
        public decimal Precio { get; set; }
        public string Nomenclatura { get; set; }
        public string Descripcion { get; set; }
        public string Cliente { get; set; }
        public DateTime FechaCotizacion { get; set; }
    }
}
