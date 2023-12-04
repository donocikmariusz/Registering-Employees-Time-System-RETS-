namespace RETS
{
    public class Parameters
    {
        public static void GetDaysInCurrentMonth()
        {
            // Pobierz obecną datę
            DateTime currentDate = DateTime.Now;

            // Pobierz liczbę dni w obecnym miesiącu
            int daysInCurrentMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
            Console.WriteLine($"Liczba dni w bieżącym miesiącu: {daysInCurrentMonth}");
        }

        public static void GetCurrentMonthName()
        {
            // Pobierz obecną datę
            DateTime currentDate = DateTime.Now;

            // Pobierz nazwę obecnego miesiąca
            string currentMonthName = currentDate.ToString("MMMM");
            Console.WriteLine($"Nazwa obecnego miesiąca: {currentMonthName}");
        }

        public static void GetMonthlyWorkingDays()
        {
            // ile dni roboczych w danym miesiacu
            DateTime currentDate = DateTime.Now;
            int workdays = WorkdayCounter.CountWorkdaysInMonth(currentDate.Year, currentDate.Month);
            Console.WriteLine("");
            Console.WriteLine($"Ilość dni roboczych w bieżącym miesiącu: {workdays}");

        }

        public static void GetMonthlyHours()
        {
            // ile godzin roboczych w obecnym miesiacu
            DateTime currentDate = DateTime.Now;
            int workHoursPerDay = 8;
            TimeSpan totalWorkHours = WorkdayCounter.CalculateTotalWorkHoursInMonth(currentDate.Year, currentDate.Month, workHoursPerDay);
            Console.WriteLine($"Łączna liczba godzin roboczych w bieżącym miesiącu: {totalWorkHours.TotalHours} godzin");
        }

        public static void TotalWorkingHours(List<TimeSpan> everyDayResult)
        {
            // ile rzeczywiscie czasu przepracował
            TimeSpan totalWorkedTime = WorkdayCounter.SumTimeSpans(everyDayResult);
            Console.WriteLine($"Łączny czas przepracowany: {TimeCalculator.FormatTotalTime(totalWorkedTime)}");
        }


        public static void showSummary(List<TimeSpan> everyDayResult, DateTime currentDate, TimeSpan totalWorkedTime, TimeSpan totalWorkHours)

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
                    Console.WriteLine($"Dnia {i} był {Math.Abs(everyDayResult[i].Hours):D2} hour {Math.Abs(everyDayResult[i].Minutes):D2} minutes - nadczas wynosi {overtime.Hours} godzin {overtime.Minutes} minut");
                }

                else if (osiemGodzin > everyDayResult[i])
                {
                    TimeSpan undertime = osiemGodzin - everyDayResult[i];
                    Console.WriteLine($"Dnia {i} był {Math.Abs(everyDayResult[i].Hours):D2} hour {Math.Abs(everyDayResult[i].Minutes):D2} minutes - niedoczas wynosi: {undertime.Hours} godzin {undertime.Minutes} minut");
                }

                else
                {
                    Console.WriteLine($"Dnia {i} był {Math.Abs(everyDayResult[i].Hours):D2} hour {Math.Abs(everyDayResult[i].Minutes):D2} minutes");
                }
            }

            GetMonthlyWorkingDays();
            GetMonthlyHours();
            TotalWorkingHours(everyDayResult);
            GetCurrentMonthName();
            GetDaysInCurrentMonth();
        }

    }

}
