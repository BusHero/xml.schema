namespace xml.schema.SchemaService;

public interface ISchemaService
{
    Result<string> GetSchema(string xmlNamespace);
}