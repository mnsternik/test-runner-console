using OpenQA.Selenium.Support.UI;

namespace FirstConsoleApp
{
    public class SelectStep(GenericStep step) : Step(step.Name, step.ActionType, step.ElementXPath, step.ElementId, step.BackupScenarioPath)
    {
        public string OptionType { get; set; } = step.OptionType ?? "";
        public string Value { get; set; } = step.Value ?? "";

        public override void HandleAction()
        {
            var selectElement = new SelectElement(GetElement());

            switch (OptionType)
            {
                case "value":
                    selectElement.SelectByValue(Value);
                    break;
                case "text":
                    selectElement.SelectByText(Value);
                    break;
                case "index":
                    selectElement.SelectByIndex(Convert.ToInt32(Value)); 
                    break; 
                default:
                    throw new InavlidStepParameterException($"Wskazano niepoprawny paramter 'OptionType': '{OptionType}', dostępne opcje to 'value', 'text', 'index'");
            }
        }
    }
}
