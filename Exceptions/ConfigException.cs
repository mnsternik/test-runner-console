namespace TestRunnerConsole
{
    public class ConfigException : Exception
    {
        public ConfigException(string message) : base(message) { }
        public ConfigException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class FileNotFoundException : ConfigException
    {
        private const string Prefix = "Nie znaleziono pliku: ";
        public FileNotFoundException(string message) : base(Prefix + message) { }
        public FileNotFoundException(string message, Exception innerException) : base(Prefix + message, innerException) { }
    }

    public class DriverInitException : ConfigException
    {
        private const string Prefix = "Błąd drivera: ";
        public DriverInitException(string message) : base(Prefix + message) { }
        public DriverInitException(string message, Exception innerException) : base(Prefix + message, innerException) { }
    }


}