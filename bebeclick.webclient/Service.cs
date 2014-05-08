﻿using System;
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

        protected override void ValidationRules()
        {
        }

        protected override Service DataSelect(Guid id)
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

        public static IEnumerable<Service> GetServices(Guid productId)
        {
            return Rainbow.Instance.GetServices(productId);
        }
    }
}
