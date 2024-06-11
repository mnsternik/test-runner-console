namespace TestRunnerConsole
{
    public class ManualStep(GenericStep step) : Step(step.Name, step.ActionType, step.ElementXPath, step.ElementId, step.BackupScenarioPath)
    {
        public override void HandleAction()
        {
            UserInputUtility.AskForUserConfirmation(); 
        }
    }
}
