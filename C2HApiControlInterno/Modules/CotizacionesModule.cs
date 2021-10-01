using DA.C2H;
using Models.Cotizaciones;
using Models.Pedidos;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarmPack.Classes;

namespace C2HApiControlInterno.Modules
{
    public class CotizacionesModule : NancyModule
    {
        private readonly DACotizaciones _DACotizaciones = null;

        public CotizacionesModule() : base("/cotizaciones")
        {
            this.RequiresAuthentication();

            _DACotizaciones = new DACotizaciones();
            Get("/obtener-cotizaciones/{cotizacion}/{fechaDesde}/{fechaHasta}", x => ObtenerCotizaciones(x));
            Post("guardar", _ => GuardarCotizacion());

        }

       
        private object ObtenerCotizaciones(dynamic x)
        {
            Result<List<Cotizacion>> result = new Result<List<Cotizacion>>();
            try
            {
                DateTime fechaDesde = x.fechaDesde;
                DateTime fechaHasta = x.fechaHasta;
                int cotizacion = x.cotizacion;

                //USUARIO PUEDE SURTIR SE REQUIERE MANDAR EL USUARIO 
                var codUsuario = this.BindUsuario().IdUsuario;
                result = _DACotizaciones.ObtenerCotizaciones(cotizacion, fechaDesde, fechaHasta, codUsuario);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GuardarCotizacion()
        {
            Result result = new Result();
            try
            {
                var codUsuario = this.BindUsuario().IdUsuario;
                var usuario = this.BindUsuario().Nombre;
                var cotizacion = this.Bind<CotizacionModel>();
                result = _DACotizaciones.GuardarCotizacion(cotizacion, codUsuario);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

    }
}