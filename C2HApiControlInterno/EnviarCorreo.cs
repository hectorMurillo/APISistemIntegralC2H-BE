using DA.C2H;
using Models;
using Nancy;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
             
                string htmlBody = @"<html>
                                     <head>
                                        <title> Correo Automatico </title>
                                          <style type = ""text/css"">
	                                        #panelCheckBox
	                                        {
                                               border: solid 3px #00B201;
	                                            background-color:#00C254;
	                                            padding - left:10px;
                                                    font - family:Arial;
                                                    font - weight:Bold;
                                                }
	                                        .check
                                            {
                                                width: 15px;
                                                height: 15px;
                                                border: solid 2px #DDDDDD;	    	                    
	                                            background-color:#FFFFFF;	            
	                                        }
	                                        .regla
                                            {
                                                    background - color:#00C254;
				                                border: solid 3px #00B201;
	                                        }
	                                        #pie{clear: both;}
	                                    </style>
                                    </head>
                                <body>
                                    <table style = ""width:100%"" >
                                         <tr>
                                             <td colspan = ""2"" style = ""font-size:24px;font-weight:bold;font-family:arial;text-align:center"" >
                                                    C&oacute;digo de barras no registrado
                                                   </td>
                                               </tr>  
                                         <tr style = ""font-size:2px;background-color:#FF3333"" >  
                                              <td colspan = ""2"" > Hi! <br>
                            <img src=cid:myImageID>
                            </td></td>
                                         </tr>
                                         <tr>
                                              <td colspan = ""2"" style = ""font-size:14px;font-family:arial;text-align:center;margin:10px"" >
                                                                </br> Cedis: <b> CULIACAN </b>
                                                               </td>
                                          </tr>
                                          <tr>       
                                              <td colspan = ""2"" style = ""font-size:14px;font-family:arial;text-align:center;margin:5px"" >
                                                     </br> C&oacute;digo de barras: <b> " + "asdasdasd" + @" </b>
                                                       </td>
                                                   </tr>
                                                   <tr>
                                                       <td colspan = ""2"" style = ""font-size:14px;font-family:arial;text-align:center;margin:10px"" >
                                                              </br> Descripci&oacute;n: <b> " + "asdasdasd" + @"</b>
                                                                   </td>
                                                               </tr>
                                                               <tr>
                                                                   <td colspan = ""2"" style = ""font-size:14px;font-family:arial;text-align:center;margin:10px"" >
                                              </br> Comentario: <b> " + "asdasdasd" + @"</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan = ""2"" style = ""font-size:14px;font-family:arial;text-align:center;margin:10px"" >
                                                       </br> Orden de Compra: <b> " + "asdasdasd" + @"</b>
                                               </td>
                                           </tr>
                                           <tr>
                                               <td colspan = ""2"" style = ""font-size:14px;font-family:arial;text-align:center;margin:10px"" > 
                                                </br> Proveedor: <b> " + "asdasdasd" + @"</b>
                                            </td>
                                           </tr>
                                           <tr>
                                           <td></td>
                                            <td>
                                              <br/> 
                                                <b> Report&oacute;:</b> " + "asdasdasd" + @"<br/>
                                           </td>
                                           </tr>
                                           <tr style= ""font-size:2px;background-color:FF3333"" >
                                           <td style= ""background-color:#ffffff"" ></td><td width = ""70%"" > &nbsp;</td>
                                           </tr>
                                           </table>
                                          <div id =""#pie"" >
                                              <div style = ""text-align: right; font-style: italic;"" >
                                                La captura del artículo se encuentra en el archivo adjunto
                                                 </div>
                                               </div>
                                    </body>
                                </html>";
                WarmPack.Utilities.MailSender email = new WarmPack.Utilities.MailSender(Globales.Host, Globales.Port, false, Globales.CorreoAutomatico, Globales.CorreoAutomaticoPassword);
                //email.Send(Globales.CorreoAutomatico, "cris_ales@live.com.mx", "Correo Automatico", htmlBody, true);
                sendHtmlEmail(Globales.CorreoAutomatico, "cris_ales@live.com.mx", htmlBody, "asdads","biiin");
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
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");

            //Add Image
            LinkedResource theEmailImage = new LinkedResource("C:\\temp\\img.png");
            theEmailImage.ContentId = "myImageID";

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