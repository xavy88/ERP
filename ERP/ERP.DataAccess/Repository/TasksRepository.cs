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
    public class TasksRepository : Repository<Tasks>, ITasksRepository
    {
        private ApplicationDbContext _db;
        public TasksRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
       
        public void Update(Tasks obj)
        {
            var taskFromDb = _db.Tasks.FirstOrDefault(d => d.Id == obj.Id);
            if (taskFromDb != null)
            {

                taskFromDb.Name = obj.Name;
                taskFromDb.JobPositionId = obj.JobPositionId;
                taskFromDb.DepartmentId = obj.DepartmentId;
                taskFromDb.Remark = obj.Remark;
                taskFromDb.UpdatedBy = obj.UpdatedBy;
                taskFromDb.UpdatedDateTime = obj.UpdatedDateTime;
            }
            
        }

        public void ChangeStatus(Tasks obj)
        {
            var tasksFromDb = _db.Tasks.FirstOrDefault(d => d.Id == obj.Id);
            if (tasksFromDb != null)
            {
                if (tasksFromDb.Active == true)
                {
                    tasksFromDb.Active = false;
                }
                else
                {
                    tasksFromDb.Active = true;
                }
            }
        }
    }
}
