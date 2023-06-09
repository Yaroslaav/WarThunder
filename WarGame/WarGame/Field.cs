﻿public class Field
{
    Random rand = new Random();

    public string ownerName;

    private GameMode gameMode;

    const int width = 10;
    const int height = 10;

    public Cell[,] cells { get; private set; }

    public Field(GameMode _gameMode, string _ownerName)
    {
        gameMode = _gameMode;
        ownerName = _ownerName;
    }

    public FieldType fieldType;
    public void GenerateField(FieldType type)
    {
        fieldType = type;

        cells = new Cell[height, width];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                cells[y, x] = new Cell();
                cells[y, x].type = CellType.Water;

            }
        }
        if (fieldType == FieldType.Own || gameMode != GameMode.PvP)
            SpawnShips(cells, 10);

    }
    public void SpawnShips(Cell[,] cells, int shipsAmount)
    {
        List<int> xPositions = new List<int>();
        List<int> yPositions = new List<int>();
        for (int i = 0; i < shipsAmount; i++)
        {
            int xPosition = rand.Next(0, 10);
            int yPosition = rand.Next(0, 10);
            if (xPositions.Count > 0)
            {
                while (xPositions.Contains(xPosition) && yPositions.Contains(yPosition))
                {
                    xPosition = rand.Next(0, 10);
                    yPosition = rand.Next(0, 10);
                }
            }
            xPositions.Add(xPosition);
            yPositions.Add(yPosition);

            cells[yPosition, xPosition].type = CellType.Ship;
        }
    }
    public CellType GetCellType(int x, int y) => cells[y, x].type;
    public void SetCellType(CellType type, int x, int y) => cells[y, x].type = type;
    public (int, int) GetRandomFreeCell()
    {
        int x = rand.Next(0, 10);
        int y = rand.Next(0, 10);
        while (cells[y, x].type != CellType.Water && cells[y, x].type != CellType.Ship)
        {
            x = rand.Next(0, 10);
            y = rand.Next(0, 10);
        }
        return (x, y);
    }


}
