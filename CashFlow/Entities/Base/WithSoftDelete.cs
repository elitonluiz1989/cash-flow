namespace CashFlow.Entities.Base
{
    public abstract class WithSofDelete<TPrimaryKey> : WithRecordingDates<TPrimaryKey>
    {
        public DateTime? DeletedAt { get; protected set; }
    }
}
