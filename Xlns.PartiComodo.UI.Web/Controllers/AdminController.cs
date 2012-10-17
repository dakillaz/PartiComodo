using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xlns.PartiComodo.Core.Repository;

namespace Xlns.PartiComodo.UI.Web.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

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

        [HttpPost]
        public ActionResult Confirm(int id)
        {
            var vr = new ViaggioRepository();
            var trip = vr.GetById(id);
            trip.Approvato = true;
            vr.Save(trip);
            var model = vr.GetUnapproved();
            return View("List", model);
        }
    }
}
