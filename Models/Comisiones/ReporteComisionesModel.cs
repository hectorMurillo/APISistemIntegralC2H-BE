using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Comisiones
{
    public class ReporteComisionesModel
    {
        public int CodOperador { get; set; }
        public string Operador { get; set; }
        public int CantViajes { get; set; }
        public decimal TotalComisionViajes { get; set; }
        public int CantDesayunos { get; set; }
        public decimal TotalComisionDesayunos { get; set; }
        public int CantCenas { get; set; }
        public decimal TotalComisionCenas { get; set; }
        public int CantNocturnos { get; set; }
        public decimal TotalComisionNocturnos { get; set; }
        public int CantForaneos { get; set; }
        public decimal TotalComisionForaneos { get; set; }
        public int CantMetrosProducidos { get; set; }
        public decimal TotalMetrosProducidos { get; set; }
        public decimal Subtotal { get; set; }
    }
}
