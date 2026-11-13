namespace DungeonPain;

// Egy szoba tartalma: bármi lehet benne, ezért a lista elemtípusa object.
class Room
{
    public string Name;
    public List<object> Contents = new List<object>();
}
