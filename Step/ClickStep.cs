namespace TestRunnerConsole
{
    public class ClickStep(GenericStep step) : Step(step.Name, step.Action, step.ElementXPath, step.ElementId, step.BackupScenarioPath)
    {
        public override void HandleAction()
        {
            GetElement().Click();
        }
    }
}
