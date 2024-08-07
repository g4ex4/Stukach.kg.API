using Dal;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddConfiguration(builder.Configuration)
    .AddUnitOfWork(builder.Configuration)
    .AddServices()
    .AddAutoMapper()
    .AddCorsPolicy();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(uiBind =>
        uiBind.SwaggerEndpoint
            ("/swagger/v1/swagger.json", "Stukach.kg Api")
    );
}
app.UseHsts();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.UseCors("CorsPolicy");
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

using var scope = app.Services.CreateScope();
SeedData.Initialize(scope.ServiceProvider);

app.Run();