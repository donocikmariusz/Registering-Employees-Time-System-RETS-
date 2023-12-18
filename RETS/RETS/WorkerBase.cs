
using System;

namespace RETS
{
    public abstract class WorkerBase : IRets
    {
        private readonly TimeSpan osiemGodzin = TimeSpan.FromHours(8);
        public WorkerBase(string intime, string outtime)
        {
            this.Intime = intime;
            this.Outtime = outtime;
        }
        private List<TimeSpan> everyDayResult = new List<TimeSpan>();
        public string Intime { get; private set; }
        public string Outtime { get; private set; }

        public TimeSpan Difference { get; set; }
        public TimeSpan Doba { get; set; }


        public void AddTimeDifference(DateTime newTime1, DateTime newTime2)

        {
            Difference = newTime2 - newTime1;
            everyDayResult.Add(Difference);
        }

        public void AddCalculated24h(DateTime newTime1, DateTime newTime2)
        {
            TimeSpan doba = TimeSpan.FromHours(24);
            Doba = (TimeSpan.FromHours(24) - (newTime1 - newTime2));
            everyDayResult.Add(Doba);
        }

        /*   public abstract Statistics GetStatistics();

           public void ShowStatistics()
           {
               var statistics = GetStatistics();
               Console.WriteLine($"Liczba dni w bieżącym miesiącu: {statistics.DaysInCurrentMonth}");
               Console.WriteLine($"Nazwa obecnego miesiąca: {statistics.CurrentMonthName}");
               Console.WriteLine($"Ilość dni roboczych w bieżącym miesiącu: {statistics.Workdays}");
               Console.WriteLine($"Łączna liczba godzin roboczych w bieżącym miesiącu: {statistics.CalculateTotalWorkHoursInMonthFormatted}");
               Console.WriteLine($"Łączny czas przepracowany: {statistics.TotalWorkedTime}");
               Console.WriteLine();

           }
        */

        public void EveryDaySummary()
        {
            Console.WriteLine("Statystki z każdego dnia:");
            Console.WriteLine("");

            for (int i = 0; i < everyDayResult.Count; i++)
            {
                if (osiemGodzin < everyDayResult[i])
                {
                    TimeSpan overtime = everyDayResult[i] - osiemGodzin;
                    Console.WriteLine($"Dnia {i + 1} był {Math.Abs(everyDayResult[i].Hours):D2} godzin {Math.Abs(everyDayResult[i].Minutes):D2} minut - nadczas wynosi {overtime.Hours} godzin {overtime.Minutes} minut");
                }
                else if (osiemGodzin > everyDayResult[i])
                {
                    TimeSpan undertime = osiemGodzin - everyDayResult[i];
                    Console.WriteLine($"Dnia {i + 1} był {Math.Abs(everyDayResult[i].Hours):D2} godzin {Math.Abs(everyDayResult[i].Minutes):D2} minut - niedoczas wynosi: {undertime.Hours} godzin {undertime.Minutes} minut");
                }
                else
                {
                    Console.WriteLine($"Dnia {i + 1} był {Math.Abs(everyDayResult[i].Hours):D2} godzin {Math.Abs(everyDayResult[i].Minutes):D2} czyli jest dokładny co do minuty");
                }
            }
        }

        public static int CountWorkdaysInMonth(int year, int month)
        {
            int daysInMonth = DateTime.DaysInMonth(year, month);
            int workdays = 0;

            for (int day = 1; day <= daysInMonth; day++)
            {
                DateTime currentDate = new DateTime(year, month, day);

                if (IsWorkday(currentDate))
                {
                    workdays++;
                }
            }
            return workdays;
        }

        // obliczenie ile godzin roboczych jest w danym miesiącu na podstawie jego ilości dni roboczych
        public static TimeSpan CalculateTotalWorkHoursInMonth(int year, int month, int workHoursPerDay)
        {
            int totalWorkdays = CountWorkdaysInMonth(year, month);
            return TimeSpan.FromHours(totalWorkdays * workHoursPerDay);
        }

        // zwrócenie całkowitej ilości dni w miesiącu oprócz sobót i niedziel (nie uwzględnia świąt)
        public static bool IsWorkday(DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
        }

        public static string CalculateTotalWorkHoursInMonthFormatted(int year, int month, int workHoursPerDay)
        {
            int totalWorkdays = CountWorkdaysInMonth(year, month);
            int totalHours = totalWorkdays * workHoursPerDay;
            return $"{totalHours} godzin";
        }

        public void PerformActionsBasedOnWorkedHourszwykły(TimeSpan totalWorkHoursInMonth, TimeSpan totalWorkHours)
        {
            double hoursWorked = totalWorkHours.TotalHours;
            double totalMonthlyHours = totalWorkHoursInMonth.TotalHours;

            switch (hoursWorked)
            {
                case double hours when hours == totalMonthlyHours:
                    Console.WriteLine("Gratulacje! Przepracowałeś dokładnie tyle godzin, ile wynosi limit w miesiącu.");
                    break;

                case double hours when hours >= 0 && hours < 25:
                    Console.WriteLine($"Przepracowano od 0 do 24 godzin.");
                    break;

                case double hours when hours >= 25 && hours < 50:
                    Console.WriteLine($"Przepracowano od 25 do 49 godzin.");
                    break;

                case double hours when hours >= 50 && hours < 75:
                    Console.WriteLine($"Przepracowano od 50 do 74 godzin.");
                    break;

                case double hours when hours >= 75 && hours < 100:
                    Console.WriteLine($"Przepracowano od 75 do 99 godzin.");
                    break;

                case double hours when hours >= 100 && hours < 125:
                    Console.WriteLine($"Przepracowano od 100 do 124 godzin.");
                    break;

                case double hours when hours >= 125 && hours < 150:
                    Console.WriteLine($"Przepracowano od 125 do 149 godzin.");
                    break;
                default:
                    throw new Exception("Coś poszło nie tak...");

            }
        }

        public Statistics GetStatistics()
        {
            var statistics = new Statistics();
            statistics.CurrentDate = DateTime.Now;
            statistics.OsiemGodzin = TimeSpan.FromHours(8);
            int workHoursPerDay = 8;

            statistics.DaysInCurrentMonth = CountWorkdaysInMonth(statistics.CurrentDate.Year, statistics.CurrentDate.Month);
            statistics.CurrentMonthName = DateTime.Now.ToString("MMMM");
            statistics.TotalWorkDays = CalculateTotalWorkHoursInMonth(statistics.CurrentDate.Year, statistics.CurrentDate.Month, workHoursPerDay);
            statistics.CalculateTotalWorkHoursInMonthFormatted = CalculateTotalWorkHoursInMonthFormatted(statistics.CurrentDate.Year, statistics.CurrentDate.Month, workHoursPerDay);

            foreach (var totalWorkedTime in everyDayResult )
            {
                statistics.TotalWorkedTime += totalWorkedTime;
            }
            return statistics;
        }
    }
}
