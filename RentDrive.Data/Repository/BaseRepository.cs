using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using RentDrive.Data.Repository.Interfaces;

namespace RentDrive.Data.Repository
{
    public class BaseRepository<TType, TId> : IRepository<TType, TId>
        where TType : class
    {
        private readonly RentDriveDbContext dbContext;
        private readonly DbSet<TType> dbSet;

        public BaseRepository(RentDriveDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = this.dbContext.Set<TType>();
        }

        public void Add(TType item)
        {
            this.dbSet.Add(item);
        }

        public async Task AddAsync(TType item)
        {
            await this.dbSet.AddAsync(item);
        }

        public void AddRange(TType[] items)
        {
            this.dbSet.AddRange(items);
        }

        public async Task AddRangeAsync(TType[] items)
        {
            await this.dbSet.AddRangeAsync(items);
        }

        public bool DeleteById(TId id)
        {
            TType entityToRemove = GetById(id);

            if (entityToRemove == null)
            {
                return false;
            }
            this.dbSet.Remove(entityToRemove);
            return true;
        }

        public async Task<bool> DeleteByIdAsync(TId id)
        {
            TType entityToRemove = await GetByIdAsync(id);

            if (entityToRemove == null)
            {
                return false;
            }
            this.dbSet.Remove(entityToRemove);
            return true;
        }

        public IEnumerable<TType> GetAll()
        {
            return this.dbSet.ToArray();
        }

        public async Task<IEnumerable<TType>> GetAllAsync()
        {
            return await this.dbSet.ToArrayAsync();
        }
        public IQueryable<TType> GetAllAsQueryable()
        {
            return this.dbSet.AsQueryable();
        }
        public TType GetById(TId id)
        {
            TType entity = this.dbSet.Find(id);
            return entity;
        }

        public async Task<TType> GetByIdAsync(TId id)
        {
            TType entity = await this.dbSet.FindAsync(id);
            return entity;
        }

        public bool Update(TType entity)
        {
            try
            {
                this.dbSet.Attach(entity);
                this.dbContext.Entry(entity).State = EntityState.Modified;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TType entity)
        {
            try
            {
                this.dbSet.Attach(entity);
                this.dbContext.Entry(entity).State = EntityState.Modified;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await this.dbContext.SaveChangesAsync();
        }
    }
}
