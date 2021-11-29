using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashFlow.Entities.Base
{
    public abstract class Entity<TPrimaryKey>
    {
        [Key, Column(Order = 0)]
        public TPrimaryKey Id { get; set; }
    }
}
