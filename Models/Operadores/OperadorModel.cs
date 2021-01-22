using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Operadores
{
    public class OperadorModel
    {
        public int codigo { get; set; }

        public string nombre { get; set; }
        public string apellidoP { get; set; }
        public string apellidoM { get; set; }

        public string nombreCompleto
        {
            get
            {
                return nombre + " " + apellidoP + " " + apellidoM;
            }
        }


        public string rFC { get; set; }

        public string celular { get; set; }
        public string correo { get; set; }


        public int codigoTipoEmpleado { get; set; }
        public bool estatus { get; set; }
        
        //public string telefono { get; set; }

        public string tipoEmpleado { get; set; }
        public DateTime fechaRegistro { get; set; }
        public object rFCValidate { get; set; }
    }
}
