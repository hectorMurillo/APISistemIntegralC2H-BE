using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Equipos
{
    public class MantenimietoEquipo
    {
        public int CodUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Operador { get; set; }
        public string PrioridadOnkey { get; set; }
        public string Motivo { get; set; }
        public DateTime Fecha { get; set; }
        public bool EstatusNuevo { get; set; }
        public string FechaFormat
        {
            get
            {
                return Fecha.ToString("dd-MM-yyyy");
            }
        }

        public string HoraFormat
        {
            get
            {
                return Fecha.ToString("HH:mm:ss");
            }
        }
    }
}
