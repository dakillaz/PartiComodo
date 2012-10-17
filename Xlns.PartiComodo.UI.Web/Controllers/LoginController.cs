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
    using Xlns.PartiComodo.Core.Mailer;
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
        public ActionResult Register(Agenzia agency)
        {
            if (ModelState.IsValid)
            {
                CryptoHelper cryptoHelper = new CryptoHelper();
                agency.RagioneSociale = agency.Nome;
                agency.PIva = "123456";
                agency.Password = cryptoHelper.CryptPassword(agency.Password);
                var ar = new AgenziaRepository();
                ar.Save(agency);
                Session.Login(agency);
                return RedirectToAction("Index", "Dashboard", new { id = agency.Id });
            }
            else return View(agency);
            
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
                return RedirectToAction("Index", "Dashboard", new { id = agency.Id });
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

        [HttpPost]
        public ActionResult ResetPassword(string email)
        {
            var ar = new AgenziaRepository();
            var agency = ar.GetByEmail(email);
            if (agency != null)
            {
                CryptoHelper cryptoHelper = new CryptoHelper();
                var random = new Random();
                var password = random.Next().ToString();
                agency.Password = cryptoHelper.CryptPassword(password);
                ar.Save(agency);
                var mailerHelper = new MailerHelper();
                var text = string.Format("Gentile {0} la tua nuova password di Parti Comodo è: {1}", agency.Nome, password);
                mailerHelper.SendMail(email, text);
            }
            return View("Register");
        }

        #endregion
    }
}
