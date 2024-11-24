using System;
using System.IO;
using System.Linq;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tools.DotNet;
using Serilog;

namespace _build;

// ReSharper disable UnusedMember.Local
// ReSharper disable AllUnderscoreLocalParameterName
class BuildProject : NukeBuild
{
    public static int Main() => Execute<BuildProject>();

    #region Parameters

    [Parameter("Verbosity level of the build output - Default is 'Minimal'")]
    readonly DotNetVerbosity DotNetVerbosityLevel = DotNetVerbosity.quiet;

    [Parameter("Skips the build step if specified.")] readonly bool NoBuild;

    #endregion

    #region Targets

    Target DeepClean => _ => _
        .Executes(() =>
        {
            var binAndObjDirectories = RootDirectory.GlobDirectories("**/bin", "**/obj")
                .Where(directory => !directory.ToString().Contains(".nuke"));
            foreach (var directory in binAndObjDirectories)
            {
                Log.Information("Cleaning directory {Directory}", directory);
                directory.DeleteDirectory();
            }

            var pubCliDirectory = RootDirectory / "pubcli";
            Log.Information("Cleaning directory {PublishDirectory}", pubCliDirectory);
            pubCliDirectory.DeleteDirectory();

            var pubWebDirectory = RootDirectory / "pubweb";
            Log.Information("Cleaning directory {PublishDirectory}", pubWebDirectory);
            pubWebDirectory.DeleteDirectory();
        });

    Target DeployCli => _ => _
        .DependsOn(Test, PublishCli)
        .Executes(() =>
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var targetDirectory = Path.Combine(appDataPath, "utils");
            if (!Directory.Exists(targetDirectory)) Directory.CreateDirectory(targetDirectory);

            const string fileName = "reddit.exe";
            var targetFile = Path.Combine(targetDirectory, fileName);
            if (File.Exists(targetFile)) File.Delete(targetFile);

            var sourceFile = RootDirectory / "pubcli" / "app.exe";
            File.Copy(sourceFile, targetFile);
            Log.Information("Deployed {TargetFile} to {TargetDirectory}", fileName, targetDirectory);
        });

    Target PublishCli => _ => _
        .DependsOn(DeepClean)
        .Before(DeployCli)
        .Executes(() =>
        {
            DotNetTasks.DotNetPublish(s => s
                .SetProject(RootDirectory / "src" / "app" / "app.csproj")
                .SetConfiguration(Configuration.Release)
                .SetVerbosity(DotNetVerbosityLevel)
                .SetOutput("pubcli"));
        });

    Target PublishWeb => _ => _
        .DependsOn(DeepClean)
        .Before(DeployCli)
        .Executes(() =>
        {
            DotNetTasks.DotNetPublish(s => s
                .SetProject(RootDirectory / "src" / "ui" / "ui.csproj")
                .SetConfiguration(Configuration.Release)
                .SetVerbosity(DotNetVerbosityLevel)
                .SetOutput("pubweb"));
        });

    Target Test => _ => _
        .Executes(() =>
        {
            DotNetTasks.DotNetTest(s => s
                .SetProjectFile(RootDirectory / "reddit.sln")
                .SetVerbosity(DotNetVerbosityLevel));
        });

    #endregion
}