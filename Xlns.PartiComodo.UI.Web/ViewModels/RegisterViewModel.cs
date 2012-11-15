namespace Xlns.PartiComodo.UI.Web.ViewModels
{

    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Xlns.PartiComodo.Core.Model;
    using System.ComponentModel.DataAnnotations;
    #endregion

    public class RegisterViewModel
    {
        public Agenzia Agenzia { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Password")]
        [StringLength(50, ErrorMessage = "Il campo può essere lungo al massimo 50 caratteri")]
        public string RepeatPassword { get; set; }
    }
}