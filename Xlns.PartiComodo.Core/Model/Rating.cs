namespace Xlns.PartiComodo.Core.Model
{

    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel.DataAnnotations;
    #endregion

    public class Rating : ModelEntity
    {
        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Commento")]
        [StringLength(150, ErrorMessage = "Il campo può essere lungo al massimo 150 caratteri")]
        public virtual string Commento { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Inserito il")]
        public virtual DateTime Data { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Inserito da")]
        public virtual string Agenzia { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Voto")]
        [Range(0, 10, ErrorMessage = "Valore intero compreso tra 0 e 10")]
        public virtual int Voto { get; set; }

        public virtual Viaggio Viaggio { get; set; }
    }
}
