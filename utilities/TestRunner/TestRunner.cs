using OpenQA.Selenium.Firefox;

namespace TestRunnerConsole
{
    public class TestRunner
    {
        private static List<Step> Steps { get; set; } = new List<Step>();
        public static FirefoxDriver? Driver { get; set; }
        private static int StepCounter = 0;

        public static void Run(string testScenarioPath)
        {
            TestScenario ts = TestScenario.LoadScenario(testScenarioPath);

            Steps.Clear();
            Steps = CreateListOfSteps(ts);

            Logger.Log($"Uruchamianie scenariusza testowego: {ts.Name}", true);
            foreach (var step in Steps)
            {
                step.ExecuteAndLog(StepCounter);
                StepCounter++;
            }
        }

        public static List<Step> CreateListOfSteps(TestScenario ts)
        {
            List<Step> steps = new List<Step>();
            if (ts.Steps == null)
            {
                throw new InvalidScenarioException("Scenariusz testowy nie zawiera żadnych kroków testowych.");
            }
            foreach (var step in ts.Steps)
            {
                steps.Add(TransformGenericIntoSpecifedStep(step));
            }
            return steps;
        }

        private static Step TransformGenericIntoSpecifedStep(GenericStep step)
        {
            return step.ActionType switch
            {
                "navigate" => new NavigateStep(step),
                "click" => new ClickStep(step),
                "select" => new SelectStep(step),
                "verify" => new VerifyStep(step),
                "iframe-change" => new ChangeContextStep(step),
                "manual" => new ManualStep(step),
                "write" or "write-login" or "write-password" => new WriteStep(step),
                _ => throw new Exception($"Nieprawidłowy rodzaj akcji ActionType: '{step.ActionType}'"),
            };
        }

        public static void InitDriverWithOptions(Config config)
        {
            FirefoxOptions options = new FirefoxOptions();
            options.BinaryLocation = config.FirefoxPath;
            Driver = new FirefoxDriver(config.DriverPath, options);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(config.ElementWaitingTimeout); // Ustawienie czekania na nieznalezione elementy
        }
    }
}
