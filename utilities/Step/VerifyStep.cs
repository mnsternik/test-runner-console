using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestRunnerConsole
{
    public class VerifyStep(GenericStep step) : Step(step.Name, step.ActionType, step.ElementXPath, step.ElementId, step.BackupScenarioPath)
    {
        public string CheckType { get; set; } = step.CheckType ?? "";
        public string ExpectedValue { get; set; } = step.ExpectedValue ?? "";
        public bool CheckInsideSelect { get; set; } = step.CheckInsideSelect ?? false;
        private IWebElement? Element { get; set; }

        public override void HandleAction()
        {
            Element = CheckInsideSelect ? GetOptionInsideSelectElement() : GetElement();
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
            if (Element != null && Element.Displayed)
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
                Element?.Text != ExpectedValue
                && !string.IsNullOrEmpty(Element?.Text)
            );

            if (isTextValid)
            {
                Logger.Log($"Sukces: Oczekiwano tekstu innego niż '{ExpectedValue}', znaleziono tekst '{Element?.Text}'");
            }
            else
            {
                throw new InavlidVerificationException($"Oczekiwano tekstu innego niż '{ExpectedValue}', znaleziono tekst '{Element?.Text}'");
            }
        }

        private void AssertTextIs()
        {
            bool isTextEqualToExpected = WaitForAndCheckCondtition(driver => Element?.Text == ExpectedValue);
            if (isTextEqualToExpected)
            {
                Logger.Log($"Sukces: Oczekiwano tekstu '{ExpectedValue}', znaleziono tekst '{Element?.Text}'");
            }
            else
            {
                throw new InavlidVerificationException($"Oczekiwano tekstu '{ExpectedValue}', znaleziono tekst '{Element?.Text}'");
            }
        }

        private void AssertValueIsNot()
        {
            bool isValueValid = WaitForAndCheckCondtition(driver =>
                Element?.GetAttribute("value") != ExpectedValue
                && !string.IsNullOrEmpty(Element?.GetAttribute("Value"))
            );

            if (isValueValid)
            {
                Logger.Log($"Sukces: Oczekiwano wartości innej niż '{ExpectedValue}', znaleziono wartość '{Element?.GetAttribute("value")}'");
            }
            else
            {
                throw new InavlidVerificationException($"Oczekiwano wartości innej niż '{ExpectedValue}', znaleziono wartość '{Element?.GetAttribute("value")}'");
            }
        }

        private void AssertValueIs()
        {
            bool isValueEqualToExpected = WaitForAndCheckCondtition(driver => Element?.GetAttribute("value") == ExpectedValue);
            if (isValueEqualToExpected)
            {
                Logger.Log($"Sukces: Oczekiwano wartości '{ExpectedValue}', znaleziono wartość '{Element?.GetAttribute("value")}'");
            }
            else
            {
                throw new InavlidVerificationException($"Oczekiwano wartości '{ExpectedValue}', znaleziono wartość '{Element?.GetAttribute("value")}'");
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