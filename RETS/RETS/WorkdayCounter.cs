namespace RETS
{

    public class WorkdayCounter : System.Object
    {
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

        public static TimeSpan SumTimeSpans(List<TimeSpan> timeSpans)
        {
            return TimeCalculator.SumTimeSpans(timeSpans);
        }

        public static TimeSpan CalculateTotalWorkHoursInMonth(int year, int month, int workHoursPerDay)
        {
            int totalWorkdays = CountWorkdaysInMonth(year, month);
            return TimeSpan.FromHours(totalWorkdays * workHoursPerDay);
        }

        public static bool IsWorkday(DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
        }
    }

}

