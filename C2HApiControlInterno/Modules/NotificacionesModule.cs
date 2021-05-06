using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C2HApiControlInterno.Modules
{
    public class NotificacionesModule : NancyModule
    {
        private readonly DA.C2H.DANotificaciones _DANotificaciones = null;
        public NotificacionesModule() : base("/notificaciones")
        {
            _DANotificaciones = new DA.C2H.DANotificaciones();
            Get("/ultimasNotificaciones", _ => getUltimasNotificaciones());
            Post("/redireccionadas", _ => postNotifRedireccionadas());
            Post("/marcarVistas", _ => postNotifVistas());
        }
        private object postNotifVistas()
        {
            var p = this.BindUsuario();
            var Result = _DANotificaciones.NotificacionesLeidas(p.IdUsuario);
            return Response.AsJson(Result);
        }

        private object postNotifRedireccionadas()
        {
            var p = this.BindUsuario();
            var datos = this.Bind<DatoNotificacion>();
            var Result = _DANotificaciones.NotificacionesRedireccionadas(p.IdUsuario, datos.CodigoNotificacion, datos.todasRedirigidas);
            return Response.AsJson(Result);
        }

        private object getUltimasNotificaciones()
        {
            var p = this.BindUsuario();
            var Result = _DANotificaciones.UltimasNotificaciones(p.IdUsuario);

            return Response.AsJson(Result);
        }
    }
    class DatoNotificacion
    {
        public bool todasRedirigidas { get; set; }
        public int CodigoNotificacion { get; set; }
    }
}
