namespace CashFlow.Entities.Base
{
    public abstract class WithRecordingDates<TPrimaryKey> : Entity<TPrimaryKey>
    {
        public DateTime CreatedAt { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }
    }
}
