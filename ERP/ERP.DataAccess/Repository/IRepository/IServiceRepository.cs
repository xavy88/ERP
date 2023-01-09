using ERP.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.Repository.IRepository
{
    public interface IServiceRepository : IRepository<Service>
    {
        void Update(Service obj);
        void ChangeStatus(Service obj);

    }
}
