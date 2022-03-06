namespace CashFlow.Entities.Base
{
    public abstract class WithSofDelete<TKey> : WithRecordingDates<TKey>
        where TKey : notnull
    {
        public DateTime? DeletedAt { get; protected set; }

        public void SetDeletedAt()
        {
            DeletedAt = DateTime.Now;
        }
    }
}
