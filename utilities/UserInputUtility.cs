using System.Runtime.InteropServices;
using System.Security;

namespace TestRunnerConsole
{
    public class UserInputUtility
    {

        public static bool AskForUserConfirmation(string message = "")
        {
            if (!string.IsNullOrEmpty(message))
            {
                Logger.Log(message); 
            }

            Logger.Log("Wstrzymano wykonywanie scenariusza testowego. Wpisz 'Y' by wznowić, lub 'N' by zakończyć:");
            string? input = Console.ReadLine()?.Trim().ToUpper();

            while (true)
            {
                if (input == "Y")
                {
                    Logger.Log("Wybrano 'Y' - scenariusz zostanie wznonwiony");
                    return true;
                }
                else if (input == "N")
                {
                    Logger.Log("Wybrano 'N' - działanie programu zostanie zakończone");
                    return false;
                }
                Logger.Log("Wprowadzono niepoprawną odpowiedź - wprowadź 'Y' by kontynuować lub 'N' by przerwać: ");
                input = Console.ReadLine()?.Trim().ToUpper();
            }
        }

        public static SecureString ReadPassword()
        {
            Boolean isDebugger = false;
            Console.WriteLine("Wprowadź hasło i kliknij Enter:");
            if (!isDebugger)
            {
                SecureString password = new SecureString();
                ConsoleKeyInfo key;
                int minPasswordLength = 6;
                int i = 0;
                do
                {
                    // TODO: Po wciśnięciu backspace usuwać znak a nie dodawać go do hasła (chociaż w Windows form i tak to będzie działać inaczej) 
                    key = Console.ReadKey(true);
                    if (key.Key != ConsoleKey.Enter)
                    {
                        password.AppendChar(key.KeyChar);
                        Console.Write("*");
                        i++;
                    }
                } while (key.Key != ConsoleKey.Enter || i < minPasswordLength);
                return password;
            }
            else
            {
                // Debbuger nie pozwala na używanie Console.ReadKey, dlatego poniżej alternatywna opcja, bez ukrywania hasła gwiazdkami
                SecureString secureString = new SecureString();
                string password = Console.ReadLine()?.Trim() ?? "";
                foreach (char c in password)
                {
                    secureString.AppendChar(c);
                }
                return secureString;
            }
        }

        public static String ReadLogin()
        {
            string? login;
            Console.WriteLine("Wprowadź login i kliknij Enter:");
            do
            {
                login = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(login))
                {
                    Console.WriteLine("Wprowadź poprawny login (nie może być pusty): ");
                }
            } while (string.IsNullOrEmpty(login));
            return login;
        }

        public static string ConvertSecureStringToString(SecureString secureString)
        {
            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(ptr) ?? string.Empty;
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                {
                    Marshal.ZeroFreeGlobalAllocUnicode(ptr);
                }
            }
        }
    }
}
