using DA.C2H;
using Models;
using Models.Dosificador;
using Nancy;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using WarmPack.Classes;

namespace C2HApiControlInterno
{
    public class EnviarCorreo : NancyModule
    {
        private readonly DAHerramientas _DAHerramientas = null;
        public EnviarCorreo()
        {
            _DAHerramientas = new DAHerramientas();
            this.CargarParametros();
        }

        private void CargarParametros()
        {
            Globales.Port = int.Parse(_DAHerramientas.ObtenerParametro("PuertoCorreo").Data.Valor);
            Globales.Host = _DAHerramientas.ObtenerParametro("HostCorreo").Data.Valor;
            Globales.CorreoAutomatico = _DAHerramientas.ObtenerParametro("CorreoPrincipal").Data.Valor;
            Globales.CorreoAutomaticoPassword = _DAHerramientas.ObtenerParametro("CorreoPrincipalPassword").Data.Valor;
        }

        public Result SendMail(DatosNotaRemision data)
        {
            Result r = new Result();
            try
            {
             
                string htmlBody = @"
        
<html>

<body style=""margin: 0; padding: 0;"">
    <table  cellpadding=""0"" cellspacing=""0"" width=""100%"">
        <tr>
            <td>
                <table align=""center""  cellpadding=""0"" cellspacing=""0"" width=""800""
                    style=""border-collapse: collapse;"">
                    <tr bgcolor=""#70bbd9"">
                        <td align=""left"" bgcolor=""white"" style=""padding: 10px 0px 10px 0;"">
                            <img src=cid:logotipo alt=""Creating Email Magic"" width=""200"" height=""95""
                                style=""display: block;"" />
                        </td>
                        <td align=""right"" bgcolor=""white"" style=""padding: 10px 0px 10px 0px;"" >
                            <div style=""font-size: 24px;"">" + data.Cliente + @"</div>
                        </td>

                    </tr>
    
                    <tr>
                        <td align=""center"" bgcolor=""#0076AC"" style=""padding: 0px 0px 10px 0;"" colspan=""2"" >
                            <div style=""font-style: oblique; font-size: 18px;color: white; text-align: right; margin-right: 10px;"">24/04/2021</div>  
                            <div style=""font-size: 25px; color: white;"">¡ESTAMOS A PUNTO DE HACER EQUIPO CONTIGO!</div>
                            <div style=""font-size: 20px; color: white;"">Vamos rumbo a tu obra, esperemos estes conforme con nuestros servicios</div>
                        </td >
                    </tr>
                    <tr>
                        <td align=""center"" bgcolor=""white"" style=""padding: 5px 0px 5px 0;"" colspan=""2"">
                            <div style=""border: 5px solid gray ;width:60%; font-size: 20px; "">
                                 <div style=""margin-bottom: 5px; margin-top: 50px;  color: black;"">
                                    <b>Número de Nota de Remisión: </b>" + data.Folio+ @"
                                 </div> 
                                <div style=""margin-top: 50px;  color: black;"">
                                    <b>Folio del Pedido: </b>" + data.FolioPedido + @"
                                 </div> 
                                 <div style=""margin-bottom: 5px; color: black;"">
                                    <b>Tu Agente de Ventas: </b>" + data.Vendedor + @"
                                 </div>
                                <div style=""margin-bottom: 50px; color: black;"">
                                    <b>Obra: </b>"+data.Obra+ @"
                                </div>
                             </div>
                        </td >
                    </tr>
                    <tr>
                        <td align=""right"" bgcolor=""#0076AC"" style=""padding: 10px 10px 20px 0;""  colspan=""2"" >
                            <div style=""font-size: 16px; margin-bottom: 5px;  color: white;"">Queremos seguir construyendo contigo </div>
                            <div style=""font-size: 16px;margin-bottom: 5px;  color: white;"">Telefono: "+data.TelefonoEmpresa+ @"</div>
                            <div style=""font-size: 16px;  color: white;"">Celular Vendedor: "+data.CelularVendedor+@"</div>
                        </td >
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>
                                    ";

                sendHtmlEmail(Globales.CorreoAutomatico, data.CorreoCliente, htmlBody, "Concretos2H","Pedido Generado");
                r.Value = true;
                return r;
            }
            catch (Exception ex)
            {
                r.Value = false;
                r.Message = ex.Message;
                return r;
            }
        }

        protected Result sendHtmlEmail(string from_Email, string to_Email, string body, string from_Name, string Subject)
        {
            Result r = new Result();
            try
            {
                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

                var path = AppDomain.CurrentDomain.BaseDirectory + "bin";
                LinkedResource theEmailImage = new LinkedResource(path + "\\Images\\logotipo.jpg", MediaTypeNames.Image.Jpeg);
                theEmailImage.ContentId = "logotipo";

                htmlView.LinkedResources.Add(theEmailImage);

                mail.AlternateViews.Add(htmlView);

                mail.From = new MailAddress(from_Email, from_Name);

                mail.To.Add(to_Email);

                mail.Subject = Subject;

                System.Net.NetworkCredential cred = new System.Net.NetworkCredential(Globales.CorreoAutomatico, Globales.CorreoAutomaticoPassword);
                SmtpClient smtp = new SmtpClient(Globales.Host, Globales.Port);
                smtp.EnableSsl = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = cred;
               
                smtp.Send(mail);
                r.Value = true;
                return r;
            }
            catch (Exception ex)
            {

                r.Value = false;
                r.Message = ex.Message;
                return r;
            }
           
        }
    }
}