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
        Get("/", x => GetCuentas());
        Post("/guardar", _ => PostProveedor());
        Get("/nombresbancos", x => GetBancos());

    }

    private object GetCuentas()
    {

        Result<List<CuentaModel>> result = new Result<List<CuentaModel>>();

        try
        {
            //Mando llamar al DA que manda llamar al stored y el resultado lo guardo en result
            result = _DACuentas.consultaCuentas();
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

        private object PostProveedor()
    {
        Result<List<int>> result = new Result<List<int>>();
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
}
}