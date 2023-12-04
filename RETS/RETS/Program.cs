
using RETS;


Console.WriteLine("Program do monitorowania czasu pracy pracowników");
Console.WriteLine("------------------------------------------------");
Console.WriteLine("Wybierz pracownika:");
Console.WriteLine("1. - Zwykły produkcyjny (pracuje zmianowo)");
Console.WriteLine("2. - Średni do wyzyskiwania Inżynier/Supervisor (pracuje hybrydowo)");
Console.WriteLine("3. - Super naczelny Kierownik/Dyrektor (pracuje zadaniowo)");
Console.WriteLine("Twój wybór: (lub 'q' lub 'Q' żeby wyjść)");

List<TimeSpan> everyDayResult = new List<TimeSpan>();

while (true)
{
    var wybor = Console.ReadLine();

    if (wybor == "1")
    {
        zwykly.HandleZwykly(everyDayResult);
            break;
    }

    else if (wybor == "2")
    {
        sredni.HandleSredni(everyDayResult);
        break;
    }

    else if (wybor == "3")
    {
        naczelny.HandleNaczelny(everyDayResult);
        break;
    }

    else if (wybor == "q" || wybor == "Q")
    {
        break;
    }

    else
    {
        Console.WriteLine("Niewłaściwy wybór. Try again");
    }

}





