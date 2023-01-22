using ERP.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.Repository.IRepository
{
    public interface IJobApplicationRepository : IRepository<JobApplication>
    {
        void Evaluated(JobApplication obj);

    }
}
