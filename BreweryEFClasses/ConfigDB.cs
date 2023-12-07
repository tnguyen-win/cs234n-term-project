using Microsoft.Extensions.Configuration;

namespace BreweryEFClasses {
    public class ConfigDB {
        public static string GetMySqlConnectionString() {
            string folder = AppContext.BaseDirectory;
            var builder = new ConfigurationBuilder()
                    .SetBasePath(folder)
                    .AddJsonFile("mySqlSettings.json", optional: true, reloadOnChange: true);
            string connectionString = builder.Build().GetConnectionString("mySql");

            return connectionString;
        }
    }
}
