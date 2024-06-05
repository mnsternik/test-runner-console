namespace FirstConsoleApp
{
    public class Config
    {
        public string DriverPath { get; set; } = "";
        public string FirefoxPath { get; set; } = "";
        public string TestScenarioPath { get; set; } = "";
        public string LogsFolderPath { get; set; } = "";
        public int ElementWaitingTimeout { get; set; } = 0;
    }

    public class ConfigManager
    {
        public static Config GetConfig()
        {
            string configPath = GetConfigPath();
            Config config = JSONFileReader.Deserialize<Config>(configPath);

            if (!File.Exists(config.DriverPath))
            {
                throw new Exception($"Nie znaleziono pliku geckodriver.exe w lokalizacji {config.DriverPath}");
            }
            if (!File.Exists(config.FirefoxPath))
            {
                throw new Exception($"Nie znaleziono pliku firefox.exe w lokalizacji {config.FirefoxPath}");
            }

            return config;
        }

        private static string GetConfigPath()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string configPath = Path.Combine(baseDir, "config.json");

            while (!File.Exists(configPath) && Directory.GetParent(baseDir) != null)
            {
                baseDir = Directory.GetParent(baseDir)?.FullName ?? throw new Exception("Nie znaleziono pliku config.js");
                configPath = Path.Combine(baseDir, "config.json");
            }

            return configPath;
        }
    }
}
