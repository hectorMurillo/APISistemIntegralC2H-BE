using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Clientes
{
    public class ClienteModel2
    {
        public int Codigo { get; set; }
        
        public string Nombre { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }

        public string NombreCompleto
        {
            get
            {
                return Nombre + " " + ApellidoP + " " + ApellidoM;
            }
        }
    }
}
