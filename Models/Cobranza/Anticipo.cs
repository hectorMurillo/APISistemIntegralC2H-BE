using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 namespace Models.Cobranza
{
    public class Anticipo
    {
        public int Codigo { get; set; }
        public string FechaRegistro { get; set; }
        public DateTime FechaRegistroDate
        {
            get
            {
                return FechaRegistro == "" || FechaRegistro == "null/null/null" ? new DateTime(1900, 1, 1) : Convert.ToDateTime(FechaRegistro);
            }
        }

        public string MontoActualFormato
        {
            get
            {
                return String.Format("{0:C}", MontoActual);
            }
        }


        public string MontoTotalFormato
        {
            get
            {
                return String.Format("{0:C}", MontoTotal);
            }
        }


        public decimal MontoTotal { get; set; }
        public int UsuarioRegistro { get; set; }
        public string NombreCliente { get; set; }
        public decimal MontoActual { get; set; }
        public int CodCliente { get; set; }
        public string CodObras { get; set; }
        public int CodListaPrec { get; set; }
        public string NombreListaPrec { get; set; }
        public int CodVendedor { get; set; }
        public string NombreVendedor { get; set; }
        public string Estatus { get; set; }
    }
}
