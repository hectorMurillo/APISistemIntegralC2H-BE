using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Herramientas
{
    public class ResultadoModel
    {
        public bool Value { get; set; }
        public int CodRespuesta { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
