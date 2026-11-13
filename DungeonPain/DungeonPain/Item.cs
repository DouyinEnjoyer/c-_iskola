namespace DungeonPain;

// A tárgyak fajtáját egy egyszerű szöveg ("potion", "sword", "shield") azonosítja.
class Item
{
    public string Kind;

    public Item(string kind)
    {
        Kind = kind;
    }
}
