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
                                              <td colspan = ""2"" > &nbsp;</td>
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
                email.Send(Globales.CorreoAutomatico, "christianal@difarmer.com", "Correo Automatico", htmlBody, true);

                r.Value = true;
                return Response.AsJson(r); ;
            }
            catch (Exception ex)
            {
                r.Value = false;
                return Response.AsJson(new Result(ex));
            }
        }
    }
}