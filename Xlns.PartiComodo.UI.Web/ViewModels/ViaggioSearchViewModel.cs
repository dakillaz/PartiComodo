using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Xlns.PartiComodo.Core.Model;

namespace Xlns.PartiComodo.UI.Web.ViewModels
{
    public class ViaggioSearchViewModel
    {
        [Display(Name = "Nome")]
        public String SearchString { get; set; }

        [Display(Name = "Data di partenza minima")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        public DateTime? DataPartenzaMin { get; set; }
        [Display(Name = "Data di partenza massima")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        public DateTime? DataPartenzaMax { get; set; }

        public GeoLocationViewModel PassaDa { get; set; }
        public TipoSearch PassaDaTipoSearch { get; set; }
        public GeoLocationViewModel ArrivaA { get; set; }
        public TipoSearch ArrivaATipoSearch { get; set; }

        [Display(Name = "Prezzo minimo")]
        [Range(typeof(decimal), "0","1000000", ErrorMessage="Il Prezzo minimo deve essere compreso tra 0 e 1000000 di euro")]
        public decimal? PrezzoMin { get; set; }
        [Display(Name = "Prezzo massimo")]
        [Range(typeof(decimal), "0", "1000000", ErrorMessage = "Il Prezzo massimo deve essere compreso tra 0 e 1000000 di euro")]
        public decimal? PrezzoMax { get; set; }

        public int idAgenzia { get; set; }
        public bool searchApproved { get; set; }
        public bool searchUnapproved { get; set; }
        public bool searchMine { get; set; }
        public bool searchTheirs { get; set; }
        public string ViewName { get; set; }


        public ViaggioSearchViewModel()
        {
            PassaDaTipoSearch = TipoSearch.Città;
            ArrivaATipoSearch = TipoSearch.Città;
        }
    }
}