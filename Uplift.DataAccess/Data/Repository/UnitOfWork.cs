﻿using System;
using System.Collections.Generic;
using System.Text;
using Uplift.DataAccess.Data.Repository.IRepository;

namespace Uplift.DataAccess.Data.Repository
{
    public class UnitOfWork : ApplicationDBContext
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Frequency = new FrequencyRepository(_db);
            Service = new ServiceRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
            OrderDetails = new OrderDetailsRepository(_db);
            User = new UserRepository(_db);
            SP_Call = new SP_Call(_db);
        }

        public ICategoryRepository Category { get; set; }
        public IFrequencyRepository Frequency { get; set; }
        public IServiceRepository Service { get; set; }
        public IOrderHeaderRepository OrderHeader { get; set; }
        public IOrderDetailsRepository OrderDetails { get; set; }
        public IUserRepository User { get; set; }
        public ISP_Call SP_Call { get; set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void save()
        {
            _db.SaveChanges();
        }
    }
}
