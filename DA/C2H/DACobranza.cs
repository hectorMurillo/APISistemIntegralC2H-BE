using Models;
using Models.Cobranza;
using Models.Dosificador;
using Models.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;

namespace DA.C2H
{
    public class DACobranza
    {

        private readonly Conexion _conexion = null;

        public DACobranza()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }

        public Result<List<Anticipo>> ObtenerAnticipos(int codigo)
        {
            Result<List<Anticipo>> result = new Result<List<Anticipo>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodigo", ConexionDbType.Int, codigo);

                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Anticipo>("ProcAnticiposObtener", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<Obra>> ObtenerObra(int codigo)
        {
            Result<List<Obra>> result = new Result<List<Obra>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodigo", ConexionDbType.Int, codigo);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Obra>("ProcObraObtener", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<ListaPrecios>> ObtenerListaPrecios(int codigo)
        {
            Result<List<ListaPrecios>> result = new Result<List<ListaPrecios>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodigo", ConexionDbType.Int, codigo);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<ListaPrecios>("ProcListaPreciosObtener", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<Anticipo>> ObtenerAnticiposPorObra(int codObra)
        {
            Result<List<Anticipo>> result = new Result<List<Anticipo>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodigoObra", ConexionDbType.Int, codObra);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output);
                parametros.Add("@pResultado", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<ListaPreciosDet>> ObtenerListasPreciosDet(int codLista)
        {
            Result<List<ListaPreciosDet>> result = new Result<List<ListaPreciosDet>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodigo", ConexionDbType.Int, codLista);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<ListaPreciosDet>("ProcListasPreciosObtenerDet", parametros);

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;

        }

        public Result GuardarAnticipo(Anticipo anticipo, int idUsuario)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pUsuarioRegistro", ConexionDbType.Int, idUsuario);
                parametros.Add("@pCodigo", ConexionDbType.Int, anticipo.Codigo);
                parametros.Add("@pCodCliente", ConexionDbType.Int, anticipo.CodCliente);
                parametros.Add("@pMontoTotal", ConexionDbType.Decimal, anticipo.MontoTotal);
                parametros.Add("@pCodObras", ConexionDbType.VarChar, anticipo.CodObras);

                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcAnticipoGuardar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
        

        public Result GuardarProductoPrecio(ListaPreciosDet producto, int idUsuario)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodUsuario", ConexionDbType.Int, idUsuario);
                parametros.Add("@pCodProducto", ConexionDbType.Int, producto.CodigoDet);
                parametros.Add("@pPrecioMin", ConexionDbType.Decimal, producto.PrecioMin);
                parametros.Add("@pPrecioMax", ConexionDbType.Decimal, producto.PrecioMax);


                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcPreciosProductosGuardar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result GuardarListaPrecios(ListaPrecios lista, int idUsuario)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                if(lista.Codigo != 0)
                {
                    parametros.Add("@pCodigo", ConexionDbType.Int, lista.Codigo);
                }

                parametros.Add("@pIdUsiario", ConexionDbType.Int, idUsuario);
                parametros.Add("@pFechaInicio", ConexionDbType.Date, lista.FechaInicio);
                parametros.Add("@pMontoAAumentar", ConexionDbType.Decimal, lista.MontoAAumentar);

                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcListaPreciosGuardar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<ListaPrecios>> getListasPrecios()
        {
            Result<List<ListaPrecios>> result = new Result<List<ListaPrecios>>();
            try
            {
                var parametros = new ConexionParameters();

                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<ListaPrecios>("ProcObtenerListasPrecios", parametros);
            
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<DireccionesXClientesModel>> ObtenerObras(int codCliente)
        {
            Result<List<DireccionesXClientesModel>> result = new Result<List<DireccionesXClientesModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodigo", ConexionDbType.Int, codCliente);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<DireccionesXClientesModel>("ProcCatObrasClientesCon", parametros);

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;

        }

        public Result<List<DatosNotaRemision>> ObtenerNotasRemision()
        {
            Result<List<DatosNotaRemision>> result = new Result<List<DatosNotaRemision>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<DatosNotaRemision>("ProcCobranzaNotasRemisionCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<DatosNotaRemision>> ObtenerDatosNota(int folioNota)
        {
            Result<List<DatosNotaRemision>> result = new Result<List<DatosNotaRemision>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pIdNotaRemision", ConexionDbType.Int, folioNota);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<DatosNotaRemision>("ProcNotaRemisionDatosCon", parametros);

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;

        }


        public Result<List<DatosNotaRemision>> ObtenerNotasRemision(int folioNota)
        {
            Result<List<DatosNotaRemision>> result = new Result<List<DatosNotaRemision>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pFolioPedido", ConexionDbType.Int, folioNota);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<DatosNotaRemision>("ProcPedidoNotasRemisionCon", parametros);

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;

        }
       


        public Result<List<DatosNotaRemision>> ObtenerNotasRemisionSurtiendo()
        {
            Result<List<DatosNotaRemision>> result = new Result<List<DatosNotaRemision>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<DatosNotaRemision>("ProcCobranzaNotasRemisionSurtiendoCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<DatosNotaRemision>> ObtenerNotasRemisionEntradasSalidas(bool entrada)
        {
            Result<List<DatosNotaRemision>> result = new Result<List<DatosNotaRemision>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pEntrada", ConexionDbType.Bit, entrada);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<DatosNotaRemision>("ProcCobranzaNotasRemisionEntradasSalidasCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<NotaRemisionCobranza>> ObtenerNotasRemisionCobranza(DateTime fechaDesde, DateTime fechaHasta)
        {
            Result<List<NotaRemisionCobranza>> result = new Result<List<NotaRemisionCobranza>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pFechaDesde", ConexionDbType.Date, fechaDesde);
                parametros.Add("@pFechaHasta", ConexionDbType.Date, fechaHasta);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<NotaRemisionCobranza>("ProcCobranzaNotasDeRemisionCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result GuardarPagoNotaRemision(int idNotasRemisionEnc, decimal importeAbonar, int codUsuario)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pIdNotasRemisionEnc", ConexionDbType.Int, idNotasRemisionEnc);
                parametros.Add("@pImportePagado", ConexionDbType.Decimal, importeAbonar);
                parametros.Add("@pCodUsuario", ConexionDbType.Int, codUsuario);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcCobranzaNotaRemisionPagoGuardar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
        public Result<List<NotaRemisionCobranza>> ObtenerNotaRemisionCobranza(int idNotasRemisionEnc)
        {
            Result<List<NotaRemisionCobranza>> result = new Result<List<NotaRemisionCobranza>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pIdNotasRemisionEnc", ConexionDbType.Int, idNotasRemisionEnc);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<NotaRemisionCobranza>("ProcCobranzaNotaDeRemisionCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<DetalleAbonosNotaRemision>> ObtenerDetalleAbonosNotaRemision(int idNotasRemisionEnc)
        {
            Result<List<DetalleAbonosNotaRemision>> result = new Result<List<DetalleAbonosNotaRemision>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pIdNotasRemisionEnc", ConexionDbType.Int, idNotasRemisionEnc);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<DetalleAbonosNotaRemision>("ProcCobranzaDetalleAbonosNotaDeRemisionCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
