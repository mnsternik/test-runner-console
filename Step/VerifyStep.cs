using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestRunnerConsole
{
    public class VerifyStep(GenericStep step) : Step(step.Name, step.ActionType, step.ElementXPath, step.ElementId, step.BackupScenarioPath)
    {
        public string CheckType { get; set; } = step.CheckType ?? string.Empty;
        public string ExpectedValue { get; set; } = step.ExpectedValue ?? string.Empty;
        public bool CheckInsideSelect { get; set; } = step.CheckInsideSelect ?? false;
        private IWebElement? _element; 

        public override void HandleAction()
        {
            _element = CheckInsideSelect ? GetOptionInsideSelectElement() : GetElement();
            VerifyBasedOnCheckType();
        }

        public void VerifyBasedOnCheckType()
        {
            switch (CheckType)
            {
                case "is-displayed":
                    CheckIfElementIsDisplayed();
                    break;
                case "text":
                    AssertTextIs();
                    break;
                case "text-is-not":
                    AssertTextIsNot();
                    break;
                case "value":
                    AssertValueIs();
                    break;
                case "value-is-not":
                    AssertValueIsNot();
                    break;
                default:
                    throw new InavlidStepParameterException($"Niepoprawna wartość paramteru CheckType: '{CheckType}'");
            }
        }

        private void CheckIfElementIsDisplayed()
        {
            if (_element != null && _element.Displayed)
            {
                Logger.Log($"Znaleziono element [ID: {ElementId}]");
            }
            else
            {
                throw new InavlidVerificationException($"Nie znaleziono elemenetu z ID {ElementId}");
            }
        }

        private void AssertTextIsNot()
        {
            bool isTextValid = WaitForAndCheckCondtition(driver =>
                _element?.Text != ExpectedValue
                && !string.IsNullOrEmpty(_element?.Text)
            );

            if (isTextValid)
            {
                Logger.Log($"Sukces: Oczekiwano tekstu innego niż '{ExpectedValue}', znaleziono tekst '{_element?.Text}'");
            }
            else
            {
                throw new InavlidVerificationException($"Oczekiwano tekstu innego niż '{ExpectedValue}', znaleziono tekst '{_element?.Text}'");
            }
        }

        private void AssertTextIs()
        {
            bool isTextEqualToExpected = WaitForAndCheckCondtition(driver => _element?.Text == ExpectedValue);
            if (isTextEqualToExpected)
            {
                Logger.Log($"Sukces: Oczekiwano tekstu '{ExpectedValue}', znaleziono tekst '{_element?.Text}'");
            }
            else
            {
                throw new InavlidVerificationException($"Oczekiwano tekstu '{ExpectedValue}', znaleziono tekst '{_element?.Text}'");
            }
        }

        private void AssertValueIsNot()
        {
            bool isValueValid = WaitForAndCheckCondtition(driver =>
                _element?.GetAttribute("value") != ExpectedValue
                && !string.IsNullOrEmpty(_element?.GetAttribute("Value"))
            );

            if (isValueValid)
            {
                Logger.Log($"Sukces: Oczekiwano wartości innej niż '{ExpectedValue}', znaleziono wartość '{_element?.GetAttribute("value")}'");
            }
            else
            {
                throw new InavlidVerificationException($"Oczekiwano wartości innej niż '{ExpectedValue}', znaleziono wartość '{_element?.GetAttribute("value")}'");
            }
        }

        private void AssertValueIs()
        {
            bool isValueEqualToExpected = WaitForAndCheckCondtition(driver => _element?.GetAttribute("value") == ExpectedValue);
            if (isValueEqualToExpected)
            {
                Logger.Log($"Sukces: Oczekiwano wartości '{ExpectedValue}', znaleziono wartość '{_element?.GetAttribute("value")}'");
            }
            else
            {
                throw new InavlidVerificationException($"Oczekiwano wartości '{ExpectedValue}', znaleziono wartość '{_element?.GetAttribute("value")}'");
            }
        }

        private static bool WaitForAndCheckCondtition(Func<IWebDriver, bool> condition)
        {
            try
            {
                WebDriverWait wait = new(TestRunner.Driver, TimeSpan.FromSeconds(5));
                wait.Until(condition);
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
            return true;
        }

        private IWebElement? GetOptionInsideSelectElement()
        {
            return GetElement().FindElements(By.TagName("option")).FirstOrDefault(option => option.Selected);
        }

    }

    public class InavlidVerificationException : Exception
    {
        public InavlidVerificationException(string message) : base(message) { }
    }
}