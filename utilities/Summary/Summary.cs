namespace TestRunnerConsole
{
    public class Summary
    {
        public static List<FailedStep> Fails = new List<FailedStep>();

        public static void AddFailedStep(int stepCounter, string errorMessage)
        {
            FailedStep fs = new FailedStep(stepCounter, errorMessage);
            Fails.Add(fs);
        }

        public static string CreateSummary()
        {
            string failsDetails = "";
            Fails.ForEach(f => failsDetails += $"Krok: {f.StepNumber} - {f.Message}\n");
            if (Fails.Count > 0)
            {
                return $"Zakończono wykonywanie scenariusza testowego. \n\nZnaleziono błędów: {Fails.Count}. \n\nSzczegóły błędów: \n{failsDetails}";
            }
            else
            {
                return $"Zakończono wykonywanie scenariusza testowego. \nNie wykryto żadych błędów.";
            }
        }
    }

    public class FailedStep(int stepCounter, string errorMessage)
    {
        public int StepNumber { get; set; } = stepCounter;
        public string Message { get; set; } = errorMessage;
    }
}