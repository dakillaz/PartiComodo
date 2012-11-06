namespace Xlns.PartiComodo.UI.Web.Controllers
{

    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Xlns.PartiComodo.Core.Repository;
    #endregion

    public class DashboardController : Controller
    {
       public ActionResult TourOperatorDashBoard(int id)
        {
            var ar = new AgenziaRepository();
            var model = ar.GetById(id);
            return View(model);
        }

        public ActionResult AgenziaDashBoard(int id)
        {
            var ar = new AgenziaRepository();
            var model = ar.GetById(id);
            return View(model);
        }

        public ActionResult AdminDashBoard()
        {
            return View();
        }
    }
}
