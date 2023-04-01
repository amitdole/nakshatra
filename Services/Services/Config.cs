//using Microsoft.Extensions.Configuration;

//namespace Services
//{
//    public static class Config
//    {
//        private static IConfiguration configuration;
//        static Config()
//        {
//            var builder = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())
//                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
//            configuration = builder.Build();
//        }

//        public static string GetValue(string name)
//        {
//            string appSettings = configuration[name];
//            return appSettings;
//        }
//    }
//}
