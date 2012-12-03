using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xlns.PartiComodo.UI.Web.ViewModels;
using Xlns.PartiComodo.Core.Model;

namespace Xlns.PartiComodo.UI.Web.Helpers
{
    public static class ViaggioHelper
    {

        public static ViaggioSearch getViaggioSearchParams(ViaggioSearchViewModel searchViewModelParams)
        {
            ViaggioSearch searchModelParams = null;

            if (searchViewModelParams != null)
            {
                searchModelParams = new ViaggioSearch()
                {
                    SearchString = searchViewModelParams.SearchString,
                    DataPartenzaMin = searchViewModelParams.DataPartenzaMin,
                    DataPartenzaMax = searchViewModelParams.DataPartenzaMax,
                    PrezzoMin = searchViewModelParams.PrezzoMin,
                    PrezzoMax = searchViewModelParams.PrezzoMax,
                    PassaDa = getGeoLocationModelFromViewModel(searchViewModelParams.PassaDa),
                    ArrivaA = getGeoLocationModelFromViewModel(searchViewModelParams.ArrivaA),
                    PassaDaTipoSearch = searchViewModelParams.PassaDaTipoSearch,
                    ArrivaATipoSearch = searchViewModelParams.ArrivaATipoSearch,
                    idAgenzia = searchViewModelParams.idAgenzia,
                    searchApproved = searchViewModelParams.searchApproved,
                    searchUnapproved = searchViewModelParams.searchUnapproved,
                    searchMine = searchViewModelParams.searchMine,
                    searchTheirs = searchViewModelParams.searchTheirs
                    
                };
            }

            return searchModelParams;
        }

        public static GeoLocation getGeoLocationModelFromViewModel(GeoLocationViewModel geoView)
        {
            GeoLocation geoModel = null;

            if (geoView != null && !geoView.isEmpty())
            {
                geoModel = new GeoLocation()
                {
                    CAP = geoView.CAP,
                    City = geoView.City,
                    Lat = geoView.Lat,
                    Lng = geoView.Lng,
                    Nation = geoView.Nation,
                    Number = geoView.Number,
                    Province = geoView.Province,
                    Region = geoView.Region,
                    Street = geoView.Street
                };
            }

            return geoModel;
        }

    }
}
