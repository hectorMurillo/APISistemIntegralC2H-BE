using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Configuracion
{
    public class FuncionModel
    {
        public int IdOpcion { get; set; }
        public string Opcion { get; set; }
        public bool Lectura { get; set; }
        public bool Escritura { get; set; }

    }
}
