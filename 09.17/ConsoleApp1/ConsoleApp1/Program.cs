namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Konyv konyv1 = new Konyv("Nichomadian ethics","Socrates", 300);
            Konyv konyv2 = new Konyv("Meditations", "Marcus Aurelius", 250);
            konyv1.Bemutat();
            konyv2.Bemutat();
           
            Mozijegy jegy = new Mozijegy(4);

            Doboz d1 = new Doboz();
            d1.Tartalom = 10;
            Doboz d2 = d1;
            d2.Tartalom = 99;
            Console.WriteLine(d1.Tartalom);

            int szam1 = 10;
            int szam2 = szam1;
            szam2 = 99;
            Console.WriteLine(szam1);

            Rendeles elso = new Rendeles("a");
            Rendeles masodik = new Rendeles("b");

            Console.WriteLine(Rendeles.OsszesRendeles);

        }
    }
}
