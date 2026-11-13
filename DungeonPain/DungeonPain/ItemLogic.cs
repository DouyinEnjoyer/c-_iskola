namespace DungeonPain;

static class ItemLogic
{
    // Rövid leírás a tárgyról.
    public static string Describe(Item item)
    {
        switch (item.Kind)
        {
            case "potion": return "Healing potion (+10 HP)";
            case "sword": return "Sword (+2 attack)";
            case "shield": return "Shield (+1 defense)";
            default: return "Unknown item";
        }
    }

    // A hős azonnal használja a megtalált tárgyat.
    public static void Use(Hero hero, Item item)
    {
        switch (item.Kind)
        {
            case "potion":
                hero.Hp = Math.Min(hero.MaxHp, hero.Hp + 10);
                Console.WriteLine("    " + hero.Name + " drinks a potion: HP " + hero.Hp + "/" + hero.MaxHp);
                break;
            case "sword":
                hero.Attack += 2;
                Console.WriteLine("    " + hero.Name + " wields the sword: attack " + hero.Attack);
                break;
            case "shield":
                hero.Defense += 1;
                Console.WriteLine("    " + hero.Name + " raises the shield: defense " + hero.Defense);
                break;
        }
    }
}
