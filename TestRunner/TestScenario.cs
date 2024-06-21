namespace TestRunnerConsole
{
    public class TestScenario
    {
        public string Name {get; set; } = "";
        public GenericStep[] Steps { get; set; } = Array.Empty<GenericStep>(); 

        public static TestScenario LoadScenario(string path)
        {
            return JSONFileReader.Deserialize<TestScenario>(path);
        }
    }

    public class InvalidScenarioException : Exception
    {
        public InvalidScenarioException(string message) : base(message) { }
    }
}
