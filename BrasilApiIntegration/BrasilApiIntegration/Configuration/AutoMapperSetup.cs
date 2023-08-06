using BrasilApiIntegration.AutoMapper;


namespace BrasilApiIntegration.Configuration
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.Contains("BrasilApiIntegration")).ToArray();

            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(assemblies);
            });

            AutoMapperConfig.RegisterMappings();
        }
    }
}
