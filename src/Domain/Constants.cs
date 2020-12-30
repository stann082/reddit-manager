using System;

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

    [Flags]
    public enum LogLevel
    {
        None = 0x00000000,
        Debug = 0x00000001,
        Error = 0x00000002,
        Fatal = 0x00000004,
        Info = 0x00000008,
        Warn = 0x00000010,

        All = Debug | Error | Fatal | Info | Warn,
    }

}
