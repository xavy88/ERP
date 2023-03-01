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
    public class JobApplicationRepository : Repository<JobApplication>, IJobApplicationRepository
    {
        private ApplicationDbContext _db;
        public JobApplicationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Evaluated(JobApplication obj)
        {
            var jobApplicationFromDb = _db.JobApplications.FirstOrDefault(ja=>ja.Id == obj.Id);
            if (jobApplicationFromDb != null)
            {
                if (jobApplicationFromDb.Evaluated == true)
                {
                    jobApplicationFromDb.Evaluated = false;
                }
                else
                {
                    jobApplicationFromDb.Evaluated = true;
                }
            }
        }
    }
}
