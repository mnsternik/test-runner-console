namespace FirstConsoleApp
{
    public class ManualStep(GenericStep step) : Step(step.Name, step.ActionType, step.ElementXPath, step.ElementId, step.BackupScenarioPath)
    {
        public override void HandleAction()
        {
            Logger.Log("Wstrzymano wykonywanie scenariusza testowego. Wpisz 'Y' by wznowić, lub 'N' by zakończyć:");
            string? input = Console.ReadLine()?.Trim().ToUpper();

            while (true)
            {
                if (input == "Y")
                {
                    Logger.Log("Wybrano 'Y' - scenariusz zostanie wznonwiony");
                    break;
                }
                else if (input == "N")
                {
                    throw new InavlidVerificationException("Wybrano 'N' - zakończono wykonywanie scenariusza.");
                }
                Console.WriteLine("Wprowadzono niepoprawną odpowiedź - wprowadź 'Y' by kontynuować lub 'N' by przerwać: ");
            }
        }
    }
}
