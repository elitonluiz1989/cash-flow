using CashFlow.Entities;
using CashFlow.Infra.Data.Context;
using CashFlow.Validators;

namespace CashFlow.Infra.Data.Repositories
{
    public class ProductsRepository : Repository<Product, uint>
    {
        public ProductsRepository(
            SQLiteContext dbContext,
            ProductValidator validator
        ) : base(
            dbContext,
            validator
        )
        {
        }
    }
}
