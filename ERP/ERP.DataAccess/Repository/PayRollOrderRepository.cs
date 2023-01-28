using ERP.DataAccess.Data;
using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.Repository
{
    public class PayRollOrderRepository : Repository<PayRollOrder>, IPayRollOrderRepository
    {
        private ApplicationDbContext _db;
        public PayRollOrderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Paid(PayRollOrder obj)
        {
            var payRollOrderFromDb = _db.PayRollOrders.FirstOrDefault(pr=>pr.Id == obj.Id);
            if (payRollOrderFromDb != null)
            {
                if (payRollOrderFromDb.Paid == false)
                {
                    payRollOrderFromDb.Paid = true;
                }
                else
                {
                    payRollOrderFromDb.Paid = true;
                }
            }
        }

        public void Closed(PayRollOrder obj)
        {
            var payRollOrderFromDb = _db.PayRollOrders.FirstOrDefault(pr => pr.Id == obj.Id);
            if (payRollOrderFromDb != null)
            {
                if (payRollOrderFromDb.Closed == false)
                {
                    payRollOrderFromDb.Closed = true;
                }
                else
                {
                    payRollOrderFromDb.Closed = true;
                }
            }
        }

        public void Update(PayRollOrder obj)
        {
            var payRollOrderFromDb = _db.PayRollOrders.FirstOrDefault(pr=>pr.Id==obj.Id);
            if (payRollOrderFromDb != null)
            {

                payRollOrderFromDb.Reference = obj.Reference;
                payRollOrderFromDb.Sequence=obj.Sequence;
                payRollOrderFromDb.Month= obj.Month;
                payRollOrderFromDb.Year = obj.Year;
                payRollOrderFromDb.PayentDate = obj.PayentDate;
                payRollOrderFromDb.UpdatedBy = obj.UpdatedBy;
                payRollOrderFromDb.UpdatedDateTime = obj.UpdatedDateTime;
                payRollOrderFromDb.Paid = obj.Paid;
                payRollOrderFromDb.Closed = obj.Closed;
                payRollOrderFromDb.Remark = obj.Remark;
               

            }
                
        }
    }
}
