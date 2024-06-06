﻿namespace TestRunnerConsole;

class Program
{
    static void Main(string[] args)
    {
        try
        {   
            Config c = ConfigManager.GetConfig(); 
            Logger.InitLogger(c.LogsFolderPath); 
            // TestRunner nie powinien być klasą statyczną. Logger może też nie. 
            TestRunner.InitDriverWithOptions(c); 
            TestRunner.Run(c.TestScenarioPath); 
        }
        catch (Exception e)
        {
            // TO CONSIDER: dodać flage result, kiedy pojawi sie jakiś exception to zmieniać na false, i w finally jeżeli jest nadal true to znaczy że test zakończony pozytywnie
            Logger.Log("Error: " + e.Message);
        }
        finally
        {
        }
    }
}
