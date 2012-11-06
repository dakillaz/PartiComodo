namespace Xlns.PartiComodo.UI.Web.Controllers
{

    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Xlns.PartiComodo.Core.Repository;
    using Xlns.PartiComodo.UI.Web.Controllers.Helper;
    using Xlns.PartiComodo.Core.Mailer;
    #endregion

    public class AdminController : Controller
    {

        public ActionResult Index()
        {
            var vr = new ViaggioRepository();
            var model = vr.GetUnapproved();
            return View(model);
        }

        public ActionResult List()
        {
            var vr = new ViaggioRepository();
            var model = vr.GetUnapproved();
            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult MailingList()
        {
            return View();
        }

        #region HttpPost
        [HttpPost]
        public ActionResult Confirm(int id)
        {
            var vr = new ViaggioRepository();
            var trip = vr.GetById(id);
            trip.Approvato = true;
            trip.DataApprovazione = DateTime.Now;
            vr.Save(trip);
            var model = vr.GetUnapproved();
            return View("List", model);
        }

        [HttpPost]
        public ActionResult SendMailingList(DateTime dataApprovazione)
        {
            var vr = new ViaggioRepository();
            var viaggi = vr.GetApproved().Where(c => c.DataApprovazione >= dataApprovazione);
            if (viaggi.Count() > 0)
            {
                var ar = new AgenziaRepository();
                var agenzie = ar.GetAllAgenzie(100, 0);
                if (agenzie.Count > 0)
                {
                    var mh = new MailerHelper();
                    var mlh = new MailingListHelper();
                    var mailText = mlh.GetMailingList(viaggi.ToList());
                    foreach (var agenzia in agenzie)
                        mh.SendMail(agenzia.Email, mailText);
                }
            }
            return RedirectToAction("AdminDashBoard", "Dashboard");
        }
        #endregion
    }
}
