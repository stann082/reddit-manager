using CommandLine;

namespace lib.options;

[Verb("saved", HelpText = "Command related to saved items.")]
public class SavedOptions : AbstractOptions, IOptions;