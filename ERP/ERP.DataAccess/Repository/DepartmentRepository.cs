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
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private ApplicationDbContext _db;
        public DepartmentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
       
        public void Update(Department obj)
        {
            var dptFromDb = _db.Departments.FirstOrDefault(d => d.Id == obj.Id);
            if (dptFromDb != null)
            {

                dptFromDb.Name = obj.Name;
                dptFromDb.Remark = obj.Remark;
                dptFromDb.UpdatedBy = obj.UpdatedBy;
                dptFromDb.UpdatedDateTime = obj.UpdatedDateTime;
            }
            
        }

        public void ChangeStatus(Department obj)
        {
            var dptFromDb = _db.Departments.FirstOrDefault(d => d.Id == obj.Id);
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
