using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Bancos
{
    public class BancoModel
    {
        public int IdBanco { get; set; }

        public int Clave { get; set; }

        public string Nombre { get; set; }

        public string RazonSocial { get; set; }
    }
}
