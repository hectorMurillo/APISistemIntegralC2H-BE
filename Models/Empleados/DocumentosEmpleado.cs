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
        public byte[] ArchivoBase64 { get; set; }
        public string Archivo { get
            {
                try
                {
                    return Convert.ToBase64String(ArchivoBase64);
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }
        public string Extension { get; set; }
    }
}
