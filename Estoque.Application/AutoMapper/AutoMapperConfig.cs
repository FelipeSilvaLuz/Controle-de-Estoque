using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Estoque.Application.AutoMapper
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            services.AddAutoMapper();
        }
    }
}
