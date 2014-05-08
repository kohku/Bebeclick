using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebeclick.WebClient
{
    public interface IPortalRepository
    {
        IEnumerable<StateProvince> GetStateProvinces(Guid? id, string name);
        void UpdateStateProvince(StateProvince stateProvince);

        void InsertStateProvince(StateProvince stateProvince);

        void DeleteStateProvince(StateProvince stateProvince);


        IEnumerable<Product> GetProducts(Guid? id, Guid? stateId, string name);

        void DeleteProduct(Product product);

        void InsertProduct(Product product);

        void UpdateProduct(Product product);

        IEnumerable<Service> GetServices(Guid? id, Guid? productId, string name);

        void DeleteService(Service service);

        void InsertService(Service service);

        void UpdateService(Service service);
    }
}
