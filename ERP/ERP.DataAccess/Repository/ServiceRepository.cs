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
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        private ApplicationDbContext _db;
        public ServiceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
       
        public void Update(Service obj)
        {
            var servFromDb = _db.Services.FirstOrDefault(d => d.Id == obj.Id);
            if (servFromDb != null)
            {

                servFromDb.Name = obj.Name;
                servFromDb.PriceRange = obj.PriceRange;
                servFromDb.DepartmentId = obj.DepartmentId;
                servFromDb.Remark = obj.Remark;
                servFromDb.UpdatedBy = obj.UpdatedBy;
                servFromDb.UpdatedDateTime = obj.UpdatedDateTime;
            }
            
        }

        public void ChangeStatus(Service obj)
        {
            var servFromDb = _db.Services.FirstOrDefault(d => d.Id == obj.Id);
            if (servFromDb != null)
            {
                if (servFromDb.Active == true)
                {
                    servFromDb.Active = false;
                }
                else
                {
                    servFromDb.Active = true;
                }
            }
        }
    }
}
