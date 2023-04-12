using System;
using System.Reflection;
using System.Text;

public class Game
{
    private bool isPlaying;
    private bool somethingChanged = false;
    private bool enemiesFieldChanged = false;

    private int width = 12;
    private int height = 12;
    private Cell[,] ownCells;
    private Cell[,] enemyCells;
    private List<Ship> ships = new List<Ship>();
    private bool shipIsPlacing = false;

    private Server_Client server_client = new Server_Client();

    public void Start()
    {
        isPlaying = true;
        GenerateField();
        ownCells.UpdateFieldOnScreen(height, width, true); 
    }
    public void GameLoop()
    {
        while(isPlaying)
        {
            if(somethingChanged)
            {
                UpdateOwnField();
            }
            if (enemiesFieldChanged)
            {
                UpdateEnemyField();
            }
        }
    }
    public void UpdateOwnField()
    {

    }
    public void UpdateEnemyField()
    {

    }
    public void Stop()
    {
        isPlaying = false;
    }
    public void SetShips()
    {
        for(int i = 0; i < 10; i++)
        {
            if(i < 4)
            {
                ships.Add( new Ship(1, Direction.Right));

            }else if(i < 7) 
            {
                ships.Add(new Ship(2, Direction.Right));

            }else if(i < 9)
            {
                ships.Add(new Ship(3, Direction.Right));
            }
            else
            {
                ships.Add(new Ship(4, Direction.Right));
            }
        }
    }



    public void SpawnShipsAboveTheTable()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < 10; i++) 
        {
            switch (ships[i].ShipLength)
            {
                case 1:
                    sb.AppendFormat("\u001b[37m{0}", "0");

                    break;
                case 2:
                    sb.AppendFormat("\u001b[37m{0}", "0-");
                    break;
                case 3:
                    sb.AppendFormat("\u001b[37m{0}", "0--");
                    break;
                case 4:
                    sb.AppendFormat("\u001b[37m{0}", "0---");
                    break;

            }
            sb.Append(" ");
        }
        Console.SetCursorPosition(0, 1);
        Console.WriteLine(sb.ToString());
            
    }
    public void SpawnShipOnTable()
    {    
        int spawnedShipsAmount = 0;
        while(spawnedShipsAmount != 10)
        {
            if (!shipIsPlacing)
            {
                Ship currentShip = ships[spawnedShipsAmount];
                Cell currentCell = ownCells[0,0];
                int freeCellsAmount = 0;

                for (int y = 0; y < height && freeCellsAmount < currentShip.ShipLength; y++)
                {
                    for (int x = 0; x < width && freeCellsAmount < currentShip.ShipLength; x++)
                    {
                        if(freeCellsAmount > 0)
                        {
                            if (currentCell.NextNeighborIsFree(ownCells, x, y, height, width, currentShip.Direction))
                            {

                            } 

                        }else if (ownCells[y,x].type == CellType.Water)
                        {
                            currentCell = ownCells[y, x];
                            freeCellsAmount++;
                        }
                    }
                }

            }
        }
    }

    public void MoveShip(Direction direction)
    {

    }

    public List<Cell> GetNeighbors(int x, int y, Direction direction)
    {
        List<Cell> cells = new List<Cell>();

        switch (direction)
        {
            case Direction.Up:
                if(y>0)
                {
                    if (ownCells[y-1, x].type == CellType.Water)
                    {
                        cells.Add(ownCells[y - 1, x]);
                    }
                }
                break;
            case Direction.Down:
                if(y < height - 1)
                {
                    if (ownCells[y + 1, x].type == CellType.Water)
                    {
                        cells.Add(ownCells[y + 1, x]);
                    }
                }
                break;
            case Direction.Left: 
                if(x > 0)
                {
                    if (ownCells[y,x-1].type == CellType.Water)
                    {
                        cells.Add(ownCells[y,x-1]);
                    }
                }
                break;
            case Direction.Right:
                if(x < width - 1)
                {
                    if (ownCells[y,x+1].type == CellType.Water)
                    {
                        cells.Add(ownCells[y,x+1]);
                    }
                }
                break;
        }
        return cells;
    }


    public void GenerateField()
    {
        ownCells = new Cell[height, width];
        for(int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                ownCells[y, x] = new Cell(x,y);
                if(x == 0 || y == 0)
                {
                    ownCells[y,x].type = CellType.NumberedBorder;
                }
                else if(x == 1 || y == 1)
                {
                    ownCells[y,x].type = CellType.Border;
                }
                else
                {
                    ownCells[y,x].type = CellType.Water;
                }
            }
        }
    }

}
