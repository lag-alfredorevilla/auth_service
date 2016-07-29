using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Hosting
{
    /// <summary>
    /// Extension methods for setting up MVC services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class MvcServiceCollectionExtensions
    {
        
        /// <summary>
        /// Adds MVC services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>An <see cref="IMvcBuilder"/> that can be used to further configure the MVC services.</returns>
        public static IMvcBuilder AddLeanMvc(this IServiceCollection services)
        {
            var builder = services.AddMvcCore();

            builder.AddJsonFormatters();

            var partManager = builder.PartManager;
            var mvcTagHelpersAssembly = typeof(InputTagHelper).GetTypeInfo().Assembly;
            if(!partManager.ApplicationParts.OfType<AssemblyPart>().Any(p => p.Assembly == mvcTagHelpersAssembly))
            {
                partManager.ApplicationParts.Add(new AssemblyPart(mvcTagHelpersAssembly));
            }

            return new MvcBuilder(builder.Services, builder.PartManager);
        }
    }
}

