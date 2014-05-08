using Bebeclick.WebClient.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bebeclick.WebClient
{
    public class StateProvince :  BusinessBase<StateProvince, Guid>
    {
        private string _name;
        private bool _visible;

        public StateProvince() 
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

        private IEnumerable<Product> _products;

        /// <summary>
        /// Lazy loading
        /// </summary>
        public IEnumerable<Product> Products
        {
            get 
            {
                if (_products == null)
                {
                    lock (syncRoot)
                    {
                        _products = Product.GetProducts(this.ID);
                    }
                }

                return _products;
            }
        }

        protected override void ValidationRules()
        {
            AddRule("EmptyName", ResourceStringLoader.GetResourceString("StateProvince_EmptyName"), 
                string.IsNullOrEmpty(this.Name));
            AddRule("MaxNameLength", ResourceStringLoader.GetResourceString("StateProvince_MaxNameLength"),
                !string.IsNullOrEmpty(this.Name) && this.Name.Length > 100);
            AddRule("EmptyCreatedBy", ResourceStringLoader.GetResourceString("StateProvince_EmptyCreatedBy"), 
                string.IsNullOrEmpty(this.CreatedBy));
            AddRule("MaxCreatedByLength", ResourceStringLoader.GetResourceString("StateProvince_MaxCreatedByLength"), 
                !string.IsNullOrEmpty(this.CreatedBy) && this.CreatedBy.Length > 256);
            AddRule("DuplicatedName", ResourceStringLoader.GetResourceString("StateProvince_DuplicatedName", this.Name),
                !string.IsNullOrEmpty(this.Name) && this.ChangedProperties.Contains("Name") && StateProvince.GetStateProvinces(this.Name).Count() > 0);
        }

        protected override StateProvince DataSelect(Guid id)
        {
            return Rainbow.Instance.GetStateProvinces(id, null).FirstOrDefault();
        }

        protected override void DataUpdate()
        {
            Rainbow.Instance.UpdateStateProvince(this);
        }

        protected override void DataInsert()
        {
            Rainbow.Instance.InsertStateProvince(this);
        }

        protected override void DataDelete()
        {
            Rainbow.Instance.DeleteStateProvince(this);
        }

        public static IEnumerable<StateProvince> GetAll()
        {
            return Rainbow.Instance.GetStateProvinces(null, null);
        }

        public static IEnumerable<StateProvince> GetStateProvinces(string name)
        {
            return Rainbow.Instance.GetStateProvinces(null, name);
        }
    }
}
