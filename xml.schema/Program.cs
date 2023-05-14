using Microsoft.EntityFrameworkCore;
using xml.schema.Data;
using xml.schema.SchemaService;
using xml.schema.SchemaService.Disk;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddScoped<ISchemaService, DiskSchemaService>();
builder.Services.AddDbContext<SchemaContext>(options => options
    .UseSqlServer(builder
        .Configuration
        .GetConnectionString("SchemaSqlServer") ?? throw new InvalidOperationException($"Connection string not found")));

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();