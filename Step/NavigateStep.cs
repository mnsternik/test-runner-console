namespace TestRunnerConsole
{
    public class NavigateStep(GenericStep step) : Step(step.Name, step.ActionType, step.ElementXPath, step.ElementId, step.BackupScenarioPath)
    {
        public string Url { get; set; } = step.Url ?? string.Empty;

        public override void HandleAction()
        {
            TestRunner.Driver?.Navigate().GoToUrl(Url);
        }
    }
}
