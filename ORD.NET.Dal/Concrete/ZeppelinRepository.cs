using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORD.NET.DAL
{
    public class ZeppelinRepository : IZeppelinRepository
    {
        private DbContext _context;

        public ZeppelinRepository(DbContext context)
        {
            this._context = context;
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

        public async Task<Model.Business.Zeppelin> GetZeppelinAsync(int zid)
        {
            return await _context.Set<Model.Tables.Zeppelin>()
                                 .Where(z => z.ID == zid)
                                 .Select(z => new Model.Business.Zeppelin
                                 {
                                     Email = z.Email,
                                     ID = z.ID,
                                     Indirizzo = z.Indirizzo,
                                     Nome = z.Descrizione,
                                     Proprietario = z.NomeProprietario,
                                     Telefono = z.Telefono
                                 })
                                 .SingleOrDefaultAsync();
        }

        public async Task<Model.Business.Zeppelin> GetZeppelinAsync(string zname)
        {
            return await _context.Set<Model.Tables.Zeppelin>()
                                 .Where(z => string.Compare(z.Descrizione, zname, true) == 0)
                                 .Select(z => new Model.Business.Zeppelin
                                 {
                                     Email = z.Email,
                                     ID = z.ID,
                                     Indirizzo = z.Indirizzo,
                                     Nome = z.Descrizione,
                                     Proprietario = z.NomeProprietario,
                                     Telefono = z.Telefono
                                 }).SingleOrDefaultAsync();
        }

        public async Task<List<Model.Business.Zeppelin>> GetAllZeppelinsAsync()
        {
            var result = from zeppelin in _context.Set<Model.Tables.Zeppelin>()
                         select new Model.Business.Zeppelin
                         {
                             Email = zeppelin.Email,
                             ID = zeppelin.ID,
                             Indirizzo = zeppelin.Indirizzo,
                             Nome = zeppelin.Descrizione,
                             Proprietario = zeppelin.NomeProprietario,
                             Telefono = zeppelin.Telefono
                         };

            return await result.ToListAsync();
        }

    }
}
