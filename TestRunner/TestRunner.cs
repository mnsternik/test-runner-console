using OpenQA.Selenium.Firefox;

namespace TestRunnerConsole
{
    public class TestRunner
    {
        private static List<Step> Steps { get; set; } = new List<Step>();
        public static FirefoxDriver? Driver { get; set; }
        private static int _stepCounter = 0;

        static TestRunner()
        {
            InitDriver();
        }

        public static void Run(TestScenario ts)
        {
            Steps.Clear();
            Steps = CreateListOfSteps(ts);

            Logger.Log($"Uruchamianie scenariusza testowego: {ts.Name}", true);
            foreach (var step in Steps)
            {
                step.ExecuteAndLog(_stepCounter);
                _stepCounter++;
            }
        }

        public static List<Step> CreateListOfSteps(TestScenario ts)
        {
            List<Step> steps = new List<Step>();
            foreach (var step in ts.Steps)
            {
                steps.Add(GenericStep.TransformIntoSpecifedStep(step));
            }
            return steps;
        }

        private static void InitDriver()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.BinaryLocation = Config.FirefoxPath;
            Driver = new FirefoxDriver(Config.DriverPath, options);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Config.ElementWaitingTimeout); // Ustawienie czekania na nieznalezione elementy
        }
    }
}
