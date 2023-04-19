using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

public enum GameState
{
    PlacingShips,
    YourTurn,
    EnemyTurn,
    UpdatingField,
}
public enum GameMode
{
    PvP,
    PvAI,
}

public class Game
{
    Random rand = new Random();

    public GameState state { get; private set; }
    public GameMode gameMode { get; private set; }

    private bool isPlaying;
    private int maxShipsAmount = 1;

    private Field ownField = new Field();
    private Field enemyField = new Field();

    public Server_Client server_client = new Server_Client();

    private int ownScore = 0;
    private int enemyScore = 0;


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
        GenerateMainMenu();
        if(gameMode == GameMode.PvP)
            SetClientOrServer();
        server_client.OnRead += CheckMessageFromAnotherPlayer;

        Console.Clear();
        ownField.GenerateField(FieldType.Own);
        enemyField.GenerateField(FieldType.Enemy);

        UpdateAllScreen();

        state = server_client.isServer ? GameState.YourTurn : GameState.EnemyTurn;
        isPlaying = true;
        GameLoop();

    }
    public void GameLoop()
    {
        while(isPlaying)
        {
            if(gameMode == GameMode.PvP)
                server_client.ReadMessage();
            switch (state)
            {
                case GameState.YourTurn:
                    {
                        if (isPlaying)
                        {
                            (int x, int y) = EnterCoords();
                            if(gameMode == GameMode.PvP)
                            {
                                server_client.SendMessage($"Shot:{x},{y}");
                            }
                            CoordsProcessing(enemyField, x, y);

                            state = GameState.EnemyTurn;
                        }
                        break;
                    }
                case GameState.EnemyTurn:
                    {
                        if(gameMode != GameMode.PvP)
                        {
                            CoordsProcessing(ownField, rand.Next(0, 10), rand.Next(0, 10));
                            state = GameState.YourTurn;
                        }
                        break;
                    }
            }
            CheckScore();
        }
        Stop();
    }
    public void Stop()
    {
        SetWinner();
        Thread.Sleep(10000);
        server_client.OnRead -= CheckMessageFromAnotherPlayer;
        server_client.Stop();
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
    public void CheckMessageFromAnotherPlayer(string message)
    {
        string[] splited = message.Split(":");
        string[] allInfo = splited[1].Split(",");
        switch (splited[0])
        {
            case "Shot":
                {
                    int x = int.Parse(allInfo[0]);
                    int y = int.Parse(allInfo[1]);
                   
                    CoordsProcessing(ownField, x, y);
                    if (ownField.GetCellType(x, y) == CellType.ShotedInShip)
                        enemyScore++;
                    server_client.SendMessage($"Coords:{x},{y},{ownField.GetCellType(x,y)}");
                    ownField.cells.UpdateFieldOnScreen(FieldType.Own);
                    state = GameState.YourTurn;

                }
                break;
            case "Coords":
                {
                    int x = int.Parse(allInfo[0]);
                    int y = int.Parse(allInfo[1]);
                    string cellType = allInfo[2];

                    if(cellType == CellType.Shoted.ToString())
                    {
                        enemyField.SetCellType(CellType.Shoted,x,y);
                    }else if(cellType == CellType.ShotedInShip.ToString())
                    {

                        enemyField.SetCellType(CellType.ShotedInShip,x,y);
                        ownScore++;    
                    }
                    enemyField.cells.UpdateFieldOnScreen(FieldType.Enemy);

                }
                break;
        }
        CheckScore();
    }

    private void CheckScore()
    {
        if (ownScore >= maxShipsAmount || enemyScore >= maxShipsAmount)
            isPlaying = false;
    }

    public void CoordsProcessing(Field _field, int x, int y)
    {
        switch (_field.cells[y, x].type)
        {
            case CellType.Water:
                _field.SetCellType(CellType.Shoted, x, y);
                break;
            case CellType.Ship:
                _field.SetCellType(CellType.ShotedInShip, x, y);
                break;
        }
    }
    public (int, int) EnterCoords()
    {
        string prefix = "";
        string[] splitedCoords;

        int x;
        int y;

        while (true)
        {
            UpdateAllScreen();
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

                x = int.Parse(splitedCoords[0]);
                y = splitedCoords[1][0] - 'A';

                if (x < 0 || x > 9 || y < 0 || y > 9)
                {
                    prefix += "Re";
                    continue;
                }
                if (enemyField.GetCellType(x,y) != CellType.Water)
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
        UpdateAllScreen();
        return (x, y);
    }
    public void UpdateAllScreen()
    {
        GameState lastState = state;
        state = GameState.UpdatingField;
        Console.Clear();
        ownField.cells.UpdateFieldOnScreen(FieldType.Own);
        enemyField.cells.UpdateFieldOnScreen(FieldType.Enemy);
        state = lastState;
    }

    public void GenerateMainMenu()
    {
        Console.Clear();

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Press `0` then `Enter` to play with player");
        sb.AppendLine("Press `1` then `Enter` to play with AI");
        Console.WriteLine(sb.ToString());
        string _mode = Console.ReadLine();

        switch (_mode)
        {
            case "0":
                gameMode = GameMode.PvP;
                break;
            case "1":
                gameMode = GameMode.PvAI;
                break;
            default:
                Console.WriteLine("you must just press `0` or `1` then `Enter`");
                GenerateMainMenu();
                break;
        }

    }
    private void SetWinner()
    {
        Console.Clear();
        Console.WriteLine(ownScore >= maxShipsAmount ? SetColor(0,255,0) : SetColor(255,0,0));
        Console.WriteLine(ownScore >= maxShipsAmount ? "You WIN!!!" : "You Lose!!!");

        isPlaying = false;
    }
    private string SetColor(byte r, byte g, byte b) => $"\x1b[38;2;{r};{g};{b}m";

}
