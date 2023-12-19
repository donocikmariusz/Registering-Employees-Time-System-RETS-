
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

        public string CurrentMonthName
        {
            get
            {
                return DateTime.Now.ToString("MMMM");
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

        public TimeSpan OsiemGodzin
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
                double ileHwmiesiacu = this.TotalWorkDays.TotalHours;

                switch (this.TotalWorkedTime.TotalHours)

                {
                    case double hours when hours == ileHwmiesiacu:
                        return "Dziwna bardzo sytuacja. Pomyśl Dlaczego";

                    case var hours when hours >= 0 && hours < 25:
                        return "Nieźle przegina, pokąsać go telefonem że za mało chodzi";

                    case var hours when hours >= 25 && hours < 125:
                        return "Może być, nie czepiać się go";

                    case var hours when hours >= 125 && hours < 300:
                        return "Dziwny gość, chyba nie ma rodziny tylko kota";

                    default:
                        throw new Exception("Coś poszło nie tak...");
                }
            }
        }
        public TimeSpan Difference { get; private set; }
        public List<TimeSpan> EveryDayResult { get; private set; }
        public TimeSpan Doba { get; private set; }

        public Statistics()
        {
            this.TotalWorkedTime = TimeSpan.Zero;
            this.TotalWorkHours = TimeSpan.Zero;
            this.Doba = TimeSpan.Zero;
            this.Difference = TimeSpan.Zero;
        }

        public void AddTimeDifference(DateTime newTime1, DateTime newTime2)
        {
            Difference = newTime2 - newTime1;
            this.EveryDayResult.Add(Difference);
        }

        public void AddCalculated24h(DateTime newTime1, DateTime newTime2)
        {
            Doba = (TimeSpan.FromHours(24) - (newTime1 - newTime2));
            this.EveryDayResult.Add(Doba);
        }

        // obliczenie ile godzin roboczych jest w danym miesiącu na podstawie jego ilości dni roboczych
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

