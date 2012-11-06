namespace Xlns.PartiComodo.UI.Web.Controllers
{

    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Xlns.PartiComodo.Core.Repository;
    using Xlns.PartiComodo.Core.Model;
    using Xlns.PartiComodo.UI.Web.ViewModels;
    using Xlns.PartiComodo.UI.Web.Controllers.Helper;

    #endregion

    public class FlyerController : Controller
    {
        FlyerRepository fr = new FlyerRepository();
        AgenziaRepository ar = new AgenziaRepository();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int id)
        {
            var agency = ar.GetById(id);
            var viewModel = new ListFlyerViewModel
            {
                Flyers = fr.GetFlyersPerAgenzia(id),
                Agenzia = ar.GetById(id)
            };
            return View(viewModel);
        }

        public ActionResult ListPubblicati()
        {
            var model = fr.GetFlyers(true);
            return View(model);
        }

        public ActionResult Create(int id)
        {
            var agency = ar.GetById(id);
            var model = new Flyer
            {
                Titolo = string.Format("Nuovo Flyer {0}", fr.GetFlyersPerAgenzia(id).Count + 1),
                Descrizione = string.Format("Nuovo Flyer di {0}", agency.Nome),
                Agenzia = agency,
                IsPubblicato = false
            };
            fr.Save(model);
            return View("Edit", model);
        }

        public ActionResult Edit(int id)
        {
            var model = fr.GetById(id);
            return View("Edit", model);
        }

        public ActionResult Populate(int id)
        {
            var flyer = fr.GetById(id);
            var vr = new ViaggioRepository();
            var viaggi = vr.GetApproved();
            var viewModel = FlyerHelper.GetViaggiSelezionabili(flyer, viaggi);
            return View(viewModel);
        }

        public ActionResult Delete(int id)
        {
            var flyer = fr.GetById(id);
            var agencyId = flyer.Agenzia.Id;
            flyer.Viaggi = null;
            fr.Save(flyer);
            fr.Delete(flyer);
            return RedirectToAction("List", new { id = agencyId });
        }

        #region HttpPost
        [HttpPost]
        public ActionResult Save(Flyer model)
        {
            var flyer = fr.GetById(model.Id);
            flyer.IsPubblicato = model.IsPubblicato;
            flyer.Titolo = model.Titolo;
            flyer.Descrizione = model.Descrizione;
            fr.Save(flyer);
            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult ToggleViaggio(int idFlyer, int idViaggio)
        {
            var vr = new ViaggioRepository();
            var viaggio = vr.GetById(idViaggio);
            var flyer = fr.GetById(idFlyer);
            FlyerHelper.ToggleViaggio(flyer, viaggio);
            fr.Save(flyer);
            return null;
        }
        #endregion
    }
}
