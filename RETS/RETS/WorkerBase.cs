namespace RETS
{
    public abstract class WorkerBase : IRets
    {
         
        public delegate void TimeAddedDelegate(object sender, EventArgs args);
        public event TimeAddedDelegate TimeAdded;
        public WorkerBase(string intime, string outtime)
        {
            this.Intime = intime;
            this.Outtime = outtime;
        }
        public string Intime { get; private set; }
        public string Outtime { get; private set; }
        public TimeSpan Difference { get; private set; }
        public TimeSpan Day { get; private set; }

        public abstract void AddCalculated24h(DateTime newTime1, DateTime newTime2);
        public abstract void AddTimeDifference(DateTime newTime1, DateTime newTime2);

        public void ShowStatistics()
        {
            {
                var stat = GetStatistics();
        
                    Console.WriteLine($"Number of days in the current month: {stat.DaysInCurrentMonth}");
                    Console.WriteLine($"The name of the current month: {stat.CurrentMonthName.ToUpper()}");
                    Console.WriteLine($"Number of working days in the current month: {stat.Workdays}");
                    Console.WriteLine($"Total number of working hours in the current month: {stat.CalculateTotalWorkHoursInMonthFormatted2}");
                    Console.WriteLine($"Total time worked: {(int)stat.TotalWorkedTime.TotalHours} h, {stat.TotalWorkedTime.Minutes:D2} min");
                    Console.WriteLine($"Summary: {stat.SumAssesment}");
                    Console.WriteLine();
            }
        }

        public abstract Statistics GetStatistics();
        public abstract void EveryDaySummary();

        protected void OnTimeAdded()
        {
            if (TimeAdded != null)
            {
                TimeAdded(this, new EventArgs());
            }
        }
    }
}
