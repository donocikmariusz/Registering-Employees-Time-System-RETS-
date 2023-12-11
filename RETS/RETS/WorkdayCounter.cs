namespace RETS
{

    public class WorkdayCounter
    {
        // zliczenie ile dni w danym miesiącu
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
    }

}

