using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Reportes
{
    public class RptPedidosReagendados
    {
        public int codCliente { get; set; }
        public string cliente { get; set; }
        public int codVendedor { get; set; }
        public string vendedor { get; set; }
        public int folioPedido { get; set; }
        public decimal cantidad { get; set; }
        public string horaAnterior { get; set; }
        public string fechaAnterior { get; set; }
        public string horaNueva { get; set; }
        public string fechaNueva { get; set; }
        public string planta { get; set; }
        public string motivo { get; set; }

        public string fechaAnteriorFormato
        {
            get
            {
                return fechaAnterior + " - " + horaAnterior;
            }
        }

        public string fechaNuevaFormato
        {
            get
            {
                return fechaNueva + " - " + horaNueva;
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
