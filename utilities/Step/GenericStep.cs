namespace TestRunnerConsole
{
    public class GenericStep
    {
        public string ActionType { get; set; } = "";
        public string Name { get; set; } = "";
        public string? Url { get; set; } = "";
        public string? ElementId { get; set; } = "";
        public string? ElementXPath { get; set; } = "";
        public string? ContextId { get; set; } = ""; 
        public string? Value { get; set; } = "";
        public string? ExpectedValue { get; set; } = "";
        public string? OptionType { get; set; } = ""; 
        public string? CheckType { get; set; } = "";
        public bool? CheckInsideSelect { get; set; } = false;
        public string? BackupScenarioPath { get; set; } = ""; 
    }
}

