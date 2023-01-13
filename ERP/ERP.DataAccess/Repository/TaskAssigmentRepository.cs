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
    public class TaskAssigmentRepository : Repository<TaskAssigment>, ITaskAssigmentRepository
    {
        private ApplicationDbContext _db;
        public TaskAssigmentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
       
        public void Update(TaskAssigment obj)
        {
            var taskAssigentFromDb = _db.TaskAssigment.FirstOrDefault(d => d.Id == obj.Id);
            if (taskAssigentFromDb != null)
            {

                taskAssigentFromDb.TasksId = obj.TasksId;
                taskAssigentFromDb.ClientId = obj.ClientId;
                taskAssigentFromDb.EmployeeId = obj.EmployeeId;
                taskAssigentFromDb.Remark = obj.Remark;
                taskAssigentFromDb.UpdatedBy = obj.UpdatedBy;
                taskAssigentFromDb.UpdatedDateTime = obj.UpdatedDateTime;
            }
            
        }

        public void ChangeStatus(TaskAssigment obj)
        {
            var taskAssigentFromDb = _db.TaskAssigment.FirstOrDefault(d => d.Id == obj.Id);
            if (taskAssigentFromDb != null)
            {
                if (taskAssigentFromDb.Closed == true)
                {
                    taskAssigentFromDb.Closed = false;
                }
                else
                {
                    taskAssigentFromDb.Closed = true;
                }
            }
        }
    }
}
