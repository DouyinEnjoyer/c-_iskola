namespace DungeonPain;

enum HeroClass
{
    Warrior,
    Mage,
    Thief
}

// Egyetlen osztály az összes kalandortípusnak.
class Hero
{
    public string Name;
    public HeroClass Class;
    public int Hp;
    public int MaxHp;
    public int Attack;
    public int Defense;
    public int Gold;
    public int Xp;
    public List<Item> Inventory = new List<Item>();

    // Statisztika az összegzéshez.
    public int Defeated;   // legyőzött szörnyek száma
    public int Fled;       // elmenekült szörnyek száma

    // Csak egyes kalandortípusoknál van értelme:
    public int Mana;          // csak a Mage-nél
    public int AttackCount;   // csak a Thiefnél: számolja a támadásait a hátbaszúráshoz
}
