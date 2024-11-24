using System;
using System.IO;
using System.Linq;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tools.DotNet;
using Serilog;

// ReSharper disable UnusedMember.Local
// ReSharper disable AllUnderscoreLocalParameterName
class BuildProject : NukeBuild
{

    public static int Main() => Execute<BuildProject>();

    #region Parameters

    [Parameter("Verbosity level of the build output - Default is 'Minimal'")]
    readonly DotNetVerbosity DotNetVerbosityLevel = DotNetVerbosity.quiet;
    
    [Parameter("Skips the build step if specified.")]
    readonly bool NoBuild;

    [Parameter("Paths to the test project(s) to run. You can provide multiple paths as separate arguments.")]
    readonly AbsolutePath[] TestProjectPaths;

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
            
            var buildDirectory = RootDirectory / "build";
            Log.Information("Cleaning directory {BuildDirectory}", buildDirectory);
            buildDirectory.DeleteDirectory();

            var publishDirectory = RootDirectory / "pub";
            Log.Information("Cleaning directory {PublishDirectory}", publishDirectory);
            publishDirectory.DeleteDirectory();
        });

    Target Deploy => _ => _
        .DependsOn(Test, Publish)
        .Executes(() =>
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var targetDirectory = Path.Combine(appDataPath, "utils");
            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            const string fileName = "reddit.exe";
            var targetFile = Path.Combine(targetDirectory, fileName);
            if (File.Exists(targetFile))
            {
                File.Delete(targetFile);
            }
            
            var sourceFile = RootDirectory / "pub" / "PersonalFinance.CLI.exe";
            File.Copy(sourceFile, targetFile);
            Log.Information("Deployed {TargetFile} to {TargetDirectory}", fileName, targetDirectory);
        });

    Target Publish => _ => _
        .DependsOn(DeepClean)
        .Before(Deploy)
        .Executes(() =>
        {
            DotNetTasks.DotNetPublish(s => s
                .SetProject(RootDirectory / "PersonalFinance.sln")
                .SetConfiguration(Configuration.Release)
                .SetVerbosity(DotNetVerbosityLevel)
                .SetOutput(RootDirectory / "pub"));
        });

    Target Test => _ => _
        .Executes(() =>
        {
            GetTestProjects().AsParallel().ForAll(project =>
            {
                DotNetTasks.DotNetTest(s => s
                    .SetProjectFile(project)
                    .SetVerbosity(DotNetVerbosityLevel));
            });
        });

    #endregion

    #region Helper Methods

    AbsolutePath[] GetTestProjects() =>
        (TestProjectPaths ?? []).Length > 0
            ? TestProjectPaths
            :
            [
                RootDirectory / "tests" / "PersonalFinance.Application.Tests" / "PersonalFinance.Application.Tests.csproj",
                RootDirectory / "tests" / "PersonalFinance.Domain.Tests" / "PersonalFinance.Domain.Tests.csproj",
                RootDirectory / "tests" / "PersonalFinance.Infrastructure.Tests" / "PersonalFinance.Infrastructure.Tests.csproj"
            ];

    #endregion

}
