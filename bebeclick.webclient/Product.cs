using Bebeclick.WebClient.Utilities;
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

        private IEnumerable<Service> _services;

        /// <summary>
        /// Lazy loading services
        /// </summary>
        public IEnumerable<Service> Services
        {
            get
            {
                if (_services == null)
                {
                    lock (syncRoot)
                    {
                        if (_services == null)
                            _services = Service.GetServices(this.ID);
                    }
                }

                return _services;
            }
        }

        protected override void ValidationRules()
        {
            AddRule("EmptyName", ResourceStringLoader.GetResourceString("Product_EmptyName"),
        string.IsNullOrEmpty(this.Name));
            AddRule("MaxNameLength", ResourceStringLoader.GetResourceString("Product_MaxNameLength"),
                !string.IsNullOrEmpty(this.Name) && this.Name.Length > 100);
            AddRule("EmptyCreatedBy", ResourceStringLoader.GetResourceString("Product_EmptyCreatedBy"),
                string.IsNullOrEmpty(this.CreatedBy));
            AddRule("MaxCreatedByLength", ResourceStringLoader.GetResourceString("Product_MaxCreatedByLength"),
                !string.IsNullOrEmpty(this.CreatedBy) && this.CreatedBy.Length > 256);
            AddRule("NullStateID", ResourceStringLoader.GetResourceString("Product_NullStateID"),
                this.StateID == null);
            AddRule("DuplicatedName", ResourceStringLoader.GetResourceString("Product_DuplicatedName", this.Name),
                !string.IsNullOrEmpty(this.Name) && this.ChangedProperties.Contains("Name") && Product.GetProducts(this.StateID, this.Name).Count() > 0);
        }

        protected override Product DataSelect(Guid id)
        {
            return Rainbow.Instance.GetProducts(id, null, null).FirstOrDefault();
        }

        protected override void DataUpdate()
        {
            Rainbow.Instance.UpdateProduct(this);
        }

        protected override void DataInsert()
        {
            Rainbow.Instance.InsertProduct(this);
        }

        protected override void DataDelete()
        {
            Rainbow.Instance.DeleteProduct(this);
        }

        public static IEnumerable<Product> GetProducts(Guid stateId)
        {
            return Rainbow.Instance.GetProducts(null, stateId, null);
        }

        public static IEnumerable<Product> GetProducts(Guid stateId, string name)
        {
            return Rainbow.Instance.GetProducts(null, stateId, name);
        }
    }
}
