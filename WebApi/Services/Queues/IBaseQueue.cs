namespace Services.Queues
{
    public interface IBaseQueue<T> where T : class
    {
        Task<T> SendMessageAsync(T message);

        Task<List<T>> ReceiveMessagesAsync();
    }
}