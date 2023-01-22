using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IDepartmentRepository Department { get;}
        IJobPositionRepository JobPosition { get; }
        IEmployeeRepository Employee { get; }
        IDayOffRepository DayOff { get; }
        IServiceRepository Service { get; }
        ITasksRepository Tasks { get; }
        ITaskAssigmentRepository TaskAssigment { get; }
        IClientRepository Client { get; }
        IOrderRepository Order { get; }
        IJobOpeningRepository JobOpening { get; }
        IJobApplicationRepository JobApplication { get; }
        void Save();
    }
}
