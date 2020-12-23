namespace Domain
{
    public class NullRedditData : IRedditData
    {

        public static IRedditData Singleton = new NullRedditData();

        public IContent[] Contents => new IContent[0];

    }
}
