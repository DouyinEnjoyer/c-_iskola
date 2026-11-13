using System.Globalization;

namespace TeremFoglalo;

// Statikus osztály: nem hozunk létre belőle példányt, csak a szabályokat és a segédmetódusokat tartalmazza.
public static class Szabalyok
{
    // 4. feladat: statikus, csak olvasható beállítások.
    // TODO: Add meg az értékeket: nyitás 8:00, zárás 16:00, egy foglalás legfeljebb 2 órás lehet.
    public static readonly TimeOnly Nyitas = new TimeOnly(8, 0);
    public static readonly TimeOnly Zaras = new TimeOnly(16, 0);
    public static readonly TimeSpan MaxHossz = TimeSpan.FromHours(2);

    // 3. feladat
    // TODO: Szövegből dátumot készít (ÉÉÉÉ-HH-NN alak, pl. 2026-11-03).
    //       Ha nem sikerül, false-t ad vissza, kivétel dobása nélkül.
    //       Tipp: DateOnly.TryParse(szoveg, out datum)
    public static bool TryDatum(string szoveg, out DateOnly datum)
    {
        return DateOnly.TryParse(szoveg, CultureInfo.InvariantCulture, out datum);
    }

    // TODO: Ugyanez időpontra (ÓÓ:PP alak, pl. 08:30), de most gyakorlásképp try-catch blokkal:
    //       a try ágban ido = TimeOnly.Parse(szoveg); és true a visszatérési érték.
    //       FormatException esetén ido = default; és false a visszatérési érték.
    public static bool TryIdo(string szoveg, out TimeOnly ido)
    {
        try
        {
            ido = TimeOnly.Parse(szoveg, CultureInfo.InvariantCulture);
            return true;
        }
        catch (FormatException)
        {
            ido = default;
            return false;
        }
    }

    // 4. feladat
    // TODO: Ellenőrzi a szabályokat, és az első megsértettnél FoglalasiHibaException-t dob
    //       a megfelelő kóddal. A sorrend a feladatleírás táblázatában van (idointervallum, hetvege, ...).
    public static void Ellenoriz(DateOnly datum, TimeOnly kezdet, TimeOnly veg)
    {
        if (veg <= kezdet)
        {
            throw new FoglalasiHibaException("idointervallum");
        }

        if (datum.DayOfWeek == DayOfWeek.Saturday || datum.DayOfWeek == DayOfWeek.Sunday)
        {
            throw new FoglalasiHibaException("hetvege");
        }

        if (kezdet < Nyitas || veg > Zaras)
        {
            throw new FoglalasiHibaException("nyitvatartas");
        }

        if (veg - kezdet > MaxHossz)
        {
            throw new FoglalasiHibaException("tul_hosszu");
        }
    }
}
