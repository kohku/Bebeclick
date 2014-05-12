using Bebeclick.WebClient.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bebeclick.WebClient
{
    public class Service : BusinessBase<Service, Guid>
    {
        private string _name;
        private bool _visible;
        private Guid _productId;

        public Service() 
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
        public Guid ProductID
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _productId; }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                if (this._productId != value)
                {
                    this.OnPropertyChanging("ProductID");
                    this.MarkChanged("ProductID");
                }

                this._productId = value;
            }
        }

        private Product _product;

        public Product Product
        {
            get 
            {
                if (_product == null)
                {
                    lock (syncRoot)
                    {
                        _product = Product.Load(this.ProductID);
                    }
                }

                return _product;
            }
        }


        protected override void ValidationRules()
        {
            AddRule("EmptyName", ResourceStringLoader.GetResourceString("Service_EmptyName"),
                string.IsNullOrEmpty(this.Name));
            AddRule("MaxNameLength", ResourceStringLoader.GetResourceString("Service_MaxNameLength"),
                !string.IsNullOrEmpty(this.Name) && this.Name.Length > 100);
            AddRule("EmptyCreatedBy", ResourceStringLoader.GetResourceString("Service_EmptyCreatedBy"),
                string.IsNullOrEmpty(this.CreatedBy));
            AddRule("MaxCreatedByLength", ResourceStringLoader.GetResourceString("Service_MaxCreatedByLength"),
                !string.IsNullOrEmpty(this.CreatedBy) && this.CreatedBy.Length > 256);
            AddRule("EmptyProductID", ResourceStringLoader.GetResourceString("Service_EmptyProductID"),
                this.ProductID == Guid.Empty);
            AddRule("DuplicatedName", ResourceStringLoader.GetResourceString("Service_DuplicatedName", new { this.Name }),
                !string.IsNullOrEmpty(this.Name) && this.ChangedProperties.Contains("Name") 
                && Service.GetServices(this.ProductID, this.Name).Where(m => m.ID != this.ID).Count() > 0);
            AddRule("EmptyLastUpdatedBy", ResourceStringLoader.GetResourceString("Service_EmptyLastUpdatedBy"),
                !this.IsNew && this.IsChanged && string.IsNullOrEmpty(this.LastUpdatedBy));
            AddRule("MaxLastUpdatedByLength", ResourceStringLoader.GetResourceString("Service_MaxLastUpdatedByLength"),
                !this.IsNew && this.IsChanged && !string.IsNullOrEmpty(this.LastUpdatedBy) && this.LastUpdatedBy.Length > 256);
        }

        protected override Service DataSelect(Guid id)
        {
            return Rainbow.Instance.GetServices(id, null, null).FirstOrDefault();
        }

        protected override void DataUpdate()
        {
            Rainbow.Instance.UpdateService(this);
        }

        protected override void DataInsert()
        {
            Rainbow.Instance.InsertService(this);
        }

        protected override void DataDelete()
        {
            Rainbow.Instance.DeleteService(this);
        }

        public static IEnumerable<Service> GetAll()
        {
            return Rainbow.Instance.GetServices(null, null, null);
        }

        public static IEnumerable<Service> GetServices(Guid productId)
        {
            return Rainbow.Instance.GetServices(null, productId, null);
        }

        public static IEnumerable<Service> GetServices(Guid productId, string name)
        {
            return Rainbow.Instance.GetServices(null, productId, name);
        }
    }
}
