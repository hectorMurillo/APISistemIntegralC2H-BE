using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DA.C2H;
using Models.Reportes;
using Models.Ventas;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using WarmPack.Classes;

namespace C2HApiControlInterno.Modules
{
    public class VentasModule : NancyModule
    {
        private readonly DAVentas _DAVentas = null;

        public VentasModule() : base("/reportes")
        {
            //this.RequiresAuthentication();
            _DAVentas = new DAVentas();

            //ReporteMensualMetrosCubicos
            Post("/obtener-reporte-mensual-metros/{fechaDesde}/{fechaHasta}", x => ObtenerReporteMensualMetrosCubicos(x));
            Post("/imprimir-reporte-mensual-metros/{fechaDesde}/{fechaHasta}", x => ImprimirReporteMensualMetrosCubicos(x));
            //ReporteMensualMetrosCubicos
            Post("/obtener-reporte-mensual-clientes/{fechaDesde}/{fechaHasta}/{agente}", x => ObtenerReporteMensualClientes(x));
            Post("/imprimir-reporte-mensual-clientes/{fechaDesde}/{fechaHasta}", x => ImprimirReporteMensualClientes(x));
            //ReporteVolumenXObras
            Post("/obtener-reporte-volumen-obra/{fechaDesde}/{fechaHasta}", x => ObtenerReporteMensualVolumenXObras(x));
            Post("/imprimir-reporte-volumen-obra/{fechaDesde}/{fechaHasta}", x => ImprimirReporteMensualVolumenXObras(x));
            //ReporteMensualProductos
            Post("/obtener-reporte-mensual-productos/{fechaDesde}/{fechaHasta}", x => ObtenerReporteMensualProductos(x));
            Post("/imprimir-reporte-mensual-productos/{fechaDesde}/{fechaHasta}", x => ImprimirReporteMensualProductos(x));
            //ReporteEntradasSalidas
            Post("/obtener-reporte-entradas-salidas/{fechaDesde}/{fechaHasta}/{codEquipo}", x => ObtenerReporteMensualEntradasSalidas(x));
            Post("/imprimir-reporte-entradas-salidas/{fechaDesde}/{fechaHasta}/{codEquipo}", x => ImprimirReporteMensualEntradasSalidas(x));
            //ReporteEquipos
            Post("/obtener-reporte-mensual-equipos/{fechaDesde}/{fechaHasta}/{codEquipo}", x => ObtenerReporteMensualEquipos(x));
            Post("/imprimir-reporte-mensual-equipos/{fechaDesde}/{fechaHasta}/{codEquipo}", x => ImprimirReporteMensualEquipos(x));
            
            Get("/obtener-demanda-articulos/", _ => ObtenerDemandaArticulo());
            //ReporteNocturnos
            Post("/obtener-reporte-nocturnos/{fechaDesde}/{fechaHasta}/{codEquipo}", x => ObtenerReporteMensualNocturnos(x));
            Post("/imprimir-reporte-nocturnos/{fechaDesde}/{fechaHasta}/{codEquipo}", x => ImprimirReporteMensualNocturnos(x));
            //ReporteForaneos
            Post("/obtener-reporte-foraneos/{fechaDesde}/{fechaHasta}/{codEquipo}", x => ObtenerReporteMensualForaneos(x));
            Post("/imprimir-reporte-foraneos/{fechaDesde}/{fechaHasta}/{codEquipo}", x => ImprimirReporteMensualForaneos(x));


            //Pediso Cancelados
            Post("/obtener-reporte-pedidos-cancelados/{fechaDesde}/{fechaHasta}/{codCliente}/{codVendedor}", x => ObtenerReportePedidosCancelados(x));
            Post("/imprimir-reporte-pedidos-cancelados/{fechaDesde}/{fechaHasta}/{codCliente}/{codVendedor}", x => ImprimirReportePedidosCancelados(x));

            //Pediso Reagendados
            Post("/obtener-reporte-pedidos-reagendados/{fechaDesde}/{fechaHasta}/{codCliente}/{codVendedor}", x => ObtenerReportePedidosReagendados(x));
            Post("/imprimir-reporte-pedidos-reagendados/{fechaDesde}/{fechaHasta}/{codCliente}/{codVendedor}", x => ImprimirReportePedidosReagendados(x));

        }

        //NUEVOS METODOS

        private object ObtenerReporteMensualMetrosCubicos(dynamic x)
        {
            Result result = new Result();

            var reporteMensualMetros = this.Bind<RptMensualMetrosModel>();
            DateTime fechaDesde = x.fechaDesde;
            DateTime fechaHasta = x.fechaHasta;

            try
            {
                var r = new Result<List<RptMensualMetros>>();
                r = _DAVentas.ReporteMensualMetrosCubicos(reporteMensualMetros, fechaDesde, fechaHasta);
                result.Data = r.Data;
                result.Value = r.Value;
                result.Message = r.Message;
            }
            catch (Exception ex)
            {
                result.Value = false;
                result.Message = ex.Message;
            }

            return Response.AsJson(result);

        }

        private object ImprimirReporteMensualMetrosCubicos(dynamic x)
        {
            Result result = new Result();

            var reporteMensualMetros = this.Bind<List<RptMensualMetros>>();
            DateTime fechaDesde = x.fechaDesde;
            DateTime fechaHasta = x.fechaHasta;

            try
            {
                var totalMetrosVendidos = reporteMensualMetros.Sum(s => s.Cantidad);
                var path = HttpRuntime.AppDomainAppPath;
                string rutaPdf = "C:\\PRUEBAPRUEBA\\RptMensualMetrosTEST.pdf";
                string pdfBase64 = "";
                Byte[] bytes;

                ReportDocument reporte = new ReportDocument();
                reporte.Load(path + "\\Reportes\\RptMensualMetros.rpt");
                reporte.SetDataSource(reporteMensualMetros);
                reporte.SetParameterValue("fechaDesde", fechaDesde);
                reporte.SetParameterValue("fechaHasta", fechaHasta);
                reporte.SetParameterValue("totalMetrosVendidos", totalMetrosVendidos);
                reporte.ExportToDisk(ExportFormatType.PortableDocFormat, rutaPdf);

                bytes = File.ReadAllBytes(rutaPdf);
                pdfBase64 = Convert.ToBase64String(bytes);
                result.Data = pdfBase64;
                result.Value = true;
                File.Delete(rutaPdf);

            }
            catch (Exception ex)
            {
                result.Value = false;
                result.Message = ex.Message;
            }

            return Response.AsJson(result);

        }

        private object ObtenerReporteMensualClientes(dynamic x)
        {
            Result result = new Result();

            DateTime fechaDesde = x.fechaDesde;
            DateTime fechaHasta = x.fechaHasta;
            int agente = x.agente;

            var r = new Result<List<RptMensualClientes>>();
            r = _DAVentas.ReporteMensualClientes(fechaDesde, fechaHasta, agente);
            result.Data = r.Data;
            result.Value = r.Value;
            result.Message = r.Message;

            return Response.AsJson(result);

        }

        private object ImprimirReporteMensualClientes(dynamic x)
        {
            Result result = new Result();
            DateTime fechaDesde = x.fechaDesde;
            DateTime fechaHasta = x.fechaHasta;
            var reporteMensualClientes = this.Bind<List<RptMensualClientes>>();


            //DateTime fechaDesde = x.fechaDesde;
            //DateTime fechaHasta = x.fechaHasta;
            //int agente = x.agente;

            //var r = new Result<List<RptMensualClientes>>();
            //r = _DAVentas.ReporteMensualClientes(fechaDesde, fechaHasta, agente);


            var path = HttpRuntime.AppDomainAppPath;
            string rutaPdf = "C:\\PRUEBAPRUEBA\\RptMensualClientesTEST.pdf";
            string pdfBase64 = "";
            Byte[] bytes;
            //var totalClientes = r.Data.GroupBy(a => a.CodCliente).Count;
            int total = reporteMensualClientes.GroupBy(i => i.CodCliente).Select(group => group.First()).Count();

            ReportDocument reporte = new ReportDocument();
            reporte.Load(path + "\\Reportes\\RptMensualClientes.rpt");
            reporte.SetDataSource(reporteMensualClientes);
            reporte.SetParameterValue("fechaDesde", fechaDesde);
            reporte.SetParameterValue("fechaHasta", fechaHasta);
            reporte.ExportToDisk(ExportFormatType.PortableDocFormat, rutaPdf);

            bytes = File.ReadAllBytes(rutaPdf);
            pdfBase64 = Convert.ToBase64String(bytes);
            result.Data = pdfBase64;
            result.Value = true;
            File.Delete(rutaPdf);


            return Response.AsJson(result);

        }

        private object ObtenerReporteMensualVolumenXObras(dynamic x)
        {
            Result result = new Result();

            var reporteVolumenXObras = this.Bind<RptReporteVolumenObras>();
            DateTime fechaDesde = x.fechaDesde;
            DateTime fechaHasta = x.fechaHasta;

            var r = new Result<List<RptMensualMetros>>();
            r = _DAVentas.ReporteVolumenXObras(reporteVolumenXObras, fechaDesde, fechaHasta);
            result.Data = r.Data;
            result.Value = r.Value;
            result.Message = r.Message;
            return Response.AsJson(result);

        }

        private object ImprimirReporteMensualVolumenXObras(dynamic x)
        {
            Result result = new Result();
            DateTime fechaDesde = x.fechaDesde;
            DateTime fechaHasta = x.fechaHasta;
            var reporteMensualVolumenXObra = this.Bind<List<RptMensualMetros>>();

            var path = HttpRuntime.AppDomainAppPath;
            string rutaPdf = "C:\\PRUEBAPRUEBA\\RptVolumenXObraTEST.pdf";
            string pdfBase64 = "";
            Byte[] bytes;

            ReportDocument reporte = new ReportDocument();
            reporte.Load(path + "\\Reportes\\RptVolumenXObra.rpt");
            reporte.SetDataSource(reporteMensualVolumenXObra);
            reporte.SetParameterValue("fechaDesde", fechaDesde);
            reporte.SetParameterValue("fechaHasta", fechaHasta);
            reporte.ExportToDisk(ExportFormatType.PortableDocFormat, rutaPdf);

            bytes = File.ReadAllBytes(rutaPdf);
            pdfBase64 = Convert.ToBase64String(bytes);
            result.Data = pdfBase64;
            result.Value = true;
            File.Delete(rutaPdf);

            return Response.AsJson(result);

        }


        private object ObtenerReporteMensualProductos(dynamic x)
        {
            Result result = new Result();

            var reporteProductos = this.Bind<RptMensualProductos>();

            DateTime fechaDesde = x.fechaDesde;
            DateTime fechaHasta = x.fechaHasta;

            var r = new Result<List<RptMensualProductos>>();
            r = _DAVentas.ReporteMensualProductos(reporteProductos, fechaDesde, fechaHasta);
            result.Data = r.Data;
            result.Value = r.Value;
            result.Message = r.Message;

            return Response.AsJson(result);

        }

        private object ImprimirReporteMensualProductos(dynamic x)
        {
            Result result = new Result();
            DateTime fechaDesde = x.fechaDesde;
            DateTime fechaHasta = x.fechaHasta;
            var reporteMensualClientes = this.Bind<List<RptMensualProductos>>();


            var path = HttpRuntime.AppDomainAppPath;
            string rutaPdf = "C:\\PRUEBAPRUEBA\\RptMensualProductosTEST.pdf";
            string pdfBase64 = "";
            Byte[] bytes;

            ReportDocument reporte = new ReportDocument();
            reporte.Load(path + "\\Reportes\\RptMensualProductos.rpt");
            reporte.SetDataSource(reporteMensualClientes);
            reporte.SetParameterValue("fechaDesde", fechaDesde);
            reporte.SetParameterValue("fechaHasta", fechaHasta);
            reporte.SetParameterValue("agente", reporteMensualClientes[0].agente);

            reporte.ExportToDisk(ExportFormatType.PortableDocFormat, rutaPdf);

            bytes = File.ReadAllBytes(rutaPdf);
            pdfBase64 = Convert.ToBase64String(bytes);
            result.Data = pdfBase64;
            result.Value = true;
            File.Delete(rutaPdf);


            return Response.AsJson(result);

        }

        private object ObtenerReporteMensualEntradasSalidas(dynamic x)
        {
            Result result = new Result();

            var codEquipo = x.codEquipo;
            DateTime fechaDesde = x.fechaDesde;
            DateTime fechaHasta = x.fechaHasta;

            var r = new Result<List<RptEntradasSalidas>>();
            r = _DAVentas.ObtenerReporteEntradasSalidas(codEquipo, fechaDesde, fechaHasta);
            result.Data = r.Data;
            result.Value = r.Value;
            result.Message = r.Message;

            return Response.AsJson(result);

        }

        private object ImprimirReporteMensualEntradasSalidas(dynamic x)
        {
            Result result = new Result();
            DateTime fechaDesde = x.fechaDesde;
            DateTime fechaHasta = x.fechaHasta;
            int equipo = x.codEquipo;

            var reporteEntradasSalidas = this.Bind<List<RptEntradasSalidas>>();
            var path = HttpRuntime.AppDomainAppPath;
            string rutaPdf = "C:\\PRUEBAPRUEBA\\RptEntradasSalidasTEST.pdf";
            string pdfBase64 = "";
            Byte[] bytes;


            string equipoReporte = equipo == 0 ? "Todos" : reporteEntradasSalidas[0].equipo;

            ReportDocument reporte = new ReportDocument();
            reporte.Load(path + "\\Reportes\\RptEntradasSalidas.rpt");
            reporte.SetDataSource(reporteEntradasSalidas);
            reporte.SetParameterValue("fechaDesde", fechaDesde);
            reporte.SetParameterValue("fechaHasta", fechaHasta);
            reporte.SetParameterValue("equipo", equipoReporte);

            reporte.ExportToDisk(ExportFormatType.PortableDocFormat, rutaPdf);

            bytes = File.ReadAllBytes(rutaPdf);
            pdfBase64 = Convert.ToBase64String(bytes);
            result.Data = pdfBase64;
            result.Value = true;
            File.Delete(rutaPdf);


            return Response.AsJson(result);

        }

        private object ObtenerReporteMensualEquipos(dynamic x)
        {
            Result result = new Result();

            var codEquipo = x.codEquipo;
            DateTime fechaDesde = x.fechaDesde;
            DateTime fechaHasta = x.fechaHasta;

            var r = new Result<List<RptEquipos>>();
            r = _DAVentas.ObtenerReporteEquipos(codEquipo, fechaDesde, fechaHasta);
            result.Data = r.Data;
            result.Value = r.Value;
            result.Message = r.Message;

            return Response.AsJson(result);

        }

        private object ImprimirReporteMensualEquipos(dynamic x)
        {
            Result result = new Result();
            DateTime fechaDesde = x.fechaDesde;
            DateTime fechaHasta = x.fechaHasta;
            int equipo = x.codEquipo;

            var reporteEntradasSalidas = this.Bind<List<RptEquipos>>();
            var path = HttpRuntime.AppDomainAppPath;
            string rutaPdf = "C:\\PRUEBAPRUEBA\\RptEquiposTEST.pdf";
            string pdfBase64 = "";
            Byte[] bytes;


            string equipoReporte = equipo == 0 ? "Todos" : reporteEntradasSalidas[0].equipo;

            ReportDocument reporte = new ReportDocument();
            reporte.Load(path + "\\Reportes\\RptEquipos.rpt");
            reporte.SetDataSource(reporteEntradasSalidas);
            reporte.SetParameterValue("fechaDesde", fechaDesde);
            reporte.SetParameterValue("fechaHasta", fechaHasta);
            reporte.SetParameterValue("equipo", equipoReporte);

            reporte.ExportToDisk(ExportFormatType.PortableDocFormat, rutaPdf);

            bytes = File.ReadAllBytes(rutaPdf);
            pdfBase64 = Convert.ToBase64String(bytes);
            result.Data = pdfBase64;
            result.Value = true;
            File.Delete(rutaPdf);


            return Response.AsJson(result);

        }

        private object ObtenerDemandaArticulo()
        {
            //Result result = new Result();

            var r = new Result<List<DemandaArticulo>>();
            r = _DAVentas.ObtenerDemandaArticulo();

            return Response.AsJson(r);


        }

        private object ObtenerReporteMensualNocturnos(dynamic x)
        {
            Result result = new Result();

            var codEquipo = x.codEquipo;
            DateTime fechaDesde = x.fechaDesde;
            DateTime fechaHasta = x.fechaHasta;

            var r = new Result<List<RptEntradasSalidas>>();
            r = _DAVentas.ObtenerReporteMensualNocturnos(codEquipo, fechaDesde, fechaHasta);
            result.Data = r.Data;
            result.Value = r.Value;
            result.Message = r.Message;

            return Response.AsJson(result);

        }

        private object ImprimirReporteMensualNocturnos(dynamic x)
        {
            Result result = new Result();
            DateTime fechaDesde = x.fechaDesde;
            DateTime fechaHasta = x.fechaHasta;
            int equipo = x.codEquipo;

            var reporteEntradasSalidas = this.Bind<List<RptEntradasSalidas>>();
            var path = HttpRuntime.AppDomainAppPath;
            string rutaPdf = "C:\\PRUEBAPRUEBA\\RptNocturnosTEST.pdf";
            string pdfBase64 = "";
            Byte[] bytes;


            string equipoReporte = equipo == 0 ? "Todos" : reporteEntradasSalidas[0].equipo;

            ReportDocument reporte = new ReportDocument();
            reporte.Load(path + "\\Reportes\\RptNocturnos.rpt");
            reporte.SetDataSource(reporteEntradasSalidas);
            reporte.SetParameterValue("fechaDesde", fechaDesde);
            reporte.SetParameterValue("fechaHasta", fechaHasta);
            reporte.SetParameterValue("equipo", equipoReporte);

            reporte.ExportToDisk(ExportFormatType.PortableDocFormat, rutaPdf);

            bytes = File.ReadAllBytes(rutaPdf);
            pdfBase64 = Convert.ToBase64String(bytes);
            result.Data = pdfBase64;
            result.Value = true;
            File.Delete(rutaPdf);


            return Response.AsJson(result);

        }

        private object ObtenerReporteMensualForaneos(dynamic x)
        {
            Result result = new Result();

            var codEquipo = x.codEquipo;
            DateTime fechaDesde = x.fechaDesde;
            DateTime fechaHasta = x.fechaHasta;

            var r = new Result<List<RptEntradasSalidas>>();
            r = _DAVentas.ObtenerReporteMensualForaneos(codEquipo, fechaDesde, fechaHasta);
            result.Data = r.Data;
            result.Value = r.Value;
            result.Message = r.Message;

            return Response.AsJson(result);

        }

        private object ImprimirReporteMensualForaneos(dynamic x)
        {
            Result result = new Result();
            DateTime fechaDesde = x.fechaDesde;
            DateTime fechaHasta = x.fechaHasta;
            int equipo = x.codEquipo;

            var reporteEntradasSalidas = this.Bind<List<RptEntradasSalidas>>();
            var path = HttpRuntime.AppDomainAppPath;
            string rutaPdf = "C:\\PRUEBAPRUEBA\\RptForaneosTEST.pdf";
            string pdfBase64 = "";
            Byte[] bytes;


            string equipoReporte = equipo == 0 ? "Todos" : reporteEntradasSalidas[0].equipo;

            ReportDocument reporte = new ReportDocument();
            reporte.Load(path + "\\Reportes\\RptForaneos.rpt");
            reporte.SetDataSource(reporteEntradasSalidas);
            reporte.SetParameterValue("fechaDesde", fechaDesde);
            reporte.SetParameterValue("fechaHasta", fechaHasta);
            reporte.SetParameterValue("equipo", equipoReporte);

            reporte.ExportToDisk(ExportFormatType.PortableDocFormat, rutaPdf);

            bytes = File.ReadAllBytes(rutaPdf);
            pdfBase64 = Convert.ToBase64String(bytes);
            result.Data = pdfBase64;
            result.Value = true;
            File.Delete(rutaPdf);


            return Response.AsJson(result);

        }

        private object ObtenerReportePedidosCancelados(dynamic x)
        {
            Result result = new Result();

            DateTime fechaDesde = x.fechaDesde;
            DateTime fechaHasta = x.fechaHasta;
            var codCliente = x.codCliente;
            var codVendedor = x.codVendedor;

            var r = new Result<List<RptPedidosCancelados>>();
            r = _DAVentas.ObtenerReportePedidosCancelados(codCliente, codVendedor, fechaDesde, fechaHasta);
            result.Data = r.Data;
            result.Value = r.Value;
            result.Message = r.Message;

            return Response.AsJson(result);

        }

        private object ImprimirReportePedidosCancelados(dynamic x)
        {
            Result result = new Result();
            
            DateTime fechaDesde = x.fechaDesde;
            DateTime fechaHasta = x.fechaHasta;
            int codCliente = x.codCliente;
            int codVendedor = x.codVendedor;

            var reportePedidosCancelados = this.Bind<List<RptPedidosCancelados>>();
            var path = HttpRuntime.AppDomainAppPath;
            string rutaPdf = "C:\\PRUEBAPRUEBA\\RptPedidosCanceladosTEST.pdf";
            string pdfBase64 = "";
            Byte[] bytes;


            string clienteReporte = codCliente == 0 ? "Todos" : reportePedidosCancelados[0].cliente;
            string vendedorReporte = codVendedor == 0 ? "Todos" : reportePedidosCancelados[0].vendedor;


            ReportDocument reporte = new ReportDocument();
            reporte.Load(path + "\\Reportes\\RptPedidosCancelados.rpt");
            reporte.SetDataSource(reportePedidosCancelados);
            reporte.SetParameterValue("fechaDesde", fechaDesde);
            reporte.SetParameterValue("fechaHasta", fechaHasta);
            reporte.SetParameterValue("cliente", clienteReporte);
            reporte.SetParameterValue("vendedor", vendedorReporte);


            reporte.ExportToDisk(ExportFormatType.PortableDocFormat, rutaPdf);

            bytes = File.ReadAllBytes(rutaPdf);
            pdfBase64 = Convert.ToBase64String(bytes);
            result.Data = pdfBase64;
            result.Value = true;
            File.Delete(rutaPdf);


            return Response.AsJson(result);

        }

        #region [ Pedidos Reagendados ]
        private object ObtenerReportePedidosReagendados(dynamic x)
        {
            try
            {
                Result result = new Result();

                DateTime fechaDesde = x.fechaDesde;
                DateTime fechaHasta = x.fechaHasta;
                var codCliente = x.codCliente;
                var codVendedor = x.codVendedor;

                var r = new Result<List<RptPedidosReagendados>>();
                r = _DAVentas.ObtenerReportePedidosReagendados(codCliente, codVendedor, fechaDesde, fechaHasta);
                result.Data = r.Data;
                result.Value = r.Value;
                result.Message = r.Message;

                return Response.AsJson(result);
            }
            catch(Exception ex)
            {
                return Response.AsJson( new Result());
            }

        }

        private object ImprimirReportePedidosReagendados(dynamic x)
        {
            Result result = new Result();

            DateTime fechaDesde = x.fechaDesde;
            DateTime fechaHasta = x.fechaHasta;
            int codCliente = x.codCliente;
            int codVendedor = x.codVendedor;

            var reportePedidosReagendados = this.Bind<List<RptPedidosReagendados>>();
            var path = HttpRuntime.AppDomainAppPath;
            string rutaPdf = "C:\\PRUEBAPRUEBA\\RptPedidosReagendadosTEST.pdf";
            string pdfBase64 = "";
            Byte[] bytes;

            string clienteReporte = codCliente == 0 ? "Todos" : reportePedidosReagendados[0].cliente;
            string vendedorReporte = codVendedor == 0 ? "Todos" : reportePedidosReagendados[0].vendedor;

            ReportDocument reporte = new ReportDocument();
            reporte.Load(path + "\\Reportes\\RptPedidosReagendados.rpt");
            reporte.SetDataSource(reportePedidosReagendados);
            reporte.SetParameterValue("fechaDesde", fechaDesde);
            reporte.SetParameterValue("fechaHasta", fechaHasta);
            reporte.SetParameterValue("cliente", clienteReporte);
            reporte.SetParameterValue("vendedor", vendedorReporte);


            reporte.ExportToDisk(ExportFormatType.PortableDocFormat, rutaPdf);

            bytes = File.ReadAllBytes(rutaPdf);
            pdfBase64 = Convert.ToBase64String(bytes);
            result.Data = pdfBase64;
            result.Value = true;
            File.Delete(rutaPdf);


            return Response.AsJson(result);

        }

        #endregion
    }
}