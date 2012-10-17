namespace Xlns.PartiComodo.Core.Model
{

    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
using System.ComponentModel.DataAnnotations;
    #endregion

    public class Gestore : ModelEntity
    {
        [Required]
        [StringLength(50, ErrorMessage = "L'email dev'essere al massimo 50 caratteri")]
        public virtual string Email { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "La password dev'essere al massimo 50 caratteri")]
        public virtual string Password { get; set; }
    }
}
