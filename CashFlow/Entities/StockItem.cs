using CashFlow.Entities.Base;
using CashFlow.Enum;

namespace CashFlow.Entities
{
    public class StockItem : WithSofDelete<uint>
    {
        public string Name { get; set; }
        public EStockItemType Type { get; set; }
        public short Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
