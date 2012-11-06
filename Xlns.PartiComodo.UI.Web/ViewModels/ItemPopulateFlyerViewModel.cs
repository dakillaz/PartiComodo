namespace Xlns.PartiComodo.UI.Web.ViewModels
{

    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Xlns.PartiComodo.Core.Model;
    #endregion
    
    public class ItemPopulateFlyerViewModel
    {
        public bool IsInFlyer { get; set; }
        public Viaggio Viaggio { get; set; }
        public Flyer Flyer { get; set; }
    }
}