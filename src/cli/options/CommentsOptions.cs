using CommandLine;
using lib;

namespace cli.options;

[Verb("comments", HelpText = "Command related to comments.")]
public class CommentsOptions : AbstractOptions, IOptions
{

}
