using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Reportes
{
    public class RptAuxiliares
    {
        public string cliente { get; set; }
        public int codVendedor { get; set; }
        public string vendedor { get; set; }
        public int folioPedido { get; set; }
        public decimal cantidad { get; set; }
        public string obra { get; set; }
        public string hora { get; set; }
        public string fecha { get; set; }
        public string planta { get; set; }
        public string motivo { get; set; }
        public string equipo { get; set; }
        public string estatus { get; set; }


        public string fechaFormato
        {
            get
            {
                return fecha + " - " + hora;
            }
        }

        public string CantidadFormato
        {
            get
            {
                return cantidad.ToString() + "m³";
            }
        }
    }
}
