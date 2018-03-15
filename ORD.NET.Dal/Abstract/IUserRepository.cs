using ORD.NET.Model.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ORD.NET.DAL
{
    public interface IUserRepository : IDisposable
    {
        Task<string> GetUserEmailAsync(string utente);
        Task<User> GetUserByNameAsync(string name, bool registeredOnly = false);
        Task<List<User>> GetAllUsersAsync();
    }
}
