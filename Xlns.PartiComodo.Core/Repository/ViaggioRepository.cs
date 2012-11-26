namespace Xlns.PartiComodo.Core.Repository
{

    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Xlns.PartiComodo.Core.Model;
    using Xlns.PartiComodo.Core.DAL;
    #endregion

    public class ViaggioRepository : CommonRepository
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public IList<Viaggio> GetUnapproved()
        {
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    var result = GetViaggiNonApprovati().ToList();
                    om.CommitOperation();
                    return result;
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = String.Format("Errore durante il recupero dei viaggi non approvati");
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }

        internal IQueryable<Viaggio> GetViaggiNonApprovati()
        {
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    var result = GetViaggi()
                                 .Where(c => !c.Approvato);
                    om.CommitOperation();
                    return result;
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = String.Format("Errore durante il recupero dei viaggi non approvati");
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }

        public IQueryable<Viaggio> GetViaggi()
        {
            return getAll<Viaggio>();
        }

        /// <summary>
        /// Prende tutti i viaggi pubblicati o proposti dall'agenzia dell'utente loggato
        /// </summary>
        /// <returns></returns>
        internal IQueryable<Viaggio> GetViaggiVisibili(Agenzia agenzia)
        {
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    var result = GetViaggi()
                                 .Where(v => v.Agenzia.Id == agenzia.Id);
                    om.CommitOperation();
                    return result;
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = String.Format("Errore durante il recupero dei viaggi pubblicati o proposti dall'agenzia {0}", agenzia);
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }

        public IList<Viaggio> GetListaViaggiByAgenzia(Agenzia agenzia)
        {
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    var result = GetViaggiVisibili(agenzia).ToList();
                    om.CommitOperation();
                    return result;
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = String.Format("Errore durante il recupero dei viaggi dell'agenzia {0}", agenzia.ToString());
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }

        public IList<Viaggio> GetListaViaggiVisibili(Agenzia agenzia)
        {
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    var result = GetViaggiVisibili(agenzia).ToList();
                    om.CommitOperation();
                    return result;
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = String.Format("Errore durante il recupero dei viaggi visibili dell'agenzia {0}", agenzia.ToString());
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }

        public Viaggio GetById(int id)
        {
            return base.getDomainObjectById<Viaggio>(id);
        }

        public Tappa GetTappaById(int id)
        {
            return base.getDomainObjectById<Tappa>(id);
        }

        public void Save(Viaggio viaggio)
        {
            base.update<Viaggio>(viaggio);
        }


        public void Save(Tappa tappa)
        {
            using (var om = new OperationManager())
            {
                try
                {
                    om.BeginOperation();
                    var destinazione = tappa.Viaggio.Tappe.Where(t => t.Tipo == TipoTappa.DESTINAZIONE).SingleOrDefault();
                    if (destinazione != null)
                    {
                        logger.Debug("L'ordinamento della destinazione verrà incrementato di 1 per fare posto alla nuova tappa");
                        destinazione.Ordinamento = tappa.Ordinamento + 1;
                        base.update<Tappa>(destinazione);
                    }
                    base.update<Tappa>(tappa);
                    om.CommitOperation();
                    logger.Info("Dati della tappa {0} salvati con successo", tappa);
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = "Errore nel salvataggio della tappa";
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }

        public void deleteTappa(Tappa tappa)
        {
            try
            {
                base.delete<Tappa>(tappa);
            }
            catch (Exception ex)
            {
                string msg = String.Format("Errore durante la cancellazione della tappa {0}", tappa);
                logger.ErrorException(msg, ex);
                throw new Exception(msg, ex);
            }
        }

        public IList<Viaggio> GetApproved()
        {
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    var result = GetViaggiApprovati().ToList();
                    om.CommitOperation();
                    return result;
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = String.Format("Errore durante il recupero dei viaggi approvati");
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }

        internal IQueryable<Viaggio> GetViaggiApprovati()
        {
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    var result = GetViaggi()
                                 .Where(c => c.Approvato);
                    om.CommitOperation();
                    return result;
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = String.Format("Errore durante il recupero dei viaggi non approvati");
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }
    }
}
