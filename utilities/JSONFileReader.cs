using Newtonsoft.Json;

namespace TestRunnerConsole
{
    public class JSONFileReader
    {
        public static T Deserialize<T>(string filePath)
        {
            string content = GetFileContent(filePath);
            if (string.IsNullOrEmpty(content))
            {
                throw new Exception($"Brak pliku lub zawartości w pliku: {filePath}");
            }

            T deserializedObject = JsonConvert.DeserializeObject<T>(content) ?? throw new Exception("Wystąpił błąd podczas deserializacji pliku JSON.");
            return deserializedObject;
        }

        private static string GetFileContent(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new Exception($"Nie znaleziono pliku ze scenariuszem testowym. Wskazano lokalizacje {filePath}.");
            }

            return File.ReadAllText(filePath);
        }
    }
}