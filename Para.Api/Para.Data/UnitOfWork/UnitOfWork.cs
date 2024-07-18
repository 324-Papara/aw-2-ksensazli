using System.Linq.Expressions;
using Para.Data.Context;
using Para.Data.Domain;
using Para.Data.GenericRepository;

namespace Para.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ParaSqlDbContext dbContext;
    
    public IGenericRepository<Customer> CustomerRepository { get; }
    public IGenericRepository<CustomerDetail> CustomerDetailRepository { get; }
    public IGenericRepository<CustomerAddress> CustomerAddressRepository { get; }
    public IGenericRepository<CustomerPhone> CustomerPhoneRepository { get; }
    
    

    public UnitOfWork(ParaSqlDbContext dbContext)
    {
        this.dbContext = dbContext;

        CustomerRepository = new GenericRepository<Customer>(this.dbContext);
        CustomerDetailRepository = new GenericRepository<CustomerDetail>(this.dbContext);
        CustomerAddressRepository = new GenericRepository<CustomerAddress>(this.dbContext);
        CustomerPhoneRepository = new GenericRepository<CustomerPhone>(this.dbContext);
    }

    public void Dispose()
    {
    }

    public async Task Complete()
    {
        using (var dbTransaction = await dbContext.Database.BeginTransactionAsync())
        {
            try
            {
                await dbContext.SaveChangesAsync();
                await dbTransaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await dbTransaction.RollbackAsync();
                Console.WriteLine(ex);
                throw;
            }
        }
    }
    
    public IQueryable<TEntity> Include<TEntity>(params Expression<Func<TEntity, object>>[] includes) where TEntity : class
    {
        var repository = GetRepository<TEntity>();
        return repository.Include(includes);
    }
    
    public async Task<List<TEntity>> Where<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
    {
        var repository = GetRepository<TEntity>();
        return await repository.Where(predicate);
    }
    
    private IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        if (typeof(TEntity) == typeof(Customer))
        {
            return (IGenericRepository<TEntity>)CustomerRepository;
        }
        else if (typeof(TEntity) == typeof(CustomerDetail))
        {
            return (IGenericRepository<TEntity>)CustomerDetailRepository;
        }
        else if (typeof(TEntity) == typeof(CustomerAddress))
        {
            return (IGenericRepository<TEntity>)CustomerAddressRepository;
        }
        else if (typeof(TEntity) == typeof(CustomerPhone))
        {
            return (IGenericRepository<TEntity>)CustomerPhoneRepository;
        }
        else
        {
            throw new ArgumentException("Repository not found for the specified entity type.");
        }
    }
}