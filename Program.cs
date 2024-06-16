﻿namespace TestRunnerConsole;

class Program
{
    static void Main(string[] args)
    {
        try
        {   
            Config c = ConfigManager.GetConfig(); 
            Logger.InitLogger(c.LogsFolderPath); 
            TestRunner.InitDriverWithOptions(c); 
            TestRunner.Run(c.TestScenarioPath); 
        }
        catch (FailedStepException e)
        {
            Logger.Log(e.Message); 
        }
        catch (Exception e)
        {
            Logger.Log($"Wystąpił nieoczekiwany błąd. Zakończenie pracy programu. Błąd: {e.Message}");
        }
        finally
        {
            Logger.Log(Summary.CreateSummary()); 
        }
    }
}
