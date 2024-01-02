namespace RETS.Tests
{
    public class Tests
    {

        [SetUp]
        public void Setup()
        {
            var statistics = new Statistics();
            statistics.EveryDayResult = new List<TimeSpan>();
        }

        [Test]
        public void AddTimeDifference_ShouldIncreaseEveryDayResultCount()
        {
            var statistics = new Statistics();
            DateTime newTime1 = new DateTime(2023, 1, 1, 8, 0, 0);
            DateTime newTime2 = new DateTime(2023, 1, 1, 16, 0, 0);

            statistics.AddTimeDifference(newTime1, newTime2);

            Assert.AreEqual(1, statistics.EveryDayResult.Count);
        }

        [Test]
        public void AddCalculated24h_ShouldIncreaseEveryDayResultCount()
        {
            var statistics = new Statistics();
            DateTime newTime1 = new DateTime(2023, 1, 1, 8, 0, 0);
            DateTime newTime2 = new DateTime(2023, 1, 2, 8, 0, 0);

            statistics.AddCalculated24h(newTime1, newTime2);

            Assert.AreEqual(1, statistics.EveryDayResult.Count);
        }

        [Test]
        public void TotalWorkedTime_ShouldBeSumOfEveryDayResult()
        {
            var statistics = new Statistics();
            DateTime startTime1 = new DateTime(2023, 1, 1, 8, 0, 0);
            DateTime endTime1 = new DateTime(2023, 1, 1, 16, 0, 0);
            DateTime startTime2 = new DateTime(2023, 1, 2, 8, 0, 0);
            DateTime endTime2 = new DateTime(2023, 1, 2, 16, 0, 0);

            statistics.AddTimeDifference(startTime1, endTime1);
            statistics.AddTimeDifference(startTime2, endTime2);

            foreach (TimeSpan totalWorkedTime in statistics.EveryDayResult)
            {
                statistics.TotalWorkedTime += totalWorkedTime;
            }

            Assert.AreEqual(16, (int)statistics.TotalWorkedTime.TotalHours);
        }

        [Test]
        public void SumAssessment_ShouldReturnCorrectAssessment()
        {
            var statistics = new Statistics();
            statistics.TotalWorkedTime = TimeSpan.FromHours(40);

            string assessment = statistics.SumAssesment;

            Assert.AreEqual("Can be, don't pick on him", assessment);
        }
    }
}
