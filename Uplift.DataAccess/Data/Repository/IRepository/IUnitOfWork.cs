using System;
using System.Collections.Generic;
using System.Text;

namespace Uplift.DataAccess.Data.Repository.IRepository
{
    public interface ApplicationDBContext : IDisposable
    {
        ICategoryRepository Category { get; set; }
        IFrequencyRepository Frequency { get; set; }
        IServiceRepository Service { get; set; }
        IOrderHeaderRepository OrderHeader { get; set; }
        IOrderDetailsRepository OrderDetails { get; set; }
        IUserRepository User { get; set; }
        ISP_Call SP_Call { get; set; }
        void save();
    }
}
