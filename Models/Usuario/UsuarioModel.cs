using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Usuario
{
    public class UsuarioModel
    {
        public int IdUsuario { get; set; }
        public int CodEmpleado { get; set; }
        //public int IdSubUsuario { get; set; }
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public int Agente { get; set; }
        public string NombreCompleto { get; set; }
        public string TipoEmpleado { get; set; }
        public string Estatus { get; set; }
        public string FechaUltimaConexion { get; set; }
    }
}
