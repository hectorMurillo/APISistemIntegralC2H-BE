using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models.Usuario
{
    public class NotificacionModel
    {
        public Int64 CodNotificacion { get; set; }
        public int CodUsuario { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaNotificacion { get; set; }
        public string FechaNotifFormat
        {
            get
            {
                return FechaNotificacion.ToString("dd/MM/yyyy");
            }
        }
        public bool Visto { get; set; }
        public bool Redirigido { get; set; }
        public string URLDestino { get; set; }
    }
}
