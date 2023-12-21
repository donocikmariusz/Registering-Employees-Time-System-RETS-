
using System;

namespace RETS
{
    public class ManufacturingWorker : WorkerBase, IRets
    {
        private const string fileName = "times.txt";

        private readonly TimeSpan eightHours = TimeSpan.FromHours(8);
        public ManufacturingWorker(string intime, string outtime) : base(intime, outtime)
        {
            this.Intime = intime;
            this.Outtime = outtime;
        }
        public string Intime { get; private set; }
        public string Outtime { get; private set; }
        public TimeSpan Difference { get; private set; }
        public TimeSpan Day { get; private set; }

        public override void AddCalculated24h(DateTime newTime1, DateTime newTime2)
        {
            Day = (TimeSpan.FromHours(24) - (newTime1 - newTime2));
            EveryDayResult.Add(Day);
            this.OnTimeAdded();
            using (var writer = File.AppendText(fileName))
            {
                writer.WriteLine($"{Day.Hours}:{Day.Minutes:D2}");
            }
        }
        public override void AddTimeDifference(DateTime newTime1, DateTime newTime2)
        {
            Difference = newTime2 - newTime1;
            EveryDayResult.Add(Difference);
            this.OnTimeAdded();

            using (var writer = File.AppendText(fileName))
            {
                writer.WriteLine($"{Difference.Hours}:{Difference.Minutes:D2}");
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

        public override void EveryDaySummary()
        {
            var timesFromFile = this.ReadTimesFromFile();
            Console.WriteLine("Statystki z każdego dnia:");
            Console.WriteLine("");
            for (int i = 0; i < timesFromFile.Count; i++)
            {
                if (eightHours < timesFromFile[i])
                {
                    TimeSpan overtime = timesFromFile[i] - eightHours;
                    Console.WriteLine($"Dnia {i + 1} był {Math.Abs(timesFromFile[i].Hours):D2} godzin {Math.Abs(timesFromFile[i].Minutes):D2} minut - nadczas wynosi {overtime.Hours} godzin {overtime.Minutes} minut");
                }
                else if (eightHours > timesFromFile[i])
                {
                    TimeSpan undertime = eightHours - timesFromFile[i];
                    Console.WriteLine($"Dnia {i + 1} był {Math.Abs(timesFromFile[i].Hours):D2} godzin {Math.Abs(timesFromFile[i].Minutes):D2} minut - niedoczas wynosi: {undertime.Hours} godzin {undertime.Minutes} minut");
                }
                else
                {
                    Console.WriteLine($"Dnia {i + 1} był {Math.Abs(timesFromFile[i].Hours):D2} godzin {Math.Abs(timesFromFile[i].Minutes):D2} czyli jest dokładny co do minuty");
                }
            }


        }
    }
}
