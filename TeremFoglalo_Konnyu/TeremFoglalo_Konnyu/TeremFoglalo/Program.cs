namespace TeremFoglalo;

class Program
{
    static void Main()
    {
        Foglalo foglalo = new Foglalo();

        // A parancsokat soronként olvassuk a bemenetről, amíg el nem fogynak.
        string sor;
        while ((sor = Console.ReadLine()) != null)
        {
            string[] reszek = sor.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (reszek.Length == 0)
            {
                continue; // az üres sort kihagyjuk
            }

            // 4. feladat: kivételkezelés.
            // TODO: Tedd az alábbi if-else láncot try-catch blokkba, és FoglalasiHibaException esetén
            //       írd ki: HIBA <kod>   (pl. HIBA utkozes)
            try
            {
                if (reszek[0] == "FOGLAL" && reszek.Length == 5)
                {
                    // 3. feladat
                    // TODO: Hívd a foglalo.Foglal(...)-t a négy paraméterrel (terem, dátum, kezdet, vég),
                    //       és írd ki: OK <azonosito>   (pl. OK 1)
                    Foglalas uj = foglalo.Foglal(reszek[1], reszek[2], reszek[3], reszek[4]);
                    Console.WriteLine($"OK {uj.Id}");
                }
                else if (reszek[0] == "LISTA" && reszek.Length == 3)
                {
                    // 3. feladat
                    // TODO: Írd ki soronként a foglalo.Lista(...) elemeit, vagy NINCS-et, ha üres a lista.
                    List<Foglalas> lista = foglalo.Lista(reszek[1], reszek[2]);
                    if (lista.Count == 0)
                    {
                        Console.WriteLine("NINCS");
                    }
                    else
                    {
                        foreach (Foglalas f in lista)
                        {
                            Console.WriteLine(f);
                        }
                    }
                }
                else if (reszek[0] == "OSSZIDO" && reszek.Length == 3)
                {
                    // 6. feladat
                    // TODO: Írd ki a foglalo.OsszIdo(...) eredményét óra:perc alakban, pl. 2:15 (vagy 0:00).
                    //       Tipp: TimeSpan formázása: ido.ToString(@"h\:mm")
                    TimeSpan ossz = foglalo.OsszIdo(reszek[1], reszek[2]);
                    Console.WriteLine(ossz.ToString(@"h\:mm"));
                }
                else
                {
                    Console.WriteLine("HIBA formatum");
                }
            }
            catch (FoglalasiHibaException ex)
            {
                Console.WriteLine($"HIBA {ex.Kod}");
            }
        }
    }
}
