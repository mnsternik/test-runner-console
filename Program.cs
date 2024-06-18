﻿namespace TestRunnerConsole;

class Program
{
    static void Main(string[] args)
    {
        try
        {   
            ConfigManager.InitConfig(); 
            Logger.InitLogger(); 
            TestRunner.Run(Config.TestScenarioPath); 
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
