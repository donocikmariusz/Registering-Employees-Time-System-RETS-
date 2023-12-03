namespace RETS
{
    public class Parameters : CalculateTime
    {
        public static void GetDaysInCurrentMonth()
        {
            // Pobierz obecną datę
            DateTime currentDate = DateTime.Now;

            // Pobierz liczbę dni w obecnym miesiącu
            int daysInCurrentMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
            Console.WriteLine($"Liczba dni w bieżącym miesiącu: {daysInCurrentMonth}");
        }

        public static void GetCurrentMonthName()
        {
            // Pobierz obecną datę
            DateTime currentDate = DateTime.Now;

            // Pobierz nazwę obecnego miesiąca
            string currentMonthName = currentDate.ToString("MMMM");
            Console.WriteLine($"Nazwa obecnego miesiąca: {currentMonthName}");
        }

        public static void showSummary()

        {
            Parameters.GetCurrentMonthName();
            Parameters.GetDaysInCurrentMonth();

        }
    }

}
