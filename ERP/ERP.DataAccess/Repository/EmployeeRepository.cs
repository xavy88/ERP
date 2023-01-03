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
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private ApplicationDbContext _db;
        public EmployeeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void ChangeStatus(Employee obj)
        {
            var empFromDb = _db.Employees.FirstOrDefault(e=>e.Id == obj.Id);
            if (empFromDb != null)
            {
                if (empFromDb.Active == true)
                {
                    empFromDb.Active = false;
                }
                else
                {
                    empFromDb.Active = true;
                }
            }
        }

        public void Update(Employee obj)
        {
            var empFromDb = _db.Employees.FirstOrDefault(e=>e.Id==obj.Id);
            if (empFromDb != null)
            {
                empFromDb.Name = obj.Name;
                empFromDb.BirthDay = obj.BirthDay;
                empFromDb.SSN=obj.SSN;
                empFromDb.IdentificationId = obj.IdentificationId;
                empFromDb.Gender = obj.Gender;
                empFromDb.MaritalStatus = obj.MaritalStatus;
                empFromDb.Address = obj.Address;
                empFromDb.MobilePhone = obj.MobilePhone;
                empFromDb.HomePhone = obj.HomePhone;
                empFromDb.WorkEmail = obj.WorkEmail;
                empFromDb.PersonalEmail = obj.PersonalEmail;
                empFromDb.ContactName = obj.ContactName;
                empFromDb.Relationship = obj.Relationship;
                empFromDb.Phone = obj.Phone;
                empFromDb.Email = obj.Email;
                empFromDb.AddressContact = obj.AddressContact;
                empFromDb.PayAmount = obj.PayAmount;
                empFromDb.ReportTo = obj.ReportTo;
                empFromDb.DepartmentId = obj.DepartmentId;
                empFromDb.JobPosistionId = obj.JobPosistionId;
                empFromDb.CreatedBy = obj.CreatedBy;
                empFromDb.CreatedDateTime = obj.CreatedDateTime;
                empFromDb.UpdatedBy = obj.UpdatedBy;
                empFromDb.UpdatedDateTime = DateTime.Now;
                empFromDb.Active = obj.Active;
                if (empFromDb.ImageUrl!=null)
                {
                    empFromDb.ImageUrl = obj.ImageUrl;
                }

            }
                //_db.Employees.Update(obj);
        }
    }
}
