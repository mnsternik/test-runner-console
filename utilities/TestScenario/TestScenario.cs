using System.Reflection.Metadata.Ecma335;

namespace FirstConsoleApp
{
    public class TestScenario
    {
        public string Name {get; set; } = "";
        public GenericStep[] Steps { get; set; } = Array.Empty<GenericStep>(); 

        public static TestScenario LoadScenario(string path)
        {
            return JSONFileReader.Deserialize<TestScenario>(path);
        }
    }
}
