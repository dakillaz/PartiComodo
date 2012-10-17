using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xlns.PartiComodo.Core.Repository;
using Xlns.PartiComodo.Core.Model;

namespace Xlns.PartiComodo.UI.Web.Controllers
{
    public class ViaggioController : Controller
    {
        //
        // GET: /Viaggio/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var vr = new ViaggioRepository();
            var model = vr.GetById(id);
            return View(model);
        }

        public ActionResult List()
        {
            var vr = new ViaggioRepository();
            var model = vr.GetApproved();
            return View(model);
        }

        public ActionResult Create()
        {
            var model = new Viaggio();
            return View(model);
        }

        public ActionResult ShowMine(int id)
        {
            var vr = new ViaggioRepository();
            var ar = new AgenziaRepository();
            var agency = ar.GetById(id);
            var model = vr.GetListaViaggiByAgenzia(agency);
            return View(model);
        }

        #region HttpPost
        [HttpPost]
        public ActionResult Save()
        {

            return RedirectToAction("ShowMine");
        }
        #endregion
    }
}
