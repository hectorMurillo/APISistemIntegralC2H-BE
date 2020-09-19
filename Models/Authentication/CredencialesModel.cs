using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Authentication
{
    public class CredencialesModel
    {
        public int IdUsuario { get; set; }
        public int IdSubUsuario { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
    }
}
