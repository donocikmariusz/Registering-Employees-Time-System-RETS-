﻿namespace RETS
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            bool exitApp = false;

            while (!exitApp)
            {
                try
                {
                    Console.WriteLine("Program do monitorowania czasu pracy pracowników");
                    Console.WriteLine("------------------------------------------------");
                    Console.WriteLine("Wybierz rodzaj pracownika:");
                    Console.WriteLine("1. - Produkcyjny ( pracujący zmianowo - zapis do pliku)");
                    Console.WriteLine("2. - Zadaniowy (pracujący hybrydowo - zapis tylko do pamięci)");
                    Console.WriteLine("Twój wybór: (lub 'q' lub 'Q' żeby wyjść)");
                    var wybor = Console.ReadLine().ToUpper();

                    switch (wybor)
                    {
                        case "1":
                            ManufacturingWorker();
                            break;

                        case "2":
                          //  HybridWorker();
                            break;

                        case "Q":
                            exitApp = true;
                            break;

                        default:
                            throw new Exception("Something went wrong...");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            }
        }

        private static void ManufacturingWorker()
        {
            int ktorydzien = 1;

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
                    var worker1time = new ManufacturingWorker(intime, outtime);

                    DateTime newTime1 = ParseTime(intime);
                    DateTime newTime2 = ParseTime(outtime);

                    if (newTime2 > newTime1)
                    {
                        exitLoop = true;
                        worker1time.AddTimeDifference(newTime1, newTime2);
                        Console.WriteLine($"Dnia {ktorydzien} {user1.Name} {user1.Surname} był : {worker1.Difference}");
                    }

                    else
                    {
                        bool loop = false;
                        while (!loop)
                        {
                            try
                            {
                                Console.WriteLine("Czy nastąpiło przenieniesienie na dzień następny? T-TAK, N-NIE");
                                var input3 = Console.ReadLine().ToUpper();

                                switch (input3)
                                {
                                    case "T":
                                        loop = true;
                                        worker1time.AddCalculated24h(newTime1, newTime2);
                                        Console.WriteLine($"Dnia {ktorydzien} {user1.Name} {user1.Surname} był : {worker1.Doba}");

                                        break;

                                    case "N":
                                        ktorydzien--;
                                        loop = true;
                                        continue;

                                    default:
                                        throw new Exception("Something went wrong...");
                                }
                                break;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }
                    break;
                }

                Console.WriteLine();
                var statistics = worker1time.GetStatistics();

                ktorydzien++;
                bool checkinput = false;

                while (!checkinput)
                {
                    Console.WriteLine("Naciśnij 'S' aby wyświetlić podsumowanie lub 'Q' aby wyjść lub Enter aby kontynuuować");
                    Console.WriteLine();
   

                    try
                    {
                        ConsoleKeyInfo keyInfo = Console.ReadKey();

                        if (keyInfo.Key == ConsoleKey.S)
                        {
                            checkinput = true;

                            Console.WriteLine($"Liczba dni w bieżącym miesiącu: {statistics.DaysInCurrentMonth}");
                            Console.WriteLine($"Nazwa obecnego miesiąca: {statistics.CurrentMonthName}");
                            Console.WriteLine($"Ilość dni roboczych w bieżącym miesiącu: {statistics.Workdays}");
                            Console.WriteLine($"Łączna liczba godzin roboczych w bieżącym miesiącu: {statistics.CalculateTotalWorkHoursInMonthFormatted}");
                            Console.WriteLine($"Łączny czas przepracowany: {statistics.TotalWorkedTime}");
                            Console.WriteLine();

                            var summary = new ManufacturingWorker();

                            bool input2a = false;

                            while (!input2a)
                            {
                                try
                                {
                                    Console.WriteLine("Czy wyświetlić statystyki z każdego dnia? Y - YES, N - NO");
                                    var input2 = Console.ReadLine().ToUpper();

                                    switch (input2)
                                    {
                                        case "Y":
                                            exitApp = true;
                                            worker1.EveryDaySummary();
                                            try
                                            {
                                                summary.PerformActionsBasedOnWorkedHourszwykły(statistics.TotalWorkHours, statistics.TotalWorkedTime);
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine($"{ex.Message}");
                                            }
                                            break;
                                        case "N":
                                            input2a = true;
                                            exitApp = true;
                                            break;
                                        default:
                                            throw new Exception();
                                    }
                                    break;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                        }

                        else if (keyInfo.Key == ConsoleKey.Q)

                        {
                            exitApp = true;
                            break;
                        }

                        else if (keyInfo.Key == ConsoleKey.Enter)
                        {
                            checkinput = true;
                        }

                        else
                        {
                            throw new Exception();
                        }
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
        static string GetTimeFromUser(string text)
        {
            while (true)
            {
                Console.WriteLine(text);
                try
                {
                    string userInput = Console.ReadLine();
                    if (!IsValidTime(userInput))
                    {
                        throw new Exception("Błędny format godziny.Poprawny format to max 24 dla hh: max 60 dla mm.");
                    }
                    else
                    {
                        return userInput;
                    }
                }

                catch (Exception e)
                { Console.WriteLine(e.Message); }

            }
        }
        static DateTime ParseTime(string timeString)
        {
            string[] formats = { "H:mm", "HH:mm", "H:m", "HH:m" };
            DateTime.TryParseExact(timeString, formats, null, System.Globalization.DateTimeStyles.None, out DateTime time);
            return time;
        }

        public static bool IsValidTime(string intime)
        {

            // Podział wprowadzonego czasu na godziny i minuty
            string[] parts = intime.Split(':');

            if (parts.Length == 2)
            {
                // Sprawdzenie, czy godziny i minuty są liczbami całkowitymi w odpowiednich zakresach
                if (int.TryParse(parts[0], out int hours) && int.TryParse(parts[1], out int minutes))
                {
                    return hours >= 0 && hours < 24 && minutes >= 0 && minutes < 60;
                }
            }
            return false;
        }
    }
}