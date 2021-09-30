using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Clientes
{
    public class DocumentoModel
    {
        public int Codigo { get; set; }
        public int CodCliente { get; set; }
        public string Titulo { get; set; }
        public byte[] DocumentoBase64 { get; set; }
        public string Descripcion { get; set; }
        public string Documento
        {
            get
            {
                return Convert.ToBase64String(DocumentoBase64);
            }
        }
        public DateTime Fecha { get; set; }
    }
}
