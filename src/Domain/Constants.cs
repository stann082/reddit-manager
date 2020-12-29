namespace Domain
{

    public static class Constants
    {

        public const string BASE_URL = "https://api.pushshift.io/reddit/search/";

    }

    public enum QueryType
    {
        Comment = 0,
        Submission = 1
    }

}
