using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Empleados
{
    public class PersonalCargaDiesel
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        public bool Estatus { get; set; }

        public string NombreCompleto
        {
            get
            {
                return Nombre + " " + ApellidoP + " " + ApellidoM;
            }
            set
            {

            }
        }
    }
}
