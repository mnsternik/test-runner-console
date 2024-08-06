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
        catch (ConfigException ex)
        {
            Logger.Log(ex.Message);
        }
        catch (ScenarioFileException ex)
        {
            Logger.Log(ex.Message);
        }
        catch (Exception ex)
        {
            Logger.Log($"Wystąpił nieoczekiwany błąd. Zakończenie pracy programu. Błąd: {ex.Message}");
        }
        finally
        {
            Logger.Log(Summary.CreateSummary());
        }
    }
}
