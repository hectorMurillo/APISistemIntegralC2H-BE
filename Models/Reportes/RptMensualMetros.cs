using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Reportes
{
    public class RptMensualMetros
    {
        public decimal Cantidad { get; set; }
        public string Cliente { get; set; }
        public string Obra { get; set; }
        public string Agente { get; set; }
        public DateTime Fecha { get; set; }
        public string Producto { get; set; }
        public string Nomenclatura { get; set; }
    }
}
