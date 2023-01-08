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
    public class DayOffRepository : Repository<DayOff>, IDayOffRepository
    {
        private ApplicationDbContext _db;
        public DayOffRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Approved(DayOff obj)
        {
            var dayOffFromDb = _db.DaysOff.FirstOrDefault(d=>d.Id == obj.Id);
            if (dayOffFromDb != null)
            {
                if (dayOffFromDb.Approved == true)
                {
                    dayOffFromDb.Approved = false;
                }
                else
                {
                    dayOffFromDb.Approved = true;
                }
            }
        }

        public void Closed(DayOff obj)
        {
            var dayOffFromDb = _db.DaysOff.FirstOrDefault(d => d.Id == obj.Id);
            if (dayOffFromDb != null)
            {
                if (dayOffFromDb.Closed == false)
                {
                    dayOffFromDb.Closed = true;
                }
                else
                {
                    dayOffFromDb.Closed = false;
                }
            }
        }

        public void Update(DayOff obj)
        {
            var dayOffFromDb = _db.DaysOff.FirstOrDefault(d=>d.Id==obj.Id);
            if (dayOffFromDb != null)
            {
                
                dayOffFromDb.Qty = obj.Qty;
                dayOffFromDb.QtyType=obj.QtyType;
                dayOffFromDb.Type= obj.Type;
                dayOffFromDb.StartDate = obj.StartDate;
                dayOffFromDb.EndDate = obj.EndDate;
                dayOffFromDb.UpdatedBy = obj.UpdatedBy;
                dayOffFromDb.UpdatedDateTime = obj.UpdatedDateTime;
                dayOffFromDb.Closed = obj.Closed;
                dayOffFromDb.Remark = obj.Remark;
               

            }
                
        }
    }
}
