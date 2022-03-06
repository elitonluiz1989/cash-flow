namespace CashFlow.Contracts
{
    public interface IRepository<TEntity, TKey>
    {
        int Insert(TEntity entity);
        int Update(TEntity entity);
        int Delete(TKey id);
        int Delete(TEntity entity);
        IList<TEntity> Select();
        IList<TEntity> Select(Func<IQueryable<TEntity>, IQueryable<TEntity>>? filter = null);
        IList<TEntity> All();
        TEntity Find(TKey id);
        TEntity Save(TEntity entity);
        void Validate(TEntity entity);
    }
}
