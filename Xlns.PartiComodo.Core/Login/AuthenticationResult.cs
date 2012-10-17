namespace Xlns.PartiComodo.Core.Login
{

    #region Namespaces
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System;
    using Xlns.PartiComodo.Core.Model;
    #endregion

    public class AuthenticationResult
    {
        public bool IsAuthenticated { get; set; }
        public String AuthErrorMessage { get; set; }
        public Agenzia AuthenticatedAgenzia { get; set; }
    }
}
