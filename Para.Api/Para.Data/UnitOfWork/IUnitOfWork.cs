using System.Linq.Expressions;
using Para.Data.Domain;
using Para.Data.GenericRepository;

namespace Para.Data.UnitOfWork;

public interface IUnitOfWork
{
    Task Complete();
    
    IGenericRepository<Customer> CustomerRepository { get; }
    IGenericRepository<CustomerDetail> CustomerDetailRepository { get; }
    IGenericRepository<CustomerAddress> CustomerAddressRepository { get; }
    IGenericRepository<CustomerPhone> CustomerPhoneRepository { get; }
    
    IQueryable<TEntity> Include<TEntity>(params Expression<Func<TEntity, object>>[] includes) where TEntity : class;
    Task<List<TEntity>> Where<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
}