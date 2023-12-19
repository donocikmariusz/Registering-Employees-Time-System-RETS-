namespace RETS
{
    public class HybridWorker : WorkerBase, IRets
    {
 
        public override event TimeAddedDelegate TimeAdded;
        private List<TimeSpan> everyDayResult = new List<TimeSpan>();
        private readonly TimeSpan osiemGodzin = TimeSpan.FromHours(8);
        public HybridWorker(string intime, string outtime) : base(intime, outtime)
        {
            this.Intime = intime;
            this.Outtime = outtime;
        }
        public string Intime { get; private set; }
        public string Outtime { get; private set; }

        public override void AddCalculated24h(DateTime newTime1, DateTime newTime2)
        {
            TimeSpan doba = TimeSpan.FromHours(24);
            Doba = (TimeSpan.FromHours(24) - (newTime1 - newTime2));
            everyDayResult.Add(Doba);
            if (TimeAdded != null)
            {
                TimeAdded(this, new EventArgs());
            }
        }
        public override void AddTimeDifference(DateTime newTime1, DateTime newTime2)
        {
            Difference = newTime2 - newTime1;
            everyDayResult.Add(Difference);

            if(TimeAdded != null)
            {
                TimeAdded(this, new EventArgs());
            }
        }

        public override void EveryDaySummary()
        {
            Console.WriteLine("Statystki z każdego dnia:");
            Console.WriteLine("");

            for (int i = 0; i < everyDayResult.Count; i++)
            {
                if (osiemGodzin < everyDayResult[i])
                {
                    TimeSpan overtime = everyDayResult[i] - osiemGodzin;
                    Console.WriteLine($"Dnia {i + 1} był {Math.Abs(everyDayResult[i].Hours):D2} godzin {Math.Abs(everyDayResult[i].Minutes):D2} minut - nadczas wynosi {overtime.Hours} godzin {overtime.Minutes} minut");
                }
                else if (osiemGodzin > everyDayResult[i])
                {
                    TimeSpan undertime = osiemGodzin - everyDayResult[i];
                    Console.WriteLine($"Dnia {i + 1} był {Math.Abs(everyDayResult[i].Hours):D2} godzin {Math.Abs(everyDayResult[i].Minutes):D2} minut - niedoczas wynosi: {undertime.Hours} godzin {undertime.Minutes} minut");
                }
                else
                {
                    Console.WriteLine($"Dnia {i + 1} był {Math.Abs(everyDayResult[i].Hours):D2} godzin {Math.Abs(everyDayResult[i].Minutes):D2} czyli jest dokładny co do minuty");
                }
            }
        }

        public override Statistics GetStatistics()
        {
            var statistics = new Statistics();
            statistics.CurrentDate = DateTime.Now;
            statistics.OsiemGodzin = TimeSpan.FromHours(8);
            int workHoursPerDay = 8;

            statistics.DaysInCurrentMonth = DateTime.DaysInMonth(statistics.CurrentDate.Year, statistics.CurrentDate.Month);
            statistics.CurrentMonthName = DateTime.Now.ToString("MMMM");
            statistics.Workdays = CountWorkdaysInMonth(statistics.CurrentDate.Year, statistics.CurrentDate.Month);
            statistics.TotalWorkDays = CalculateTotalWorkHoursInMonth(statistics.CurrentDate.Year, statistics.CurrentDate.Month, workHoursPerDay);
            statistics.CalculateTotalWorkHoursInMonthFormatted = CalculateTotalWorkHoursInMonthFormatted(statistics.CurrentDate.Year, statistics.CurrentDate.Month, workHoursPerDay);

            foreach (TimeSpan totalWorkedTime in everyDayResult)
            {
                statistics.TotalWorkedTime += totalWorkedTime;
            }

            double ileHwmiesiacu = statistics.TotalWorkDays.TotalHours;

            switch (statistics.TotalWorkedTime.TotalHours)

            {
                case double hours when hours == ileHwmiesiacu:
                    statistics.SumAssesment = "Dziwna bardzo sytuacja. Pomyśl Dlaczego";
                    break;

                case var hours when hours >= 0 && hours < 25:
                    statistics.SumAssesment = "Nieźle przegina, pokąsać go telefonem że za mało chodzi";
                    break;

                case var hours when hours >= 25 && hours < 125:
                    statistics.SumAssesment = "Może być, nie czepiać się go";
                    break;

                case var hours when hours >= 125 && hours < 300:
                    statistics.SumAssesment = "Dziwny gość, chyba nie ma rodziny tylko kota";
                    break;
                default:
                    throw new Exception("Coś poszło nie tak...");
            }
            return statistics;
        }

        public override void ShowStatistics()
        {
            {
                var statistics = GetStatistics();
                Console.WriteLine($"Liczba dni w bieżącym miesiącu: {statistics.DaysInCurrentMonth}");
                Console.WriteLine($"Nazwa obecnego miesiąca: {statistics.CurrentMonthName}");
                Console.WriteLine($"Ilość dni roboczych w bieżącym miesiącu: {statistics.Workdays}");
                Console.WriteLine($"Łączna liczba godzin roboczych w bieżącym miesiącu: {statistics.CalculateTotalWorkHoursInMonthFormatted}");
                Console.WriteLine($"Łączny czas przepracowany: {(int)statistics.TotalWorkedTime.TotalHours} h, {statistics.TotalWorkedTime.Minutes:D2} min");
                Console.WriteLine($"Podsumowanie: {statistics.SumAssesment}");
                Console.WriteLine();
            }
        }

    }
}
