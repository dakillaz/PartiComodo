﻿namespace Xlns.PartiComodo.Core.Repository
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

    public class AgenziaRepository : CommonRepository
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public void Save(Agenzia agenzia)
        {
            base.update<Agenzia>(agenzia);
        }

        public IList<Agenzia> GetAllAgenzie(int maximumRows, int startRowIndex)
        {
            return base.getAll<Agenzia>(maximumRows, startRowIndex);
        }

        public Agenzia GetById(int id)
        {
            return base.getDomainObjectById<Agenzia>(id);
        }

        public Agenzia GetByName(string name)
        {
            using (var manager = new OperationManager())
            {
                try
                {
                    var session = manager.BeginOperation();
                    var res = session.Query<Agenzia>()
                                    .Where(u => u.Nome.ToLower().Equals(name)).SingleOrDefault();
                    manager.CommitOperation();
                    return res;
                }
                catch (Exception ex)
                {
                    manager.RollbackOperation();
                    string message = String.Format("Impossibile recuperare l'agenzia con nome = {0}", name);
                    logger.ErrorException(message, ex);
                    throw new Exception(message, ex);
                }
            }
        }

        public Agenzia GetByEmail(string email)
        {
            using (var manager = new OperationManager())
            {
                try
                {
                    var session = manager.BeginOperation();
                    var res = session.Query<Agenzia>()
                                    .Where(u => u.Email.ToLower().Equals(email)).SingleOrDefault();
                    manager.CommitOperation();
                    return res;
                }
                catch (Exception ex)
                {
                    manager.RollbackOperation();
                    string message = String.Format("Impossibile recuperare l'agenzia con email = {0}", email);
                    logger.ErrorException(message, ex);
                    throw new Exception(message, ex);
                }
            }
        }

        /// <summary>
        /// Gets all Agenzia where isTourOperator is false.
        /// </summary>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <returns></returns>
        public IList<Agenzia> GetAllAdV(int maximumRows, int startRowIndex)
        {
             using (var manager = new OperationManager())
            {
                try
                {
                    var session = manager.BeginOperation();
                    var res = session.Query<Agenzia>()
                                    .Where(c => !c.IsTourOperator).ToList();
                    manager.CommitOperation();
                    return res;
                }
                catch (Exception ex)
                {
                    manager.RollbackOperation();
                    string message = String.Format("Impossibile recuperare la lista delle AdV");
                    logger.ErrorException(message, ex);
                    throw new Exception(message, ex);
                }
            }
        }

        /// <summary>
        /// Gets all Agenzia where isTourOperator is true.
        /// </summary>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <returns></returns>
        public IList<Agenzia> GetAllTO(int maximumRows, int startRowIndex)
        {
            using (var manager = new OperationManager())
            {
                try
                {
                    var session = manager.BeginOperation();
                    var res = session.Query<Agenzia>()
                                    .Where(c => c.IsTourOperator).ToList();
                    manager.CommitOperation();
                    return res;
                }
                catch (Exception ex)
                {
                    manager.RollbackOperation();
                    string message = String.Format("Impossibile recuperare la lista dei TO");
                    logger.ErrorException(message, ex);
                    throw new Exception(message, ex);
                }
            }
        }

    }
}
