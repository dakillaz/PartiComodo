namespace Xlns.PartiComodo.Core.Login
{

    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Xlns.PartiComodo.Core.Crypto;
    using Xlns.PartiComodo.Core.Repository;
    #endregion

    public class LoginHelper
    {

        public static AuthenticationResult AuthenticateUtente(String name, String password)
        {
            AuthenticationResult result = null;

            AgenziaRepository ur = new AgenziaRepository();
            var registeredAgenzia = ur.GetByName(name);

            if (registeredAgenzia == null)
            {
                result = new AuthenticationResult { IsAuthenticated = false, AuthErrorMessage = "Username/Password errata!" };
            }
            else
            {
                CryptoHelper crypter = new CryptoHelper();
                if (crypter.CryptPassword(password).Equals(registeredAgenzia.Password))
                    result = new AuthenticationResult { IsAuthenticated = true, AuthenticatedAgenzia = registeredAgenzia };
                else
                    result = new AuthenticationResult { IsAuthenticated = false, AuthErrorMessage = "Username/Password errata!" };
            }
            return result;
        }
    }
}
