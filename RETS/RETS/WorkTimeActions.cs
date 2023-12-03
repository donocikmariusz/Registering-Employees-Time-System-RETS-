namespace RETS
{
    public class WorkTimeActions
    {
        public static void PerformActionsBasedOnWorkedHours(double totalWorkedHours, double totalWorkHoursInMonth)
        {
            switch (totalWorkedHours)
            {
                case double hours when hours == totalWorkHoursInMonth:
                    Console.WriteLine("Gratulacje! Przepracowałeś dokładnie tyle godzin, ile wynosi limit w miesiącu.");
                
                    break;

                case double hours when hours >= 0 && hours < 25:
                    Console.WriteLine($"Przepracowano od 0 do 24 godzin.");
                    break;

                case double hours when hours >= 25 && hours < 50:
                    Console.WriteLine($"Przepracowano od 25 do 49 godzin.");
                    break;

                case double hours when hours >= 50 && hours < 75:
                    Console.WriteLine($"Przepracowano od 50 do 74 godzin.");
                    break;

                case double hours when hours >= 75 && hours < 100:
                    Console.WriteLine($"Przepracowano od 75 do 99 godzin.");
                    break;

                case double hours when hours >= 100 && hours < 125:
                    Console.WriteLine($"Przepracowano od 100 do 124 godzin.");
                    break;

                case double hours when hours >= 125 && hours < 150:
                    Console.WriteLine($"Przepracowano od 125 do 149 godzin.");
                    break;

                default:
                    Console.WriteLine("Coś poszło nie tak...");
                    break;
            }
        }
    }
}