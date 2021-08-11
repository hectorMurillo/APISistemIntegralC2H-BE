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
        public int NumViajes { get; set; }
        public decimal TotalComisionViajes { get; set; }
        public int Desayunos { get; set; }
        public decimal TotalComisionDesayunos { get; set; }
        public int Cenas { get; set; }
        public decimal TotalComisionCenas { get; set; }
        public int Nocturnos { get; set; }
        public decimal TotalComisionNocturnos { get; set; }
        public int Foraneos { get; set; }
        public decimal TotalComisionForaneos { get; set; }
    }
}
