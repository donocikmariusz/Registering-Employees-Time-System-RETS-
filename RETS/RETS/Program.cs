
using RETS;


Console.WriteLine("Program do monitorowania czasu pracy pracowników");
Console.WriteLine("------------------------------------------------");
Console.WriteLine("Wybierz pracownika:");
Console.WriteLine("1. - Zwykły parch produkcyjny (pracuje zmianowo)");
// Console.WriteLine("2. - Średni parch do wyzyskiwania Inżynier/Supervisor (pracuje hybrydowo)");
// Console.WriteLine("3. - Super parch naczelny Kierownik/Dyrektor (pracuje zadaniowo");
Console.WriteLine("Twój wybór: (lub 'q' lub 'Q' żeby wyjść)");


while (true)
{
    var wyborParcha = Console.ReadLine();

    if (wyborParcha == "1")
    {
        zwyklyParch.HandleZwyklyParch();
                break;
    }

    else if (wyborParcha == "2")
    {
        sredniParch.HandleSredniParch();
        break;
    }

    else if (wyborParcha == "3")
    {
        naczelnyParch.HandleNaczelnyParch();
        break;
    }

    else if (wyborParcha == "q" || wyborParcha == "Q")
    {
        break;
    }

    else
    {
        Console.WriteLine("Niewłaściwy wybór. Try again");
    }

}





