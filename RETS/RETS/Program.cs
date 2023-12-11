namespace RETS
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Program do monitorowania czasu pracy pracowników");
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Wybierz czy program ma:");
            Console.WriteLine("1. - Tylko zebrać i zanalizować statystyki");
            Console.WriteLine("2. - Zebrać i zanalizować statystyki i zrzucić je do pliku .txt");
            Console.WriteLine("Twój wybór: (lub 'q' lub 'Q' żeby wyjść)");

            bool exitApp = false;

            while (!exitApp)
            {
                var wybor = Console.ReadLine().ToUpper();

                switch (wybor)
                {
                    case "1":
                        OnlyCalculateandShowTime();
                        break;

                    case "2":
                        OnlyCalculateandShowTime();
                        break;

                    case "Q":
                        exitApp = true;
                        break;

                    default:
                        Console.WriteLine("Something went wrong...");
                        continue;
                }
                break;
            }
        }

        private static void OnlyCalculateandShowTime()
        {
            int ktorydzien = 1;
            CalculateTime time1 = new CalculateTime("", "");
            User user1 = new User("Jarosław", "Kaczyński");

            while (true)
            {
                Console.WriteLine($"Dzień: {ktorydzien}");
                Console.WriteLine();
                string intime = GetTimeFromUser("Podaj godzinę wejścia w formacie hh:mm:");

                while (!TimeValidator.IsValidTime(intime))
                {
                    Console.WriteLine("Błędny format godziny.Poprawny format to max 24 dla hh: max 60 dla mm.");
                    Console.WriteLine("Podaj godzinę wejścia w formacie hh:mm:");
                    intime = Console.ReadLine();

                }

                string outtime = GetTimeFromUser("Podaj godzinę wyjścia w formacie hh:mm:");

                while (!TimeValidator.IsValidTime(outtime))
                {
                    Console.WriteLine("Błędny format godziny.Poprawny format to max 24 dla hh: max 60 dla mm.");
                    Console.WriteLine("Podaj godzinę wyjścia w formacie hh:mm:");
                    outtime = Console.ReadLine();
                }

                time1.AddTimeDifference(intime, outtime);
                Console.WriteLine();

                TimeSpan difference = time1.Difference;
                Console.WriteLine($"Dnia {ktorydzien} {user1.Name} {user1.Surname} był : {difference}");
                ktorydzien++;

                Console.WriteLine("Naciśnij 'S' aby wyświetlić podsumowanie lub 'Q' aby wyjść lub Enter aby kontynuuować");
                var input = Console.ReadLine();

                if (input == "s" || input == "S")
                {
                    var formattedResult = time1.FormattedEveryDayResult;
                    Console.WriteLine($"W sumie był: {formattedResult}");

                    Console.WriteLine();

                    Parameters parameter = new Parameters();
                    parameter.GetDaysInCurrentMonth();
                    parameter.GetCurrentMonthName();
                    parameter.GetMonthlyWorkingDays();
                    parameter.GetMonthlyHours();
                    parameter.TotalWorkingHours(time1);
                    parameter.showSummary(time1);

                    break;
                }

                else if (input == "Q" || input == "q")

                {
                    break;
                }

                else
                {
                    continue;
                }

            }
        }
        static string GetTimeFromUser(string text)
        {
            Console.WriteLine(text);
            string userInput = Console.ReadLine();
            return userInput;
        }
    }

}