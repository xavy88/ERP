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

        //public void Update(JobOpening obj)
        //{
        //    var jobOpeningFromDb = _db.JobOpenings.FirstOrDefault(jo=>jo.Id==obj.Id);
        //    if (jobOpeningFromDb != null)
        //    {
        //        jobOpeningFromDb.JobTitle = obj.JobTitle;
        //        jobOpeningFromDb.DepartmentId = obj.DepartmentId;
        //        jobOpeningFromDb.JobType=obj.JobType;
        //        jobOpeningFromDb.Benefit = obj.Benefit;
        //        jobOpeningFromDb.JobPositionId = obj.JobPositionId;
        //        jobOpeningFromDb.Skills = obj.Skills;
        //        jobOpeningFromDb.Education = obj.Education;
        //        jobOpeningFromDb.Salary = obj.Salary;
        //        jobOpeningFromDb.Shift = obj.Shift;
        //        jobOpeningFromDb.MinimumExperience = obj.MinimumExperience;
        //        jobOpeningFromDb.Vacancy = obj.Vacancy;
        //        jobOpeningFromDb.Email = obj.Email;
        //        jobOpeningFromDb.Expire = obj.Expire;
        //        jobOpeningFromDb.Language = obj.Language;
        //        jobOpeningFromDb.JobDescription = obj.JobDescription;
        //        jobOpeningFromDb.HiringLead = obj.HiringLead;
        //        jobOpeningFromDb.UpdatedBy = obj.UpdatedBy;
        //        jobOpeningFromDb.UpdatedDateTime = DateTime.Now;
        //        jobOpeningFromDb.Active = obj.Active;
        //        if (jobOpeningFromDb.ImageUrl!=null)
        //        {
        //            jobOpeningFromDb.ImageUrl = obj.ImageUrl;
        //        }

        //    }
        //}
    }
}
