namespace DungeonPain;

static class Combat
{
    // Egy teljes harc a hős és egy szörny között.
    public static void Fight(Hero hero, Monster monster)
    {
        Console.WriteLine("  [" + MonsterInfo.GetSymbol(monster) + "] " + monster.Name + " appears (" + monster.Hp + " HP). " + MonsterInfo.Describe(monster));

        while (hero.Hp > 0 && monster.Hp > 0)
        {
            int damage = HeroAttack(hero, monster);
            monster.Hp -= damage;
            Console.WriteLine("    " + hero.Name + " hits " + monster.Name + " for " + damage + " (" + Math.Max(0, monster.Hp) + " HP left)");

            if (monster.Hp <= 0)
            {
                break;
            }

            MonsterAct(hero, monster);
        }

        if (hero.Hp <= 0)
        {
            Console.WriteLine("    " + hero.Name + " has fallen to the " + monster.Name + "!");
            return;
        }

        // Győzelem
        hero.Defeated++;
        hero.Xp += MonsterInfo.GetXp(monster);
        hero.Gold += monster.StolenGold;
        Console.WriteLine("    " + monster.Name + " defeated! XP " + hero.Xp + ", gold " + hero.Gold);

        Item loot = MonsterInfo.GetLoot(monster);
        if (loot != null)
        {
            hero.Inventory.Add(loot);
            Console.WriteLine("    Loot: " + ItemLogic.Describe(loot));
            ItemLogic.Use(hero, loot);
        }
    }

    // A hős támadása: a hős osztálya szerint más-más képlet.
    static int HeroAttack(Hero hero, Monster monster)
    {
        int damage = 0;

        switch (hero.Class)
        {
            case HeroClass.Warrior:
                damage = hero.Attack - monster.Defense;
                if (hero.Hp < hero.MaxHp / 2)
                {
                    damage += 2;   // dühöngés
                }
                if (monster.Type == MonsterType.Skeleton)
                {
                    damage += 2;   // a csontok összetörnek
                }
                break;

            case HeroClass.Mage:
                if (hero.Mana >= 3)
                {
                    hero.Mana -= 3;
                    damage = hero.Attack + 4;   // tűzgolyó: a védelmet figyelmen kívül hagyja
                }
                else
                {
                    damage = hero.Attack - monster.Defense;
                }
                break;

            case HeroClass.Thief:
                hero.AttackCount++;
                damage = hero.Attack - monster.Defense;
                if (hero.AttackCount % 2 == 0)
                {
                    damage *= 2;   // hátbaszúrás
                }
                break;
        }

        if (damage < 1)
        {
            damage = 1;
        }
        return damage;
    }

    // A szörny köre: fajtánként más viselkedés.
    static void MonsterAct(Hero hero, Monster monster)
    {
        monster.TurnCounter++;
        int damage;

        switch (monster.Type)
        {
            case MonsterType.Goblin:
                damage = monster.Attack - hero.Defense;
                if (damage < 1) damage = 1;
                hero.Hp -= damage;
                Console.WriteLine("    Goblin stabs " + hero.Name + " for " + damage + " (HP " + hero.Hp + ")");
                int stolen = Math.Min(2, hero.Gold);
                if (stolen > 0)
                {
                    hero.Gold -= stolen;
                    monster.StolenGold += stolen;
                    Console.WriteLine("    Goblin steals " + stolen + " gold!");
                }
                break;

            case MonsterType.Orc:
                if (!monster.IsEnraged && monster.Hp <= monster.MaxHp / 2)
                {
                    monster.IsEnraged = true;
                    monster.Attack += 3;
                    Console.WriteLine("    Orc flies into a rage!");
                }
                damage = monster.Attack - hero.Defense;
                if (damage < 1) damage = 1;
                hero.Hp -= damage;
                Console.WriteLine("    Orc smashes " + hero.Name + " for " + damage + " (HP " + hero.Hp + ")");
                break;

            case MonsterType.Skeleton:
                damage = monster.Attack - hero.Defense;
                if (damage < 1) damage = 1;
                hero.Hp -= damage;
                Console.WriteLine("    Skeleton slashes " + hero.Name + " for " + damage + " (HP " + hero.Hp + ")");
                break;

            case MonsterType.Slime:
                damage = monster.Attack - hero.Defense;
                if (damage < 1) damage = 1;
                hero.Hp -= damage;
                Console.WriteLine("    Slime splashes " + hero.Name + " for " + damage + " (HP " + hero.Hp + ")");
                if (hero.Defense > 0)
                {
                    hero.Defense--;
                    Console.WriteLine("    The acid corrodes the armor: defense " + hero.Defense);
                }
                break;

            case MonsterType.Dragon:
                if (monster.TurnCounter % 3 == 0)
                {
                    damage = 8;   // tűzlehelet: a védelmet figyelmen kívül hagyja
                    hero.Hp -= damage;
                    Console.WriteLine("    Dragon breathes fire on " + hero.Name + " for " + damage + " (HP " + hero.Hp + ")");
                }
                else
                {
                    damage = monster.Attack - hero.Defense;
                    if (damage < 1) damage = 1;
                    hero.Hp -= damage;
                    Console.WriteLine("    Dragon claws " + hero.Name + " for " + damage + " (HP " + hero.Hp + ")");
                }
                break;
            case MonsterType.Troll:
                damage = monster.Attack - hero.Defense;
                if (damage < 1) damage = 1;
                hero.Hp -= damage;
                Console.WriteLine("    Troll hits " + hero.Name + " for " + damage + " (HP " + hero.Hp + ")");
                if (hero.Defense > 0)
                {
                    hero.Defense--;
                    Console.WriteLine("    The acid corrodes the armor: defense " + hero.Defense);
                }
                monster.Hp += 2;
                if (monster.Hp > 0)
                {
                    int max = monster.MaxHp;
                    int active = monster.Hp * 100;
                      if(active / max > monster.FleddingRate)
                    {
                        
                        
                        break;
                    }
                }
                break;

        }
    }
}
