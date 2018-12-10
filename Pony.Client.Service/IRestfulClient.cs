namespace Pony.Client.Service
{
    using Pony.Client.Service.Model;

    public interface IRestfulClient
    {
        T Execute<T>(RestClientModel model)
            where T : class, new();
    }
}
