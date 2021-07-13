using Models.Authentication;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
namespace API.Models
{
    public static class RefreshTokenRepository
    {
        public static void Guardar(RefreshTokenItemModel item)
        {
            Globales.ObtenerInformacionGlobal();
            //using (var db = new LiteDatabase("h:\\root\\home\\hector14-001\\www\\api\\temp\\refresh.db"))                        
            using (var db = new LiteDatabase(Globales.PathDB))
            {
                var tokens = db.GetCollection<RefreshTokenItemModel>("refreshTokens");
                tokens.Delete(x => x.Usuario.IdUsuario == item.Usuario.IdUsuario);


                tokens.Insert(item);

                tokens.EnsureIndex(x => x.Usuario.IdUsuario);

            }
        }

        public static RefreshTokenItemModel Encontrar(string uuid)
        {
            RefreshTokenItemModel item = null;
            Globales.ObtenerInformacionGlobal();

            //using (var db = new LiteDatabase("h:\\root\\home\\hector14-001\\www\\api\\temp\\refresh.db"))
            using (var db = new LiteDatabase(Globales.PathDB))
            {
                var tokens = db.GetCollection<RefreshTokenItemModel>("refreshTokens");

                item = tokens.FindOne(x => x.Uid == uuid);
            }

            return item;
        }
    }
}
