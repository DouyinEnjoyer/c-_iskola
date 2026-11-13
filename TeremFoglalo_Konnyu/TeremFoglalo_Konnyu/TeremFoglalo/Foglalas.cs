using System.Globalization;

namespace TeremFoglalo;

public class Foglalas
{
    // 2. feladat: statikus számláló.
    // TODO: Vegyél fel egy privát statikus mezőt (pl. kovetkezoId), ami a következő kiosztandó
    //       azonosítót tárolja. Az első foglalás azonosítója 1 legyen.

    public int Id { get; }
    public string Terem { get; }
    public DateOnly Datum { get; }
    public TimeOnly Kezdet { get; }
    public TimeOnly Veg { get; }
    private static int kovetkezoId = 1;

    // TODO: A foglalás hossza (TimeSpan): a Veg és a Kezdet különbsége.
    public TimeSpan Hossz => Veg - Kezdet;
    

    public Foglalas(string terem, DateOnly datum, TimeOnly kezdet, TimeOnly veg)
    {
        // TODO: Állítsd be a tulajdonságokat. Az Id a statikus számlálóból jöjjön,
        //       és a számláló utána nőjön eggyel.
        Id = kovetkezoId++;
        Terem = terem;
        Datum = datum;
        Kezdet = kezdet;
        Veg = veg;
    }

    // TODO (5. feladat): Igaz, ha az itt megadott idősáv ütközik ezzel a foglalással,
    //       vagyis azonos a terem és a nap, és az idősávok átfedik egymást.
    //       Ha az egyik pont akkor ér véget, amikor a másik kezdődik, az NEM ütközés.
    public bool Utkozik(string terem, DateOnly datum, TimeOnly kezdet, TimeOnly veg)
    {
        return Terem == terem && Datum == datum && kezdet < Veg && Kezdet < veg;
    }

    // TODO: A foglalás szöveges alakja: azonosító, szóköz, kezdet-vég, pl. "1 08:00-09:30".
    //       Tipp: a TimeOnly.ToString("HH:mm", CultureInfo.InvariantCulture) pontosan ezt a formát adja
    //       (fent kell hozzá a using System.Globalization; sor).
    public override string ToString()
    {
        string kezdetStr = Kezdet.ToString("HH:mm", CultureInfo.InvariantCulture);
        string vegStr = Veg.ToString("HH:mm", CultureInfo.InvariantCulture);
        return $"{Id} {kezdetStr}-{vegStr}";

    }
}
