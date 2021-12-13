using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Noticias
{
    public class Noticias
    {
        public int IdNoticias { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public byte[] ImagenBase64 { get; set; }
        public DateTime Fecha { get; set; }
        public string FechaFormat { 
            get 
            {
                return Fecha.ToString("dd-MM-yyyy");
            }   
        }
        public int CodUsuario { get; set; }
        public Boolean Activado { get; set; }
        public int Rank { get; set; }
        public string Imagen { get
            {
                try
                {
                    return Convert.ToBase64String(ImagenBase64);
                }
                catch (Exception)
                {
                    return "";
                }
                
            }
        }
    }
}
