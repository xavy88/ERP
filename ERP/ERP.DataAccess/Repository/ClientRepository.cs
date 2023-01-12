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
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        private ApplicationDbContext _db;
        public ClientRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void ChangeStatus(Client obj)
        {
            var cliFromDb = _db.Clients.FirstOrDefault(e=>e.Id == obj.Id);
            if (cliFromDb != null)
            {
                if (cliFromDb.Active == true)
                {
                    cliFromDb.Active = false;
                }
                else
                {
                    cliFromDb.Active = true;
                }
            }
        }

        public void Update(Client obj)
        {
            var cliFromDb = _db.Clients.FirstOrDefault(e=>e.Id==obj.Id);
            if (cliFromDb != null)
            {
                cliFromDb.BusinessEmail = obj.BusinessName;
                cliFromDb.Url = obj.Url;
                cliFromDb.BusinessPhone=obj.BusinessPhone;
                cliFromDb.BusinessEmail = obj.BusinessEmail;
                cliFromDb.BusinessAddress = obj.BusinessAddress;
                cliFromDb.City = obj.City;
                cliFromDb.State = obj.State;
                cliFromDb.Industry = obj.Industry;
                cliFromDb.ClientName = obj.ClientName;
                cliFromDb.ClientEmail = obj.ClientEmail;
                cliFromDb.ClientPhone = obj.ClientPhone;
                cliFromDb.Access = obj.Access;
                cliFromDb.Notes = obj.Notes;
                cliFromDb.UpdatedBy = obj.UpdatedBy;
                cliFromDb.UpdatedDateTime = DateTime.Now;
                cliFromDb.Active = obj.Active;
                if (cliFromDb.Logo!=null)
                {
                    cliFromDb.Logo = obj.Logo;
                }

            }
        }
    }
}
