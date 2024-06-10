using OpenQA.Selenium;

namespace TestRunnerConsole
{
    public class Step(string name, string actionType, string? elementXPath, string? elementId, string? backupScenarioPath)
    {
        public string Name { get; set; } = name ?? "";
        public string ActionType { get; set; } = actionType ?? "";
        public string? ElementXPath { get; set; } = elementXPath;
        public string? ElementId { get; set; } = elementId;
        public string? BackupScenarioPath { get; set; } = backupScenarioPath;

        public virtual void HandleAction() { }
        public virtual IWebElement GetElement()
        {
            if (TestRunner.Driver == null)
            {
                throw new Exception("Driver nie został poprawnie zaninicjowany.");
            }

            if (!string.IsNullOrEmpty(ElementXPath))
            {
                return TestRunner.Driver.FindElement(By.XPath(ElementXPath));
            }
            else if (!string.IsNullOrEmpty(ElementId))
            {
                return TestRunner.Driver.FindElement(By.Id(ElementId));
            }
            else
            {
                throw new InavlidStepParameterException($"Wykryto próbe odnalezienia elementu bez podania jego ID lub XPath - potrzebna weryfikacja poprawności scenariusza testowego.");
            }
        }

        public void ExectueAndLog()
        {
            try
            {
                Logger.Log($"Wykonywanie kroku: {Name}");
                HandleAction();
                Logger.Log($"{Name} -> OK");
            }
            catch (NoSuchElementException e)
            {
                Logger.Log($"Wystąpił błąd - nie znaleziono elementu na stronie. {e}");
                HandleFailure();
            }
            catch (Exception e)
            {
                Logger.Log($"Wystąpił nieoczekiwany błąd: {e}");
                HandleFailure();
            }

        }

        public void HandleFailure()
        {
            if (!string.IsNullOrEmpty(BackupScenarioPath))
            {
                TestRunner.Run(BackupScenarioPath);
            }
            else
            {
                // bool shouldContiniue = ManualStep.AskForUserConfirmation(); // or should retry also
                string? shouldContiniue;
                Console.WriteLine("Wystąpił błąd - wpisz 'Y' by przejść do następnego kroku, lub 'N' by zakończyć działanie programu");
                shouldContiniue = Console.ReadLine();

                if (shouldContiniue?.ToLower() != "y")
                {
                    throw new Exception("Wykonywanie scenariusza przerwane.");
                }
            }
        }
    }

    public class InavlidStepParameterException : Exception
    {
        public InavlidStepParameterException(string message) : base(message) { }
    }
}