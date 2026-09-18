namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Kurzus kurzus = new Kurzus();
            Hallgato hallgato1 = new Hallgato();
            hallgato1.Atlag1 = 1;
            hallgato1.Nev1 = "a";
            Hallgato hallgato2 = new Hallgato();
            hallgato2.Atlag1 = 4;
            hallgato2.Nev1 = "b";
            kurzus.Felvesz(hallgato1);
            kurzus.Felvesz(hallgato2);
            kurzus.Listaz();


        }
    }
}
