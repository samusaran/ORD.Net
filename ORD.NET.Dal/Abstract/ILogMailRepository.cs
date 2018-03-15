using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORD.NET.Model.Business;

namespace ORD.NET.DAL
{
    public interface ILogMailRepository : IDisposable
    {
        Task<int> AddLogMail(int zeppelin, DateTime date);
    }
}
