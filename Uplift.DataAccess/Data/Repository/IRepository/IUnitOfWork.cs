using System;
using System.Collections.Generic;
using System.Text;

namespace Uplift.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; set; }
        IFrequencyRepository Frequency { get; set; }
        IServiceRepository Service { get; set; }
        void save();
    }
}
