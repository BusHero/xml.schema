namespace xml.schema.Services.Impl;

public class InMemorySchemaService : ISchemaService
{
    public string GetSchema(string xmlNamespace)
    {
        return $@"
<html>
    {xmlNamespace}
</html>
";
    }
}