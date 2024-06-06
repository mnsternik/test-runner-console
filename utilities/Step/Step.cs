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

        public void HandleFailure(Exception e)
        {
            Logger.Log(e.Message);
            if (!string.IsNullOrEmpty(BackupScenarioPath))
            {
                Logger.Log("Przełączanie scenariusza testowego...");
                TestRunner.Run(BackupScenarioPath);
            }
            else 
            {
                throw new Exception("Wykonywanie scenariusza przerwane.");
            }
        }
    }

    public class InavlidStepParameterException : Exception
    {
        public InavlidStepParameterException(string message) : base(message) { }
    }
}