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
            JobPosition = new JobPositionRepository(_db);
            Employee = new EmployeeRepository(_db);
            DayOff = new DayOffRepository(_db);
            Service = new ServiceRepository(_db);
            Tasks = new TasksRepository(_db);
            TaskAssigment = new TaskAssigmentRepository(_db);
            Client = new ClientRepository(_db);
            Order = new OrderRepository(_db);
            JobOpening = new JobOpeningRepository(_db);
            JobApplication = new JobApplicationRepository(_db);
            PayRollOrder = new PayRollOrderRepository(_db);
            PayRollDetail = new PayRollDetailRepository(_db);
            Note = new NoteRepository(_db);
            ClientAssigment = new ClientAssigmentRepository(_db);
        }
        public IDepartmentRepository Department { get; private set; }
        public IJobPositionRepository JobPosition { get; private set; }
        public IEmployeeRepository Employee { get; private set; }
        public IDayOffRepository DayOff { get; private set; }
        public IServiceRepository Service { get; private set; }
        public ITasksRepository Tasks { get; private set; }
        public ITaskAssigmentRepository TaskAssigment { get; private set; }
        public IClientRepository Client { get; private set; }
        public IOrderRepository Order { get; private set; }
        public IJobOpeningRepository JobOpening { get; private set; }
        public IJobApplicationRepository JobApplication { get; private set; }
        public IPayRollOrderRepository PayRollOrder { get; private set; }
        public IPayRollDetailRepository PayRollDetail { get; private set; }
        public INoteRepository Note { get; private set; }
        public IClientAssigmentRepository ClientAssigment { get; private set; }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
