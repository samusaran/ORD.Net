using Microsoft.EntityFrameworkCore;
using ORD.NET.Model.Business;
using ORD.NET.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORD.NET.DAL
{
    public class DishTypeRepository : IDishTypeRepository
    {
        private DbContext _context;

        public DishTypeRepository(DbContext context)
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

        public async Task<List<DishType>> GetDishTypesAsync(int? zeppelin)
        {
            var result = from t in _context.Set<TipoPiatti>()
                         join rel in _context.Set<RelTipiPiattiZeppelin>() on t.ID equals rel.TipoPiatto
                         join z in _context.Set<Model.Tables.Zeppelin>() on rel.Zeppelin equals z.ID
                         where (!zeppelin.HasValue || z.ID == zeppelin)
                         select new DishType
                         {
                             ID = t.ID,
                             MultipleCount = t.TipoMultiplo,
                             Name = t.Descrizione,
                             VisibleOnGui = t.VisibileSuUI
                         };

            return await result.ToListAsync();
        }

        public async Task<DishType> GetDishTypeAsync(int id)
        {
            string cacheKeyPattern = $"dishType={id}";

            // TODO: cache!!!!
            var result = await _context.Set<TipoPiatti>()
                                       .Where(x => x.ID == id)
                                       .Select(d => new DishType
                                       {
                                           ID = d.ID,
                                           MultipleCount = d.TipoMultiplo,
                                           Name = d.Descrizione,
                                           VisibleOnGui = d.VisibileSuUI
                                       })
                                       .SingleAsync();

            return result;
        }

        public async Task<DishType> CalculateDishType(string order, int zeppelin)
        {
            // TODO cache?!
            var keywords = await GetDishTypeDictionary();
            List<DishType> tipiPerZeppelin = await this.GetDishTypesAsync(zeppelin);

            // Metodo anti mario :D
            string[] paroleOrdine = keywords.Where(x => System.Text.RegularExpressions.Regex.IsMatch(order, String.Format(@"\b{0}\b", x.Keyword)
                                                      , System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                                            .Select(s => s.Keyword)
                                            .ToArray();

            var tipiValidi = keywords.FindAll(t => paroleOrdine.Any(p => string.Compare(p, t.Keyword, true) == 0))
                                     .Distinct()
                                     .Join(tipiPerZeppelin,
                                           tipi => tipi.Type.ID,
                                           tzp => tzp.ID,
                                           (t, tz) => new { TipoPiatto = t.Type, Multiplo = tz.MultipleCount });

            DishType result = DishType.Altro;

            // Se ho trovato una sola parola, prendo quella e basta
            if (tipiValidi.Count() == 1)
            {
                result = tipiValidi.First().TipoPiatto;
            }
            else if (tipiValidi.Count() > 1)
            {
                // prima cerco per il prefisso normalmente la gente scrive (Menu, Bis, Tris)
                result = tipiValidi.Where(w => w.Multiplo != null)
                                   .FirstOrDefault()?
                                   .TipoPiatto;

                // Se non ho trovato alcun prefisso, cerco per numero di corrispondenze (3 piatti = tris)
                if (result == null)
                {
                    result = tipiPerZeppelin.Where(w => w.MultipleCount <= tipiValidi.Count())
                                            .OrderByDescending(o => o.MultipleCount)
                                            .FirstOrDefault();
                }
            }

            return result;
        }

        private async Task<List<DishTypeKeywordPair>> GetDishTypeDictionary()
        {
            var result = from dizionario in _context.Set<DizionarioPiatti>()
                         join tipi in _context.Set<TipoPiatti>() on dizionario.TipoPiatto equals tipi.ID
                         select new DishTypeKeywordPair
                         {
                             Keyword = dizionario.Parola,
                             Type = new DishType
                             {
                                 ID = tipi.ID,
                                 MultipleCount = tipi.TipoMultiplo,
                                 Name = tipi.Descrizione,
                                 VisibleOnGui = tipi.VisibileSuUI
                             }
                         };

            return await result.ToListAsync();
        }
    }
}
