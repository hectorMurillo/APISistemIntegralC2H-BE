using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Cobranza
{
    public class ListaPrecios
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }

        public string FechaInicio { get; set; }
        public DateTime FechaInicioDate
        {
            get
            {
                return FechaInicio == "" || FechaInicio == "null/null/null" ? new DateTime(1900, 1, 1) : Convert.ToDateTime(FechaInicio);
            }
        }

        public decimal MontoAAumentar { get; set; }
        public int Anio { get; set; }
    }
}
