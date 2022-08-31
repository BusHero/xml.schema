namespace xml.schema.Services.Impl;
using  System.IO;

public class InMemorySchemaService : ISchemaService
{
    private readonly Dictionary<string, string> _namespaces;
    public InMemorySchemaService()
    {
        _namespaces = new Dictionary<string, string>
        {
            ["http://schemas.microsoft.com/powershell/help/2010/05"] = "HelpInfo"
        };
    }
    
    public string GetSchema(string xmlNamespace)
    {
        if (!_namespaces.TryGetValue(xmlNamespace, out var filename))
        {
            return "Namespace not found";
        }
        var path = $@"C:\Users\Petru\projects\web\xml.schema\xml.schema\xml.schema\Services.Impl\InMemorySchemaService\{filename}.xsd";
        var text = File.ReadAllText(path);
        return text;
    }
}