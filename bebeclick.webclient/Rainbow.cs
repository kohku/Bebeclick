using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity.Configuration;

namespace Bebeclick.WebClient
{
    public sealed class Rainbow :  IDisposable
    {
        private static volatile Rainbow instance;
        private static object syncRoot = new object();

        private IUnityContainer container;

        private Rainbow()
        {
            LoadConfiguration();
        }
        private void LoadConfiguration()
        {
            container = new UnityContainer().LoadConfiguration();
        }

        /// <summary>
        /// Gets a singleton instance of the <see cref="Rainbow"/> class.
        /// </summary>
        public static Rainbow Instance
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Rainbow();
                    }
                }
                return instance;
            }
        }

        internal IEnumerable<StateProvince> GetAllStateProvinces()
        {
            return container.Resolve<IPortalRepository>().GetAllStateProvinces();
        }

        public void Dispose()
        {
        }

        internal IEnumerable<Service> GetServices(Guid productId)
        {
            return container.Resolve<IPortalRepository>().GetServices(productId);
        }

        internal IEnumerable<Product> GetAllProducts(Guid stateId)
        {
            return container.Resolve<IPortalRepository>().GetAllProducts(stateId);
        }
    }
}
