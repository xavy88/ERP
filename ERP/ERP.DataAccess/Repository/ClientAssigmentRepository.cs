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
    public class ClientAssigmentRepository : Repository<ClientAssigment>, IClientAssigmentRepository
    {
        private ApplicationDbContext _db;
        public ClientAssigmentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
       
        public void Update(ClientAssigment obj)
        {
            var clientAssigentFromDb = _db.ClientAssigments.FirstOrDefault(d => d.Id == obj.Id);
            if (clientAssigentFromDb != null)
            {

                clientAssigentFromDb.ClientId = obj.ClientId;
                clientAssigentFromDb.DepartmentId = obj.DepartmentId;
                clientAssigentFromDb.JobPositionId = obj.JobPositionId;
                clientAssigentFromDb.EmployeeId = obj.EmployeeId;
                clientAssigentFromDb.Remark = obj.Remark;
                clientAssigentFromDb.UpdatedBy = obj.UpdatedBy;
                clientAssigentFromDb.UpdatedDateTime = obj.UpdatedDateTime;
            }
            
        }
    }
}
