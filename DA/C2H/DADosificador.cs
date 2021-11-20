using Models;
using Models.Dosificador;
using Models.Equipos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;
using WarmPack.Extensions;

namespace DA.C2H
{
    public class DADosificador
    {
        private readonly Conexion _conexion = null;

        public DADosificador()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }

        public Result<List<int>> ObtenerUltimoFolioGinco()
        {
            Result<List<int>> result = new Result<List<int>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<int>("ProcObtenerFolioGinco", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<DatosNotaRemision>> ObtenerNotasRemisionCanceladas()
        {
            Result<List<DatosNotaRemision>> result = new Result<List<DatosNotaRemision>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<DatosNotaRemision>("ProcNotasRemisionEncCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result CancelarNotaRemision(DatosNotaRemision notaRemision)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pFolio", ConexionDbType.Int, notaRemision.Folio);
                parametros.Add("@pFolioGinco", ConexionDbType.Int, notaRemision.FolioGinco);
                parametros.Add("@pObservacion", ConexionDbType.VarChar, notaRemision.Observacion);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcNotasRemisionCancelar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }
        public Result<List<int>> ObtenerUltimoFolioNR()
        {
            Result<List<int>> result = new Result<List<int>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<int>("ProcObtenerFolioNotaRemision", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<FormulaModel>> ObtenerProductos(string producto)
        {
            Result<List<FormulaModel>> result = new Result<List<FormulaModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodigo", ConexionDbType.VarChar, producto);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<FormulaModel>("ProcCatProductosCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
        public Result<List<ObrasModel>> ObtenerObrasClientes(int codCliente)
        {
            Result<List<ObrasModel>> result = new Result<List<ObrasModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodigo", ConexionDbType.Int, codCliente);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<ObrasModel>("ProcCatDireccionesClienteCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<OperadorModel>> ObtenerOperadores(bool bombeable)
        {
            Result<List<OperadorModel>> result = new Result<List<OperadorModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pEsBombeable", ConexionDbType.Bit, bombeable);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<OperadorModel>("ProcOperadoresCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<OperadorModel>> ObtenerOperadoresCamionRevolvedor()
        {
            Result<List<OperadorModel>> result = new Result<List<OperadorModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<OperadorModel>("ProcOperadoresCamionRevolvedorCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<OperadorModel>> ObtenerOperadoresCamionBombeable()
        {
            Result<List<OperadorModel>> result = new Result<List<OperadorModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<OperadorModel>("ProcOperadoresCamionBombeableCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }


        public Result<List<EquipoModel>> ObtenerEquipoOperador(bool bombeable, int codOperador)
        {
            Result<List<EquipoModel>> result = new Result<List<EquipoModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pEsBombeable", ConexionDbType.Bit, bombeable);
                parametros.Add("@pCodOperador", ConexionDbType.Int, codOperador);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<EquipoModel>("ProcEquipoOperadorCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<PedidoModel>> ObtenerPedido(int folioPedido, int codUsuario)
        {
            Result<List<PedidoModel>> result = new Result<List<PedidoModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodUsuario", ConexionDbType.Int, codUsuario);
                parametros.Add("@pFolioPedido", ConexionDbType.Int, folioPedido);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<PedidoModel>("ProcCatPedidoCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result GuardarNotaRemision(NotaRemisionEncModel notaRemision, int codUsuario)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pFolio", ConexionDbType.Int, notaRemision.Folio);
                parametros.Add("@pFolioGinco", ConexionDbType.Int, notaRemision.FolioGinco);
                parametros.Add("@pFolioPedido", ConexionDbType.Int, notaRemision.FolioPedido);
                parametros.Add("@pHoraSalida", ConexionDbType.VarChar, notaRemision.HoraSalida);
                parametros.Add("@pCodCliente", ConexionDbType.Int, notaRemision.CodCliente);
                parametros.Add("@pCodObra", ConexionDbType.Int, notaRemision.CodObra);
                parametros.Add("@pCodFormula", ConexionDbType.Int, notaRemision.CodProducto);
                parametros.Add("@pProducto", ConexionDbType.VarChar, notaRemision.Producto);
                parametros.Add("@pCantidad", ConexionDbType.Decimal, notaRemision.Cantidad);
                parametros.Add("@pCodVendedor", ConexionDbType.Int, notaRemision.CodVendedor);
                parametros.Add("@pCodOperador1", ConexionDbType.Int, notaRemision.CodOperador_1);
                parametros.Add("@pCodOperador2", ConexionDbType.Int, notaRemision.CodOperador_2);
                parametros.Add("@pCodEquipo_CR", ConexionDbType.Int, notaRemision.CodEquipo_CR);
                parametros.Add("@pCodEquipo_BB", ConexionDbType.Int, notaRemision.CodEquipo_BB);
                parametros.Add("@pEsBombeable", ConexionDbType.Bit, notaRemision.ChKBombeable);
                parametros.Add("@pFibra", ConexionDbType.Bit, notaRemision.ChKFibra);
                parametros.Add("@pImper", ConexionDbType.Bit, notaRemision.ChKImper);
                parametros.Add("@pCantidadRestantePedido", ConexionDbType.Decimal, notaRemision.CantidadRestantePedido);
                parametros.Add("@pForaneo", ConexionDbType.Bit, notaRemision.Foraneo);
                parametros.Add("@pIva", ConexionDbType.Decimal, notaRemision.parametrosEspeciales.Iva);
                parametros.Add("@pCodOperadorReubicado", ConexionDbType.Int, notaRemision.parametrosEspeciales.CodOperador);
                parametros.Add("@pReubicado", ConexionDbType.Decimal, notaRemision.parametrosEspeciales.Reubicado);
                parametros.Add("@pCodUsuario", ConexionDbType.Int, codUsuario);
                parametros.Add("@pOperadorExterno", ConexionDbType.VarChar, notaRemision.OperadorExterno);
                parametros.Add("@pEquipoExterno", ConexionDbType.VarChar, notaRemision.EquipoExterno);
                parametros.Add("@pIdNotaRemision", ConexionDbType.Int, System.Data.ParameterDirection.Output);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);


                _conexion.Execute("ProcNotaRemisionGuardar_Provisional", parametros);

                result.Data = parametros.Value("@pIdNotaRemision").ToInt32();
                result.Value = parametros.Value("@pResultado").ToBoolean();
                result.Message = parametros.Value("@pMsg").ToString();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result AgregarNotaRemisionEspecial(NotaRemisionEncModel notaRemision, int codUsuario)
        {
            Result result = new Result();
            try
            {

                var parametros = new ConexionParameters();
                parametros.Add("@pIdNotasRemisionEnc", ConexionDbType.Int, notaRemision.IdNotasRemisionEnc);
                parametros.Add("@pFolio", ConexionDbType.Int, notaRemision.Folio);
                parametros.Add("@pFolioGinco", ConexionDbType.Int, notaRemision.FolioGinco);
                parametros.Add("@pFolioPedido", ConexionDbType.Int, notaRemision.FolioPedido);
                parametros.Add("@pFolioPadre", ConexionDbType.Int, notaRemision.FolioPadre);
                parametros.Add("@pHoraSalida", ConexionDbType.VarChar, notaRemision.HoraSalida);
                parametros.Add("@pCodCliente", ConexionDbType.Int, notaRemision.CodCliente);
                parametros.Add("@pCodObra", ConexionDbType.Int, notaRemision.CodObra);
                parametros.Add("@pCodFormula", ConexionDbType.Int, notaRemision.CodProducto);
                parametros.Add("@pProducto", ConexionDbType.VarChar, notaRemision.Producto);
                parametros.Add("@pCantidad", ConexionDbType.Decimal, notaRemision.Cantidad);
                parametros.Add("@pCodVendedor", ConexionDbType.Int, notaRemision.CodVendedor);
                parametros.Add("@pCodOperador1", ConexionDbType.Int, notaRemision.CodOperador_1);
                parametros.Add("@pCodOperador2", ConexionDbType.Int, notaRemision.CodOperador_2);
                parametros.Add("@pCodEquipo_CR", ConexionDbType.Int, notaRemision.CodEquipo_CR);
                parametros.Add("@pCodEquipo_BB", ConexionDbType.Int, notaRemision.CodEquipo_BB);
                parametros.Add("@pEsBombeable", ConexionDbType.Bit, notaRemision.ChKBombeable);
                parametros.Add("@pFibra", ConexionDbType.Bit, notaRemision.ChKFibra);
                parametros.Add("@pImper", ConexionDbType.Bit, notaRemision.ChKImper);
                parametros.Add("@pCantidadRestantePedido", ConexionDbType.Decimal, notaRemision.CantidadRestantePedido);
                parametros.Add("@pForaneo", ConexionDbType.Bit, notaRemision.Foraneo);
                parametros.Add("@pIva", ConexionDbType.Decimal, notaRemision.parametrosEspeciales.Iva);
                parametros.Add("@pCodOperadorReubicado", ConexionDbType.Int, notaRemision.parametrosEspeciales.CodOperador);
                parametros.Add("@pReubicado", ConexionDbType.Decimal, notaRemision.parametrosEspeciales.Reubicado);
                parametros.Add("@pCodUsuario", ConexionDbType.Int, codUsuario);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                _conexion.ExecuteWithResults("ProcNotaRemisionEspecialGuardar", parametros, row =>
                {
                    NotaRemisionEncModel nota = new NotaRemisionEncModel();
                    nota.CodOperador_1 = row["CodOperador"].ToInt32();
                    nota.CodEquipo_CR = row["CodEquipo"].ToInt32();
                    nota.FolioGinco = row["FolioGinco"].ToInt32();
                    nota.CodProducto = row["CodFormula"].ToInt32();
                    nota.FolioPadre = row["FolioPadre"].ToInt32();
                    result.Data = nota;
                });
                result.Value = parametros.Value("@pResultado").ToBoolean();
                result.Message = parametros.Value("@pMsg").ToString();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<int>> GuardarFormula(FormulaModel formula)
        {
            Result<List<int>> result = new Result<List<int>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pDescripcion", ConexionDbType.VarChar, formula.Descripcion);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<int>("ProcCatProductosGuardar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<DatosNotaRemision>> ObtenerDatosNota(int idNotaRemision)
        {
            Result<List<DatosNotaRemision>> result = new Result<List<DatosNotaRemision>>();
            try
            {
                var parametros = new ConexionParameters();
                DataTable ds = new DataTable();
                parametros.Add("@pIdNotaRemision", ConexionDbType.Int, idNotaRemision);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<DatosNotaRemision>("ProcNotaRemisionDatosCon", parametros);

                //foreach (DataRow renglon in table.Rows)
                //{
                //    cot = new PaletTarimaModel();
                //    cot.Palet = renglon["Palet"].ToString();
                //    cot.Ubicacion = renglon["Ubicacion"].ToString();
                //    cot.CodigoArticulo = Convert.ToInt32(renglon[2].ToString());
                //    cot.Articulo = renglon["Artículo         "].ToString();
                //    cot.Lote = renglon["Lote"].ToString();
                //    cot.Contenido = Convert.ToInt32(renglon["con."].ToString());

                //    retorno.Add(cot);
                //}
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;

        }

        public Result<List<DatosNotaRemision>> ObtenerNotasRemisionEspecial(int codigo, int folioGinco)
        {
            Result<List<DatosNotaRemision>> result = new Result<List<DatosNotaRemision>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pIdNotaRemisionPadre", ConexionDbType.Int, codigo);
                parametros.Add("@pFolioGinco", ConexionDbType.Int, folioGinco);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<DatosNotaRemision>("ProcNotaRemisionEspecialCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;

        }

        public Result<DatoModel> VerificarNotaRemisionPedido(int folioPedido)
        {
            Result<DatoModel> result = new Result<DatoModel>();
            try
            {
                DatoModel dato = new DatoModel();
                var parametros = new ConexionParameters();
                parametros.Add("@pFolioPedido", ConexionDbType.Int, folioPedido);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                _conexion.ExecuteWithResults("ProcNotaRemisionVerificaPedidoCon", parametros, row =>
                {

                    dato.cantidadRecomendar = row["Cantidad"].ToDecimal();
                    dato.cantidadRestantePedido = row["CantidadRestante"].ToDecimal();


                });
                result.Data = dato;
                result.Value = parametros.Value("@pResultado").ToBoolean();
                result.Message = parametros.Value("@pMsg").ToString();

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result GuardarProductosFormula(List<FormulaModel> excelCargado,Boolean modificarDatos)
        {
            Result r = new Result();
            String productos = excelCargado.ToXml("Formula");

            try
            {
                var parametros = new ConexionParameters();
                //parametros.Add("@pCodBanco", ConexionDbType.Bit, codBan/*co);*/
                parametros.Add("@pXML", ConexionDbType.Xml, productos);
                parametros.Add("@pModificar", ConexionDbType.Bit, modificarDatos);           
                parametros.Add("@pCodError", ConexionDbType.Int, System.Data.ParameterDirection.Output);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                r = _conexion.Execute("ProcCatProductosGuardar", parametros);

                int codError  = parametros.Value("@pCodError").ToInt32();

                if (codError > 0)
                {
                    r.Data = codError;
                }      

            }
            catch (Exception ex)
            {
                r.Message = ex.Message;
            }

            return r;
        }

        public Result<List<DatosNotaRemision>> ObtenerDatosClienteNota(int idNotaRemicion)
        {
            Result<List<DatosNotaRemision>> result = new Result<List<DatosNotaRemision>>();

            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pidNotaRemision", ConexionDbType.Int, idNotaRemicion);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);
                result = _conexion.ExecuteWithResults<DatosNotaRemision>("ProcCatClienteNotaRemision", parametros);


            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
