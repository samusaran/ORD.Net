using ORD.NET.Model.Business;
using ORD.NET.Model.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ORD.NET.DAL
{
    public interface IOrderRepository : IDisposable
    {
        Task<OrderDTO> InsertAsync(OrderDTO o);
        Task<bool> HasUserOrdered(string user);
        Task<bool> DeleteAsync(int id);
        Task<bool> IsOrderAlreadySentAsync(int zp);
        Task<bool> IsOrderAlreadySentAsync(string user);
        Task<int> GetOrderCountAsync(int zeppelin);
        Task<List<ChartOrderItem>> GetOrdersForGraphAsync();
        Task<List<Order>> GetOrdersAsync(int? gruppo, int? zeppelin, string user, bool includeMissing);
        Task<Order> GetOrderAsync(int id);
        Task<List<string>> GetUserHistoryAsync(string user);
        Task<OrderDTO> SetUserAsMissingAsync(string user);
        Task<Zeppelin> GetZeppelinAsync(string user);
        Task<Order> GetOrderOfUserAsync(string user);
    }
}
