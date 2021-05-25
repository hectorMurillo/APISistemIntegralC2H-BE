using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Clientes
{
    public class ContactoXClienteModel
    {
        public int CodContacto { get; set; }
        public int CodCliente { get; set; }
        public int Consecutivo { get; set; }
        public string NombreContacto { get; set; }
        public string Telefono { get; set; }
        public string TelefonoMovil { get; set; }
        public string Correo { get; set; }
        public bool ContactoPrincipal { get; set; }        
    }
}
