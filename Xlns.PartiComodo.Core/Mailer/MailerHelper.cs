namespace Xlns.PartiComodo.Core.Mailer
{

    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Net.Mail;
    using System.Net;
    #endregion

    public class MailerHelper
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="message">The message.</param>
        public void SendMail(string to, string message)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(ConfigurationManager.Configurator.Istance.mailFrom);
                mail.To.Add(new MailAddress(to));
                mail.Subject = ConfigurationManager.Configurator.Istance.mailSubject;
                mail.Body = message;
                SmtpClient smtp = new SmtpClient();
                smtp.Port = ConfigurationManager.Configurator.Istance.smtpPort;
                smtp.Host = ConfigurationManager.Configurator.Istance.smtpHost;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(ConfigurationManager.Configurator.Istance.smtpUsername,
                     ConfigurationManager.Configurator.Istance.smtpPassword);
                smtp.EnableSsl = true;
                smtp.Send(mail);
                logger.Debug("Email inviata con successo all'indirizzo {0}", to);
            }
            catch
            {
                throw;
            }
        }
    }
}
