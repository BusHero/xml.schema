using Microsoft.EntityFrameworkCore;
using xml.schema.Data;
using xml.schema.Services;
using xml.schema.Services.Impl.sql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddScoped<ISchemaService, SqlSchemaService>();
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