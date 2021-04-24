using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Ventas
{
    public class DemandaArticulo
    {
        public int CodArticulo { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public int Demanda { get; set; }
        public int Surtido { get; set; }
        public int Negado { get; set; }
    }
}
