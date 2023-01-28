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
    public class PayRollDetailRepository : Repository<PayRollDetail>, IPayRollDetailRepository
    {
        private ApplicationDbContext _db;
        public PayRollDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Paid(PayRollDetail obj)
        {
            var payRollDetailFromDb = _db.PayRollDetails.FirstOrDefault(prd=>prd.Id == obj.Id);
            if (payRollDetailFromDb != null)
            {
                if (payRollDetailFromDb.Paid == false)
                {
                    payRollDetailFromDb.Paid = true;
                }
                else
                {
                    payRollDetailFromDb.Paid = true;
                }
            }
        }

        public void Update(PayRollDetail obj)
        {
            var payRollDetailFromDb = _db.PayRollDetails.FirstOrDefault(prd=>prd.Id==obj.Id);
            if (payRollDetailFromDb != null)
            {

                payRollDetailFromDb.PayRollOrderId=obj.PayRollOrderId;
                payRollDetailFromDb.EmployeeId= obj.EmployeeId;
                payRollDetailFromDb.Amount = obj.Amount;
                payRollDetailFromDb.Deduction = obj.Deduction;
                payRollDetailFromDb.ExtraPayment = obj.ExtraPayment;
                payRollDetailFromDb.UpdatedBy = obj.UpdatedBy;
                payRollDetailFromDb.UpdatedDateTime = obj.UpdatedDateTime;
                payRollDetailFromDb.Paid = obj.Paid;
                payRollDetailFromDb.Remark = obj.Remark;
               

            }
                
        }
    }
}
