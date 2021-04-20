using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace C2HApiControlInterno
{
    public class EnviarCorreo
    {
        public EnviarCorreo()
        {

        }

        public void sendMail()
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
    }
}