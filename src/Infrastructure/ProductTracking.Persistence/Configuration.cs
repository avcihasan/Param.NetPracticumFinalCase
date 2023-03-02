using Microsoft.Extensions.Configuration;

namespace ProductTracking.Persistence
{
    static class Configuration
    {
        public static string ConnectionString
        {
            get
            {


                Microsoft.Extensions.Configuration.ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/ProductTracking.API"));
                configurationManager.AddJsonFile("appsettings.json");


                return configurationManager.GetConnectionString("SqlServer");

            }
        }
    }
}
