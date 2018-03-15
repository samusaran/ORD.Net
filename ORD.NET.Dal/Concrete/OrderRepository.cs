using ORD.NET.Model.DTO;
using ORD.NET.Model.Business;
using ORD.NET.Model.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ORD.NET.DAL
{
    public class OrderRepository : IOrderRepository
    {
        private DbContext _context;

        public OrderRepository(DbContext context)
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

        /// <summary>
        /// Inserisco nuovo ordine.
        /// Se presente, cancello il precedente.
        /// </summary>
        public async Task<FlatOrder> InsertAsync(FlatOrder o)
        {
            if (o == null)
                throw new ArgumentNullException("E' necessario specificare un ordine da inserire", nameof(o));

            if (o.Piatto == null || o.Piatto == "")
                throw new ArgumentException("Il piatto selezionato non può essere null o stringa vuota");

            Ordinazioni row = _context.Set<Ordinazioni>().SingleOrDefault(a => a.Data == o.Data.Date
                                                                            && a.Utente == o.Utente);

            if (row != null)
            {
                _context.Set<Ordinazioni>().Attach(row);
                //_context.Entry(row).State = EntityState.Modified;

                row.OraOrdinazione = o.OraOrdinazione;
                row.Piatto = o.Piatto;
                row.Shottini = o.Shottini;
                row.TipoPiatto = o.TipoPiattoID;
                row.Zeppelin = o.Zeppelin;
                row.Gruppo = o.Gruppo;
            }
            else
            {
                row = new Ordinazioni();
                row.Data = DateTime.Today;
                row.OraOrdinazione = o.OraOrdinazione;
                row.Piatto = o.Piatto;
                row.Shottini = o.Shottini;
                row.TipoPiatto = o.TipoPiattoID;
                row.Utente = o.Utente;
                row.Zeppelin = o.Zeppelin;
                row.Gruppo = o.Gruppo;

                _context.Set<Ordinazioni>().Add(row);
            }

            await _context.SaveChangesAsync();

            // TODO: SignalR
            //Events.OnOrderPlaced(new Events.OrderEventArgs(o, os));

            o.IdOrdinazione = row.IdOrdinazione;

            return o;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <param name="overwrite"></param>
        //public async Task<OrderStatus> InsertAsync(FlatOrder o, bool overwrite)
        //{
        //    if (overwrite || !(await HasUserOrdered(o.Utente)))
        //        return await InsertAsync(o);

        //    return OrderStatus.NotModified;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public async Task<bool> HasUserOrdered(string user)
        {
            return (await GetOrdersAsync(null, null, user)) != null;
        }

        /// <summary>
        /// Elimina l'ordine dell'utente alla data corrente.
        /// </summary>
        /// <param name="user">Utente</param>
        public async Task<bool> DeleteAsync(int id)
        {
            Ordinazioni ordine = _context.Set<Ordinazioni>().SingleOrDefault(o => o.IdOrdinazione == id);

            _context.Set<Ordinazioni>().Remove(ordine);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Funzione per sapere se l'ordine relativo allo zeppelin è già stato inviato.
        /// </summary>
        /// <param name="zeppelinID"></param>
        /// <returns></returns>
        public async Task<bool> IsOrderAlreadySentAsync(int zp)
        {
            return await _context.Set<Model.Tables.LogMail>()
                           .AnyAsync(m => (m.DataInvio >= DateTime.Today && m.DataInvio < DateTime.Today.AddDays(1))
                                       && m.Zeppelin == zp);
        }

        /// <summary>
        /// Funzione per sapere se l'ordine relativo all'utente è già stato inviato.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> IsOrderAlreadySentAsync(string user)
        {
            var result = from ord in _context.Set<Ordinazioni>()
                         join mail in _context.Set<Model.Tables.LogMail>() on new { Zeppelin = ord.Zeppelin.Value, Data = ord.Data } equals new { Zeppelin = mail.Zeppelin, Data = mail.DataInvio.Date }
                         where ord.Utente == user && ord.Data == DateTime.Today
                         select ord;

            return await result.AnyAsync();
        }

        /// <summary>
        /// Lettura conteggio ordini per zeppelin selezionato e data corrente
        /// </summary>
        /// <param name="zeppelin"></param>
        /// <returns></returns>
        public async Task<int> GetOrderCountAsync(int zeppelin)
        {
            return await _context.Set<Ordinazioni>()
                                 .CountAsync(o => o.Zeppelin == zeppelin
                                               && o.Data == DateTime.Today && o.TipoPiatto != 99);
        }

        /// <summary>
        /// Calcola il conteggio delle persone per mostrare il grafico in homepage
        /// </summary>
        /// <returns>Lista di oggetti appositi per il grafico</returns>
        public async Task<List<ChartOrderItem>> GetOrdersForGraphAsync()
        {
            // TODO Valutare quanto questo metodo possa ancora servire
            // TODO Gruppo mancante dal metodo
            var result = from u in _context.Set<Utenti>()
                         from o in _context.Set<Ordinazioni>().Where(o => o.Data == DateTime.Today && o.Utente == u.Utente).DefaultIfEmpty()
                         from z in _context.Set<Model.Tables.Zeppelin>().Where(z => z.ID == o.Zeppelin).DefaultIfEmpty()
                         where !(u.Registrato == false && o == null)
                         group new { Ordine = o, Utente = u } by new { Zeppelin = z, Presente = o != null } into g
                         select new ChartOrderItem
                         {
                             Presente = g.Key.Presente,
                             IdZeppelin = (int?)g.Key.Zeppelin.ID ?? -1,
                             NomeZeppelin = g.Key.Zeppelin.Descrizione,
                             Utenti = g.Select(e => e.Utente.Nickname).ToList(),
                             CakeDay = g.Any(o => o.Utente.Compleanno.HasValue && o.Utente.Compleanno.Value.Month == DateTime.Today.Month && o.Utente.Compleanno.Value.Day == DateTime.Today.Day),
                             Shots = g.Any(o => o.Ordine.Shottini),
                             OrderQuantity = g.Count()
                         };


            return await result.ToListAsync();
        }

        /// <summary>
        /// Restituisce la sua lista degli ordini del giorno.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Order>> GetOrdersAsync(int? gruppo, int? zeppelin, string user, bool includeMissing = false)
        {
            var query = from order in _context.Set<Ordinazioni>()
                        join users in _context.Set<Utenti>() on order.Utente equals users.Utente
                        from zps in _context.Set<Model.Tables.Zeppelin>().Where(z => order.Zeppelin == z.ID).DefaultIfEmpty()
                        join disht in _context.Set<TipoPiatti>() on order.TipoPiatto equals disht.ID
                        where order.Data == DateTime.Today
                              && (order.Gruppo == gruppo)
                              && (!zeppelin.HasValue || (order.Zeppelin == zeppelin.Value) || (includeMissing && order.Zeppelin == null))
                              && (user == null || users.Utente == user)
                        select new Order
                        {
                            Data = order.Data,
                            OraOrdinazione = order.OraOrdinazione,
                            Piatto = order.Piatto,
                            Shottini = order.Shottini,
                            Utente = new User
                            {
                                BirthdayDate = users.Compleanno,
                                DisplayName = users.Nickname,
                                Email = users.Email,
                                Name = users.Utente
                            },
                            Zeppelin = zps == null ? null : new Model.Business.Zeppelin
                            {
                                ID = zps.ID,
                                Nome = zps.Descrizione
                            },
                            TipoPiatto = new DishType
                            {
                                ID = disht.ID,
                                Name = disht.Descrizione
                            }
                        };

            return await query.ToListAsync();
        }

        /// <summary>
        /// Restituisce l'ordine richiesto
        /// </summary>
        /// <returns></returns>
        public async Task<Order> GetOrderAsync(int id)
        {
            var query = from order in _context.Set<Ordinazioni>()
                        join users in _context.Set<Utenti>() on order.Utente equals users.Utente
                        join zps in _context.Set<Model.Tables.Zeppelin>() on order.Zeppelin equals zps.ID
                        join disht in _context.Set<TipoPiatti>() on order.TipoPiatto equals disht.ID
                        where order.IdOrdinazione == id
                        select new Order
                        {
                            Data = order.Data,
                            OraOrdinazione = order.OraOrdinazione,
                            Piatto = order.Piatto,
                            Shottini = order.Shottini,
                            Utente = new User
                            {
                                BirthdayDate = users.Compleanno,
                                DisplayName = users.Nickname,
                                Email = users.Email,
                                Name = users.Utente
                            },
                            Zeppelin = new Model.Business.Zeppelin
                            {
                                ID = zps.ID,
                                Nome = zps.Descrizione
                            },
                            TipoPiatto = new DishType
                            {
                                ID = disht.ID,
                                Name = disht.Descrizione
                            },
                            Gruppo = order.Gruppo
                        };

            return await query.SingleOrDefaultAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetUserHistoryAsync(string user)
        {
            return await _context.Set<Ordinazioni>()
                                 .Where(o => o.Utente == user && o.TipoPiatto != DishType.Assente.ID)
                                 .Select(s => s.Piatto)
                                 .ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        public async Task<FlatOrder> SetUserAsMissingAsync(string user)
        {
            return await this.InsertAsync(new FlatOrder
            {
                Utente = user,
                Piatto = "Assente",
                TipoPiattoID = DishType.Assente.ID,
                Shottini = false,
                Zeppelin = null
            });
        }

        /// <summary>
        /// Restituisce lo zeppelin relativo all'ordine piazzato dall'utente. restituisce null se non esiste l'ordine.
        /// </summary>
        /// <returns></returns>
        public async Task<Model.Business.Zeppelin> GetZeppelinAsync(string user)
        {
            return (await GetOrdersAsync(null, null, user)).Select(x => x.Zeppelin).SingleOrDefault();
        }

        public async Task<Order> GetOrderOfUserAsync(string user)
        {
            var query = from order in _context.Set<Ordinazioni>()
                        join users in _context.Set<Utenti>() on order.Utente equals users.Utente
                        join zps in _context.Set<Model.Tables.Zeppelin>() on order.Zeppelin equals zps.ID
                        join disht in _context.Set<TipoPiatti>() on order.TipoPiatto equals disht.ID
                        where order.Data == DateTime.Today
                           && order.Utente == user
                        select new Order
                        {
                            Data = order.Data,
                            OraOrdinazione = order.OraOrdinazione,
                            Piatto = order.Piatto,
                            Shottini = order.Shottini,
                            Utente = new User
                            {
                                BirthdayDate = users.Compleanno,
                                DisplayName = users.Nickname,
                                Email = users.Email,
                                Name = users.Utente
                            },
                            Zeppelin = new Model.Business.Zeppelin
                            {
                                ID = zps.ID,
                                Nome = zps.Descrizione
                            },
                            TipoPiatto = new DishType
                            {
                                ID = disht.ID,
                                Name = disht.Descrizione
                            },
                            Gruppo = order.Gruppo
                        };

            return await query.SingleOrDefaultAsync();
        }
    }
}
