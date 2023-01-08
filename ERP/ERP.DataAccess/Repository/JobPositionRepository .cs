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
    public class JobPositionRepository : Repository<JobPosition>, IJobPositionRepository
    {
        private ApplicationDbContext _db;
        public JobPositionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
       
        public void Update(JobPosition obj)
        {
            var dptFromDb = _db.JobPositions.FirstOrDefault(d => d.Id == obj.Id);
            if (dptFromDb != null)
            {

                dptFromDb.Name = obj.Name;
                dptFromDb.Remark = obj.Remark;
                dptFromDb.UpdatedBy = obj.UpdatedBy;
                dptFromDb.UpdatedDateTime = obj.UpdatedDateTime;
            }

        }

        public void ChangeStatus(JobPosition obj)
        {
            var dptFromDb = _db.JobPositions.FirstOrDefault(d => d.Id == obj.Id);
            if (dptFromDb != null)
            {
                if (dptFromDb.Active == true)
                {
                    dptFromDb.Active = false;
                }
                else
                {
                    dptFromDb.Active = true;
                }
            }
        }
    }
}
