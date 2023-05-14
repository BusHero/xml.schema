using xml.schema.Data;

namespace xml.schema.SchemaService.Sql;

internal sealed class SqlSchemaService: ISchemaService
{
    private readonly SchemaContext _context;

    public SqlSchemaService(SchemaContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public Result<string> GetSchema(string xmlNamespace)
    {
        var result = (
            from schema in _context.Schemas
            where schema.Namespace == xmlNamespace
            select schema.Xsd
        ).FirstOrDefault();
        
        return result switch
        {
            null => Result<string>.CreateFailure("Not found"),
            _ => Result<string>.CreateSuccess(result),
        };
    }
}