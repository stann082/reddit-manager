using CommandLine;

namespace lib.options;

[Verb("cache", HelpText = "Cache saved comments.")]
public class CacheOptions
{
    
    [Option('a', "archive", HelpText = "Search in Pushshift file dumps on disk.")]
    public bool IsArchive { get; set; }

}