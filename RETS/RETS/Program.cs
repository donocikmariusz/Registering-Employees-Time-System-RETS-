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
            CalculateTime time1 = new CalculateTime("", "") ;
            User user1 = new User("Jarosław", "Kaczyński");

            bool exitApp = false;

            while (!exitApp)
            {
                bool exitLoop = false;

                while (!exitLoop)
                {
                    Console.WriteLine($"Dzień: {ktorydzien}");
                    string intime = GetTimeFromUser("Podaj godzinę wejścia w formacie hh:mm:");
                    string outtime = GetTimeFromUser("Podaj godzinę wyjścia w formacie hh:mm:");

                    DateTime newTime1 = ParseTime(intime);
                    DateTime newTime2 = ParseTime(outtime);

                    if (newTime2 > newTime1)
                    {
                        exitLoop = true;
                        time1.AddTimeDifference(newTime1, newTime2);
                    }

                    else
                    {
                        Console.WriteLine("Nie mógł przenieść się w czasie... Czy doliczyć mu prawie dobę? T-TAK, N-NIE");
                        var input3 = Console.ReadLine().ToUpper();

                        switch (input3)
                        {
                            case "T":
                                time1.AddCalculated24h(newTime1, newTime2);

                                break;

                            case "N":
                                ktorydzien--;
                                exitLoop = true;
                                break;

                            default:
                                Console.WriteLine("Something went wrong...");
                                break;
                        }
                        break;
                    }

                }

                Console.WriteLine();
                string difference = time1.FormattedEveryDayResult;
                Console.WriteLine($"Dnia {ktorydzien} {user1.Name} {user1.Surname} był : {difference}");
                ktorydzien++;

                Console.WriteLine("Naciśnij 'S' aby wyświetlić podsumowanie lub 'Q' aby wyjść lub Enter aby kontynuuować");
                Console.WriteLine();

                var input = Console.ReadLine();

                if (input == "s" || input == "S")
                {
                    Parameters parameter = new Parameters();
                    parameter.showSummary(time1);

                    Console.WriteLine();
                    Console.WriteLine("Czy wyświetlić statystyki z każdego dnia? Y - YES, N - NO");
                    var input2 = Console.ReadLine().ToUpper();

                    switch (input2)
                    {
                        case "Y":
                            parameter.EveryDaySummary(time1);
                            break;
                        case "N":
                            exitApp = true;
                            break;
                        default:
                            Console.WriteLine("Something went wrong...");
                            break;
                    }
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
            while (true)
            {
                Console.WriteLine(text);
                string userInput = Console.ReadLine();

                if (!TimeValidator.IsValidTime(userInput))
                {
                    Console.WriteLine("\"Błędny format godziny.Poprawny format to max 24 dla hh: max 60 dla mm.");
                }
                else
                {
                    return userInput;
                }
            }
        }
        static DateTime ParseTime(string timeString)
        {
            string[] formats = { "H:mm", "HH:mm", "H:m", "HH:m" };
            DateTime.TryParseExact(timeString, formats, null, System.Globalization.DateTimeStyles.None, out DateTime time);
            return time;
        }
    }
}