using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using xml.schema.Services;

namespace xml.schema.Pages;

public class IndexModel : PageModel
{
    private readonly ISchemaService _schemaService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ISchemaService schemaService,  ILogger<IndexModel> logger)
    {
        _schemaService = schemaService ?? throw new ArgumentNullException(nameof(schemaService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [BindProperty]
    public string? Schema { get; set; }
    
    public string? Result { get; set; }
        
    public void OnPost()
    {
        if (Schema is not null)
        {
            Result = _schemaService.GetSchema(Schema);
        }
    }

    public void OnGet()
    {
    }
}