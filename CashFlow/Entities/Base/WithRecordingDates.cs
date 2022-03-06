namespace CashFlow.Entities.Base
{
    public abstract class WithRecordingDates<TKey> : Entity<TKey>
        where TKey : notnull
    {
        public DateTime CreatedAt { get; protected set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; protected set; }

        public void SetUpdatedAt()
        {
            UpdatedAt = DateTime.Now;
        }
    }
}
