namespace TeremFoglalo;

public class Foglalo
{
    private readonly List<Foglalas> foglalasok = new List<Foglalas>();

    // 3. feladat
    // TODO: Szövegekből (Szabalyok.TryDatum, TryIdo) dátumot és időpontokat készít, létrehoz egy
    //       Foglalas-t, eltárolja a listában, és visszaadja.
    //       Ha valamelyik szöveg hibás: throw new FoglalasiHibaException("formatum");
    //       4. feladat: a létrehozás előtt hívd meg a Szabalyok.Ellenoriz-t.
    //       5. feladat: a létrehozás előtt nézd meg, ütközik-e valamelyik meglévő foglalással
    //       ("utkozes"). A hibás kérés ne használjon el azonosítót!
    public Foglalas Foglal(string terem, string datumSzoveg, string kezdetSzoveg, string vegSzoveg)
    {
        if (!Szabalyok.TryDatum(datumSzoveg, out DateOnly datum) ||
           !Szabalyok.TryIdo(kezdetSzoveg, out TimeOnly kezdet) ||
           !Szabalyok.TryIdo(vegSzoveg, out TimeOnly veg))
        {
            throw new FoglalasiHibaException("formatum");
        }
        Szabalyok.Ellenoriz(datum, kezdet, veg);
        foreach (Foglalas f in foglalasok)
        {
            if (f.Utkozik(terem, datum, kezdet, veg))
            {
                throw new FoglalasiHibaException("utkozes");
            }
        }

        Foglalas ujFoglalas = new Foglalas(terem, datum, kezdet, veg);
        foglalasok.Add(ujFoglalas);
        return ujFoglalas;
    }

    // 3. feladat
    // TODO: Visszaadja az adott terem adott napi foglalásait kezdési időpont szerint rendezve.
    //       Hibás dátumszövegre: "formatum" kódú FoglalasiHibaException.
    public List<Foglalas> Lista(string terem, string datumSzoveg)
    {
        if (!Szabalyok.TryDatum(datumSzoveg, out DateOnly datum))
        {
            throw new FoglalasiHibaException("formatum");
        }

        return foglalasok
            .Where(f => f.Terem == terem && f.Datum == datum)
            .OrderBy(f => f.Kezdet)
            .ToList();
    }

    // 6. feladat
    // TODO: Az adott terem adott napi foglalásainak összesített hossza.
    public TimeSpan OsszIdo(string terem, string datumSzoveg)
    {
        List<Foglalas> napiFoglalasok = Lista(terem, datumSzoveg);
        TimeSpan osszeg = TimeSpan.Zero;

        foreach (Foglalas f in napiFoglalasok)
        {
            osszeg += f.Hossz;
        }

        return osszeg;
    }
}
