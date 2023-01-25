using ERP.DataAccess.Data;
using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private ApplicationDbContext _db;
        public OrderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
       
        public void Update(Order obj)
        {
            var orderFromDb = _db.Orders.FirstOrDefault(o => o.Id == obj.Id);
            if (orderFromDb != null)
            {
                orderFromDb.Reference = obj.Reference;
                orderFromDb.ClientId = obj.ClientId;
                orderFromDb.ServiceId = obj.ServiceId;
                orderFromDb.EmployeeId = obj.EmployeeId;
                orderFromDb.Amount = obj.Amount;
                orderFromDb.StartDate = obj.StartDate;
                orderFromDb.EndDate = obj.EndDate;
                orderFromDb.BillingDate = obj.BillingDate;
                orderFromDb.Remark = obj.Remark;
                orderFromDb.UpdatedBy = obj.UpdatedBy;
                orderFromDb.UpdatedDateTime = obj.UpdatedDateTime;
            }
            
        }

        public void Close(Order obj)
        {
            var orderFromDb = _db.Orders.FirstOrDefault(d => d.Id == obj.Id);
            if (orderFromDb != null)
            {
                if (orderFromDb.Closed == true)
                {
                    orderFromDb.Closed = false;
                }
                else
                {
                    orderFromDb.Closed = true;
                }
            }
        }
        public void Pay(Order obj)
        {
            var orderFromDb = _db.Orders.FirstOrDefault(d => d.Id == obj.Id);
            if (orderFromDb != null)
            {
                if (orderFromDb.Paid == false)
                {
                    orderFromDb.Paid = true;
                }
                else
                {
                    orderFromDb.Paid = false;
                }
            }
        }
    }
}
