namespace Xlns.PartiComodo.Core.Repository
{

    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Xlns.PartiComodo.Core.Model;
    using Xlns.PartiComodo.Core.DAL;
    using NHibernate.Linq;
    #endregion

    public class GestoreRepository : CommonRepository
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public Gestore GetById(int id)
        {
            return base.getDomainObjectById<Gestore>(id);
        }

        public Gestore GetByEmail(string email)
        {
            using (var manager = new OperationManager())
            {
                try
                {
                    var session = manager.BeginOperation();
                    var res = session.Query<Gestore>()
                                    .Where(u => u.Email.ToLower().Equals(email)).SingleOrDefault();
                    manager.CommitOperation();
                    return res;
                }
                catch (Exception ex)
                {
                    manager.RollbackOperation();
                    string message = String.Format("Impossibile recuperare il gestore con email = {0}", email);
                    logger.ErrorException(message, ex);
                    throw new Exception(message, ex);
                }
            }
        } 
    }
}
