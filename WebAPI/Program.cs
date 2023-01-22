using DAL;
using Service.AutoMapperProfiles;
using WebAPI;
using WebAPI.Middleware;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<ApplicationDbContext>();
        builder.Services.InitialiseRepositories();
        builder.Services.InitialiseServices();
        builder.Services.AddControllers();
        builder.Services.AddAutoMapper(typeof(Profiles));
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

        app.Run();
    }
}