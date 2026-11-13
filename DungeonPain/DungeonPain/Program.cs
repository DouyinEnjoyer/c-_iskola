namespace DungeonPain;

// Mielőtt bármihez nyúlsz, olvasd el a README.md fájlt!
class Program
{
    static void Main()
    {
        // Ugyanazt a kazamatát végigjátssza három különböző hős.
        List<Hero> heroes = new List<Hero>();
        heroes.Add(HeroFactory.Create("Aron", HeroClass.Warrior));
        heroes.Add(HeroFactory.Create("Bela", HeroClass.Mage));
        heroes.Add(HeroFactory.Create("Csilla", HeroClass.Thief));

        foreach (Hero hero in heroes)
        {
            Console.WriteLine("==================================================");
            Console.WriteLine(HeroFactory.Describe(hero));
            Dungeon.Run(hero);
            Console.WriteLine();
        }

        // ---- Összegzés: ezt a részt NE írd át! A kimenetet ezzel ellenőrizzük. ----
        Console.WriteLine("==================== SUMMARY ====================");
        foreach (Hero h in heroes)
        {
            string state = h.Hp > 0 ? "alive" : "FALLEN";
            Console.WriteLine(h.Name.PadRight(7)
                + "HP " + Math.Max(0, h.Hp) + "/" + h.MaxHp
                + "  ATK " + h.Attack
                + "  DEF " + h.Defense
                + "  Gold " + h.Gold
                + "  XP " + h.Xp
                + "  Defeated " + h.Defeated
                + "  Fled " + h.Fled
                + "  " + state);
        }
    }
}
