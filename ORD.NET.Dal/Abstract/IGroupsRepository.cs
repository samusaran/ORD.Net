using ORD.NET.Model.Business;
using ORD.NET.Model.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ORD.NET.DAL
{
    public interface IGroupsRepository
    {
        Task<List<Group>> GetGroups(string user);
    }
}
