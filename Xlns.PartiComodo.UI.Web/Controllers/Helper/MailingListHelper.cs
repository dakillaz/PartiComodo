namespace Xlns.PartiComodo.UI.Web.Controllers.Helper
{

    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Xlns.PartiComodo.Core.Model;
    using System.Text;
    #endregion

    public class MailingListHelper
    {
        public string GetMailingList(IList<Viaggio> viaggi)
        {
            var builder = new StringBuilder("MailingList della Settimana \n");
            foreach (var viaggio in viaggi)
                builder.AppendLine(GetMailText(viaggio));
            return builder.ToString();
        }

        private string GetMailText(Viaggio viaggio)
        {
            var builder = new StringBuilder();
            var title = string.Format("Iniziativa: {0}, proposta da: {1}", viaggio.Nome, viaggio.Agenzia.Nome);
            builder.AppendLine(title);
            var details = string.Format("{0}", viaggio.Descrizione);
            builder.AppendLine(details);
            var tappe = string.Format("Partenza il: {0}", viaggio.DataPartenza);
            builder.AppendLine(tappe);
            var link = string.Format("Per dettagli: http://www.xlns.it/Viaggio/Details/{0}", viaggio.Id);
            builder.AppendLine(link);
            return builder.ToString();
        }
    }
}