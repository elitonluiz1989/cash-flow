namespace CashFlow.Contracts
{
    public interface IRepository<TEntity, TKey>
    {
        int Insert(TEntity entity);
        int Update(TEntity entity);
        int Delete(TKey id);
        int Delete(TEntity entity);
        IEnumerable<TEntity> Select();
        IEnumerable<TEntity> Select(Func<IQueryable<TEntity>, IQueryable<TEntity>>? filter = null);
        IEnumerable<TEntity> All();
        TEntity Find(TKey id);
        TEntity Save(TEntity entity);
        void Validate(TEntity entity);
    }
}
