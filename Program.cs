﻿namespace TestRunnerConsole;

class Program
{
    static void Main(string[] args)
    {
        try
        {   
            ConfigManager.InitConfig(); 
            Logger.InitLogger(); 
            TestScenario ts = TestScenario.LoadScenario(Config.TestScenarioPath); 
            TestRunner.Run(ts); 
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
