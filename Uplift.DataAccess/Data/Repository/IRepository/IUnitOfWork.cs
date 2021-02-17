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
        IOrderHeaderRepository OrderHeader { get; set; }
        IOrderDetailsRepository OrderDetails { get; set; }
        void save();
    }
}
