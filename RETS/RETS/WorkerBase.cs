namespace RETS
{
    public abstract class WorkerBase : IRets
    {
        private readonly TimeSpan EightHours = TimeSpan.FromHours(8);

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
            var stat = GetStatistics();

            Console.WriteLine($"Number of days in the current month: {stat.DaysInCurrentMonth}");
            Console.WriteLine($"The name of the current month: {stat.CurrentMonthName.ToUpper()}");
            Console.WriteLine($"Number of working days in the current month: {stat.Workdays}");
            Console.WriteLine($"Total number of working hours in the current month: {stat.CalculateTotalWorkHoursInMonthFormatted2}");
            Console.WriteLine($"Total time worked: {(int)stat.TotalWorkedTime.TotalHours} h, {stat.TotalWorkedTime.Minutes:D2} min");
            Console.WriteLine($"Summary: {stat.SumAssesment}");
            Console.WriteLine();
        }

        public abstract Statistics GetStatistics();

        public abstract void EveryDaySummary();

        protected void EveryDaySummaryCommon(List<TimeSpan> timeList)
        {
            Console.WriteLine("Every day statistics:");
            Console.WriteLine("");

            for (int i = 0; i < timeList.Count; i++)
            {
                ConsoleColor textColor;

                if (EightHours < timeList[i])
                {
                    TimeSpan overtime = timeList[i] - EightHours;
                    textColor = ConsoleColor.Green;
                    Console.ForegroundColor = textColor;
                    Console.WriteLine($"Day {i + 1} was {Math.Abs(timeList[i].Hours):D2} hours {Math.Abs(timeList[i].Minutes):D2} minutes - overtime value {overtime.Hours} hours {overtime.Minutes} minutes");
                }
                else if (EightHours > timeList[i])
                {
                    TimeSpan undertime = EightHours - timeList[i];
                    textColor = ConsoleColor.Red;
                    Console.ForegroundColor = textColor;
                    Console.WriteLine($"Day {i + 1} was {Math.Abs(timeList[i].Hours):D2} hours {Math.Abs(timeList[i].Minutes):D2} minutes - undertime value: {undertime.Hours} hours {undertime.Minutes} minutes");
                }
                else
                {
                    textColor = ConsoleColor.Green;
                    Console.ForegroundColor = textColor;
                    Console.WriteLine($"Day {i + 1} was {Math.Abs(timeList[i].Hours):D2} hours {Math.Abs(timeList[i].Minutes):D2} it is accurate down to the minute");
                }

                Console.ResetColor();
            }
        }

        protected void OnTimeAdded()
        {
            if (TimeAdded != null)
            {
                TimeAdded(this, new EventArgs());
            }
        }
    }
}
