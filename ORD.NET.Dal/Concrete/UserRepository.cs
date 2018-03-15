using Microsoft.EntityFrameworkCore;
using ORD.NET.Model.Business;
using ORD.NET.Model.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ORD.NET.DAL
{
    public class UserRepository : IUserRepository
    {
        private DbContext _context;

        public UserRepository(DbContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Restituisce l'email di un utente
        /// </summary>
        /// <param name="utente"></param>
        /// <returns></returns>
        public async Task<string> GetUserEmailAsync(string utente)
        {
            var user = _context.Set<User>()
                               .Where(u => u.Name == utente)
                               .Select(u => u.Email);

            return await user.SingleOrDefaultAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="registeredOnly"></param>
        /// <returns></returns>
        public async Task<User> GetUserByNameAsync(string name, bool registeredOnly = false)
        {
            var user = from u in _context.Set<Utenti>()
                       where string.Compare(u.Utente, name, true) == 0 && u.Registrato == registeredOnly
                       select new User
                       {
                           BirthdayDate = u.Compleanno,
                           DisplayName = u.Nickname,
                           Email = u.Email,
                           Name = u.Utente,
                           IsAdmin = u.IsAdmin,
                           Image = u.ProfilePic,
                           Registrato = u.Registrato
                       };

            return await user.SingleOrDefaultAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetAllUsersAsync()
        {
            var result = from u in _context.Set<Utenti>()
                         select new User
                         {
                             BirthdayDate = u.Compleanno,
                             DisplayName = u.Nickname,
                             Email = u.Email,
                             IsAdmin = u.IsAdmin,
                             Name = u.Utente,
                             Image = u.ProfilePic,
                             Registrato = u.Registrato
                         };

            return await result.ToListAsync();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
