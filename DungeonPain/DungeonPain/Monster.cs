namespace DungeonPain;

// A szörnyek fajtáit egy felsorolás (enum) különbözteti meg.
enum MonsterType
{
    Goblin,
    Orc,
    Skeleton,
    Slime,
    Dragon,
    Troll
}

// Egyetlen osztály az ÖSSZES szörnyfajtának.
class Monster
{
    public MonsterType Type;
    public string Name;
    public int Hp;
    public int MaxHp;
    public int Attack;
    public int Defense;
    public int FleddingRate; 

    // Az alábbi mezők csak bizonyos fajtáknál jelentenek valamit,
    // a többinél kihasználatlanul ott lógnak.
    public int StolenGold;   // csak a Goblinnál: az ellopott arany
    public bool IsEnraged;   // csak az Orcnál: dühöngő állapot
    public int TurnCounter;  // csak a Sárkánynál: számolja a köreit a tűzlehelethez
}
