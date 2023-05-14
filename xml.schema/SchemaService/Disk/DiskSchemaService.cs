namespace xml.schema.SchemaService.Disk;

internal sealed class DiskSchemaService : ISchemaService
{
    private readonly Dictionary<string, string> _namespaces;
    public DiskSchemaService() => _namespaces = new Dictionary<string, string>
    {
        ["http://schemas.microsoft.com/powershell/help/2010/05"] = "HelpInfo",
    };

    public string GetSchema(string xmlNamespace)
    {
        if (!_namespaces.TryGetValue(xmlNamespace, out var filename))
        {
            return "Namespace not found";
        }
        var path = $@"resources\schemas\{filename}.xsd";
        var text = File.ReadAllText(path);
        return text;
    }
}