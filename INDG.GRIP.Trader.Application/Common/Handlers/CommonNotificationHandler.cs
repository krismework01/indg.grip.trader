using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace INDG.GRIP.Trader.Application.Common.Handlers
{
    public abstract class CommonNotificationHandler<T> : INotificationHandler<T> where T : INotification
    {
        public abstract Task Handle(T notification, CancellationToken cancellationToken);
    }
}