using System;

namespace RETS
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            bool exitApp = false;
            while (!exitApp)
            {
                try
                {
                    Console.WriteLine("Program for monitoring employee working time");
                    Console.WriteLine("------------------------------------------------");
                    Console.WriteLine("Select employee type:");
                    Console.WriteLine("1. - Production (working in shifts - saving to a file)");
                    Console.WriteLine("2. - Task-based (working hybrid - saving only to memory)");
                    Console.WriteLine("Your choice: (or 'q' or 'Q' to exit)");

                    var choice = Console.ReadLine().ToUpper();

                    switch (choice)
                    {
                        case "1":
                            EnterTimes("Jarosław", "Kaczyński", new ManufacturingWorker("", ""));
                            break;

                        case "2":
                            EnterTimes("Zbigniew", "Stonoga", new HybridWorker("", ""));
                            break;

                        case "Q":
                            exitApp = true;
                            break;

                        default:
                            throw new Exception("Something went wrong...");
                    }
                    Console.ResetColor();
                    Console.WriteLine("");
                    Console.WriteLine("Press any key to close the app...");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                break;
            }
        }

        private static void EnterTimes(string firstName, string lastName, IRets worker)
        {
            User user = new User(firstName, lastName);
            worker.TimeAdded += WorkerTimeAdded;

            int counter = 1;

            bool exitApp = false;
            while (!exitApp)
            {
                bool exitLoop = false;

                while (!exitLoop)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine($"Day: {counter}");

                    string intime = GetTimeFromUser("Enter the entry time in the format hh:mm:");
                    string outtime = GetTimeFromUser("Enter the exit time in the format hh:mm:");
                    DateTime newTime1 = ParseTime(intime);
                    DateTime newTime2 = ParseTime(outtime);

                    if (newTime2 > newTime1)
                    {
                        exitLoop = true;

                        worker.AddTimeDifference(newTime1, newTime2);
                        Console.WriteLine($"Day {counter} {user.Name} {user.Surname} was: {worker.Difference.Hours}h {worker.Difference.Minutes}min at work");
                    }
                    else
                    {
                        bool loop = false;
                        while (!loop)
                        {
                            try
                            {
                                Console.WriteLine("Has it been carried over to the next day? Y-YES, N-NO");
                                var input3 = Console.ReadLine().ToUpper();

                                switch (input3)
                                {
                                    case "Y":
                                        loop = true;

                                        worker.AddCalculated24h(newTime1, newTime2);
                                        Console.WriteLine($"Dnia {counter} {user.Name} {user.Surname} był : {worker.Day.Hours}h {worker.Day.Minutes}min");
                                        break;

                                    case "N":
                                        counter--;
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

                counter++;

                bool checkinput = false;
                while (!checkinput)
                {
                    Console.WriteLine("Press 'S' to view summary or 'Q' to exit or Enter to continue");
                    try
                    {
                        ConsoleKeyInfo keyInfo = Console.ReadKey();

                        if (keyInfo.Key == ConsoleKey.S)
                        {
                            Console.WriteLine("");
                            checkinput = true;
                            Console.ForegroundColor = ConsoleColor.Green;
                            worker.ShowStatistics();

                            bool input2a = false;

                            while (!input2a)
                            {
                                try
                                {
                                    Console.WriteLine("View daily statistics? Y - YES, N - NO");
                                    var input2 = Console.ReadLine().ToUpper();
                                    Console.WriteLine("");
                                    switch (input2)
                                    {
                                        case "Y":
                                            exitApp = true;
                                            try
                                            {
                                                worker.EveryDaySummary();
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

        private static void WorkerTimeAdded(object sender, EventArgs args)
        {
            Console.WriteLine("Worker time added OK!");
        }

        private static string GetTimeFromUser(string text)
        {
            while (true)
            {
                Console.WriteLine(text);
                try
                {
                    string userInput = Console.ReadLine();
                    if (!IsValidTime(userInput))
                    {
                        throw new Exception("Incorrect time format. The correct format is max 23 for hh: max 59 for mm.");
                    }
                    else
                    {
                        return userInput;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static DateTime ParseTime(string timeString)
        {
            string[] formats = { "H:mm", "HH:mm" };
            DateTime.TryParseExact(timeString, formats, null, System.Globalization.DateTimeStyles.None, out DateTime time);
            return time;
        }

        public static bool IsValidTime(string intime)
        {
            // Podział wprowadzonego czasu na godziny i minuty
            string[] parts = intime.Split(':');

            if (parts.Length == 2 && parts[0].Length <= 2 && parts[1].Length == 2)
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
