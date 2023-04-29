using System.Runtime.InteropServices;
using System.Text;

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
    AIvAI,
}

public class Game
{
    Random rand = new Random();


    SaveLoad saveLoad = new SaveLoad();

    Rounds rounds = new Rounds();

    private string playerName;

    public GameState state { get; private set; }
    public GameMode gameMode { get; private set; }

    private bool isPlaying;
    private int maxShipsAmount = 10;

    private Field ownField;
    private Field enemyField;

    public Server_Client server_client = new Server_Client();

    private int ownScore = 0;
    private int enemyScore = 0;

    private int lastRoundWhenScoreWasChanged = 0;

    private bool ownFieldWasChanged = false;
    private bool enemyFieldWasChanged = false;

    public void Start()
    {
        SetupDLL();
        SetLoadedData();

        SetActions();

        if (gameMode == GameMode.PvP)
            SetClientOrServer();

        rounds.StartRounds();
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
                    YouTurn();
                    break;
                case GameState.EnemyTurn:
                    EnemyTurn();
                    break;
            }
            TryUpdateFields();
        }
        rounds.TryStartNextRound();
    }
    public void Stop(Winner winner)
    {

        SetWinner(winner);
        Thread.Sleep(10000);
        if(gameMode == GameMode.PvP)
        {
            server_client.OnRead -= CheckMessageFromAnotherPlayer;
            server_client.Stop();
        }
    }

    private void SetLoadedData()
    {
        string[] data = saveLoad.Load();
        playerName = data[0];
        switch(data[1])
        {
            case "PvP":
                gameMode = GameMode.PvP;
                break;
            case "PvAI":
                gameMode = GameMode.PvAI;
                break;
            case "AIvAI":
                gameMode = GameMode.AIvAI;
                break;
        }

    }
    public void StartNextRound()
    {
        ownScore = 0;
        enemyScore = 0;

        Thread.Sleep(3000);

        Console.WriteLine(SetColor(255,255,255));
        if(gameMode == GameMode.PvP)
            server_client.OnRead += CheckMessageFromAnotherPlayer;


        ownField = new Field(gameMode, playerName);
        enemyField = new Field(gameMode, playerName);

        Console.SetCursorPosition(0, 0);
        Console.Clear();

        ownField.GenerateField(FieldType.Own);
        enemyField.GenerateField(FieldType.Enemy);

        UpdateAllScreen();

        state = server_client.isServer ? GameState.YourTurn : GameState.EnemyTurn;

        isPlaying = true;
        GameLoop();
    }
    private void SetActions()
    {
        rounds.OnEndMatch += Stop;
        rounds.OnStartRound += StartNextRound;
    }

    public void TryUpdateFields()
    {
        if (ownFieldWasChanged)
        {
            ownField.cells.UpdateFieldOnScreen(FieldType.Own, gameMode);
            ownFieldWasChanged = false;
        }
        if (enemyFieldWasChanged)
        {
            enemyField.cells.UpdateFieldOnScreen(FieldType.Enemy, gameMode);
            enemyFieldWasChanged = false;
        }
    }

    public void YouTurn()
    {
        int x = -1;
        int y = -1;
        if (gameMode != GameMode.AIvAI)
            (x, y) = EnterCoords();
        else
            (x, y) = enemyField.GetRandomFreeCell();

        if (gameMode == GameMode.PvP)
        {
            server_client.SendMessage($"HitInOwnField:{x},{y}");
        }

        if (gameMode == GameMode.AIvAI)
            Thread.Sleep(400);

        if (x == -1 || y == -1)
            return;

        if (CoordsProcessing(enemyField, x, y) && gameMode != GameMode.PvP)
        {
            ownScore++;
        }
        CheckScore();
            state = GameState.EnemyTurn;
    }
    public void EnemyTurn()
    {
        if (gameMode != GameMode.PvP)
        {
            (int x, int y) = ownField.GetRandomFreeCell();
            if (gameMode == GameMode.AIvAI)
                Thread.Sleep(400);
            if (CoordsProcessing(ownField, x, y))
            {
                enemyScore++;
            }
            CheckScore();
            state = GameState.YourTurn;
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
    public (int, int) EnterCoords()
    {

        while (true)
        {
            UpdateAllScreen();
            Console.SetCursorPosition(0,27);

            Console.WriteLine("Enter coords like `1,A`: ");
            string coords = Console.ReadLine();

            if (!IsItCoords(coords))
                continue;

            string[] splitedCoords = coords.Split(",");

            int x = int.Parse(splitedCoords[0]);
            int y = char.ToUpper(splitedCoords[1][0]) - 'A';

            if (!ValidateCoords(x, y))
                continue;

            return (x, y);
        }
    }
    public bool ValidateCoords(int x, int y)
    {
        try
        {
            if (x < 0 || x > 9 || y < 0 || y > 9)
            {
                return false;
            }
            if (enemyField.GetCellType(x, y) != CellType.Water && enemyField.GetCellType(x, y) != CellType.Ship)
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
        return true;

    }
    public bool IsItCoords(string coords)
    {
        if (!coords.Contains(','))
            return false;

        string[] splitedCoords = coords.Split(",");

        if (coords.Length <= 2 || splitedCoords.Length != 2)
            return false;

        return true;
    }
    public bool CoordsProcessing(Field _field, int x, int y)
    {
        bool hitInShip = false;
        switch (_field.cells[y, x].type)
        {
            case CellType.Water:
                _field.SetCellType(CellType.Shoted, x, y);
                break;
            case CellType.Ship:
                hitInShip = true;
                _field.SetCellType(CellType.ShotedInShip, x, y);
                break;
        }
        UpdateAllScreen();
        return hitInShip;
    }
    public void CheckMessageFromAnotherPlayer(string message)
    {
        if(!isPlaying) 
            return;
        string[] splited = message.Split(":");
        string[] allInfo = splited[1].Split(",");

        switch (splited[0])
        {
            case "HitInOwnField":
                {
                    int x = int.Parse(allInfo[0]);
                    int y = int.Parse(allInfo[1]);
                   
                    OnHitInYourField(x, y);
                }
                break;
            case "HitInEnemyField":
                {
                    int x = int.Parse(allInfo[0]);
                    int y = int.Parse(allInfo[1]);
                    string cellType = allInfo[2];

                    OnHitInEnemyField(x, y, cellType);
                }
                break;
        }
    }

    private void OnHitInYourField(int x, int y)
    {
        CoordsProcessing(ownField, x, y);
        if (ownField.GetCellType(x, y) == CellType.ShotedInShip)
            enemyScore++;
        CheckScore();
        server_client.SendMessage($"HitInEnemyField:{x},{y},{ownField.GetCellType(x, y)}");
        ownFieldWasChanged = true;
        state = GameState.YourTurn;
    }
    private void OnHitInEnemyField(int x, int y, string cellType)
    {
        if (cellType == CellType.Shoted.ToString())
        {
            enemyField.SetCellType(CellType.Shoted, x, y);
        }
        else if (cellType == CellType.ShotedInShip.ToString())
        {
            enemyField.SetCellType(CellType.ShotedInShip, x, y);
            ownScore++;

        }
        CheckScore();
        enemyFieldWasChanged = true;
    }

    private void CheckScore()
    {
        if (ownScore >= maxShipsAmount && rounds.currentRound != lastRoundWhenScoreWasChanged)
        {
            lastRoundWhenScoreWasChanged = rounds.currentRound;
            rounds.ownWonRoundsAmount++;
            isPlaying = false;
        }else if (enemyScore >= maxShipsAmount && rounds.currentRound != lastRoundWhenScoreWasChanged)
        {
            lastRoundWhenScoreWasChanged = rounds.currentRound;
            rounds.enemyWonRoundsAmount++;
            isPlaying = false;
        }
    }

    public void UpdateAllScreen()
    {
        GameState lastState = state;

        state = GameState.UpdatingField;
        Console.SetCursorPosition(0, 0);
        Console.Clear();
        Console.Write($"Round: {rounds.currentRound};  Own score: {rounds.ownWonRoundsAmount}; Enemy score: {rounds.enemyWonRoundsAmount}");

        ownField.cells.UpdateFieldOnScreen(FieldType.Own, gameMode);
        enemyField.cells.UpdateFieldOnScreen(FieldType.Enemy, gameMode);
        state = lastState;
    }

    private void SetWinner(Winner winner)
    {   
        Console.SetCursorPosition(0, 0);
        Console.Clear();
        Console.WriteLine(winner == Winner.You ? SetColor(0,255,0) : SetColor(255,0,0));
        Console.WriteLine(winner == Winner.You ? "You WIN!!!" : "You Lose!!!");
        isPlaying = false;
    }
    private string SetColor(byte r, byte g, byte b) => $"\x1b[38;2;{r};{g};{b}m";

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

}


