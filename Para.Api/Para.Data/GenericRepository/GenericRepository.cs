using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Para.Base.Entity;
using Para.Data.Context;

namespace Para.Data.GenericRepository;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly ParaSqlDbContext dbContext;

    public GenericRepository(ParaSqlDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task Save()
    {
        await dbContext.SaveChangesAsync();
    }

    public async Task<TEntity> GetById(long Id)
    {
        return await dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == Id);
    }

    public async Task Insert(TEntity entity)
    {
        entity.IsActive = true;
        entity.InsertDate = DateTime.UtcNow;
        entity.InsertUser = "System";
        await dbContext.Set<TEntity>().AddAsync(entity);
    }

    public async Task Update(TEntity entity)
    {
        dbContext.Set<TEntity>().Update(entity);
    }

    public async Task Delete(TEntity entity)
    {
        dbContext.Set<TEntity>().Remove(entity);
    }

    public async Task Delete(long Id)
    {
        var entity = await dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == Id);
        dbContext.Set<TEntity>().Remove(entity);
    }

    public async Task<List<TEntity>> GetAll()
    {
       return await dbContext.Set<TEntity>().ToListAsync();
    }
    
    public IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includes)
    {
        var query = dbContext.Set<TEntity>().AsQueryable();
        return includes.Aggregate(query, (current, include) => current.Include(include));
    }
    
    public async Task<List<TEntity>> Where(Expression<Func<TEntity, bool>> predicate)
    {
        return await dbContext.Set<TEntity>().Where(predicate).ToListAsync();
    }
}