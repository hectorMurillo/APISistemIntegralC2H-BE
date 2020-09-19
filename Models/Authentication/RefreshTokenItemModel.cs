using Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Authentication
{
    public class RefreshTokenItemModel
    {
        public int Id { get; set; }
        public UsuarioModel Usuario { get; set; }
        public string Uid { get; set; }
    }
}
