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
        public int Desayunos { get; set; }
        public int Cenas { get; set; }
        public int Nocturnos { get; set; }
        public int Foraneos { get; set; }
    }
}
