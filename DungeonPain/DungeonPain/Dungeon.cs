namespace DungeonPain;

static class Dungeon
{
    // A kazamata szobáinak felépítése. Minden hős saját, friss példányokat kap.
    public static List<Room> Build()
    {
        List<Room> rooms = new List<Room>();

        rooms.Add(MakeRoom("Entrance hall", new Item("sword"), MonsterFactory.Create(MonsterType.Goblin)));
        rooms.Add(MakeRoom("Spiky corridor", new Trap("spike"), MonsterFactory.Create(MonsterType.Orc)));
        rooms.Add(MakeRoom("Crypt", MonsterFactory.Create(MonsterType.Skeleton), new Item("shield")));
        rooms.Add(MakeRoom("Slimy cave", new Trap("fire"), MonsterFactory.Create(MonsterType.Slime)));

        // ===== A KÖRÖKHÖZ TARTOZÓ SZOBÁK: a megfelelő körben vedd ki a kommentjelet! =====
        // 1. kör:
        rooms.Add(MakeRoom("Troll bridge", MonsterFactory.Create(MonsterType.Troll)));
        // 3. kör:
        // rooms.Add(MakeRoom("Storage room", new Item("elixir"), new Item("helmet")));
        // 4. kör:
        // rooms.Add(MakeRoom("Spring cave", new Fountain()));
        // rooms.Add(MakeRoom("Treasury", new Trap("spike"), new Chest(5, new Item("elixir"), new Item("helmet"))));
        // 5. kör:
        // rooms.Add(MakeRoom("Bat cave", MonsterFactory.Create(MonsterType.Bat)));
        // rooms.Add(MakeRoom("Spider nest", MonsterFactory.Create(MonsterType.Spider)));
        // rooms.Add(MakeRoom("Wyvern roost", MonsterFactory.Create(MonsterType.Wyvern)));
        // =================================================================================

        rooms.Add(MakeRoom("Dragon's lair", MonsterFactory.Create(MonsterType.Dragon)));

        return rooms;
    }

    static Room MakeRoom(string name, params object[] contents)
    {
        Room room = new Room();
        room.Name = name;
        room.Contents.AddRange(contents);
        return room;
    }

    // A hős végigmegy a szobákon, és mindennel foglalkozik, ami bennük van.
    public static void Run(Hero hero)
    {
        List<Room> rooms = Build();

        foreach (Room room in rooms)
        {
            Console.WriteLine();
            Console.WriteLine("Room: " + room.Name);

            foreach (object thing in room.Contents)
            {
                if (hero.Hp <= 0)
                {
                    break;
                }

                if (thing is Monster)
                {
                    Monster monster = (Monster)thing;
                    Combat.Fight(hero, monster);
                }
                else if (thing is Item)
                {
                    Item item = (Item)thing;
                    hero.Inventory.Add(item);
                    Console.WriteLine("  Found: " + ItemLogic.Describe(item));
                    ItemLogic.Use(hero, item);
                }
                else if (thing is Trap)
                {
                    Trap trap = (Trap)thing;
                    switch (trap.Kind)
                    {
                        case "spike":
                            hero.Hp -= 3;
                            Console.WriteLine("  Spikes shoot up! " + hero.Name + " takes 3 damage (HP " + hero.Hp + ")");
                            break;
                        case "fire":
                            hero.Hp -= 5;
                            Console.WriteLine("  A fire trap! " + hero.Name + " takes 5 damage (HP " + hero.Hp + ")");
                            break;
                    }
                }
            }

            if (hero.Hp <= 0)
            {
                Console.WriteLine("  " + hero.Name + " did not survive the dungeon.");
                break;
            }

            // Rövid pihenő a szobák között.
            hero.Hp = Math.Min(hero.MaxHp, hero.Hp + 4);
            Console.WriteLine("  Short rest: HP " + hero.Hp + "/" + hero.MaxHp);
        }
    }
}
