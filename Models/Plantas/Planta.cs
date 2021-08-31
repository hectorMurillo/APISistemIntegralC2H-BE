using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Plantas
{
    public class Planta
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public bool Estatus { get; set; }
        public string Coordenadas { get; set; }
    }
}
