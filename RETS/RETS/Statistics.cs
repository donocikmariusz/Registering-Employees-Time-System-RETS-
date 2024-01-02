namespace RETS
{
    public class Statistics
    {
        public DateTime CurrentDate
        {
            get
            {
                return DateTime.Now;
            }
        }

        public int DaysInCurrentMonth
        {
            get
            {
                return DateTime.DaysInMonth(this.CurrentDate.Year, this.CurrentDate.Month);
            }
        }

        public int WorkHoursPerDay
        {
            get
            {
                return 8;
            }
        }

        public enum EnglishMonths
        {
            January, February, March, April, May, June, July, August, September, October, November, December
        }

        public string CurrentMonthName
        {
            get
            {
                int currentMonth = DateTime.Now.Month;

                if (Enum.IsDefined(typeof(EnglishMonths), currentMonth - 1))
                {
                    return ((EnglishMonths)(currentMonth - 1)).ToString();
                }
                else
                {
                    return "Unknown";
                }
            }
        }

        public int Workdays
        {
            get
            {
                return CountWorkdaysInMonth(this.CurrentDate.Year, this.CurrentDate.Month);
            }
        }

        public TimeSpan TotalWorkHours { get; private set; }

        public TimeSpan TotalWorkDays
        {
            get
            {
                return CalculateTotalWorkHoursInMonth(this.CurrentDate.Year, this.CurrentDate.Month, this.WorkHoursPerDay);
            }
        }

        public TimeSpan TotalWorkedTime { get; set; }

        public TimeSpan EightHours
        {
            get
            {
                return TimeSpan.FromHours(8);
            }
        }

        public string CalculateTotalWorkHoursInMonthFormatted2
        {
            get
            {
                return CalculateTotalWorkHoursInMonthFormatted(this.CurrentDate.Year, this.CurrentDate.Month, this.WorkHoursPerDay);
            }
        }

        public string TotalTimeFormatted { get; private set; }

        public string SumAssesment
        {
            get
            {
                double hoursInMonth = this.TotalWorkDays.TotalHours;

                switch (this.TotalWorkedTime.TotalHours)
                {
                    case double hours when hours == hoursInMonth:
                        return "Strange situation. Think Why";

                    case var hours when hours >= 0 && hours < 25:
                        return "He's overdoing it quite a bit, try to bite him with a phone that doesn't work enough";

                    case var hours when hours >= 25 && hours < 125:
                        return "Can be, don't pick on him";

                    case var hours when hours >= 125 && hours < 400:
                        return "A strange guy, I don't think he has any family, only a cat probably";

                    case var hours when hours >= 400 && hours < 300:
                        return "Space-time has bent..";

                    default:
                        throw new Exception("Something went wrong...");
                }
            }
        }

        public TimeSpan Difference { get; private set; }

        public List<TimeSpan> EveryDayResult { get; set; }

        public TimeSpan Day { get; private set; }

        public Statistics()
        {
            this.TotalWorkedTime = TimeSpan.Zero;
            this.TotalWorkHours = TimeSpan.Zero;
            this.Day = TimeSpan.Zero;
            this.Difference = TimeSpan.Zero;
            this.EveryDayResult = new List<TimeSpan>();
        }

        public void AddTimeDifference(DateTime newTime1, DateTime newTime2)
        {
            Difference = newTime2 - newTime1;
            this.EveryDayResult.Add(Difference);
        }

        public void AddCalculated24h(DateTime newTime1, DateTime newTime2)
        {
            Day = (TimeSpan.FromHours(24) - (newTime1 - newTime2));
            this.EveryDayResult.Add(Day);
        }

        public static TimeSpan CalculateTotalWorkHoursInMonth(int year, int month, int workHoursPerDay)
        {
            int totalWorkdays = CountWorkdaysInMonth(year, month);
            return TimeSpan.FromHours(totalWorkdays * workHoursPerDay);
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

        public static bool IsWorkday(DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
        }

        public static string CalculateTotalWorkHoursInMonthFormatted(int year, int month, int workHoursPerDay)
        {
            int totalWorkdays = CountWorkdaysInMonth(year, month);
            int totalHours = totalWorkdays * workHoursPerDay;
            return $"{totalHours} hours";
        }
    }
}
