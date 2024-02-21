using Microsoft.Extensions.Configuration;

namespace Gate.backend.Utils.Settings
{
    public static class AppSettingsAcessor 
    {
        public static string GetAPIUrl(string appSettingsParameter) 
        {
            var settings = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
            return settings.GetSection("ApiUrls").GetSection(appSettingsParameter).Value;
        }

        public static string GetConnectionString() 
        {
            IConfigurationRoot settings;
            settings = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
            return settings.GetConnectionString("gate");
        }

        public static string GetGlobalAPIKey() 
        {
            IConfigurationRoot settings;
            settings = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
            return settings.GetSection("ApiKeys").GetSection("global").Value;
        }

        public static string GetApplicationMD5() 
        {
            IConfigurationRoot settings;
            settings = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
            return settings.GetSection("ApplicationKey").GetSection("md5").Value;
        }
    }
}
