namespace Xlns.PartiComodo.Core.Crypto
{

    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Security.Cryptography;
    #endregion

    public class CryptoHelper
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Effettua la criptazione della password sfruttando l'algoritmo di criptazione MD5
        /// </summary>
        /// <param name="password">Stringa da criptare di qualsivoglia lunghezza</param>
        /// <returns>Stringa criptata in una di 128 bit</returns>
        public string CryptPassword(string password)
        {
            try
            {
                MD5 md5 = MD5.Create();
                byte[] hashPassword = md5.ComputeHash(Encoding.Default.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashPassword.Length; i++)
                    builder.Append(hashPassword[i].ToString());
                var result = builder.ToString();
                logger.Debug("Password criptata con successo - hash = {0}", result);
                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}
