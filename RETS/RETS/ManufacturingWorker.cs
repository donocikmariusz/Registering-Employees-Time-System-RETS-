
using System;

namespace RETS
{
    public class ManufacturingWorker : WorkerBase, IRets
    {
        private const string fileName = "times.txt";
        public override event TimeAddedDelegate TimeAdded;

        private List<TimeSpan> everyDayResult = new List<TimeSpan>();
        private readonly TimeSpan osiemGodzin = TimeSpan.FromHours(8);
        public ManufacturingWorker(string intime, string outtime) : base(intime, outtime)
        {
            this.Intime = intime;
            this.Outtime = outtime;
        }
        public string Intime { get; private set; }
        public string Outtime { get; private set; }
        public TimeSpan Difference { get; private set; }
        public TimeSpan Doba { get; private set; }

        public override void AddCalculated24h(DateTime newTime1, DateTime newTime2)
        {
            Doba = (TimeSpan.FromHours(24) - (newTime1 - newTime2));
            everyDayResult.Add(Doba);

            if (TimeAdded != null)
            {
                TimeAdded(this, new EventArgs());
            }

            using (var writer = File.AppendText(fileName))
            {
                writer.WriteLine($"{Doba.Hours}:{Doba.Minutes:D2}");
            }
        }
        public override void AddTimeDifference(DateTime newTime1, DateTime newTime2)
        {
            Difference = newTime2 - newTime1;
            everyDayResult.Add(Difference);


            if (TimeAdded != null)
            {
                TimeAdded(this, new EventArgs());
            }

            using (var writer = File.AppendText(fileName))
            {
                writer.WriteLine($"{Difference.Hours}:{Difference.Minutes:D2}");
            }
        }
        public override void EveryDaySummary()
        {
            var timesFromFile = this.ReadTimesFromFile();
            Console.WriteLine("Statystki z każdego dnia:");
            Console.WriteLine("");

            for (int i = 0; i < timesFromFile.Count; i++)
            {
                if (osiemGodzin < timesFromFile[i])
                {
                    TimeSpan overtime = timesFromFile[i] - osiemGodzin;
                    Console.WriteLine($"Dnia {i + 1} był {Math.Abs(timesFromFile[i].Hours):D2} godzin {Math.Abs(timesFromFile[i].Minutes):D2} minut - nadczas wynosi {overtime.Hours} godzin {overtime.Minutes} minut");
                }
                else if (osiemGodzin > timesFromFile[i])
                {
                    TimeSpan undertime = osiemGodzin - timesFromFile[i];
                    Console.WriteLine($"Dnia {i + 1} był {Math.Abs(timesFromFile[i].Hours):D2} godzin {Math.Abs(timesFromFile[i].Minutes):D2} minut - niedoczas wynosi: {undertime.Hours} godzin {undertime.Minutes} minut");
                }
                else
                {
                    Console.WriteLine($"Dnia {i + 1} był {Math.Abs(timesFromFile[i].Hours):D2} godzin {Math.Abs(timesFromFile[i].Minutes):D2} czyli jest dokładny co do minuty");
                }
            }
        }

        public override Statistics GetStatistics()
        {
            var timesFromFile = this.ReadTimesFromFile();
            var result = this.CountStatistics(timesFromFile);
            return result;
        }

        private List<TimeSpan> ReadTimesFromFile()
        {
            var times = new List<TimeSpan>();

            if (File.Exists(fileName))
            {
                using (var reader = File.OpenText(fileName))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        var parts = line.Split(':');
                        if (parts.Length == 2 && int.TryParse(parts[0], out int hours) && int.TryParse(parts[1], out int minutes))
                        {
                            var timeSpan = new TimeSpan(hours, minutes, 0);
                            times.Add(timeSpan);
                        }
                        line = reader.ReadLine();
                    }
                }
            }
            return times;
        }

        public Statistics CountStatistics(List<TimeSpan> times)
        {
            var statistics = new Statistics();

            foreach (TimeSpan totalWorkedTime in times)
            {
                statistics.TotalWorkedTime += totalWorkedTime;
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
                Console.WriteLine($"Łączna liczba godzin roboczych w bieżącym miesiącu: {statistics.CalculateTotalWorkHoursInMonthFormatted2}");
                Console.WriteLine($"Łączny czas przepracowany: {(int)statistics.TotalWorkedTime.TotalHours} h, {statistics.TotalWorkedTime.Minutes:D2} min");
                Console.WriteLine($"Podsumowanie: {statistics.SumAssesment}");
                Console.WriteLine();
            }
        }
    }
}
