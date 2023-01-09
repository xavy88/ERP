using ERP.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.Repository.IRepository
{
    public interface ITasksRepository : IRepository<Tasks>
    {
        void Update(Tasks obj);
        void ChangeStatus(Tasks obj);

    }
}
