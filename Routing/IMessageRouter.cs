namespace Routing
{
    public interface IMessageRouter
    {
        Task RouteAsync(string RawJson, CancellationToken cancellationToken);
    }
}