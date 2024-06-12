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

        public void ExecuteAndLog()
        {
            try
            {
                Logger.Log($"Wykonywanie kroku: {Name}");
                HandleAction();
                Logger.Log($"{Name} -> OK", true);
            }
            catch (InavlidVerificationException e)
            {
                Logger.Log($"Wystąpił błąd podczas weryfikacji. {e.Message}", true);
                HandleFailure();
            }
            catch (NoSuchElementException e)
            {
                Logger.Log($"Wystąpił błąd - nie znaleziono elementu na stronie. {e.Message}", true);
                HandleFailure();
            }
            catch (Exception e)
            {
                Logger.Log($"Wystąpił nieoczekiwany błąd: {e.Message}", true);
                HandleFailure();
            }
        }


        // Każdy klasa pochodna posiada własną implementacje metody HandleAction
        public virtual void HandleAction() {}

        public void HandleFailure()
        {
            if (!string.IsNullOrEmpty(BackupScenarioPath))
            {
                TestRunner.Run(BackupScenarioPath);
            }
            else
            {
                string decision = GetFailureDecisionFromUser();
                if (decision == "N")
                {
                    throw new Exception("Wykonywanie scenariusza przerwane. Koniec działania programu.");
                }
                else if (decision == "R")
                {
                    ExecuteAndLog(); 
                }
            }
        }

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

        private string GetFailureDecisionFromUser()
        {
            Logger.Log("Wstrzymano wykonywanie scenariusza testowego. Wpisz 'Y' by przejść do kolejnego kroku, 'R' by spróbować wykonać krok ponownie, lub 'N' by zakończyć:';");
            string? input = Console.ReadLine()?.Trim().ToUpper();

            while (true)
            {
                if (input == "Y" || input == "N" || input == "R")
                {
                    Logger.Log($"Udzielono odpowiedzi: {input}", true);
                    return input;
                }
                Logger.Log("Wprowadzono niepoprawną odpowiedź - wprowadź 'Y' by kontynuować lub 'N' by przerwać: ");
                input = Console.ReadLine()?.Trim().ToUpper();
            }
        }
    }

    public class InavlidStepParameterException : Exception
    {
        public InavlidStepParameterException(string message) : base(message) { }
    }
}