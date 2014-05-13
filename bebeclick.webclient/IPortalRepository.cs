using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebeclick.WebClient
{
    public interface IPortalRepository
    {
        IEnumerable<StateEntity> GetStateEntities(Guid? id, string name);
        void UpdateStateEntity(StateEntity province);

        void InsertStateEntity(StateEntity province);

        void DeleteStateEntity(StateEntity province);

        IEnumerable<Province> GetProvinces(Guid? id, Guid? stateId, string name);

        void UpdateProvince(Province province);

        void InsertProvince(Province province);

        void DeleteProvince(Province province);

        IEnumerable<Product> GetProducts(Guid? id, Guid? stateId, Guid? provinceId, string name);

        void DeleteProduct(Product product);

        void InsertProduct(Product product);

        void UpdateProduct(Product product);

        IEnumerable<Service> GetServices(Guid? id, Guid? productId, Guid? stateId, Guid? provinceId, string name);

        void DeleteService(Service service);

        void InsertService(Service service);

        void UpdateService(Service service);

    }
}
