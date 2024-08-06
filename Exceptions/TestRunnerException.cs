namespace TestRunnerConsole
{
    public class TestRunnerException : Exception
    {
        public TestRunnerException(string message) : base(message) { }
        public TestRunnerException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class InavlidVerificationException : TestRunnerException
    {
        private const string Prefix = "Błąd weryfikacji: ";
        public InavlidVerificationException(string message) : base(Prefix + message) { }
        public InavlidVerificationException(string message, Exception innerException) : base(Prefix + message, innerException) { }
    }

    public class FailedStepException : TestRunnerException
    {
        private const string Prefix = "Wystąpił błąd w kroku: ";
        public FailedStepException(string message) : base(Prefix + message) { }
        public FailedStepException(string message, Exception innerException) : base(Prefix + message, innerException) { }
    }


}
