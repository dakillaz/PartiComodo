namespace Xlns.PartiComodo.UI.Web.Controllers.Helper
{

    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.SessionState;
    using Xlns.PartiComodo.Core.Model;
    #endregion

    public static class SessionHelper
    {
        public static int getItemsPerPage(this HttpSessionStateBase session)
        {
            return (int)session["maxPageNumer"];
        }

        public static void setItemsPerPage(this HttpSessionStateBase session, int maxPageNumer)
        {
            session["maxPageNumer"] = maxPageNumer;
        }

        public static int getItemsPerPage(this HttpSessionState session)
        {
            return (int)session["maxPageNumer"];
        }

        public static void setItemsPerPage(this HttpSessionState session, int maxPageNumer)
        {
            session["maxPageNumer"] = maxPageNumer;
        }

        private static T getDataFromSession<T>(this HttpSessionStateBase session, string key)
        {
            return (T)session[key];
        }

        private static void setDataInSession<T>(this HttpSessionStateBase session, string key, object value)
        {
            session[key] = value;
        }

        public static Agenzia GetLoggedAgenzia(this HttpSessionStateBase session)
        {
            return getDataFromSession<Agenzia>(session, "loggedAgenzia");
        }

        public static Agenzia GetLoggedAgenzia(this HttpSessionState session)
        {
            return (Agenzia)session["loggedAgenzia"];
        }

        public static Agenzia getLoggedAgenzia(this HttpSessionState session)
        {
            var agenzia = GetLoggedAgenzia(session);
            return agenzia == null ? null : agenzia;
        }

        public static Agenzia getLoggedAgenzia(this HttpSessionStateBase session)
        {
            var agenzia = GetLoggedAgenzia(session);
            return agenzia == null ? null : agenzia;
        }

        public static void Login(this HttpSessionStateBase session, Agenzia agenzia)
        {
            setDataInSession<Agenzia>(session, "loggedAgenzia", agenzia);
        }

        public static void Logout(this HttpSessionStateBase session)
        {
            session.Remove("loggedAgenzia");
        }

        //public static Flyer getFlyerInModifica(this HttpSessionState session)
        //{
        //    return (Flyer)session["flyer"];
        //}

        //public static Flyer getFlyerInModifica(this HttpSessionStateBase session)
        //{
        //    return (Flyer)session["flyer"];
        //}

        //public static void setFlyerInModifica(this HttpSessionState session, Flyer flyer)
        //{
        //    session["flyer"] = flyer;
        //}

        //public static void setFlyerInModifica(this HttpSessionStateBase session, Flyer flyer)
        //{
        //    session["flyer"] = flyer;
        //}
    }
}