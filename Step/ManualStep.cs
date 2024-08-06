namespace TestRunnerConsole
{
    public class ManualStep(GenericStep step) : Step(step.Name, step.Action, step.ElementXPath, step.ElementId, step.BackupScenarioPath)
    {
        public override void HandleAction()
        {
            bool shouldContiniue = UserInputUtility.GetConfirmationFromUser(); 
            if (!shouldContiniue)
            {
                throw new FailedStepException("Stwierdzono błąd podczas kroku wykonywanego ręcznie.");
            }
        }
    }
}
