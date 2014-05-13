using Bebeclick.WebClient.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bebeclick.WebClient
{
    public enum Directive
    {
        Deny = 0,
        Allow = 1
    }

    public class AccessControl :  BusinessBase<AccessControl, KeyValuePair<Guid, Guid>>
    {
        private Directive _directive;

        public AccessControl()
            : base(new KeyValuePair<Guid, Guid>(Guid.NewGuid(), Guid.NewGuid()))
        {
        }

        [DataMember]
        public Guid RestrictedID
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return this.ID.Key; }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                if (this.ID.Key != value)
                {
                    this.OnPropertyChanging("RestrictedID");
                    this.MarkChanged("RestrictedID");
                }

                this.ID = new KeyValuePair<Guid,Guid>(value, this.ID.Value);
            }
        }

        [DataMember]
        public Guid RestrictedByID
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return this.ID.Value; }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                if (this.ID.Value != value)
                {
                    this.OnPropertyChanging("RestrictedByID");
                    this.MarkChanged("RestrictedByID");
                }

                this.ID = new KeyValuePair<Guid, Guid>(this.ID.Key, value);
            }
        }

        [DataMember]
        public Directive Directive
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _directive; }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                if (this._directive != value)
                {
                    this.OnPropertyChanging("Directive");
                    this.MarkChanged("Directive");
                }

                this._directive = value;
            }
        }

        protected override void ValidationRules()
        {
            AddRule("EmptyCreatedBy", ResourceStringLoader.GetResourceString("AccessControl_EmptyCreatedBy"), 
                string.IsNullOrEmpty(this.CreatedBy));
            AddRule("MaxCreatedByLength", ResourceStringLoader.GetResourceString("AccessControl_MaxCreatedByLength"), 
                !string.IsNullOrEmpty(this.CreatedBy) && this.CreatedBy.Length > 256);
        }

        protected override AccessControl DataSelect(KeyValuePair<Guid, Guid> id)
        {
            throw new NotImplementedException();
        }

        protected override void DataUpdate()
        {
            throw new NotImplementedException();
        }

        protected override void DataInsert()
        {
            throw new NotImplementedException();
        }

        protected override void DataDelete()
        {
            throw new NotImplementedException();
        }
    }
}
