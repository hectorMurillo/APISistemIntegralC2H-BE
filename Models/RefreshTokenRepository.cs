using Models.Authentication;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Models
{
    public static class RefreshTokenRepository
    {
        public static void Guardar(RefreshTokenItemModel item)
        {
            using (var db = new LiteDatabase("h:\\root\\home\\hector14-001\\www\\api\\temp\\refresh.db"))
            //using (var db = new LiteDatabase(@"c:\temp\refresh.db"))
            {
                var tokens = db.GetCollection<RefreshTokenItemModel>("refreshTokens");
                tokens.DeleteMany(x => x.Usuario.IdUsuario == item.Usuario.IdUsuario);


                tokens.Insert(item);

                tokens.EnsureIndex(x => x.Usuario.IdUsuario);

            }
        }

        public static RefreshTokenItemModel Encontrar(string uuid)
        {
            RefreshTokenItemModel item = null;

            using (var db = new LiteDatabase("h:\\root\\home\\hector14-001\\www\\api\\temp\\refresh.db"))
            //using (var db = new LiteDatabase(@"c:\temp\refresh.db"))
            {
                var tokens = db.GetCollection<RefreshTokenItemModel>("refreshTokens");

                item = tokens.FindOne(x => x.Uid == uuid);
            }

            return item;
        }
    }
}
