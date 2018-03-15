using ORD.NET.Model.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ORD.NET.DAL
{
    public interface IZeppelinRepository :IDisposable
    {
        Task<Zeppelin> GetZeppelinAsync(int zid);

        Task<Zeppelin> GetZeppelinAsync(string zname);

        Task<List<Zeppelin>> GetAllZeppelinsAsync();
    }
}
