namespace TestRunnerConsole
{
    public class Logger
    {
        public static string LogsPath { get; set; } = string.Empty;

        public static void InitLogger()
        {
            string fileName = DateTime.Now.ToString("ddMMyyyy") + ".txt";
            LogsPath = Path.Combine(Config.LogsFolderPath, fileName);

            Directory.CreateDirectory(Config.LogsFolderPath); 

            string text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + ": " + "Rozpoczędzie działania programu";
            using (StreamWriter outputFile = new StreamWriter(LogsPath))
            {
                outputFile.WriteLine(text);
                Console.WriteLine(text);
                outputFile.WriteLine();
                Console.WriteLine();
            }
        }

        public static void Log(string message, Boolean addSpaceBetweenLines = false)
        {
            string text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + ": " + message;
            using (StreamWriter outputFile = new StreamWriter(LogsPath, true))
            {
                outputFile.WriteLine(text);
                Console.WriteLine(text);
                if (addSpaceBetweenLines)
                {
                    outputFile.WriteLine();
                    Console.WriteLine();
                }
            }
        }
    }
}

