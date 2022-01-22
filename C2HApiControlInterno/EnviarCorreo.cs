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
            //Globales.Port = int.Parse(_DAHerramientas.ObtenerParametro("PuertoCorreo").Data.Valor);
            //Globales.Host = _DAHerramientas.ObtenerParametro("HostCorreo").Data.Valor;
            //Globales.CorreoAutomatico = _DAHerramientas.ObtenerParametro("CorreoPrincipal").Data.Valor;
            //Globales.CorreoAutomaticoPassword = _DAHerramientas.ObtenerParametro("CorreoPrincipalPassword").Data.Valor;
            //Globales.URLEncuesta = _DAHerramientas.ObtenerParametro("URLEncuesta").Data.Valor;
            //Globales.URLFacebook = _DAHerramientas.ObtenerParametro("URLFacebook").Data.Valor;
            //Globales.URLWeb = _DAHerramientas.ObtenerParametro("URLWeb").Data.Valor;
        }

        public Result SendMail(PedidoCorreoModel data)
        {
            Result r = new Result();
            try
            {

                string html = "<img src=cid:correo usemap ='#clickMap'>";
                html +=  "<map id =\"clickMap\" name=\"clickMap\">" +
                         "<area shape =\"rect\" coords =\"146,381,383,441\" href =" + Globales.URLEncuesta + ">" +
                         " <area shape =\"rect\" coords =\"194,584,226,615\" href =" + Globales.URLFacebook + ">" +
                         " <area shape =\"rect\" coords =\"315,585,348,615\" href =" + Globales.URLWeb + "></map>";


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
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    client.Credentials = new System.Net.NetworkCredential(Globales.CorreoAutomatico, Globales.CorreoAutomaticoPassword);

                    try
                    {
                        client.Send(mail);
                        client.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("En Correo no fue enviado");
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