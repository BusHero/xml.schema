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
        tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(60));
    }
    
    [Fact] public async Task Test1()
    {
        await Cli.Wrap("dotnet")
            .WithArguments("publish/XmlSchemaApi.dll")
            .WithWorkingDirectory("/home/runner/work/xml.schema/xml.schema")
            .WithStandardOutputPipe(PipeTarget.ToDelegate(Console.WriteLine))
            .WithStandardErrorPipe(PipeTarget.ToDelegate(Console.WriteLine))
            .ExecuteAsync(tokenSource.Token);
        // var client = new HttpClient();
        // client.BaseAddress = new Uri("http://localhost:5000");
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
        return Task.Run(() => Cli.Wrap("dotnet")
            .WithArguments(args => args
                .Add(Path.Combine("publish", "XmlSchemaApi.dll")))
            .WithWorkingDirectory("/home/runner/work/xml.schema/xml.schema")
            .WithStandardOutputPipe(PipeTarget.ToDelegate(outputHelper.WriteLine))
            .WithStandardErrorPipe(PipeTarget.ToDelegate(outputHelper.WriteLine))
            .ExecuteAsync(tokenSource.Token));
    }
    
    public Task DisposeAsync()
    {
        // tokenSource.Cancel();
        return Task.CompletedTask;
    }
}