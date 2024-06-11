using OpenQA.Selenium;

namespace TestRunnerConsole
{
    public class ChangeContextStep(GenericStep step) : Step(step.Name, step.ActionType, step.ElementXPath, step.ElementId, step.BackupScenarioPath)
    {
        public string ContextId { get; set; } = step.ContextId ?? "";

        public override void HandleAction()
        {
            HandleChangeContext();
        }

        private void HandleChangeContext()
        {
            if (ContextId == "default")
            {
                TestRunner.Driver?.SwitchTo().DefaultContent();
            }
            else
            {
                var iframe = TestRunner.Driver?.FindElement(By.Id(ContextId));
                TestRunner.Driver?.SwitchTo().Frame(iframe);
            }
        }
    }
}


