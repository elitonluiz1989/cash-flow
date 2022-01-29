namespace CashFlow.Entities.Base
{
    public abstract class WithSofDelete<TPrimaryKey> : WithRecordingDates<TPrimaryKey>
        where TPrimaryKey : notnull
    {
        public DateTime? DeletedAt { get; protected set; }
    }
}
