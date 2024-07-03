namespace TestRunnerConsole
{
    public class GenericStep
    {
        public string Action { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Url { get; set; } = string.Empty;
        public string? ElementId { get; set; } = string.Empty;
        public string? ElementXPath { get; set; } = string.Empty;
        public string? ContextId { get; set; } = string.Empty;
        public string? Value { get; set; } = string.Empty;
        public string? ExpectedValue { get; set; } = string.Empty;
        public string? OptionType { get; set; } = string.Empty;
        public string? CheckType { get; set; } = string.Empty;
        public bool? CheckInsideSelect { get; set; } = false;
        public string? BackupScenarioPath { get; set; } = string.Empty;

        public static Step TransformIntoSpecifedStep(GenericStep step)
        {
            return step.Action switch
            {
                "navigate" => new NavigateStep(step),
                "click" => new ClickStep(step),
                "select" => new SelectStep(step),
                "verify" => new VerifyStep(step),
                "iframe-change" => new ChangeContextStep(step),
                "manual" => new ManualStep(step),
                "write" or "write-login" or "write-password" => new WriteStep(step),
                _ => throw new InavlidStepParameterException($"Nieprawidłowy rodzaj akcji ActionType: '{step.Action}'"),
            };
        }
    }
}

