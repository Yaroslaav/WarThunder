
internal class Ship
{
    public int ShipLength { get; }
    public int XStartPosition { get; set; }
    public int YStartPosition { get; set; }
    public bool Spawned { get; set; }
    public Direction Direction { get; set; }

    public Ship(int xStartPosition, int yStartPosition, int length, Direction direction)
    {
        XStartPosition = xStartPosition;
        YStartPosition = yStartPosition;
        ShipLength = length;
        Direction = direction;
    }
    public Ship(int length, Direction direction) 
    {
        ShipLength = length;
        Spawned = false;
    }
}
