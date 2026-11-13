namespace TeremFoglalo;

// 1. feladat: saját kivételtípus.
// Ezt a kivételt fogjuk dobni, ha egy foglalás nem teljesíthető.
// TODO: Egészítsd ki az osztályt:
//   - legyen egy csak olvasható Kod (string) tulajdonsága, pl. "utkozes" vagy "hetvege";
//   - legyen egy konstruktora, ami a kódot kapja paraméterként, eltárolja a Kod-ban,
//     és a base(...) hívással üzenetet is ad a kivételnek.
public class FoglalasiHibaException : Exception
{
    public string Kod { get; }
    public FoglalasiHibaException(string kod): base(kod)
    {
        Kod = kod;
    }
}
