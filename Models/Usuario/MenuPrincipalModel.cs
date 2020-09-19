using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Usuario
{
    public class UsuarioMenuPrincipalModel
    {
        public int CodModulo { get; set; }
        public int CodFuncion { get; set; }
        public int CodSubFuncion { get; set; }
        public string Funcion { get; set; }
        public string SubFuncion { get; set; }
    }
}
