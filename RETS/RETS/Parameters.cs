
namespace RETS
{
    public class Parameters
    {
        DateTime currentDate = DateTime.Now;

        public void GetDaysInCurrentMonth()
        {
            // Pobierz liczbę dni w obecnym miesiącu
            int daysInCurrentMonth = DateTime.DaysInMonth(this.currentDate.Year, this.currentDate.Month);
            Console.WriteLine($"Liczba dni w bieżącym miesiącu: {daysInCurrentMonth}");
        }

        public void GetCurrentMonthName()
        {
            // Pobierz nazwę obecnego miesiąca
            string currentMonthName = currentDate.ToString("MMMM");
            Console.WriteLine($"Nazwa obecnego miesiąca: {currentMonthName}");
        }

        public void GetMonthlyWorkingDays()
        {
            // ile dni roboczych w danym miesiacu
            int workdays = WorkdayCounter.CountWorkdaysInMonth(this.currentDate.Year, this.currentDate.Month);
                      Console.WriteLine($"Ilość dni roboczych w bieżącym miesiącu: {workdays}");
        }

        public void GetMonthlyHours()
        {
            int workHoursPerDay = 8;
            TimeSpan totalWorkHours = WorkdayCounter.CalculateTotalWorkHoursInMonth(currentDate.Year, currentDate.Month, workHoursPerDay);
            Console.WriteLine($"Łączna liczba godzin roboczych w bieżącym miesiącu: {totalWorkHours.TotalHours} godzin");
        }

        public void TotalWorkingHours(CalculateTime time1)
        {
            // ile rzeczywiscie czasu przepracował
            TimeSpan totalWorkedTime = CalculateTime.SumTimeSpans(new List<TimeSpan> { time1.EveryDayResult });
            Console.WriteLine($"Łączny czas przepracowany: {CalculateTime.FormatTotalTime(totalWorkedTime)}");
        }

        public void showSummary(CalculateTime time1)
        {

            TotalWorkingHours(time1);
            GetDaysInCurrentMonth();
            GetCurrentMonthName();
            GetMonthlyWorkingDays();
            GetMonthlyHours();
        }

        public void EveryDaySummary(CalculateTime time1)
        {

            Console.WriteLine("Statystki z każdego dnia:");
            Console.WriteLine("");

            TimeSpan osiemGodzin = TimeSpan.FromHours(8);

            for (int i = 0; i < time1.everyDayResult.Count; i++)
            {
                if (osiemGodzin < time1.everyDayResult[i])
                {
                    TimeSpan overtime = time1.everyDayResult[i] - osiemGodzin;
                    Console.WriteLine($"Dnia {i + 1} był {Math.Abs(time1.everyDayResult[i].Hours):D2} godzin {Math.Abs(time1.everyDayResult[i].Minutes):D2} minut - nadczas wynosi {overtime.Hours} godzin {overtime.Minutes} minut");
                }
                else if (osiemGodzin > time1.everyDayResult[i])
                {
                    TimeSpan undertime = osiemGodzin - time1.everyDayResult[i];
                    Console.WriteLine($"Dnia {i + 1} był {Math.Abs(time1.everyDayResult[i].Hours):D2} godzin {Math.Abs(time1.everyDayResult[i].Minutes):D2} minut - niedoczas wynosi: {undertime.Hours} godzin {undertime.Minutes} minut");
                }
                else
                {
                    Console.WriteLine($"Dnia {i + 1} był {Math.Abs(time1.everyDayResult[i].Hours):D2} godzin {Math.Abs(time1.everyDayResult[i].Minutes):D2} minut");
                }
            }
        }
    }

}


