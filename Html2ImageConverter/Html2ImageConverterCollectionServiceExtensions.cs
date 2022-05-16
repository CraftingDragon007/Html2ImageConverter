using Html2ImageConverter.Converter;
using Html2ImageConverter.Converter.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
	/// <summary>
	/// Extension methods for setting up Html2ImageConverter related services in an <see cref="IServiceCollection" />.
	/// </summary>
	public static class Html2ImageConverterCollectionServiceExtensions
	{
		/// <summary>
		/// Adds the standard implementation of <see cref="IHtml2Image"/> to the <see cref="IServiceCollection" />.
		/// </summary>
		/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
		/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
		public static IServiceCollection Html2ImageConverter(this IServiceCollection services)
		{
			services.AddScoped<IHtml2Image, Html2Image>();
			return services;
		}
		
	}
}
