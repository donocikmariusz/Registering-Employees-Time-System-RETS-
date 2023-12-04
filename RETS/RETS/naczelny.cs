using static RETS.Parameters;

namespace RETS
{
    public class naczelny
    {
        public const string fileName = "czasnaczelny.txt";
        public static void HandleNaczelny(List<TimeSpan> everyDayResult)
        {

            Console.WriteLine("Wybrano 3 - naczelny");
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
                    writerin.WriteLine(intime);
                }

                using (var writerout = File.AppendText(fileName))
                {
                    writerout.WriteLine(outtime);
                }

                Console.WriteLine();

                TimeSpan difference = CalculateTime.TotalTimeForOneDay(intime, outtime, ktorydzien, out difference);
                ktorydzien++;
                everyDayResult.Add(difference);

                Console.WriteLine("Naciśnij 'S' aby wyświetlić podsumowanie lub 'Q' aby wyjść lub Enter aby kontynuuować");
                var input = Console.ReadLine();

                if (input == "s" || input == "S")
                {

                    DateTime currentDate = DateTime.Now;
                    TimeSpan totalWorkedTime = WorkdayCounter.SumTimeSpans(everyDayResult);
                    int workHoursPerDay = 8;
                    TimeSpan totalWorkHours = WorkdayCounter.CalculateTotalWorkHoursInMonth(currentDate.Year, currentDate.Month, workHoursPerDay);
                    Parameters.showSummary(everyDayResult, currentDate, totalWorkedTime, totalWorkHours);

                    // podsumowanie jego godzin pracy
                    WorkTimeActions.PerformActionsBasedOnWorkedHoursnaczelny(totalWorkedTime.TotalHours, totalWorkHours.TotalHours);

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
