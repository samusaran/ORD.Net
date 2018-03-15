using System;
using ORD.NET.Model.Business;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ORD.NET.DAL
{
    public interface IDishTypeRepository : IDisposable
    {
        Task<List<DishType>> GetDishTypesAsync(int? zeppelin);

        Task<DishType> GetDishTypeAsync(int id);

        Task<DishType> CalculateDishType(string order, int zeppelin);
    }
}
