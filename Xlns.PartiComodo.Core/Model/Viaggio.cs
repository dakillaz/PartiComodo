﻿namespace Xlns.PartiComodo.Core.Model
{

    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Xlns.PartiComodo.Core.Repository;
    using System.ComponentModel.DataAnnotations;
    #endregion

    public class Viaggio : ModelEntity
    {
        [Required]
        [Display(Name = "Nome")]
        public virtual String Nome { get; set; }

        [Display(Name = "Descrizione")]
        [StringLength(500, ErrorMessage = "La descrizione dev'essere al massimo 500 caratteri")]
        public virtual String Descrizione { get; set; }

        [Display(Name = "Data di partenza")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Informazione obbligatoria")]
        public virtual DateTime? DataPartenza { get; set; }

        [Display(Name = "Data di chiusura delle prenotazioni")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Informazione obbligatoria")]
        public virtual DateTime? DataChiusuraPrenotazioni { get; set; }

        [Display(Name = "Data di pubblicazione")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public virtual DateTime? DataPubblicazione { get; set; }

        [Display(Name = "Data di approvazione")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public virtual DateTime? DataApprovazione { get; set; }

        public virtual IList<Tappa> Tappe { get; set; }

        public virtual IList<Rating> Rating { get; set; }

        public virtual Agenzia Agenzia { get; set; }

        public virtual int DistanzaPercorsa { get; set; }

        public virtual bool Approvato { get; set; }

        [Display(Name = "Capienza del bus")]
        [Required(ErrorMessage = "Informazione obbligatoria")]
        public virtual int TotalePosti { get; set; }

        [Display(Name = "Prezzo base")]
        public virtual decimal PrezzoStandard { get; set; }
        public virtual decimal DeltaCameraSingola { get; set; }
        public virtual decimal DeltaCameraTripla { get; set; }
        public virtual decimal DeltaMezzaPensione { get; set; }
        public virtual decimal DeltaPensioneCompleta { get; set; }

        public override string ToString()
        {
            return String.Format("{0} - {1}", Id, Nome);
        }
    }
}
