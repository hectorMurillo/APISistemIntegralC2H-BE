using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;
using Model = Models.Equipos;
namespace DA.C2H
{
    public class DAEquipo
    {
        private readonly Conexion _conexion = null;
        public DAEquipo()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }
        public Result<List<Model.EquipoModel>> ConsultaEquipos(int codEquipo)
        {
            Result<List<Model.EquipoModel>> result = new Result<List<Model.EquipoModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodEquipo", ConexionDbType.Int, codEquipo);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Model.EquipoModel>("ProcCatEquiposCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result GuardarModelo(Model.ModeloEquipoModel modelo)
        {
            Result result = new Result();

            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodigo", ConexionDbType.Int, modelo.Codigo);
                parametros.Add("@pDescripcion", ConexionDbType.VarChar, modelo.Descripcion);
                parametros.Add("@pEstatus", ConexionDbType.Bit, modelo.Estatus);
                parametros.Add("@pCodMarca", ConexionDbType.Int, modelo.CodMarca);
                parametros.Add("@pAño", ConexionDbType.Int, modelo.Año);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcCatModeloGuardar", parametros);

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result GuardarTanque(Model.TanquesCombustibleModel tanque)
        {
            Result result = new Result();

            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodigo", ConexionDbType.Int, tanque.Codigo);
                parametros.Add("@pIdentificador", ConexionDbType.VarChar, tanque.Identificador);
                parametros.Add("@pCodEmpresa", ConexionDbType.Int, tanque.CodEmpresa);
                parametros.Add("@pCapacidad", ConexionDbType.Int, tanque.Capacidad);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcTanquesCombustibleGuardar", parametros);

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<Model.TanquesCombustibleModel>> consultaTanque(int codTanque)
        {
            Result<List<Model.TanquesCombustibleModel>> result = new Result<List<Model.TanquesCombustibleModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodTanque", ConexionDbType.Int, codTanque);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Model.TanquesCombustibleModel>("ProcTanquesCombustibleCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<Model.MarcaEquipoModel>> consultaMarca(int codMarca)
        {
            Result<List<Model.MarcaEquipoModel>> result = new Result<List<Model.MarcaEquipoModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodMarca", ConexionDbType.Int, codMarca);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Model.MarcaEquipoModel>("ProcMarcasEquipoCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result GuardarMarca(Model.MarcaEquipoModel marca)
        {
            Result result = new Result();

            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodigo", ConexionDbType.Int, marca.Codigo);                
                parametros.Add("@pDescripcion", ConexionDbType.VarChar, marca.Descripcion);                                
                parametros.Add("@pEstatus", ConexionDbType.Bit, marca.Estatus);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcCatMarcaGuardar", parametros);

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result GuardarEquipo(Model.EquipoModel equipo)
        {
            Result result = new Result();

            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodigo", ConexionDbType.Int, equipo.Codigo);
                parametros.Add("@pNombreEquipo", ConexionDbType.VarChar, equipo.Nombre);
                parametros.Add("@pDescripcion", ConexionDbType.VarChar, equipo.Descripcion);
                parametros.Add("@pCodModelo", ConexionDbType.Int, equipo.CodigoModelo);
                parametros.Add("@pCodMarca", ConexionDbType.Int, equipo.CodigoMarca);
                parametros.Add("@pCodTipoEquipo", ConexionDbType.Int, equipo.CodigoTipoEquipo);
                parametros.Add("@pIdentificador", ConexionDbType.VarChar, equipo.Identificador);
                parametros.Add("@pNumeroSerie", ConexionDbType.VarChar, equipo.NumeroSerie);
                parametros.Add("@pNumeroSerieMotor", ConexionDbType.VarChar, equipo.NumeroSerieMotor);
                parametros.Add("@pEstatus", ConexionDbType.Bit, equipo.Estatus);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcCatEquipoGuardar", parametros);

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<Model.ModeloEquipoModel>> consultaModelo(int codModelo)
        {
            Result<List<Model.ModeloEquipoModel>> result = new Result<List<Model.ModeloEquipoModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodModelo", ConexionDbType.Int, codModelo);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Model.ModeloEquipoModel>("ProcModelosEquipoCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<Model.ConsumbibleCombo>> ConsultaConsumibleCombo()
        {
            Result<List<Model.ConsumbibleCombo>> result = new Result<List<Model.ConsumbibleCombo>>();
            try
            {
                var parametros = new ConexionParameters();

                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Model.ConsumbibleCombo>("ProcConsumiblesComboCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<Model.ModeloEquipoCombo>> ConsultaModelosCombo()
        {
            Result<List<Model.ModeloEquipoCombo>> result = new Result<List<Model.ModeloEquipoCombo>>();
            try
            {
                var parametros = new ConexionParameters();

                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Model.ModeloEquipoCombo>("ProcModelosEquiposComboCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<Model.MarcaEquipoCombo>> ConsultaMarcaCombo()
        {
            Result<List<Model.MarcaEquipoCombo>> result = new Result<List<Model.MarcaEquipoCombo>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Model.MarcaEquipoCombo>("ProcMarcasEquiposComboCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<Model.TipoEquipoCombo>> CosultarTipoEquipo()
        {
            Result<List<Model.TipoEquipoCombo>> result = new Result<List<Model.TipoEquipoCombo>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Model.TipoEquipoCombo>("ProcTipoEquipoComboCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<Model.TipoEquipoModel>> ConsultaTiposEquipo(int codTipoEquipo)
        {
            Result<List<Model.TipoEquipoModel>> result = new Result<List<Model.TipoEquipoModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodCatTipoEquipo", ConexionDbType.Int, codTipoEquipo);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Model.TipoEquipoModel>("ProcCatTipoEquipoCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result GuardarTipoEquipo(Model.TipoEquipoModel tipoEquipo)
        {
            Result result = new Result();

            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodTipoEquipo", ConexionDbType.Int, tipoEquipo.Codigo);
                parametros.Add("@pNombre", ConexionDbType.VarChar, tipoEquipo.Nombre);
                parametros.Add("@pDescripcion", ConexionDbType.VarChar, tipoEquipo.Descripcion);
                parametros.Add("@pAbreviacion", ConexionDbType.VarChar, tipoEquipo.Abreviacion);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcCatTipoEquipoGuardar", parametros);

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<Model.EquipoModel>> ObtenerEquiposEntradasSalidas(bool entrada)
        {
            Result<List<Model.EquipoModel>> result = new Result<List<Model.EquipoModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pEntrada", ConexionDbType.Bit, entrada);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Model.EquipoModel>("ProcCatEquiposEntradasSalidasCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result ActualizarEstatusEquipo(int codUsuario, Model.RazonCambioEstatusEquiposModel equipo)
        {

            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                //parametros.Add("@pActivar", ConexionDbType.Bit, equipo.);
                parametros.Add("@pCodEquipo", ConexionDbType.Int, equipo.Codigo);
                parametros.Add("@pCodUsuario", ConexionDbType.Int, codUsuario);
                parametros.Add("@pPrioridadOnkey", ConexionDbType.VarChar, equipo.PrioridadOnKey);
                parametros.Add("@pMotivo", ConexionDbType.VarChar, equipo.Motivo);            
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcEquiposActualizarEstatus", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;

        }
        public Result<List<Model.MantenimietoEquipo>> ObtenerHistorialMantenimiento(int codEquipo)
        {
            Result<List<Model.MantenimietoEquipo>> result = new Result<List<Models.Equipos.MantenimietoEquipo>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodigo", ConexionDbType.Int, codEquipo);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Model.MantenimietoEquipo>("ProcHistorialMantenimientoEquipoCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
