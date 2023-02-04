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
    public class NoteRepository : Repository<Note>, INoteRepository
    {
        private ApplicationDbContext _db;
        public NoteRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
       
        public void Update(Note obj)
        {
            var noteFromDb = _db.Notes.FirstOrDefault(n => n.Id == obj.Id);
            if (noteFromDb != null)
            {

                noteFromDb.ClientId = obj.ClientId;
                noteFromDb.ApplicationUserId = obj.ApplicationUserId;
                noteFromDb.CreatedDateTime = obj.CreatedDateTime;
                noteFromDb.Comments = obj.Comments;
            }
            
        }

    }
}
