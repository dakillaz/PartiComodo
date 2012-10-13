namespace Xlns.PartiComodo.UI.Web.Controllers
{
    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using ViewModels;
    using Xlns.PartComodo.Core.Crypto;
    #endregion

    public class LoginController : Controller
    {
        public ActionResult Register()
        {
            return View();
        }

        #region HttpPost
        [HttpPost]
        public ActionResult Register(RegisterViewModel viewModel)
        {
            return View("Register");
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel viewModel)
        {
            CryptoHelper helper = new CryptoHelper();
            return View("Register");
        }
        #endregion
    }
}
