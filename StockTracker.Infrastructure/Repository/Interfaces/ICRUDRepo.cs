using System;
namespace StockTracker.Infrastructure.Repository.Interfaces
{
	public interface ICRUDRepo<T>:IRespository<T>
	{
        void Add(T obj);
        Task<int> AddAsync(T obj);
        void Update(T obj);
        void Delete(T obj);
        void Delete(int id);
        void AddRange(List<T> values);
        Task<int> AddRangeAsync(List<T> values);
    }
}

