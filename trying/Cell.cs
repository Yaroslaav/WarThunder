public enum CellType
{
    Water,
    Shoted,
    ShotedInShip,
    Ship,
}
public class Cell
{

    public CellType type { get; set; }

    public Cell()
    {
        type = CellType.Water;
    }

}
