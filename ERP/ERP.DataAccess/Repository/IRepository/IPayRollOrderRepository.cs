using ERP.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.Repository.IRepository
{
    public interface IPayRollOrderRepository : IRepository<PayRollOrder>
    {
        void Update(PayRollOrder obj);
        void Paid(PayRollOrder obj);
        void Closed(PayRollOrder obj);

    }
}
