namespace Domain
{
    public interface IServiceFactory
    {

        IRedditApiService CreateRedditService();

    }
}
