using DAL.DBContext;
using DAL.Implementation;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IoC
{
    /// <summary>
    /// Clase estática que centraliza la configuración de la inyección de dependencias del sistema.
    /// </summary>
    /// <remarks>
    /// Proporciona un método de extensión para <see cref="IServiceCollection"/> que registra:
    /// <list type="bullet">
    /// <item>El contexto de base de datos (<see cref="AssistivaContext"/>)</item>
    /// <item>Los repositorios genéricos (<see cref="IGenericRepository{T}"/> y <see cref="GenericRepository{T}"/>)</item>
    /// </list>
    /// </remarks>
    public static class Dependency
    {
        /// <summary>
        /// Configura las dependencias principales de la aplicación.
        /// </summary>
        /// <param name="services">Colección de servicios para registrar las dependencias</param>
        /// <param name="configuration">Configuración de la aplicación para obtener connection strings</param>
        /// <example>
        /// Uso en Program.cs:
        /// <code>
        /// var builder = WebApplication.CreateBuilder(args);
        /// builder.Services.DependencyInjection(builder.Configuration);
        /// </code>
        /// </example>
        /// <remarks>
        /// <para><strong>Registros realizados:</strong></para>
        /// <list type="number">
        /// <item>
        /// <term>DbContext:</term>
        /// <description>Configura Entity Framework Core con SQL Server usando el connection string "SQLString"</description>
        /// </item>
        /// <item>
        /// <term>Repositorio Genérico:</term>
        /// <description>Registra la implementación genérica del repositorio con ciclo de vida Transient</description>
        /// </item>
        /// </list>
        /// </remarks>
        public static void DependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AssistivaContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SQLString"));
            });

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
