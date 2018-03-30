using ORD.NET.Model.Business;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ORD.NET.DAL
{
    public interface IGroupsRepository
    {
        Task<List<Group>> GetGroups(string user);
        Task<bool> SaveGroup(Group group);
    }
}
