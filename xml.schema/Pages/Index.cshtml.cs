using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xml.schema.SchemaService;

namespace xml.schema.Pages;

public sealed class IndexModel : PageModel
{
    private readonly ISchemaService _schemaService;

    public IndexModel(ISchemaService schemaService)
    {
        _schemaService = schemaService ?? throw new ArgumentNullException(nameof(schemaService));
    }

    [BindProperty]
    public string? Schema { get; set; }
    
    public string? Result { get; set; }
        
    public void OnPost()
    {
        if (Schema is null)
        {
            return;
        }
        var result = _schemaService.GetSchema(Schema);
        Result = result is Result<string>.Success { Value: var value } ? value : "Not found";
    }
}