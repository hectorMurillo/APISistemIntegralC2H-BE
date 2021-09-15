using Models.Cuentas;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WarmPack.Classes;
using Model = Models.Cuentas;

namespace C2HApiControlInterno.Modules
{
    public class CuentasModule : NancyModule
    { 
    private readonly DA.C2H.DACuentas _DACuentas = null;

    public CuentasModule() : base("/cuentas")
    {
        this.RequiresAuthentication();

        _DACuentas = new DA.C2H.DACuentas();
      //  Get("/", x => GetCuentas());
        Get("/{IdProveedor}", x => GetCuentas(x));
        Get("/cuenta/{IdCuenta}", x => GetCuenta(x));
        Post("/guardar", _ => PostCuenta());
        Post("/desactivar", _ => DesactivarCuenta());
        Get("/nombresbancos", x => GetBancos());

    }

      
    private object GetCuentas(dynamic x)
    {

        Result result = new Result();

        try
        {
                int idProveedor = x.idProveedor == null ? 0 : x.idProveedor;
                //Mando llamar al DA que manda llamar al stored y el resultado lo guardo en result
                result = _DACuentas.consultaCuentas(idProveedor);
        }
        catch (Exception ex)
        {
            result.Message = ex.Message;
        }
        return Response.AsJson(result);

    }

        private object GetCuenta(dynamic x)
        {

            Result result = new Result();

            try
            {
                int idCuenta = x.idCuenta == null ? 0 : x.idCuenta;
                //Mando llamar al DA que manda llamar al stored y el resultado lo guardo en result
                result = _DACuentas.consultaCuenta(idCuenta);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);

        }


        private object GetBancos()
        {

            Result result = new Result();

            try
            {
                //Mando llamar al DA que manda llamar al stored y el resultado lo guardo en result
                result = _DACuentas.consultaBancosNombres();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);

        }

        private object PostCuenta()
        {
            Result result = new Result();
            try
            {
                var Cuenta = this.Bind<Model.CuentaModel>();
                result = _DACuentas.CuentaGuardar(Cuenta);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object DesactivarCuenta()
        {
            Result result = new Result();
            try
            {
                var Cuenta = this.Bind<Model.CuentaModel>();
                result = _DACuentas.CuentaDesactivar(Cuenta);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
    }
}