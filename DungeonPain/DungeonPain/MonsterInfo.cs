namespace DungeonPain;

// Szörnyekkel kapcsolatos "segédfüggvények": mindegyik a szörny fajtája szerint ágazik el.
static class MonsterInfo
{
    // A szörny jele a naplóban.
    public static char GetSymbol(Monster m)
    {
        switch (m.Type)
        {
            case MonsterType.Goblin: return 'g';
            case MonsterType.Orc: return 'O';
            case MonsterType.Skeleton: return 's';
            case MonsterType.Slime: return '~';
            case MonsterType.Dragon: return 'D';
            case MonsterType.Troll: return 'T';
            default: return '?';
        }
    }

    // Rövid leírás a szörnyről.
    public static string Describe(Monster m)
    {
        switch (m.Type)
        {
            case MonsterType.Goblin:
                return "A sneaky goblin. Steals gold when it hits.";
            case MonsterType.Orc:
                return "A brutal orc. Gets enraged when badly hurt.";
            case MonsterType.Skeleton:
                return "A rattling skeleton. Its bones shatter under blunt weapons.";
            case MonsterType.Slime:
                return "A gooey slime. Its acid corrodes armor.";
            case MonsterType.Dragon:
                return "An ancient dragon. Breathes fire every third turn.";
            default:
                return "";
        }
    }

    // Mennyi tapasztalati pontot ér a szörny legyőzése.
    public static int GetXp(Monster m)
    {
        switch (m.Type)
        {
            case MonsterType.Goblin: return 5;
            case MonsterType.Orc: return 10;
            case MonsterType.Skeleton: return 8;
            case MonsterType.Slime: return 4;
            case MonsterType.Dragon: return 40;
            case MonsterType.Troll: return 20;
            default: return 0;
        }
    }

    // Mit dob a szörny, ha legyőzik. (null = semmit)
    public static Item GetLoot(Monster m)
    {
        switch (m.Type)
        {
            case MonsterType.Goblin: return new Item("potion");
            case MonsterType.Orc: return new Item("sword");
            case MonsterType.Skeleton: return new Item("shield");
            case MonsterType.Dragon: return new Item("Potion");
            case MonsterType.Troll: return new Item("shield");
            default: return null;
        }
    }
}
