using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xlns.PartiComodo.Core.Repository;
using Xlns.PartiComodo.Core.Model;
using Xlns.PartiComodo.UI.Web.Controllers.Helper;
using Xlns.PartiComodo.UI.Web.ViewModels;
using Xlns.PartiComodo.UI.Web.Helpers;

namespace Xlns.PartiComodo.UI.Web.Controllers
{
    public class ViaggioController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult List()
        {
            var vr = new ViaggioRepository();
            var model = vr.GetApproved();
            var mlh = new MailingListHelper();
            mlh.GetMailingList(model);
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var vr = new ViaggioRepository();
            var model = vr.GetById(id);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var vr = new ViaggioRepository();
            var model = vr.GetById(id);
            return View(model);
        }

        public ActionResult Create()
        {
            var model = new Viaggio();
            return View(model);
        }
        
        [ChildActionOnly]
        public ActionResult Search(int idAgenzia, bool searchApproved, bool searchUnapproved, bool searchMine, bool searchTheirs, string ViewName)
        {
            return PartialView(new ViaggioSearchViewModel() { idAgenzia = idAgenzia, searchApproved = searchApproved,  searchUnapproved = searchUnapproved,  searchMine = searchMine,  searchTheirs = searchTheirs,  ViewName = ViewName});
        }

        public ActionResult SearchTappa(int tipo)
        {
            var tappaSearch = new Tappa()
            {
                Tipo = (TipoTappa)tipo,
            };
            return PartialView("SearchTappa", tappaSearch);
        }

        #region TO Actions
        public ActionResult ListMine(int id)
        {
            var vr = new ViaggioRepository();
            var ar = new AgenziaRepository();
            var agency = ar.GetById(id);
            var model = vr.GetListaViaggiByAgenzia(agency);
            return View(model);
        }

        public ActionResult ListMineNotApproved(int id)
        {
            var vr = new ViaggioRepository();
            var ar = new AgenziaRepository();
            var agency = ar.GetById(id);
            var model = vr.GetUnapproved().Where(c => c.Agenzia.Id == agency.Id);
            return View(model.ToList());
        }
        #endregion

        #region HttpPost
        [HttpPost]
        public ActionResult Save()
        {
            return RedirectToAction("ShowMine");
        }

        [HttpPost]
        public ActionResult Search(ViaggioSearchViewModel searchParams)
        {

            var viaggiFound = new ViaggioRepository().Search(ViaggioHelper.getViaggioSearchParams(searchParams));

            return View("List", viaggiFound);

        }
        #endregion
    }
}
