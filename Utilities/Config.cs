namespace TestRunnerConsole
{
    public class Config
    {
        public static string DriverPath { get; set; } = string.Empty;
        public static string FirefoxPath { get; set; } = string.Empty;
        public static string TestScenarioPath { get; set; } = string.Empty;
        public static string LogsFolderPath { get; set; } = string.Empty;
        public static int ElementWaitingTimeout { get; set; } = 0;
    }

    public class ConfigManager
    {
        public static void InitConfig()
        {
            string configPath = GetConfigPath();
            ConfigModel config = JSONFileReader.Deserialize<ConfigModel>(configPath);

            if (!File.Exists(config.DriverPath))
            {
                throw new FileNotFoundException($"Nie znaleziono pliku geckodriver.exe w lokalizacji {config.DriverPath}");
            }
            if (!File.Exists(config.FirefoxPath))
            {
                throw new FileNotFoundException($"Nie znaleziono pliku firefox.exe w lokalizacji {config.FirefoxPath}");
            }

            Config.DriverPath = config.DriverPath; 
            Config.FirefoxPath = config.FirefoxPath;
            Config.TestScenarioPath = config.TestScenarioPath;
            Config.LogsFolderPath = config.LogsFolderPath;  
            Config.ElementWaitingTimeout = config.ElementWaitingTimeout;
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

        private class ConfigModel
        {
            public required string DriverPath { get; set; }
            public required string FirefoxPath { get; set; }
            public required string TestScenarioPath { get; set; }
            public required string LogsFolderPath { get; set; }
            public int ElementWaitingTimeout { get; set; }
        }
    }
}

