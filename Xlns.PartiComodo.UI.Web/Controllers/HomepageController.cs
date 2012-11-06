namespace Xlns.PartiComodo.UI.Web.Controllers
{

    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Xlns.PartiComodo.UI.Web.ViewModels;
    #endregion

    public class HomepageController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Facebook()
        {
            var facebookApplicationId = ConfigurationManager.Configurator.Istance.facebookApplicationId;
            var viewModel = new FacebookViewModel
            {
                Facebook = facebookApplicationId
            };
            return PartialView(viewModel);
        }
    }
}
