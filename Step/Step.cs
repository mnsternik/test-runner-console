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
            catch (TestRunnerException ex)
            {
                Logger.Log(ex.Message, true);
                HandleFailure(stepCounter, ex.Message);
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
                TestScenario ts = TestScenario.LoadScenario(BackupScenarioPath);
                TestRunner.Run(ts);
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
                        throw new FailedStepException("Wykonywanie scenariusza zostało przerwane."); // how to catch it? Maybe it should be other type of exception
                }
            }
        }

        public virtual IWebElement GetElement()
        {
            if (TestRunner.Driver == null)
            {
                throw new DriverInitException("Driver nie został poprawnie zaninicjowany.");
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
                throw new InvalidStepParameterException($"Wykryto próbe odnalezienia elementu bez podania jego ID lub XPath - potrzebna weryfikacja poprawności scenariusza testowego.");
            }
        }
    }
}