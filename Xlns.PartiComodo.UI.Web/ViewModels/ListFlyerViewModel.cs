namespace Xlns.PartiComodo.UI.Web.ViewModels
{

    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Xlns.PartiComodo.Core.Model;
    #endregion

    public class ListFlyerViewModel
    {
        public IList<Flyer> Flyers { get; set; }
        public Agenzia Agenzia { get; set; }
    }
}