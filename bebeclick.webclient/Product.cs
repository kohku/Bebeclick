using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bebeclick.WebClient
{
    public class Product :  BusinessBase<Product, Guid>
    {
        private string _name;
        private bool _visible;
        private Guid _stateId;

        public Product() 
            : base(Guid.NewGuid())
        {
        }

        [DataMember]
        public Guid StateID
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _stateId; }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                if (this._stateId != value)
                {
                    this.OnPropertyChanging("StateID");
                    this.MarkChanged("StateID");
                }

                this._stateId = value;
            }
        }

        private StateProvince _stateProvince;

        /// <summary>
        /// Lazy loading state
        /// </summary>
        public StateProvince StateProvince
        {
            get
            {
                if (_stateProvince == null)
                {
                    lock (syncRoot)
                    {
                        _stateProvince = StateProvince.Load(this.StateID);
                    }
                }

                return _stateProvince;
            }
        }

        [DataMember]
        public string Name
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _name; }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                if (this._name != value)
                {
                    this.OnPropertyChanging("Name");
                    this.MarkChanged("Name");
                }

                this._name = value;
            }
        }

        [DataMember]
        public bool Visible
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _visible; }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                if (this._visible != value)
                {
                    this.OnPropertyChanging("Visible");
                    this.MarkChanged("Visible");
                }

                this._visible = value;
            }
        }

        protected override void ValidationRules()
        {
        }

        protected override Product DataSelect(Guid id)
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

        internal static IEnumerable<Product> GetAll(Guid stateId)
        {
            return Rainbow.Instance.GetAllProducts(stateId);
        }
    }
}
