using xml.schema.Data;

namespace xml.schema.Services.Impl.sql;

internal class SqlSchemaService: ISchemaService
{
    private readonly SchemaContext _context;

    public SqlSchemaService(SchemaContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public string? GetSchema(string xmlNamespace) =>
    (
        from schema in _context.Schemas
        where schema.Namespace == xmlNamespace
        select schema.Xsd
    ).FirstOrDefault();
}