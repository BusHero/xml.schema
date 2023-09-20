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
    
    [Fact] public async Task Test1()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5000");
        var result = await client.GetAsync("greet");
        
        result.EnsureSuccessStatusCode();
        
        var response = await result.Content.ReadAsStringAsync();
        
        response.Should().Be("Hello, World!");
    }
    
    public async Task InitializeAsync()
    {
        _ = Task.Run(() => Cli.Wrap("dotnet")
            .WithArguments("publish/XmlSchemaApi.dll")
            .WithWorkingDirectory("/home/runner/work/xml.schema/xml.schema")
            .WithStandardOutputPipe(PipeTarget.ToDelegate(Console.WriteLine))
            .WithStandardErrorPipe(PipeTarget.ToDelegate(Console.WriteLine))
            .ExecuteAsync(tokenSource.Token));
        await Task.Delay(1000);
    }
    
    public Task DisposeAsync()
    {
        tokenSource.Cancel();
        return Task.CompletedTask;
    }
}