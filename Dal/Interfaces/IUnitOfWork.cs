namespace Dal.interfaces;

public interface IUnitOfWork
{
    public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IBaseEntity;
    public Task SaveChanges();
}