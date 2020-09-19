using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Equipos
{
    public class TanquesCombustibleModel
    {
        public int Codigo { get; set; }
        public int CodEmpresa { get; set; }
        public string Identificador { get; set; }
        public string Empresa { get; set; }
        public int Capacidad { get; set; }
        public string CapacidadFormat { get { return String.Format("{0:n0}", Capacidad); } }
        public string Consumible { get; set; }
        public bool EsInflamable { get; set; }
    }
}
