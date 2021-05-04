using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Reportes
{
    public class RptEntradasSalidas
    {
        public int codEquipo { get; set; }
        public int codOperador { get; set; }
        public decimal kilometrajeSalida { get; set; }
        public decimal horometrajeSalida { get; set; }
        public string fechaSalida { get; set; }
        public string horaSalida { get; set; }
        public decimal kilometrajeEntrada { get; set; }
        public decimal horometrajeEntrada { get; set; }
        public string fechaEntrada { get; set; }
        public string horaEntrada { get; set; }
        public int notaRemision { get; set; }
        public string tiempoFuera { get; set; }
        public decimal kilometrosRecorridos { get; set; }
        public decimal horometrajeRecorrido { get; set; }
        public string equipo { get; set; }
        public string operador { get; set; }
    }
}
