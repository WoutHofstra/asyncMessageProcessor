using System.Threading.Tasks;
using System.Threading;
namespace Models
{
    public interface IMessageHandler<TMessage>
    {
        Task HandleAsync(TMessage message, CancellationToken cancellationToken);
    }
}