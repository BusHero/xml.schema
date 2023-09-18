using CliWrap;

using Xunit.Abstractions;

namespace SchemaRetrievalTests;

public class UnitTest1 : IAsyncLifetime
{
    private readonly ITestOutputHelper outputHelper;
    private readonly CancellationTokenSource tokenSource;
    
    public UnitTest1(ITestOutputHelper outputHelper)
    {
        this.outputHelper = outputHelper;
        tokenSource = new CancellationTokenSource();
    }
    
    [Fact] public void Test1()
    {
        true.Should().BeFalse();
        // var client = new HttpClient();
        // client.BaseAddress = new Uri("http://localhost:5160");
        // var result = await client.GetAsync("greet");
        //
        // result.EnsureSuccessStatusCode();
        //
        // var response = await result.Content.ReadAsStringAsync();
        //
        // response.Should().Be("Hello, World!");
    }
    
    public Task InitializeAsync()
    {
        return Task.CompletedTask;
        // return Task.Run(() => Cli.Wrap("dotnet")
        //     .WithArguments(args => args
        //         .Add("run")
        //         .Add("--no-build")
        //         .Add("--project")
        //         .Add("XmlSchemaApi"))
        //     .WithWorkingDirectory(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", ".."))
        //     .WithStandardOutputPipe(PipeTarget.ToDelegate(outputHelper.WriteLine))
        //     .WithStandardErrorPipe(PipeTarget.ToDelegate(outputHelper.WriteLine))
        //     .ExecuteAsync(tokenSource.Token));
    }
    
    public Task DisposeAsync()
    {
        tokenSource.Cancel();
        return Task.CompletedTask;
    }
}