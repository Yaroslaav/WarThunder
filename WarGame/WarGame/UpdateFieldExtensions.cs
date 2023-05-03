using System.Text;
public enum FieldType
{
    Own,
    Enemy,
}
public static class UpdateFieldExtensions
{
    const int width = 10;
    const int height = 10;
    public static void UpdateFieldOnScreen(this Cell[,] cells, FieldType type, GameMode gameMode)
    {
        if(cells == null) 
            return;
        StringBuilder sb = new StringBuilder();

        bool ownField = type == FieldType.Own;

        sb.Append(ownField ? SetColor(0, 255, 0): SetColor(255, 0, 0));
        sb.Append(ownField ? "Your table\n" : "Enemy table\n");
        sb.AppendLine($"{SetColor(255, 255, 255)}  0 1 2 3 4 5 6 7 8 9");

        for (int y = 0; y < height; y++)
        {

            sb.Append($"{SetColor(255, 255, 255)}{Convert.ToChar('A' + y)}");
            for (int x = 0; x < width; x++)
            {
                switch (cells[y, x].type)
                {
                    case CellType.Water:
                        sb.Append($"{SetColor(0, 0, 255)} .");
                        break;
                    case CellType.Ship:
                        if(ownField || gameMode == GameMode.AIvAI)
                            sb.Append($"{SetColor(150, 150, 150)} #");
                        else
                            sb.Append($"{SetColor(0, 0, 255)} +");
                        break;
                    case CellType.Shoted:
                        sb.Append($"{SetColor(100,100,100)} X");
                        break;
                    case CellType.ShotedInShip:
                        sb.Append($"{SetColor(255, 0, 0)} X");
                        break;

                }
            }
            sb.Append("\n");
            sb.Append(SetColor(255,255,255));
        }

        Console.SetCursorPosition(0, ownField ? 1 : 15);
        Console.WriteLine(sb);
    }
    private static string SetColor(byte r, byte g, byte b) => $"\x1b[38;2;{r};{g};{b}m";


}
