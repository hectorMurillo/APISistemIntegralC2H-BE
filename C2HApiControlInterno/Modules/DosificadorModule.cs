using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DA.C2H;
using Models.Dosificador;
using Models.Equipos;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WarmPack.Classes;
using Models;
using Model = Models.Dosificador;

namespace C2HApiControlInterno.Modules
{
    public class DosificadorModule : NancyModule
    {
        DADosificador _DADosificador = new DADosificador();
        public DosificadorModule() : base("/dosificador")
        {
            this.RequiresAuthentication();

            Get("/ultimo-folio-ginco/", _ => UltimoFolioGinco());
            Get("/notasRemision-canceladas/{codVendedor}/{cliente}/{obra}/{desde}/{hasta}", parametros => NotasRemisionCanceladas(parametros));
            //Get("/notasRemision-canceladas/", _ => NotasRemisionCanceladas());  
            Get("/formulas/{codigo}", parametros => Productos(parametros));
            Get("/ultimo-folio-notaRemision/", _ => UltimoFolioNotaRemision());
            Get("/obras-clientes/{codCliente}", parametros => ObrasCliente(parametros));
            Get("/operadores/{bombeable}", parametros => Operadores(parametros));
            Get("/operadores-camion-revolvedor", _ => ObtenerOperadoresCamionRevolvedor());
            Get("/operadores-camion-bombeable", _ => ObtenerOperadoresCamionBombeable());
            Get("/equipo-operador/{codOperador}/{esBombeable}", parametros => EquipoOperador(parametros));
            Get("/folio-pedido/{folioPedido}", parametros => FolioPedido(parametros));
            Get("/verificar-notasRemision-pedido/{folioPedido}", parametros => VerificarNotasRemisionPedido(parametros));
            Get("/notasRemision-especial/{codigo}/{folioGinco}", parametros => ObtenerNotasRemisionEspecial(parametros));
            Get("/notaRemision/pdf/{folio}", parametros => ObtenerPdfNotaRemision(parametros));



            Post("notaRemision/cancelar", _ => CancelarNotaRemision());
            Post("notaRemision/guardar", _ => GuardarNotaRemision());
            Post("notaRemision/agregar-nota", _ => AgregarNotaRemisionEspecial());
            Post("formula/guardar", _ => GuardarFormulaProducto());
            Post("productos-formula/guardar", _ => GuardarProductoFormula());

            //NOTA REMISION AUXILIAR
            
            Post("nota-remision-auxiliar/guardar", _ => GuardarNotaRemisionAuxiliar());
            //Get("/nota-remision-auxiliar/")
            Get("/notaRemision-auxiliar/pdf/{folio}", parametros => ObtenerPdfNotaRemisionAuxiliar(parametros));
            Post("nota-remision-firma/guardar", _ => GuardarFirmaNotaRemisionAuxiliar());
            Get("/operadores-auxiliar", _ => ObtenerOperadoresAuxiliar());
            Get("/equipos-auxiliar/{codEmpleado}", parametros => ObtenerEquiposAuxiliar(parametros));
            Get("/bombas-auxiliar", _ => ObtenerBombasAuxiliar());
            Get("/clientes/{cod}", parametros => ObtenerClientesVendedor(parametros));
            Get("/obras/{cliente}", parametros => ObtenerObrasCliente(parametros));
            Get("nota-remision-auxiliar/toExcel/{fechaDesde}/{fechaHasta}", parametros => obtenerNotaRemisionAuxExcel(parametros));
            Post("notaRemisionAuxiliar/cancelar", _ => CancelarNotaRemisionAux());


            //OperadorEquipo
            Get("/operador-equipo", _ => obtenerOperadoresEquipos());
            Get("/equipo-corto", _ => ObtenerEquipoNomCorto());
            Post("/operador-equipo-guardar", _ => guardarOperadorEquipo());
        }
        private object ObtenerEquipoNomCorto()
        {
            Result<List<Model.EquipoNomCortoModel>> result = new Result<List<Model.EquipoNomCortoModel>>();
            try
            {
                result = _DADosificador.ObtenerEquipoNomCorto();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
        private object guardarOperadorEquipo()
        {
            Result result = new Result();
            try
            {

                var operador = this.Bind<Model.OperadorEquipo>();
                result = _DADosificador.GuardarEquipoOperador(operador);
            }
            catch (Exception ex)
            {

                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object CancelarNotaRemisionAux()
        {
            Result result = new Result();
            try
            {
                var notaRemision = this.Bind<NotaRemisionAuxiliarModel>();
                var codUsuario = this.BindUsuario().IdUsuario;
                result = _DADosificador.CancelarNotaRemisionAuxiliar(notaRemision,codUsuario);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object obtenerNotaRemisionAuxExcel(dynamic paremeters)
        {
            Result<List<DatosNotaRemision>> result = new Result<List<DatosNotaRemision>>();
            try
            {
                DateTime FechaDesde = paremeters.fechaDesde;
                DateTime FechaHasta = paremeters.fechaHasta;
                result = _DADosificador.ObtenerDatosNotaRemisionAExcel(FechaDesde,FechaHasta);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GuardarFirmaNotaRemisionAuxiliar()
        {

            Result result = new Result();
            try
            {
                var codUsuario = this.BindUsuario().IdUsuario;
                var usuario = this.BindUsuario().Nombre;
                var notaRemision = this.Bind<NotaRemisionFirmaModel>();
                result = _DADosificador.GuardarFirmaNotaRemisionAuxiliar(notaRemision, codUsuario);
            }
            catch(Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object obtenerOperadoresEquipos()
        {
            Result<List<Model.OperadorEquipo>> result = new Result<List<Model.OperadorEquipo>>();
            try
            {
                result = _DADosificador.ObtenerEquipoOperador();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObtenerClientesVendedor(dynamic parametros)
        {
            Result<List<ClientesVendedorAuxModel>> result = new Result<List<ClientesVendedorAuxModel>>();
            try
            {
                int cod = parametros.cod;
                result = _DADosificador.ObtenerClientesVendedor(cod);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
        private object ObtenerObrasCliente(dynamic parametros)
        {
            Result<List<ObrasClientesAuxModel>> result = new Result<List<ObrasClientesAuxModel>>();
            try
            {
                string cliente = parametros.cliente;
                result = _DADosificador.ObtenerObrasCliente(cliente);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
        private object ObtenerOperadoresAuxiliar()
        {
            Result<List<OperadorModel>> result = new Result<List<OperadorModel>>();
            try
            {
                result = _DADosificador.ObtenerOperadoresAux();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObtenerBombasAuxiliar()
        {
            Result<List<EquipoModel>> result = new Result<List<EquipoModel>>();
            try
            {
                result = _DADosificador.ObtenerBombasAux();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObtenerEquiposAuxiliar(dynamic parametros)
        {
            Result<List<EquipoModel>> result = new Result<List<EquipoModel>>();
            try
            {
                int codEmpleado = parametros.codEmpleado;
                result = _DADosificador.ObtenerEquiposAux(codEmpleado);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
        private object ObtenerPdfNotaRemisionAuxiliar(dynamic parametros)
        {
            Result result = new Result();
            var usuario = this.BindUsuario().Nombre;
            var folio = parametros.folio;

            var datos = _DADosificador.ObtenerDatosNotaAuxiliar(folio);

            //var nota = datos?.Data[0];
            //var nota = null;
            //try
            //{
                var nota = datos.Data.Count > 0 ? datos.Data[0] : "";
            //}
            //catch (Exception ex)
            //{
            //    var a = ex.Message;
            //}

            var pathdirectorio = Globales.FolderPDF;
            //var pathdirectorio = "h:\\root\\home\\hector14-001\\www\\api\\PRUEBAPRUEBA";
            if (!Directory.Exists(pathdirectorio))
            {
                DirectoryInfo di = Directory.CreateDirectory(pathdirectorio);
            }

            var path = HttpRuntime.AppDomainAppPath;
            //string rutapdf = "c:\\pruebaprueba\\prueba.pdf";
            //string rutapdf = "h:\\root\\home\\hector14-001\\www\\api\\PRUEBAPRUEBA\\prueba.pdf";
            string rutapdf = $"{ Globales.FolderPDF}\\prueba.pdf";
            byte[] bytes;
            bool cancelado = nota.Estatus == "C";
            ReportDocument reporte = new ReportDocument();
            nota.Referencia = nota.Referencia == null ? "" : nota.Referencia;
            if (nota.Bombeable)
            {
                try
                {
                    reporte.Load(path + "\\reportes\\rptnotaBombeable.rpt");
                    reporte.SetParameterValue("@folio", nota.Folio);
                    reporte.SetParameterValue("@folioginco", nota.FolioGinco);
                    reporte.SetParameterValue("@cliente", nota.Cliente);
                    reporte.SetParameterValue("@obra", nota.Obra);
                    reporte.SetParameterValue("@producto", nota.Producto);
                    reporte.SetParameterValue("@cantidad", nota.Cantidad);
                    reporte.SetParameterValue("@nomenclatura", nota.Nomenclatura);
                    reporte.SetParameterValue("@operadorCr", nota.Operador);
                    reporte.SetParameterValue("@equipoCr", nota.Equipo);
                    reporte.SetParameterValue("@operadorBomba", nota.OperadorBomba);
                    reporte.SetParameterValue("@equipoBombeable", nota.EquipoBomba);
                    reporte.SetParameterValue("@vendedor", nota.Vendedor);
                    reporte.SetParameterValue("@usuario", nota.NombreUsuario);
                    reporte.SetParameterValue("@bombeable", nota.Bombeable);
                    reporte.SetParameterValue("@imper", nota.Imper);
                    reporte.SetParameterValue("@fibra", nota.Fibra);
                    reporte.SetParameterValue("@esMaquilado", nota.Maquilado);
                    reporte.SetParameterValue("@cancelado", cancelado);
                    reporte.SetParameterValue("@fecha", nota.Fecha);
                    reporte.SetParameterValue("@horaSalidaPlanta", nota.HoraSalidaPlanta);
                    reporte.SetParameterValue("@referencia", nota.Referencia);

                }catch(Exception ex)
                {
                    var msg = ex.Message;
                }
            }
            else
            {
                reporte.Load(path + "\\reportes\\rptnota.rpt");
                //reporte.SetDataSource(nota);
                try
                {
                    reporte.SetParameterValue("@folio", nota.Folio);
                    reporte.SetParameterValue("@folioginco", nota.FolioGinco);
                    reporte.SetParameterValue("@cliente", nota.Cliente);
                    reporte.SetParameterValue("@obra", nota.Obra);
                    reporte.SetParameterValue("@producto", nota.Producto);
                    reporte.SetParameterValue("@cantidad", nota.Cantidad);
                    reporte.SetParameterValue("@operador", nota.Operador);
                    reporte.SetParameterValue("@nomenclatura", nota.Nomenclatura);
                    reporte.SetParameterValue("@equipo", nota.Equipo);
                    reporte.SetParameterValue("@vendedor", nota.Vendedor);
                    reporte.SetParameterValue("@usuario", nota.NombreUsuario);
                    reporte.SetParameterValue("@imper", nota.Imper);
                    reporte.SetParameterValue("@fibra", nota.Fibra);
                    reporte.SetParameterValue("@esMaquilado", nota.Maquilado);
                    reporte.SetParameterValue("@cancelado", cancelado);
                    reporte.SetParameterValue("@fecha", nota.Fecha);
                    reporte.SetParameterValue("@horaSalidaPlanta", nota.HoraSalidaPlanta);
                    reporte.SetParameterValue("@referencia", nota.Referencia);
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
            }





            //reporte.setparametervalue("@sello", usuario);

            //reporte.setdatasource();
            try
            {
                reporte.ExportToDisk(ExportFormatType.PortableDocFormat, rutapdf);
            }
            catch(Exception ex)
            {
                var mensaje = ex.Message;
            }

            bytes = File.ReadAllBytes(rutapdf);
            string pdfbase64 = Convert.ToBase64String(bytes);
            result.Data = pdfbase64;
            File.Delete(rutapdf);
            result.Value = true;

            return Response.AsJson(result); ;
        }

        private object GuardarNotaRemisionAuxiliar()
        {

            Result result = new Result();
            try
            {
                var codUsuario = this.BindUsuario().IdUsuario;
                var usuario = this.BindUsuario().Nombre;
                var notaRemision = this.Bind<NotaRemisionAuxiliarModel>();
                result = _DADosificador.GuardarNotaRemisionAuxiliar(notaRemision, codUsuario);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GuardarProductoFormula()
        {
            Result result = new Result();
            try
            {
                var parametro = this.BindModel();
                var lstFormulas = new List<FormulaModel>();
                bool modificarDatos = parametro.modificarDatos;
                var productos = parametro.productos;

                foreach (var element in productos)
                {
                    FormulaModel producto = new FormulaModel();
                    producto.Nomenclatura = element.Nomenclatura;
                    producto.Descripcion = element.Descripcion;
                    producto.Edad = element.Edad;
                    producto.Resistencia = element.Resistencia;
                    producto.TMA = element.TMA;
                    producto.Revenimiento = element.Revenimiento;
                    producto.PrecioMinimo = element.PrecioMinimo;
                    producto.PrecioMaximo = element.PrecioMaximo;
                    lstFormulas.Add(producto);
                }

                //var formula = this.Bind<List<FormulaModel>>();
                result = _DADosificador.GuardarProductosFormula(lstFormulas, modificarDatos);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }


        private object VerificarNotasRemisionPedido(dynamic parametros)
        {
            Result<DatoModel> result = new Result<DatoModel>();
            try
            {
                int folioPedido = parametros.folioPedido;
                result = _DADosificador.VerificarNotaRemisionPedido(folioPedido);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object UltimoFolioGinco()
        {
            Result<List<int>> result = new Result<List<int>>();
            try
            {
                result = _DADosificador.ObtenerUltimoFolioGinco();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
        private object FolioPedido(dynamic parametros)
        {
            Result<List<PedidoModel>> result = new Result<List<PedidoModel>>();
            try
            {
                int codUsuario = this.BindUsuario().IdUsuario;
                int folioPedido = parametros.folioPedido;
                result = _DADosificador.ObtenerPedido(folioPedido,codUsuario);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object NotasRemisionCanceladas(dynamic parametros)
        {
            Result<List<DatosNotaRemision>> result = new Result<List<DatosNotaRemision>>();
            try
            {
                int codVendedor = parametros.codVendedor;
                string cliente = parametros.cliente;
                string obra = parametros.obra;
                string desde = parametros.desde;
                string hasta = parametros.hasta;
                result = _DADosificador.ObtenerNotasRemisionCanceladas(codVendedor,cliente,obra,desde,hasta);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }


        private object UltimoFolioNotaRemision()
        {
            Result<List<int>> result = new Result<List<int>>();
            try
            {
                result = _DADosificador.ObtenerUltimoFolioNR();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GuardarFormulaProducto()
        {
            Result<List<int>> result = new Result<List<int>>();
            try
            {
                var formula = this.Bind<FormulaModel>();
                result = _DADosificador.GuardarFormula(formula);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
        private object GuardarNotaRemision()
        {

            Result result = new Result();
            try
            {
                var codUsuario = this.BindUsuario().IdUsuario;
                var usuario = this.BindUsuario().Nombre;
                var notaRemision = this.Bind<NotaRemisionEncModel>();
                result = _DADosificador.GuardarNotaRemision(notaRemision, codUsuario);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObtenerPdfNotaRemision(dynamic parametros)
        {
            Result result = new Result();
            var usuario = this.BindUsuario().Nombre;
            var folio = parametros.folio;
            var nota = new DatosNotaRemision();
            var datos = new Result<List<DatosNotaRemision>>();
            datos = _DADosificador.ObtenerDatosNota(folio);

            nota = datos.Data[0];   

            var pathdirectorio = Globales.FolderPDF;
            //var pathdirectorio = "h:\\root\\home\\hector14-001\\www\\api\\PRUEBAPRUEBA";
            if (!Directory.Exists(pathdirectorio))
            {
                DirectoryInfo di = Directory.CreateDirectory(pathdirectorio);
            }

            var path = HttpRuntime.AppDomainAppPath;
            //string rutapdf = "c:\\pruebaprueba\\prueba.pdf";
            //string rutapdf = "h:\\root\\home\\hector14-001\\www\\api\\PRUEBAPRUEBA\\prueba.pdf";
            string rutapdf = $"{ Globales.FolderPDF}\\prueba.pdf";
            byte[] bytes;
            bool cancelado = nota.Estatus == "C";
            ReportDocument reporte = new ReportDocument();

            if (nota.Bombeable)
            {
                reporte.Load(path + "\\reportes\\rptnotaBombeable.rpt");
                reporte.SetParameterValue("@folio", nota.Folio);
                reporte.SetParameterValue("@folioginco", nota.FolioGinco);
                reporte.SetParameterValue("@cliente", nota.Cliente);
                reporte.SetParameterValue("@obra", nota.Obra);
                reporte.SetParameterValue("@producto", nota.Producto);
                reporte.SetParameterValue("@cantidad", nota.Cantidad);
                reporte.SetParameterValue("@nomenclatura", nota.Nomenclatura);
                reporte.SetParameterValue("@operadorCr", nota.Operador);
                reporte.SetParameterValue("@equipoCr", nota.Equipo);
                reporte.SetParameterValue("@operadorBomba", nota.OperadorBomba);
                reporte.SetParameterValue("@equipoBombeable", nota.EquipoBomba);
                reporte.SetParameterValue("@vendedor", nota.Vendedor);
                reporte.SetParameterValue("@usuario", usuario);
                reporte.SetParameterValue("@bombeable", nota.Bombeable);
                reporte.SetParameterValue("@imper", nota.Imper);
                reporte.SetParameterValue("@fibra", nota.Fibra);
                reporte.SetParameterValue("@esMaquilado", nota.Maquilado);
                reporte.SetParameterValue("@cancelado", cancelado);
                reporte.SetParameterValue("@fecha", nota.Fecha);
            }
            else
            {
                reporte.Load(path + "\\reportes\\rptnota.rpt");
                reporte.SetParameterValue("@folio", nota.Folio);
                reporte.SetParameterValue("@folioginco", nota.FolioGinco);
                reporte.SetParameterValue("@cliente", nota.Cliente);
                reporte.SetParameterValue("@obra", nota.Obra);
                reporte.SetParameterValue("@producto", nota.Producto);
                reporte.SetParameterValue("@cantidad", nota.Cantidad);
                reporte.SetParameterValue("@operador", nota.Operador);
                reporte.SetParameterValue("@nomenclatura", nota.Nomenclatura);
                reporte.SetParameterValue("@equipo", nota.Equipo);
                reporte.SetParameterValue("@vendedor", nota.Vendedor);
                reporte.SetParameterValue("@usuario", usuario);
                reporte.SetParameterValue("@imper", nota.Imper);
                reporte.SetParameterValue("@fibra", nota.Fibra);
                reporte.SetParameterValue("@esMaquilado", nota.Maquilado);
                reporte.SetParameterValue("@cancelado", cancelado);
                reporte.SetParameterValue("@fecha", nota.Fecha);
            }

           



            //reporte.setparametervalue("@sello", usuario);

            //reporte.setdatasource();
            reporte.ExportToDisk(ExportFormatType.PortableDocFormat, rutapdf);

            bytes = File.ReadAllBytes(rutapdf);
            string pdfbase64 = Convert.ToBase64String(bytes);
            result.Data = pdfbase64;
            File.Delete(rutapdf);
            result.Value = true;

            return Response.AsJson(result); ;
        }

        private object AgregarNotaRemisionEspecial()
        {

            Result result = new Result();
            try
            {
                var codUsuario = this.BindUsuario().IdUsuario;
                var usuario = this.BindUsuario().Nombre;
                var notaRemision = this.Bind<NotaRemisionEncModel>();
                result = _DADosificador.AgregarNotaRemisionEspecial(notaRemision, codUsuario);

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }


        private object Productos(dynamic parametros)
        {
            Result<List<FormulaModel>> result = new Result<List<FormulaModel>>();
            try
            {
                string producto = parametros.codigo;
                result = _DADosificador.ObtenerProductos(producto);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObtenerNotasRemisionEspecial(dynamic parametros)
        {
            Result<List<DatosNotaRemision>> result = new Result<List<DatosNotaRemision>>();
            try
            {
                int codigo = parametros.codigo;
                int folioGinco = parametros.folioGinco;
                result = _DADosificador.ObtenerNotasRemisionEspecial(codigo, folioGinco);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObrasCliente(dynamic parametros)
        {
            Result<List<ObrasModel>> result = new Result<List<ObrasModel>>();
            try
            {
                int codCliente = parametros.codCliente;
                result = _DADosificador.ObtenerObrasClientes(codCliente);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
        private object Operadores(dynamic parametros)
        {
            Result<List<OperadorModel>> result = new Result<List<OperadorModel>>();
            try
            {
                bool esBombeable = parametros.bombeable;
                result = _DADosificador.ObtenerOperadores(esBombeable);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObtenerOperadoresCamionRevolvedor()
        {
            Result<List<OperadorModel>> result = new Result<List<OperadorModel>>();
            try
            {
                result = _DADosificador.ObtenerOperadoresCamionRevolvedor();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObtenerOperadoresCamionBombeable()
        {
            Result<List<OperadorModel>> result = new Result<List<OperadorModel>>();
            try
            {
                result = _DADosificador.ObtenerOperadoresCamionBombeable();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object EquipoOperador(dynamic parametros)
        {
            Result<List<EquipoModel>> result = new Result<List<EquipoModel>>();
            try
            {
                bool bombeable = parametros.esBombeable;
                int codOperador = parametros.codOperador;
                result = _DADosificador.ObtenerEquipoOperador(bombeable, codOperador);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object CancelarNotaRemision()
        {
            Result result = new Result();
            try
            {
                var notaRemision = this.Bind<DatosNotaRemision>();
                result = _DADosificador.CancelarNotaRemision(notaRemision);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
    }
}