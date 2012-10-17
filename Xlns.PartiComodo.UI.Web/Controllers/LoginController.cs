namespace Xlns.PartiComodo.UI.Web.Controllers
{
    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using ViewModels;
    using Xlns.PartiComodo.Core.Model;
    using Xlns.PartiComodo.Core.Repository;
    using Xlns.PartiComodo.Core.Crypto;
    using Xlns.PartiComodo.UI.Web.Controllers.Helper;
    #endregion

    public class LoginController : Controller
    {
        public ActionResult Register()
        {
            var viewModel = new Agenzia();
            return View(viewModel);
        }

        public ActionResult ResetPassword()
        {
            var viewModel = new Agenzia();
            return View(viewModel);
        }

        public ActionResult Logout()
        {
            Session.Logout();
            return RedirectToAction("Index", "Homepage");
        }

        #region HttpPost
        [HttpPost]
        public ActionResult Register(RegisterViewModel viewModel)
        {
            return View("Register");
        }

        [HttpPost]
        public ActionResult Login(string password, string email)
        {
            CryptoHelper cryptoHelper = new CryptoHelper();
            var ar = new AgenziaRepository();
            var agency = ar.GetByEmail(email);
            if (agency == null)
                return View("Register");
            var cryptedPassword = cryptoHelper.CryptPassword(password);
            if (cryptedPassword.Equals(agency.Password))
            {
                Session.Login(agency);
                return RedirectToAction("Index", "Dashboard", new { id = agency.Id});
            }
            return View("Register");
        }

        [HttpPost]
        public ActionResult AdminLogin(string password, string email)
        {
            CryptoHelper cryptoHelper = new CryptoHelper();
            var gr = new GestoreRepository();
            var admin = gr.GetByEmail(email);
            if (admin == null)
                return RedirectToAction("Index", "Homepage");
            var cryptedPassword = cryptoHelper.CryptPassword(password);
            if (cryptedPassword.Equals(admin.Password))
            {
                Session.LoginAsAdmin();
                return RedirectToAction("List", "Admin");
            }
            return RedirectToAction("Index", "Homepage");
        }

        #endregion
    }
}
