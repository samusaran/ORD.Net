using Microsoft.EntityFrameworkCore;
using ORD.NET.Model.Business;
using ORD.NET.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORD.NET.DAL
{
    public class DishRepository : IDishRepository
    {
        private DbContext _context;
        public DishRepository(DbContext context)
        {
            this._context = context;
        }

        public async Task<List<DishEntry>> GetMenu(int id)
        {
            var piatti = from d in _context.Set<Menu>()
                         join disht in _context.Set<TipoPiatti>() on d.Tipo equals disht.ID
                         where d.IDZeppelin == id
                         select new DishEntry
                         {
                             Piatto = d.Piatto,
                             Progressivo = d.Progressivo,
                             TipoPiatto = new DishType
                             {
                                 ID = disht.ID,
                                 Name = disht.Descrizione,
                                 MultipleCount = disht.TipoMultiplo,
                                 VisibleOnGui = disht.VisibileSuUI
                             }
                         };

            return await piatti.ToListAsync();
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
