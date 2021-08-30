namespace INDG.GRIP.Trader.Domain.Common
{
    public abstract class IdentityEntity<T> : Entity
    {
        public virtual T Id { get; protected set; }
    }
}