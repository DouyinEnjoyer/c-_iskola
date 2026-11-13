namespace DungeonPain;

static class HeroFactory
{
    public static Hero Create(string name, HeroClass heroClass)
    {
        Hero h = new Hero();
        h.Name = name;
        h.Class = heroClass;
        h.Gold = 5;

        switch (heroClass)
        {
            case HeroClass.Warrior:
                h.MaxHp = 50;
                h.Attack = 7;
                h.Defense = 3;
                break;
            case HeroClass.Mage:
                h.MaxHp = 38;
                h.Attack = 4;
                h.Defense = 1;
                h.Mana = 15;
                break;
            case HeroClass.Thief:
                h.MaxHp = 40;
                h.Attack = 6;
                h.Defense = 2;
                break;
        }

        h.Hp = h.MaxHp;
        return h;
    }

    // Rövid leírás a hősről.
    public static string Describe(Hero h)
    {
        switch (h.Class)
        {
            case HeroClass.Warrior:
                return h.Name + " the Warrior: strong, and fights harder when badly hurt.";
            case HeroClass.Mage:
                return h.Name + " the Mage: casts fireballs that ignore armor, while the mana lasts.";
            case HeroClass.Thief:
                return h.Name + " the Thief: every second attack is a deadly backstab.";
            default:
                return h.Name;
        }
    }
}
