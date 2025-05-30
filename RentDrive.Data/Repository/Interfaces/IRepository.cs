﻿namespace RentDrive.Data.Repository.Interfaces
{
    public interface IRepository<TType, TId>
    {
        TType GetById(TId id);
        Task<TType> GetByIdAsync(TId id);
        IEnumerable<TType> GetAll();
        Task<IEnumerable<TType>> GetAllAsync();
        IQueryable<TType> GetAllAsQueryable();
        void Add(TType item);
        Task AddAsync(TType item);
        void AddRange(TType[] items);
        Task AddRangeAsync(TType[] items);
        bool Update(TType item);
        Task<bool> UpdateAsync(TType item);
        bool DeleteById(TId id);
        Task<bool> DeleteByIdAsync(TId id);
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
