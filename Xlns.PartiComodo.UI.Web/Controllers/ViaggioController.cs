using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xlns.PartiComodo.Core.Repository;
using Xlns.PartiComodo.Core.Model;
using Xlns.PartiComodo.UI.Web.Controllers.Helper;
using Xlns.PartiComodo.Core;

namespace Xlns.PartiComodo.UI.Web.Controllers
{
    public class ViaggioController : Controller
    {

        ViaggioRepository vr = new ViaggioRepository();
        private ViaggioManager vm = new ViaggioManager();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var model = vr.GetApproved();
            var mlh = new MailingListHelper();
            mlh.GetMailingList(model);
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var model = vr.GetById(id);
            return View(model);
        }

        public ActionResult Edit(int id)
        {

            var model = vr.GetById(id);
            return View(model);
        }

        public ActionResult Create(int id)
        {
            var ar = new AgenziaRepository();
            var agency = ar.GetById(id);
            var model = new Viaggio
            {
                Agenzia = agency,
                Nome = string.Format("Nuovo Viaggio {0}", vr.GetListaViaggiByAgenzia(agency).Count + 1),
                Descrizione = string.Format("Nuovo Viaggio di {0}", agency.Nome),
                Approvato = false,
            };
            vr.Save(model);
            return RedirectToAction("Edit", new { id = model.Id });
        }

        public ActionResult CreateTappa(int tipo, int idViaggio)
        {
            var viaggio = vr.GetById(idViaggio);
            var nuovaTappa = new Tappa()
            {
                Tipo = (TipoTappa)tipo,
                Viaggio = viaggio,
                Ordinamento = CalcolaOrdinamentoPerNuovaTappa(viaggio)
            };
            return PartialView("EditTappa", nuovaTappa);
        }

        public ActionResult EditTappa(int id)
        {
            var tappa = vr.GetTappaById(id);
            return PartialView(tappa);
        }

        public ActionResult EditTappeViaggio(int idViaggio)
        {
            var viaggio = vr.GetById(idViaggio);
            return PartialView(viaggio);
        }

        #region TO Actions
        public ActionResult ListMine(int id)
        {
            var ar = new AgenziaRepository();
            var agency = ar.GetById(id);
            var model = vr.GetListaViaggiByAgenzia(agency);
            return View(model);
        }

        public ActionResult ListMineNotApproved(int id)
        {
            var ar = new AgenziaRepository();
            var agency = ar.GetById(id);
            var model = vr.GetUnapproved().Where(c => c.Agenzia.Id == agency.Id);
            return View(model.ToList());
        }
        #endregion

        #region HttpPost
        [HttpPost]
        public ActionResult Save(Viaggio model)
        {
            if (ModelState.IsValid)
            {
                vr.Save(model);
                return RedirectToAction("ListMine", new { id = model.Agenzia.Id });
            }
            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult SaveTappa(Tappa tappa)
        {
            if (tappa.Viaggio != null && tappa.Viaggio.Id != 0)
            {
                tappa.Viaggio = vr.GetById(tappa.Viaggio.Id);
            }
            if (!ModelState.IsValid)
            {
                vr.Save(tappa);
                return RedirectToAction("EditTappeViaggio", new { idViaggio = tappa.Viaggio.Id });
            }
            else
            {
                string msg = "Impossibile salvare la tappa modificata o creata";
                throw new Exception(msg);
            }
        }

        [HttpPost]
        public void DeleteTappaAjax(int id)
        {
            try
            {
                vm.DeleteTappa(id);
            }
            catch (Exception ex)
            {
                string msg = String.Format("Errore durante l'eliminazione della tappa con id={0}", id);
                throw new Exception(msg);
            }
        }

        #endregion

        private int CalcolaOrdinamentoPerNuovaTappa(Viaggio viaggio)
        {
            var tappe = viaggio.Tappe.Where(t => t.Tipo != TipoTappa.DESTINAZIONE);
            if (tappe != null && tappe.Count() > 0)
            {
                int result = tappe.Max(t => t.Ordinamento) + 1;
                return result;
            }
            else
                return 1;
        }
    }
}
