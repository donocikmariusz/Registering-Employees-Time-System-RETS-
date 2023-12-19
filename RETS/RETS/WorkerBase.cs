namespace RETS
{
    public abstract class WorkerBase : IRets
    {
        public delegate void TimeAddedDelegate(object sender, EventArgs args);
        public abstract event TimeAddedDelegate TimeAdded;
        public WorkerBase(string intime, string outtime)
        {
            this.Intime = intime;
            this.Outtime = outtime;
        }
        public string Intime { get; private set; }
        public string Outtime { get; private set; }
        public TimeSpan Difference { get; set; }
        public TimeSpan Doba { get; set; }

        public abstract void AddTimeDifference(DateTime newTime1, DateTime newTime2);
        public abstract void AddCalculated24h(DateTime newTime1, DateTime newTime2);
        public abstract void ShowStatistics();
        public abstract void EveryDaySummary();
        public abstract Statistics GetStatistics();

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
    }
}
