using System;
using System.Collections.Generic;
using System.IO;

namespace RETS
{
    public class ManufacturingWorker : WorkerBase, IRets
    {
        private const string FileName = "times.txt";

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

            this.OnTimeAdded();
            using (var writer = File.AppendText(FileName))
            {
                writer.WriteLine($"{Day.Hours}:{Day.Minutes:D2}");
            }
        }

        public override void AddTimeDifference(DateTime newTime1, DateTime newTime2)
        {
            Difference = newTime2 - newTime1;

            this.OnTimeAdded();

            using (var writer = File.AppendText(FileName))
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

            if (File.Exists(FileName))
            {
                using (var reader = File.OpenText(FileName))
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
            EveryDaySummaryCommon(timesFromFile);
        }
    }
}
