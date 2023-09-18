using JetBrains.Annotations;

using Nuke.Common;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace _build;

internal class Build : NukeBuild
{
    public static int Main () => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")] 
    private readonly Configuration configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [UsedImplicitly]
    private Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            DotNetClean(s => s
                .EnableNoLogo());
        });

    private Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore();
        });

    private Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(_ => _
                .EnableNoLogo()
                .SetConfiguration(configuration)
                .EnableNoRestore());
        });

    [UsedImplicitly]
    private Target Test => _ => _
        .TriggeredBy(Compile)
        .Executes(() =>
        {
            // DotNetTest(_ => _
            //     .EnableNoLogo()
            //     .EnableNoBuild()
            //     .EnableNoRestore()
            //     .SetFilter("")
            //     .SetConfiguration(configuration));
        });
}