using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dosificador
{
    public class FormulaModel
    {
        public int Codigo { get; set; }
        public string Nomenclatura { get; set; }
        public string Descripcion { get; set; }
        public string Edad { get; set; }
        public int Resistencia { get; set; }
        public int TMA { get; set; }
        public string Revenimiento { get; set; }
        public string Adicionantes { get; set; }
    }
}
