using static QRCoder.PayloadGenerator;

namespace Interface
{
    public static class Ioc
    {
        public static IServiceCollection AddWinApp(this IServiceCollection services)
        {
            services.AddSingleton<Program>(); 
            return services;
        }
    }

}
