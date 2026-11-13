namespace DungeonPain;

// A csapdák fajtáját is egy szöveg azonosítja ("spike", "fire").
class Trap
{
    public string Kind;

    public Trap(string kind)
    {
        Kind = kind;
    }
}
