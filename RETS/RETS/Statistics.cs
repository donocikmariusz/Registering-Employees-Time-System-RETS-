
namespace RETS
{
    public class Statistics
    {
        public DateTime CurrentDate { get; set; }
        public int DaysInCurrentMonth { get; set; }
        public string CurrentMonthName { get; set; }
        public int Workdays { get; set; }
        public TimeSpan TotalWorkHours { get; set; }
        public TimeSpan TotalWorkDays { get; set; }
        public TimeSpan TotalWorkedTime { get; set; }
        public TimeSpan OsiemGodzin { get; set; }
        public string CalculateTotalWorkHoursInMonthFormatted { get; set; }
        public string TotalTimeFormatted { get; set; }
        public string SumAssesment { get; set; }

    }
}

