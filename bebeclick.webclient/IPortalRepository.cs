using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebeclick.WebClient
{
    public interface IPortalRepository
    {
        IEnumerable<StateProvince> GetAllStateProvinces();
    }
}
