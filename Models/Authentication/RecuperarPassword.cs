using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Authentication
{
    public class RecuperarPassword
    {
        public string Usuario { get; set; }
        public string Referencia { get; set; }
        public string Contrasena { get; set; }
        public string NuevaContrasena { get; set; }
    }
}
