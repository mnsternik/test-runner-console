namespace FirstConsoleApp
{
    public class ClickStep(GenericStep step): Step(step.Name, step.ActionType, step.ElementXPath, step.ElementId, step.BackupScenarioPath)
    {
        public override void HandleAction()
        {
            GetElement().Click();
        }
    }
}
