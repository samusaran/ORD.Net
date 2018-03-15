using Microsoft.EntityFrameworkCore;
using ORD.NET.Model.Tables;
using System;
using System.Threading.Tasks;

namespace ORD.NET.DAL
{
    public class LogMailRepository : ILogMailRepository
    {
        private DbContext _context;
        public LogMailRepository(DbContext context)
        {
            _context = context;
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

        public async Task<int> AddLogMail(int zeppelin, DateTime date)
        {
            var row = new LogMail();

            row.DataInvio = date;
            row.Zeppelin = zeppelin;

            _context.Set<LogMail>().Add(row);
            return await _context.SaveChangesAsync();
        }
    }
}
