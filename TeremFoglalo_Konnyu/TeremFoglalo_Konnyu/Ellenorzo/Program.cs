using System.Diagnostics;
using System.Text;

namespace Ellenorzo;

// Ez az ellenőrző program. Nem kell módosítanod.
// Minden forgatókönyvhöz (Forgatokonyvek mappa) elindítja a TeremFoglalo programot,
// beadja neki a *.be.txt parancsait, és összeveti a kimenetét a *.ki.txt elvárt kimenettel.
class Program
{
    static int Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        string gyoker = MappaKeres(AppContext.BaseDirectory, "Forgatokonyvek");
        if (gyoker == null)
        {
            Console.WriteLine("Nem találom a Forgatokonyvek mappát.");
            return 2;
        }

        string dll = args.Length > 0 ? args[0] : ProgramKeres(gyoker);
        if (dll == null || !File.Exists(dll))
        {
            Console.WriteLine("Nem találom a lefordított TeremFoglalo programot.");
            Console.WriteLine("Fordítsd le a megoldást (Build > Build Solution), majd indítsd újra az ellenőrzőt.");
            return 2;
        }

        Console.WriteLine("Ellenőrzött program: " + dll);
        Console.WriteLine();

        string[] bemenetek = Directory.GetFiles(Path.Combine(gyoker, "Forgatokonyvek"), "*.be.txt");
        Array.Sort(bemenetek);

        int kotelezoOsszes = 0;
        int kotelezoJo = 0;
        int bonuszOsszes = 0;
        int bonuszJo = 0;

        foreach (string bemenet in bemenetek)
        {
            string nev = Path.GetFileName(bemenet).Replace(".be.txt", "");
            bool bonusz = nev.StartsWith("B");
            string[] vart = File.ReadAllLines(bemenet.Replace(".be.txt", ".ki.txt"));

            string hiba = Futtat(dll, bemenet, vart);

            if (bonusz)
            {
                bonuszOsszes++;
            }
            else
            {
                kotelezoOsszes++;
            }

            if (hiba == null)
            {
                Console.WriteLine("[OK]    " + nev);
                if (bonusz)
                {
                    bonuszJo++;
                }
                else
                {
                    kotelezoJo++;
                }
            }
            else
            {
                Console.WriteLine("[ROSSZ] " + nev);
                Console.WriteLine("        " + hiba.Replace("\n", "\n        "));
            }
        }

        Console.WriteLine();
        Console.WriteLine("Kötelező forgatókönyvek: " + kotelezoJo + "/" + kotelezoOsszes);
        if (bonuszOsszes > 0)
        {
            Console.WriteLine("Bónusz forgatókönyvek:   " + bonuszJo + "/" + bonuszOsszes);
        }

        return kotelezoJo == kotelezoOsszes ? 0 : 1;
    }

    // Lefuttatja a programot egy forgatókönyvvel. Null a visszatérési érték, ha minden egyezik,
    // különben a hiba leírása.
    static string Futtat(string dll, string bemenetFajl, string[] vart)
    {
        ProcessStartInfo inditas = new ProcessStartInfo("dotnet", "\"" + dll + "\"")
        {
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            StandardInputEncoding = new UTF8Encoding(false)
        };

        using Process folyamat = Process.Start(inditas);
        Task<string> kimenetFeladat = folyamat.StandardOutput.ReadToEndAsync();
        Task<string> hibaFeladat = folyamat.StandardError.ReadToEndAsync();

        try
        {
            folyamat.StandardInput.Write(File.ReadAllText(bemenetFajl));
            folyamat.StandardInput.Close();
        }
        catch (IOException)
        {
            // A program már leállt, mielőtt az összes parancsot megkapta volna. Lent kiderül, miért.
        }

        if (!folyamat.WaitForExit(5000))
        {
            folyamat.Kill(true);
            return "A program 5 másodperc alatt sem fejeződött be (végtelen ciklus?).";
        }

        string[] kapott = Sorok(kimenetFeladat.Result);
        string[] elvart = Sorok(string.Join("\n", vart));
        StringBuilder uzenet = new StringBuilder();

        if (folyamat.ExitCode != 0)
        {
            string elsoSor = hibaFeladat.Result.Split('\n')[0].Trim();
            uzenet.AppendLine("A program összeomlott: " + elsoSor);
        }

        int hossz = Math.Max(kapott.Length, elvart.Length);
        for (int i = 0; i < hossz; i++)
        {
            string v = i < elvart.Length ? elvart[i] : "(nincs több sor)";
            string k = i < kapott.Length ? kapott[i] : "(nincs több sor)";
            if (v != k)
            {
                uzenet.AppendLine((i + 1) + ". kimeneti sor: várt: " + v + " | kapott: " + k);
                break;
            }
        }

        return uzenet.Length == 0 ? null : uzenet.ToString().TrimEnd();
    }

    // Sorokra bontja a szöveget: a sorvégi szóközöket és a záró üres sorokat figyelmen kívül hagyja.
    static string[] Sorok(string szoveg)
    {
        List<string> sorok = szoveg.Replace("\r\n", "\n").Split('\n').Select(s => s.TrimEnd()).ToList();
        while (sorok.Count > 0 && sorok[sorok.Count - 1] == "")
        {
            sorok.RemoveAt(sorok.Count - 1);
        }
        return sorok.ToArray();
    }

    // Felfelé haladva keresi azt a mappát, amelyben megvan a keresett almappa.
    static string MappaKeres(string kezdo, string almappa)
    {
        DirectoryInfo mappa = new DirectoryInfo(kezdo);
        while (mappa != null)
        {
            if (Directory.Exists(Path.Combine(mappa.FullName, almappa)))
            {
                return mappa.FullName;
            }
            mappa = mappa.Parent;
        }
        return null;
    }

    // A legutóbb lefordított TeremFoglalo.dll megkeresése.
    static string ProgramKeres(string gyoker)
    {
        string bin = Path.Combine(gyoker, "TeremFoglalo", "bin");
        if (!Directory.Exists(bin))
        {
            return null;
        }
        return Directory.GetFiles(bin, "TeremFoglalo.dll", SearchOption.AllDirectories)
            .OrderByDescending(File.GetLastWriteTime)
            .FirstOrDefault();
    }
}
