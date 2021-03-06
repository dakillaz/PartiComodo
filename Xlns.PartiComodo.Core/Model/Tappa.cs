﻿namespace Xlns.PartiComodo.Core.Model
{

    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    #endregion

    public class Tappa : ModelEntity
    {
        private TipoTappa tipo;
        public virtual TipoTappa Tipo
        {
            get { return tipo; }
            set
            {
                tipo = value;
                idTipo = (int)tipo;
            }
        }
        public virtual int Ordinamento { get; set; }
        public virtual GeoLocation Location { get; set; }
        public virtual Viaggio Viaggio { get; set; }

        /// <summary>
        /// Necessario per la mappatura in NHibernate come intero
        /// </summary>
        private int idTipo;

        public virtual int IdTipo
        {
            get { return idTipo; }
            set
            {
                idTipo = value;
                Tipo = (TipoTappa)value;
            }
        }

        public override string ToString()
        {
            return String.Format("{0}", Id);
        }

    }

    public enum TipoTappa
    {
        PICK_UP_POINT = 2, DESTINAZIONE = 3
    }
}

