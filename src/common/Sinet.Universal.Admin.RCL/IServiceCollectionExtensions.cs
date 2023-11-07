namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddSinetUniversalAdminBlazor(this IServiceCollection services)
        {
            services.AddMasaBlazor(builder =>
             {
                 builder.ConfigureTheme(theme =>
                 {
                     theme.Themes.Light.Primary = "#4318FF";
                     theme.Themes.Light.Accent = "#4318FF";
                 });
             }).AddI18nForServer("wwwroot/i18n");

            return services;
        }
    }
}
