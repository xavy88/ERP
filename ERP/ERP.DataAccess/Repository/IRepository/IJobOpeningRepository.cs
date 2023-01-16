using ERP.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.Repository.IRepository
{
    public interface IJobOpeningRepository : IRepository<JobOpening>
    {
        void Update(JobOpening obj);
        void ChangeStatus(JobOpening obj);

    }
}
