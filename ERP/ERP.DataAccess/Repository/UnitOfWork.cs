﻿using ERP.DataAccess.Data;
using ERP.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Department = new DepartmentRepository(_db);
        }
        public IDepartmentRepository Department { get; private set; }
        public IJobPositionRepository JobPosition { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}