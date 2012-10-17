namespace Xlns.PartiComodo.Core.Model
{

    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    #endregion

    public abstract class ModelEntity
    {
        public virtual int Id { get; set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;
            else
                return this.Id == ((ModelEntity)obj).Id;
        }
    }
}
