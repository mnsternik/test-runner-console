using OpenQA.Selenium;

namespace FirstConsoleApp
{
    public class WriteStep(GenericStep step) : Step(step.Name, step.ActionType, step.ElementXPath, step.ElementId, step.BackupScenarioPath)
    {
        public string Value { get; set; } = step.Value ?? "";

        public override void HandleAction()
        {
            IWebElement element = GetElement();
            string targetValue;

            switch (ActionType)
            {
                case "write-password":
                    targetValue = UserCredentialUtility.ConvertSecureStringToString(UserCredentialUtility.ReadPassword());
                    break;
                case "write-login":
                    targetValue = UserCredentialUtility.ReadLogin();
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