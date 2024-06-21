namespace TestRunnerConsole
{
    public class ManualStep(GenericStep step) : Step(step.Name, step.ActionType, step.ElementXPath, step.ElementId, step.BackupScenarioPath)
    {
        public override void HandleAction()
        {
            bool shouldContiniue = UserInputUtility.GetConfirmationFromUser(); 
            if (!shouldContiniue)
            {
                throw new FailedStepException("Anulowano dalsze wykonywanie scenariusza testowego.");
            }
        }
    }
}
