namespace RETS
{
    public class zwyklyParch
    {
        public const string fileName = "czaszwykły.txt";

        public static void HandleZwyklyParch()
        {
            List<TimeSpan> everyDayResult = new List<TimeSpan>();
            Console.WriteLine("Wybrano 1 - zwykłego parcha produkcyjnego");
            int ktorydzien = 1;

            while (true)
            {
                Console.WriteLine("Dzień: " + ktorydzien);
                Console.WriteLine("Podaj godzinę wejścia w formacie hh:mm:");
                string intime = Console.ReadLine();

                while (!TimeValidator.IsValidTime(intime))
                {
                    try
                    {
                        Console.WriteLine("Błędny format godziny.Poprawny format to max 24 dla hh: max 60 dla mm.");
                        Console.WriteLine("Podaj godzinę wejścia w formacie hh:mm:");
                        intime = Console.ReadLine();
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Błąd formatu. Upewnij się, że wprowadziłeś godzinę w formacie hh:mm.");
                        Console.WriteLine("Podaj godzinę wejścia w formacie hh:mm:");
                    }
                }

                Console.WriteLine("Podaj godzinę wyjścia w formacie hh:mm:");
                string outtime = Console.ReadLine();
                while (!TimeValidator.IsValidTime(outtime))
                {
                    try
                    {
                        Console.WriteLine("Błędny format godziny.Poprawny format to max 24 dla hh: max 60 dla mm.");
                        Console.WriteLine("Podaj godzinę wyjścia w formacie hh:mm:");
                        outtime = Console.ReadLine();
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Błąd formatu. Upewnij się, że wprowadziłeś godzinę w formacie hh:mm.");
                        Console.WriteLine("Podaj godzinę wyjścia w formacie hh:mm:");
                    }
                }

                using (var writerstring = File.AppendText(fileName))
                {
                    writerstring.WriteLine(ktorydzien);
                }

                using (var writerin = File.AppendText(fileName))
                {
                    writerin.WriteLine(intime, intime);
                }

                using (var writerout = File.AppendText(fileName))
                {
                    writerout.WriteLine(intime, outtime);
                }

                Console.WriteLine();

                TimeSpan difference = CalculateTime.TotalTimeForOneDay(intime, outtime, ktorydzien, out difference);
                ktorydzien++;
                everyDayResult.Add(difference);

                Console.WriteLine("Naciśnij 'S' aby wyświetlić podsumowanie lub 'Q' aby wyjść lub Enter aby kontynuuować");
                var input = Console.ReadLine();

                if (input == "s" || input == "S")
                {
                    TimeSpan osiemGodzin = TimeSpan.FromHours(8);
                    Console.WriteLine("");
                    Console.WriteLine("Statystki:");
                    Console.WriteLine("");

                    for (int i = 0; i < everyDayResult.Count; i++)
                    {
                        if (osiemGodzin < everyDayResult[i])
                        {
                            TimeSpan overtime = everyDayResult[i] - osiemGodzin;
                            Console.WriteLine($"Dnia {i} parch był {Math.Abs(everyDayResult[i].Hours):D2} hour {Math.Abs(everyDayResult[i].Minutes):D2} minutes - nadczas wynosi {overtime.Hours} godzin {overtime.Minutes} minut");
                        }

                        else if (osiemGodzin > everyDayResult[i])
                        {
                            TimeSpan undertime = osiemGodzin - everyDayResult[i];
                            Console.WriteLine($"Dnia {i} parch był {Math.Abs(everyDayResult[i].Hours):D2} hour {Math.Abs(everyDayResult[i].Minutes):D2} minutes - niedoczas wynosi: {undertime.Hours} godzin {undertime.Minutes} minut");
                        }

                        else
                        {
                            Console.WriteLine($"Dnia {i} parch był {Math.Abs(everyDayResult[i].Hours):D2} hour {Math.Abs(everyDayResult[i].Minutes):D2} minutes");
                        }
                    }
                    // weź więcej z klasy parameters
                    Parameters.showSummary();

                    DateTime currentDate = DateTime.Now;
                    int workdays = WorkdayCounter.CountWorkdaysInMonth(currentDate.Year, currentDate.Month);
                    Console.WriteLine("");
                    Console.WriteLine($"Ilość dni roboczych w bieżącym miesiącu: {workdays}");

                    // ile godzin roboczych w obecnym miesiacu
                    int workHoursPerDay = 8;
                    TimeSpan totalWorkHours = WorkdayCounter.CalculateTotalWorkHoursInMonth(currentDate.Year, currentDate.Month, workHoursPerDay);
                    Console.WriteLine($"Łączna liczba godzin roboczych w bieżącym miesiącu: {totalWorkHours.TotalHours} godzin");

                    // ile rzeczywiscie czasu przepracował
                    TimeSpan totalWorkedTime = WorkdayCounter.SumTimeSpans(everyDayResult);
                    Console.WriteLine($"Łączny czas przepracowany: {TimeCalculator.FormatTotalTime(totalWorkedTime)}");

                    // podsumowanie jego godzin pracy
                    WorkTimeActions.PerformActionsBasedOnWorkedHours(totalWorkedTime.TotalHours, totalWorkHours.TotalHours);

                }

                else if (input == "Q" || input == "q")

                {
                    break;
                }

                else
                {
                    continue;
                }
                break;
            }

        }

    }
}
