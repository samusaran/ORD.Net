using ORD.NET.Model.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ORD.NET.DAL
{
    public interface IDishRepository : IDisposable
    {
        Task<List<DishEntry>> GetMenu(int zp);
    }
}
