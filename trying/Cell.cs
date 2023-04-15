public class Cell
{
    public int X { get; }
    public int Y{ get; }

    public CellType type { get; set; }

    public Cell(int x, int y)
    {
        X = x;
        Y = y;
        type = CellType.Water;
    }

}
