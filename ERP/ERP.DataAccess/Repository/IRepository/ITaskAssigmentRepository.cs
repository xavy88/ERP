using ERP.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.Repository.IRepository
{
    public interface ITaskAssigmentRepository : IRepository<TaskAssigment>
    {
        void Update(TaskAssigment obj);
        void ChangeStatus(TaskAssigment obj);

    }
}
