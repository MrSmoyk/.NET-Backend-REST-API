using DAL.Interfases;
using DAL.Repositories;
using Service.Implementations;
using Service.Interfaces;

namespace WebAPI
{
    public static class Initialiser
    {
        public static void InitialiseRepositories(this IServiceCollection services)
        {
            services.AddScoped<IOperationRepository, OperationRepository>();
            services.AddScoped<IOperationTypeRepository, OperationTypeRepository>();
        }

        public static void InitialiseServices(this IServiceCollection services)
        {
            services.AddScoped<IOperationService, OperationService>();
            services.AddScoped<IOperationTypeService, OperationTypeService>();
        }
    }
}
