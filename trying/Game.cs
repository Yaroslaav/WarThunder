using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
public enum GameState
{
    PlacingShips,
    YourTurn,
    EnemyTurn,
    UpdatingField,
}
public enum Direction
{
    Up,
    Down,
    Left,
    Right,
}

public class Game
{
    Random rand = new Random();

    public GameState state { get; private set; }

    private bool isPlaying;

    private int width = 10;
    private int height = 10;
    private Cell[,] ownCells;
    private Cell[,] enemyCells;

    public Server_Client server_client = new Server_Client();

    int count = 0;

    #region DLL
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool SetConsoleMode(IntPtr hConsoleHandle, int mode);
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool GetConsoleMode(IntPtr handle, out int mode);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr GetStdHandle(int handle);

    private static IntPtr Screen;

    private void SetupDLL()
    {
        var handle = GetStdHandle(-11);
        int mode;
        GetConsoleMode(handle, out mode);
        SetConsoleMode(handle, mode | 0x4);
    }
    #endregion
    public void Start()
    {
        SetupDLL();
        SetClientOrServer();
        server_client.OnRead += CheckMessageFromAnotherPlayer;

        Console.Clear();
        GenerateOwnField();
        GenerateEnemyField();

        UpdateAllScreen();

        state = server_client.isServer ? GameState.YourTurn : GameState.EnemyTurn;
        isPlaying = true;
        GameLoop();

    }
    public void GameLoop()
    {
        while(isPlaying)
        {
            server_client.ReadMessage();
            switch (state)
            {
                case GameState.YourTurn:
                    (int x, int y) = EnterCoords();

                    server_client.SendMessage($"Shot:{x},{y}");
                    state = GameState.EnemyTurn;
                    break;
            }
        }
    }

    public void SetClientOrServer()
    {
        Console.WriteLine("Press `0` then `Enter` to become a server");
        Console.WriteLine("Press `1` then `Enter` to become a client");
        string _type = Console.ReadLine();
        server_client = new Server_Client();

        switch (_type)
        {
            case "0":
                server_client.isServer = true;
                server_client.StartServer();
                break;
            case "1":
                server_client.isServer = false;
                server_client.StartClient();
                break;
            default:
                Console.WriteLine("you must just press `0` or `1` then `Enter`");
                SetClientOrServer();
                break;
        }

    }

    public void CoordsProcessing(int x, int y)
    {
        switch (ownCells[y, x].type)
        {
            case CellType.Water:
                ownCells[y, x].type = CellType.Shoted;
                break;
            case CellType.Ship:
                ownCells[y, x].type = CellType.ShotedInShip;
                break;
        }
    }
    public void UpdateAllScreen()
    {
        GameState lastState = state;
        state = GameState.UpdatingField;
        Console.Clear();
        ownCells.UpdateFieldOnScreen(height, width, true);
        enemyCells.UpdateFieldOnScreen(height, width, false);
        state = lastState;
    }
    public void Stop()
    {
        isPlaying = false;
        server_client.OnRead -= CheckMessageFromAnotherPlayer;
    }

    public void CheckMessageFromAnotherPlayer(string message)
    {
        string[] splited = message.Split(":");
        string[] allInfo;
        switch (splited[0])
        {
            case "Shot":
                {
                    allInfo = splited[1].Split(",");
                    int x = int.Parse(allInfo[0]);
                    int y = int.Parse(allInfo[1]);
                    CoordsProcessing(x, y);
                    server_client.SendMessage($"Coords:{x},{y},{ownCells[y,x].type}");
                    ownCells.UpdateFieldOnScreen(height, width, true);
                    state = GameState.YourTurn;

                }
                break;
            case "Coords":
                {
                    allInfo = splited[1].Split(",");
                    int x = int.Parse(allInfo[0]);
                    int y = int.Parse(allInfo[1]);
                    string cellType = allInfo[2];
                    if(cellType == CellType.Shoted.ToString())
                    {
                        enemyCells[y,x].type = CellType.Shoted;
                    }else if(cellType == CellType.ShotedInShip.ToString())
                    {

                        enemyCells[y,x].type = CellType.ShotedInShip;
                        count++;
                        if(count >= 7)
                        {
                            isPlaying = false;
                        }
                    }
                   enemyCells.UpdateFieldOnScreen(height,width, false);

                }
                break;
        }
    }
    string prefix = "";
    public (int, int) EnterCoords()
    {
        string[] splitedCoords;

        while (true)
        {
            UpdateAllScreen();
            Console.SetCursorPosition(0, 27);
            Console.Write(prefix + "Enter coords like `1,A`: ");
            string coords = Console.ReadLine();
            try
            {
                if (!coords.Contains(','))
                {
                    prefix += "Re";
                    continue;
                }
                splitedCoords = coords.Split(",");
                if (coords.Length<=2 ||splitedCoords.Length != 2)
                {
                    prefix += "Re";
                    continue;
                }
                if (int.Parse(splitedCoords[0]) < 0 || int.Parse(splitedCoords[0]) > 9 || splitedCoords[1][0] - 'A' < 0 || splitedCoords[1][0] - 'A' > 9)
                {
                    prefix += "Re";
                    continue;
                }
            }
            catch
            {
                continue;
            }
            break;
        }
        prefix = "";
        return (int.Parse(splitedCoords[0]), splitedCoords[1][0] - 'A');
    }

    public void SpawnShips(int shipsAmount)
    {
        List<int> xPositions = new List<int>();
        List<int> yPositions = new List<int>();
        for (int i = 0; i < shipsAmount; i++)
        {
            int xPosition = rand.Next(0, 10);
            int yPosition = rand.Next(0, 10);
            if(xPositions.Count > 0)
            {
                while (xPositions.Contains(xPosition) && yPositions.Contains(yPosition))
                {
                    xPosition = rand.Next(0, 10);
                    yPosition = rand.Next(0, 10);
                }
            }
            xPositions.Add(xPosition);
            yPositions.Add(yPosition);

            ownCells[yPosition, xPosition].type = CellType.Ship;
        }
    }

    public void GenerateOwnField()
    {
        ownCells = new Cell[height, width];
        for(int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                ownCells[y, x] = new Cell(x,y);
                ownCells[y,x].type = CellType.Water;
                
            }
        }
        SpawnShips(7);
    }
    public void GenerateEnemyField()
    {
        enemyCells = new Cell[height, width];
        for(int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                enemyCells[y, x] = new Cell(x,y);
                enemyCells[y,x].type = CellType.Water;
                
            }
        }
    }
}
