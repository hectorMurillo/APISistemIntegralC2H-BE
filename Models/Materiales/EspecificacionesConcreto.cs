using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Materiales
{
    public class EspecificacionesConcreto
    {
        public int Codigo { get; set; }
        public string DescripcionCorta { get; set; }
        public string Descripcion { get; set; }
        public bool Estatus { get; set; }
    }
}
