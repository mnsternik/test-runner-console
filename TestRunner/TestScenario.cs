namespace TestRunnerConsole
{
    public class TestScenario
    {
        public string Name { get; set; } = string.Empty;
        public GenericStep[] Steps { get; set; } = Array.Empty<GenericStep>();

        public static TestScenario LoadScenario(string path)
        {
            TestScenario ts = JSONFileReader.Deserialize<TestScenario>(path);
            ValidateScenario(ts); 
            return ts;
        }

        private static void ValidateScenario(TestScenario ts)
        {
            if (ts == null)
            {
                throw new InvalidScenarioException("Scenariusz testowy jest pusty");
            }
            else if (ts.Steps == null || ts.Steps.Length == 0)
            {
                throw new InvalidScenarioException("Scenariusz testowy nie zawiera żadnych kroków testowych");
            }
            else if (string.IsNullOrEmpty(ts.Name))
            {
                throw new InvalidScenarioException("Scenariusz testowy nie posiada nazwy, należy dospiać wartość w atrybucie 'name' w pliku scenariuszowym");
            }
        }
    }
}
