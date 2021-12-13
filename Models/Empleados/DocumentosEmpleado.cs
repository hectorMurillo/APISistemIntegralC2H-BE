using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Empleados
{
    public class DocumentosEmpleado
    {
        public int Codigo { get; set; }
        public int CodigoEmpleado { get; set; }
        public int CodigoTipoDocumento { get; set; }
        public byte[] Archivo { get; set; }
        public string ArchivoBase64 { get; set; }
        public string Extension { get; set; }
    }
}
