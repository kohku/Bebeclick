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

        public void Dispose()
        {
        }

        #region State entity

        internal IEnumerable<StateEntity> GetStateEntities(Guid? id, string name)
        {
            return container.Resolve<IPortalRepository>().GetStateEntities(id, name);
        }

        internal void UpdateStateEntity(StateEntity stateProvince)
        {
            container.Resolve<IPortalRepository>().UpdateStateEntity(stateProvince);
        }

        internal void InsertStateEntity(StateEntity stateProvince)
        {
            container.Resolve<IPortalRepository>().InsertStateEntity(stateProvince);
        }

        internal void DeleteStateEntity(StateEntity stateProvince)
        {
            container.Resolve<IPortalRepository>().DeleteStateEntity(stateProvince);
        }

        #endregion

        #region Province

        internal IEnumerable<Province> GetProvinces(Guid? id, Guid? stateId, string name)
        {
            return container.Resolve<IPortalRepository>().GetProvinces(id, stateId, name);
        }

        internal void UpdateProvince(Province province)
        {
            container.Resolve<IPortalRepository>().UpdateProvince(province);
        }

        internal void InsertProvince(Province province)
        {
            container.Resolve<IPortalRepository>().InsertProvince(province);
        }

        internal void DeleteProvince(Province province)
        {
            container.Resolve<IPortalRepository>().DeleteProvince(province);
        }

        #endregion

        #region Products

        internal IEnumerable<Product> GetProducts(Guid? id, Guid? stateId, Guid? provinceId, string name)
        {
            return container.Resolve<IPortalRepository>().GetProducts(id, stateId, provinceId, name);
        }
        internal void DeleteProduct(Product product)
        {
            container.Resolve<IPortalRepository>().DeleteProduct(product);
        }

        internal void InsertProduct(Product product)
        {
            container.Resolve<IPortalRepository>().InsertProduct(product);
        }

        internal void UpdateProduct(Product product)
        {
            container.Resolve<IPortalRepository>().UpdateProduct(product);
        }

        #endregion

        #region Services

        internal IEnumerable<Service> GetServices(Guid? id, Guid? productId, Guid? stateId, Guid? provinceId, string name)
        {
            return container.Resolve<IPortalRepository>().GetServices(id, productId, stateId, provinceId, name);
        }

        internal void DeleteService(Service service)
        {
            container.Resolve<IPortalRepository>().DeleteService(service);
        }

        internal void InsertService(Service service)
        {
            container.Resolve<IPortalRepository>().InsertService(service);
        }

        internal void UpdateService(Service service)
        {
            container.Resolve<IPortalRepository>().UpdateService(service);
        }

        #endregion

    }
}
