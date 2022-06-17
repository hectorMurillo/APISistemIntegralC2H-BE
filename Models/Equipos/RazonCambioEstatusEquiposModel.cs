using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Equipos
{
    public class RazonCambioEstatusEquiposModel:EquipoModel
    {
        public string Motivo { get; set; }
        public int CodUsuario { get; set; }
        public DateTime FechaCambio { get; set; }
        public string PrioridadOnKey { get; set; }
    }
}
