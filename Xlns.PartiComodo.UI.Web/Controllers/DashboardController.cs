using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xlns.PartiComodo.Core.Repository;

namespace Xlns.PartiComodo.UI.Web.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/

        public ActionResult Index(int id)
        {
            var ar = new AgenziaRepository();
            var model = ar.GetById(id);
            return View(model);
        }

    }
}
