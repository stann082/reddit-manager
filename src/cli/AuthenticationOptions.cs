using CommandLine;
using lib;

namespace cli;

[Verb("auth", HelpText = "Retrieve auth token.")]
public class AuthenticationOptions : IAuthOptions
{

    [Option("app-id", Required = true, HelpText = "Your app id.")]
    public string AppId { get; set; }

    [Option("app-secret", HelpText = "Your app secret.")]
    public string AppSecret { get; set; }

    [Option('p', "port", Default = 8080, HelpText = "Listen port of the authentication webserver.")]
    public int Port { get; set; }

}
