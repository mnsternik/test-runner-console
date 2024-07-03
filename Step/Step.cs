using OpenQA.Selenium;

namespace TestRunnerConsole
{
    public class Step(string name, string actionType, string? elementXPath, string? elementId, string? backupScenarioPath)
    {
        public string Name { get; set; } = name ?? string.Empty;
        public string ActionType { get; set; } = actionType ?? string.Empty;
        public string? ElementXPath { get; set; } = elementXPath;
        public string? ElementId { get; set; } = elementId;
        public string? BackupScenarioPath { get; set; } = backupScenarioPath;

        public void ExecuteAndLog(int stepCounter)
        {
            try
            {
                Logger.Log($"Wykonywanie kroku {stepCounter}: {Name}");
                HandleAction();
                Logger.Log($"{Name} -> OK", true);
            }
            catch (InavlidVerificationException e)
            {
                Logger.Log($"Wystąpił błąd podczas weryfikacji. {e.Message}", true);
                HandleFailure(stepCounter, e.Message);
            }
            catch (NoSuchElementException e)
            {
                Logger.Log($"Wystąpił błąd - nie znaleziono elementu na stronie. {e.Message}", true);
                HandleFailure(stepCounter, e.Message);
            }
            catch (Exception e)
            {
                Logger.Log($"Wystąpił nieoczekiwany błąd: {e.Message}", true);
                HandleFailure(stepCounter, e.Message);
            }
        }

        public virtual void HandleAction()
        {
            // Każdy klasa pochodna posiada własną implementacje metody HandleAction
        }

        public void HandleFailure(int stepCounter, string message)
        {
            if (!string.IsNullOrEmpty(BackupScenarioPath))
            {
                TestRunner.Run(BackupScenarioPath);
            }
            else
            {
                string decision = UserInputUtility.GetFailureDecisionFromUser();
                switch (decision)
                {
                    case "Y": // dodaj błąd do podsumowania i przejdź do kolejnego kroku
                        Summary.AddFailedStep(stepCounter, message);
                        break;
                    case "R": // wykonaj krok ponownie
                        ExecuteAndLog(stepCounter);
                        break;
                    case "N": // dodaj błąd do posumowania i zakończ działanie programu 
                        Summary.AddFailedStep(stepCounter, message); 
                        throw new FailedStepException("Wykonywanie scenariusza zostało przerwane.");
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
    }

    public class InavlidStepParameterException : Exception
    {
        public InavlidStepParameterException(string message) : base(message) { }
    }

    public class FailedStepException : Exception
    {
        public FailedStepException(string message) : base(message) { }
    }
}