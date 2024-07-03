using OpenQA.Selenium;

namespace TestRunnerConsole
{
    public class WriteStep(GenericStep step) : Step(step.Name, step.Action, step.ElementXPath, step.ElementId, step.BackupScenarioPath)
    {
        public string Value { get; set; } = step.Value ?? string.Empty;

        public override void HandleAction()
        {
            HandleWrite();
        }

        private void HandleWrite()
        {
            IWebElement element = GetElement();
            string targetValue;

            switch (ActionType)
            {
                case "write-password":
                    targetValue = UserInputUtility.ConvertSecureStringToString(UserInputUtility.ReadPassword());
                    break;
                case "write-login":
                    targetValue = UserInputUtility.ReadLogin();
                    break;
                default:
                    targetValue = Value;
                    break;
            }

            element.Clear();
            element.SendKeys(targetValue);
        }
    }
}