using DA.C2H;
using Models;
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

        public void sendMailTest()
        {
            SmtpClient client = new SmtpClient()
            {
                Host = "mail.concretos2h.com",
                Port = 8889,
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = "atencion@concretos2h.com",
                    Password = "_atencion1234"

                }
            };

            MailAddress fromEmail = new MailAddress("atencion@concretos2h.com", "prueba");
            MailAddress toEmail = new MailAddress("cris_ales@live.com.mx", "bien");
            MailMessage message = new MailMessage(){
                From = fromEmail,
                Subject = "asd",
                Body = "epalemipiernita"
            };

            message.To.Add(toEmail);
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public object SendMail(string mensaje,string destinatario)
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
                            <div style=""font-size: 24px;"">Christian alvarado</div>
                        </td>

                    </tr>
    
                    <tr>
                        <td align=""center"" bgcolor=""#0076AC"" style=""padding: 0px 0px 10px 0;"" colspan=""2"" >
                            <div style=""font-style: oblique; font-size: 18px;color: white; text-align: right; margin-right: 10px;"">12/05/2015</div>  
                            <div style=""font-size: 25px; color: white;"">¡Estamos a punto de hacer equipo contigo!</div>
                            <div style=""font-size: 20px; color: white;"">Vamos rumbo a tu obra, esperemos estes conforme con nuestros servicios</div>
                        </td >
                    </tr>
                    <tr>
                        <td align=""center"" bgcolor=""white"" style=""padding: 5px 0px 5px 0;"" colspan=""2"">
                            <div style=""border: 5px solid gray ;width:60%; font-size: 20px; "">
                                 <div style=""margin-bottom: 5px; margin-top: 50px;  color: black;"">
                                    <b>Número de Nota de Remisión: </b>158787 
                                 </div> 
                                 <div style=""margin-bottom: 5px; color: black;"">
                                    <b>Tu Agente de Ventas: </b>Miguel Perez 
                                 </div>
                                <div style=""margin-bottom: 50px; color: black;"">
                                    <b>Obra: </b>colonia nice 
                                </div>
                             </div>
                        </td >
                    </tr>
                    <tr>
                        <td align=""right"" bgcolor=""#0076AC"" style=""padding: 10px 10px 20px 0;""  colspan=""2"" >
                            <div style=""font-size: 16px; margin-bottom: 5px;  color: white;"">Queremos seguir construyendo contigo </div>
                            <div style=""font-size: 16px;margin-bottom: 5px;  color: white;"">Telefono: 654-87-89</div>
                            <div style=""font-size: 16px;  color: white;"">WhatsApp Vendedor: 656-54-54</div>
                        </td >
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>
                                    ";

                //WarmPack.Utilities.MailSender email = new WarmPack.Utilities.MailSender(Globales.Host, Globales.Port, false, Globales.CorreoAutomatico, Globales.CorreoAutomaticoPassword);
                //email.Send(Globales.CorreoAutomatico, "cris_ales@live.com.mx", "Correo Automatico", htmlBody, true);
               
                sendHtmlEmail(Globales.CorreoAutomatico, "c21.hector@gmail.com", htmlBody, "Concretos2H","Pedido Generado");
                r.Value = true;
                return null;
            }
            catch (Exception ex)
            {
                r.Value = false;
                return Response.AsJson(new Result(ex));
            }
        }

        protected void sendHtmlEmail(string from_Email, string to_Email, string body, string from_Name, string Subject)
        {
            //create an instance of new mail message
            MailMessage mail = new MailMessage();

            //set the HTML format to true
            mail.IsBodyHtml = true;

            //create Alrternative HTML view
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

            var prueba = AppDomain.CurrentDomain.BaseDirectory + "bin";
            //Add Image
            LinkedResource theEmailImage = new LinkedResource(prueba + "\\logotipo.jpg", MediaTypeNames.Image.Jpeg);
            theEmailImage.ContentId = "logotipo";

            //Add the Image to the Alternate view
            htmlView.LinkedResources.Add(theEmailImage);

            //Add view to the Email Message
            mail.AlternateViews.Add(htmlView);

            //set the "from email" address and specify a friendly 'from' name
            mail.From = new MailAddress(from_Email, from_Name);

            //set the "to" email address
            mail.To.Add(to_Email);

            //set the Email subject
            mail.Subject = Subject;

            //set the SMTP info
            System.Net.NetworkCredential cred = new System.Net.NetworkCredential(Globales.CorreoAutomatico,Globales.CorreoAutomaticoPassword);
            SmtpClient smtp = new SmtpClient(Globales.Host, Globales.Port);
            smtp.EnableSsl = false;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = cred;
            //send the email
            smtp.Send(mail);
        }
    }
}