namespace Xlns.PartiComodo.UI.Web.Controllers.Helper
{

    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Xlns.PartiComodo.UI.Web.ViewModels;
    using Xlns.PartiComodo.Core.Model;
    #endregion

    public static class FlyerHelper
    {
        public static List<ItemPopulateFlyerViewModel> GetViaggiSelezionabili(Flyer flyer, IList<Viaggio> viaggi)
        {
            var _viaggi = new List<ItemPopulateFlyerViewModel>();
            foreach (var viaggio in viaggi)
            {
                bool selected = false;
                if (flyer.Viaggi != null && flyer.Viaggi.Any(v => v.Id == viaggio.Id))
                    selected = true;
                var viaggioSelezionabile = new ItemPopulateFlyerViewModel()
                {
                    Viaggio = viaggio,
                    IsInFlyer = selected,
                    Flyer = flyer
                };
                _viaggi.Add(viaggioSelezionabile);
            }
            return _viaggi;
        }

        public static void ToggleViaggio(Flyer flyer, Viaggio viaggio)
        {
            if (flyer.Viaggi.Any(v => v.Id == viaggio.Id))
                flyer.Viaggi.Remove(viaggio);
            else
                flyer.Viaggi.Add(viaggio);
        }
    }
}