using Broxel.Logger.Web.Extensions;

try
{
    var builder = WebApplication.CreateBuilder(args);

    //Agregar esta línea
    builder.AddBroxelLoggerWeb("GoogleCloudLogging", "GoogleCloudLoggingAudit", "GoogleCloudLoggingApp");

    //Add services to the container.
    builder.Services.AddHttpClient();

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }


    app.UseAuthorization();

    app.MapControllers();

    app.Run();


}
catch (Exception)
{

	throw;
}

