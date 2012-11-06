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
    #endregion

    public class AgenziaController : Controller
    {

        AgenziaRepository ar = new AgenziaRepository();

        public ActionResult List()
        {
            var model = ar.GetAllAgenzie(100, 0);
            return View(model);
        }

        public ActionResult ListTO(IList<Agenzia> agenzie)
        {
            var model = ar.GetAllTO(100, 0);
            return View("List", model);
        }

        public ActionResult ListAdV(IList<Agenzia> agenzie)
        {
            var model = ar.GetAllAdV(100, 0);
            return View("List", model);
        }

        public ActionResult Edit(int id)
        {
            var model = ar.GetById(id);
            return View(model);
        }

        #region HttpPost
        public ActionResult Save(Agenzia agenzia)
        {
            if (ModelState.IsValid)
                ar.Save(agenzia);
            return Edit(agenzia.Id);
        }
        #endregion
    }
}
