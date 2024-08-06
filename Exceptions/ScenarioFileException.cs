namespace TestRunnerConsole
{
    public class ScenarioFileException : Exception
    {
        public ScenarioFileException(string message) : base(message) { }
        public ScenarioFileException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class InvalidStepParameterException : ScenarioFileException
    {
        private const string Prefix = "Błąd w parametrze kroku testowego: ";
        public InvalidStepParameterException(string message) : base(Prefix + message) { }
        public InvalidStepParameterException(string message, Exception innerException) : base(Prefix + message, innerException) { }
    }

    public class InvalidScenarioException : Exception
    {
        private const string Prefix = "Błąd scenariusza testowego: ";
        public InvalidScenarioException(string message) : base(message) { }
        public InvalidScenarioException(string message, Exception innerException) : base(Prefix + message, innerException) { }
    }
}

