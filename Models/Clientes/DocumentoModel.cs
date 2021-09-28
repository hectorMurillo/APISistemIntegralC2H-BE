using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Clientes
{
    public class DocumentoModel
    {
        public int IdDocumento { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public Object Imagen { get; set; }
    }
}
