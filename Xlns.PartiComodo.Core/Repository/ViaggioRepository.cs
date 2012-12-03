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


        public List<Viaggio> Search(ViaggioSearch searchParams)
        {
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    var viaggiFound = getAll<Viaggio>();

                    if (searchParams != null)
                    {
                        if ( !(searchParams.searchApproved && searchParams.searchUnapproved)   ) 
                        {
                            if (searchParams.searchApproved)
                                viaggiFound = viaggiFound.Where(v => v.Approvato == true);
                            if (searchParams.searchUnapproved)
                                viaggiFound = viaggiFound.Where(v => v.Approvato == false);
                        }
                        if (!(searchParams.searchMine && searchParams.searchTheirs))
                        {
                            if (searchParams.searchMine)
                                viaggiFound = viaggiFound.Where(v => v.Agenzia.Id == searchParams.idAgenzia);
                            if (searchParams.searchTheirs)
                                viaggiFound = viaggiFound.Where(v => v.Agenzia.Id != searchParams.idAgenzia);
                        }

                        if (!String.IsNullOrEmpty(searchParams.SearchString))
                            viaggiFound = viaggiFound.Where(v => v.Nome.ToUpper().StartsWith(searchParams.SearchString.ToUpper()));
                        if (searchParams.DataPartenzaMin != null)
                            viaggiFound = viaggiFound.Where(v => v.DataPartenza >= searchParams.DataPartenzaMin);
                        if (searchParams.DataPartenzaMax != null)
                            viaggiFound = viaggiFound.Where(v => v.DataPartenza <= searchParams.DataPartenzaMax);
                        if (searchParams.PrezzoMin != null)
                            viaggiFound = viaggiFound.Where(v => v.PrezzoStandard >= searchParams.PrezzoMin);
                        if (searchParams.PrezzoMax != null)
                            viaggiFound = viaggiFound.Where(v => v.PrezzoStandard <= searchParams.PrezzoMax);
                        if (searchParams.PassaDa != null)
                            viaggiFound = AddTappaSearchFilter(viaggiFound, searchParams.PassaDa, searchParams.PassaDaTipoSearch, TipoTappa.PICK_UP_POINT);
                        if (searchParams.ArrivaA != null)
                            viaggiFound = AddTappaSearchFilter(viaggiFound, searchParams.ArrivaA, searchParams.ArrivaATipoSearch, TipoTappa.DESTINAZIONE);
                    }
                    var result = viaggiFound.ToList();
                    om.CommitOperation();
                    return result;
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = String.Format("Errore nella ricerca viaggio");
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }

        private IQueryable<Viaggio> AddTappaSearchFilter(IQueryable<Viaggio> viaggiToBeFiltered, GeoLocation locationFilter, TipoSearch tipoSearch, TipoTappa tipoTappa)
        {
            try
            {
                IQueryable<Viaggio> viaggiFiltered = null;

                if (tipoSearch == null)
                    tipoSearch = TipoSearch.Città;

                switch (tipoSearch)
                {
                    case TipoSearch.Città:
                        viaggiFiltered = viaggiToBeFiltered.Where(v => v.Tappe.Any(t => t != null && t.Location != null && t.Location.City != null && t.Tipo == tipoTappa && t.Location.City.Equals(locationFilter.City)));
                        break;
                    case TipoSearch.Provincia:
                        viaggiFiltered = viaggiToBeFiltered.Where(v => v.Tappe.Any(t => t != null && t.Location != null && t.Location.Province != null && t.Tipo == tipoTappa && t.Location.Province.Equals(locationFilter.Province)));
                        break;
                    case TipoSearch.Regione:
                        viaggiFiltered = viaggiToBeFiltered.Where(v => v.Tappe.Any(t => t != null && t.Location != null && t.Location.Region != null && t.Tipo == tipoTappa && t.Location.Region.Equals(locationFilter.Region)));
                        break;
                    case TipoSearch.Nazione:
                        viaggiFiltered = viaggiToBeFiltered.Where(v => v.Tappe.Any(t => t != null && t.Location != null && t.Location.Nation != null && t.Tipo == tipoTappa && t.Location.Nation.Equals(locationFilter.Nation)));
                        break;
                    default:
                        throw new Exception("TipoSearch sconosciuto: " + Enum.GetName(typeof(TipoSearch), tipoSearch));

                }


                return viaggiFiltered;
            }
            catch (Exception ex)
            {
                string msg = String.Format("Errore nell'aggiunta del filtro per tappa/destinazione");
                logger.ErrorException(msg, ex);
                throw new Exception(msg, ex);
            }
        }

    
    
    }
}
