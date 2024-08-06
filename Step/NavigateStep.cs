namespace TestRunnerConsole
{
    public class NavigateStep(GenericStep step) : Step(step.Name, step.Action, step.ElementXPath, step.ElementId, step.BackupScenarioPath)
    {
        public string Url { get; set; } = step.Url ?? string.Empty;

        public override void HandleAction()
        {
            TestRunner.Driver?.Navigate().GoToUrl(Url);
        }
    }
}
