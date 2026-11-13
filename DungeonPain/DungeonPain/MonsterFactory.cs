namespace DungeonPain;

// Szörnyek létrehozása: fajtánként más-más kezdőértékekkel.
static class MonsterFactory
{
    public static Monster Create(MonsterType type)
    {
        Monster m = new Monster();
        m.Type = type;

        switch (type)
        {
            case MonsterType.Goblin:
                m.Name = "Goblin";
                m.MaxHp = 12;
                m.Attack = 4;
                m.Defense = 1;
                m.FleddingRate = 30;
                break;
            case MonsterType.Orc:
                m.Name = "Orc";
                m.MaxHp = 25;
                m.Attack = 6;
                m.Defense = 2;
                break;
            case MonsterType.Skeleton:
                m.Name = "Skeleton";
                m.MaxHp = 18;
                m.Attack = 5;
                m.Defense = 1;
                break;
            case MonsterType.Slime:
                m.Name = "Slime";
                m.MaxHp = 15;
                m.Attack = 3;
                m.Defense = 0;
                break;
            case MonsterType.Dragon:
                m.Name = "Dragon";
                m.MaxHp = 40;
                m.Attack = 11;
                m.Defense = 4;
                m.FleddingRate = 15;
                break;
            case MonsterType.Troll:
                m.Attack = 5;
                m.Defense = 2;
                m.MaxHp = 24;
                m.Name = "Troll";
                break;
        }

        m.Hp = m.MaxHp;
        return m;
    }
}
