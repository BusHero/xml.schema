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
        client.BaseAddress = new Uri("http://localhost:5160");
        var result = await client.GetAsync("greet");
        
        result.EnsureSuccessStatusCode();
        
        var response = await result.Content.ReadAsStringAsync();
        
        response.Should().Be("Hello, World!");
    }
    
    public Task InitializeAsync()
    {
        return Task.Run(() => Cli.Wrap("dotnet")
            .WithArguments(args => args
                .Add("run")
                .Add("--no-build")
                .Add("--project")
                .Add("XmlSchemaApi"))
            .WithWorkingDirectory(@"C:\Users\Petru\projects\csharp\xml.schema")
            .WithStandardOutputPipe(PipeTarget.ToDelegate(outputHelper.WriteLine))
            // .WithStandardErrorPipe(PipeTarget.ToDelegate(outputHelper.WriteLine))
            .ExecuteAsync(tokenSource.Token));
    }
    
    public Task DisposeAsync()
    {
        tokenSource.Cancel();
        return Task.CompletedTask;
    }
}