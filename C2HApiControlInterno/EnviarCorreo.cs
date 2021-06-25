using DA.C2H;
using Models;
using Models.Dosificador;
using Models.Porteros;
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

        public Result SendMail(PedidoCorreoModel data)
        {
            Result r = new Result();
            try
            {
                string urlEvaluacion = "http://localhost:8080/#/encuesta/" + data.FolioPedido.ToString();
                string urlGoogle = "https://www.google.com";
                string urlFb = "https://www.facebook.com/Concretos-2H-Culiacan-2240349032865063";
                string urlWeb = "https://www.concretos2h.com.mx/";
                //string htmlBody = @"
                //                <html>
                //                    <body>
                //                        <img src=cid:correo usemap=""#image-map"">
                //                        <map name =""image-map"">
                //                            <area target = ""_blank"" alt="""" title ="""" href=""www.google.com"" coords=""140,420,361,473"" shape=""rect"">
                //                        </map>
                //                    </body>
                //                 </html>
                //";
                string html = "<img src=cid:correo usemap ='#clickMap'>";
                html +=  "<map id =\"clickMap\" name=\"clickMap\">" +
                         "<area shape =\"rect\" coords =\"146,381,383,441\" href =" + urlGoogle + ">" +
                         " <area shape =\"rect\" coords =\"194,584,226,615\" href =" + urlFb + ">" +
                         " <area shape =\"rect\" coords =\"315,585,348,615\" href =" + urlWeb + "></map>";
                //                string htmlBody = @"

                //<html>

                //<body style=""margin: 0; padding: 0;"">
                //    <table  cellpadding=""0"" cellspacing=""0"" width=""100%"">
                //        <tr>
                //            <td>
                //                <table align=""center""  cellpadding=""0"" cellspacing=""0"" width=""800""
                //                    style=""border-collapse: collapse;"">
                //                    <tr bgcolor=""#70bbd9"">
                //                        <td align=""left"" bgcolor=""white"" style=""padding: 10px 0px 10px 0;"">
                //                            <img src=cid:logotipo alt=""Creating Email Magic"" width=""200"" height=""95""
                //                                style=""display: block;"" />
                //                        </td>
                //                        <td align=""right"" bgcolor=""white"" style=""padding: 10px 0px 10px 0px;"" >
                //                            <div style=""font-size: 24px;""</div>
                //                        </td>

                //                    </tr>

                //                    <tr>
                //                        <td  bgcolor=""#0076AC"" style=""padding: 10px 0px 10px 0;"" colspan=""2"" >
                //                            <div style=""font-style: oblique; font-size: 18px;color: white; text-align: right; margin-right: 10px;"">24/04/2021</div>  
                //                            <div style=""font-size: 20px; color: white; margin-right: 10px; margin-left: 10px"">Estimado " + data.NombreComercial + @"</div>
                //                            <BR><BR>
                //                            <div style=""font-size: 17px; color: white;  margin-right: 10px; margin-left: 10px"">
                //                                Concretos 2H, Empresa experta en la fabricación y comercialización de concreto y materiales para la construcción en cualquier obra, especializados en concretos de alta resistencia.
                //                            </div>
                //                            <BR>
                //                             <div style=""font-size: 17px; color: white; margin-right: 10px; margin-left: 10px "">
                //                               La encuesta llevará aproximadamente 5 minutos responderla. 
                //                               Las respuestas que otorgue se mantendrán estrictamente confidenciales y serán usadas únicamente con fines estadísticos.
                //                            </div>
                //                        </td >
                //                    </tr>
                //                    <tr>
                //                        <td align=""center"" bgcolor=""white"" style=""padding: 5px 0px 5px 0;"" colspan=""2"">
                //                            <div style=""border: 5px solid gray ;width:40%; font-size: 20px; "">
                //                                 <div style=""margin-bottom: 15px; margin-top: 10px;  color: black;"">
                //                                 <a style=""font: bold 18px Arial;
                //                                            text-decoration: none;   
                //                                            background-color:orange;  
                //                                            color: #333333; 
                //                                          border-left: 10px solid orange; 
                //                                            border-bottom: 10px solid orange;  
                //                                            border-right: 10px solid orange;  
                //                                            border-top: 10px solid orange; "" 
                //                                    href=" + urlEvaluacion + ">" + @" Aceptar y responder</a>
                //                                   </div> 
                //                             </div>
                //                        </td >
                //                    </tr>
                //                    <tr>
                //                        <td align=""right"" bgcolor=""#0076AC"" style=""padding: 10px 10px 20px 0;""  colspan=""2"" >
                //                            <div style=""font-size: 16px; margin-bottom: 5px;  color: white;"">Queremos seguir construyendo contigo </div>
                //                            <div style=""font-size: 16px;margin-bottom: 5px;  color: white;"">Telefono: 72 71488</div>
                //                            <div style=""font-size: 16px;  color: white;"">Celular Vendedor: 672 111 8956</div>
                //                        </td >
                //                    </tr>
                //                </table>
                //            </td>
                //        </tr>
                //    </table>
                //</body>
                //</html>
                //";


                sendHtmlEmail(Globales.CorreoAutomatico, data.Correo , html, "Concretos2H", "Estimado Cliente ¡Queremos saber tu opinión!");
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
                LinkedResource theEmailImage = new LinkedResource(path + "\\Images\\correo.jpg", MediaTypeNames.Image.Jpeg);
                theEmailImage.ContentId = "correo";

                htmlView.LinkedResources.Add(theEmailImage);

                mail.AlternateViews.Add(htmlView);

                mail.From = new MailAddress(from_Email, from_Name);

                mail.To.Add(to_Email);

                mail.Subject = Subject;

                using(var client = new SmtpClient(Globales.Host, Globales.Port))
                {
                    client.Credentials = new System.Net.NetworkCredential(Globales.CorreoAutomatico, Globales.CorreoAutomaticoPassword);
                    //client.EnableSsl = false;

                    try
                    {
                        client.Send(mail);
                        client.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("The email was not sent.");
                        Console.WriteLine("Error message: " + ex.Message);
                    }
                }
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