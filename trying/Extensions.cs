using System.Text;
public static class Extensions
{
    public static void UpdateFieldOnScreen(this Cell[,] cells, int height, int width, bool ownField)
    {
        if (ownField)
        {
            Console.SetCursorPosition(2, 2);
        }
        else
        {
            Console.SetCursorPosition(18, 2);
        }

        StringBuilder sb = new StringBuilder();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                switch (cells[y, x].type)
                {
                    case CellType.Water:
                        sb.AppendFormat("\u001b[37m{0}", ".");
                        break;
                    case CellType.Ship:
                        sb.AppendFormat("\u001b[37m{0}", "-");
                        break;
                    case CellType.Shoted:
                        sb.AppendFormat("\u001b[37m{0}", "X");
                        break;
                    case CellType.ShotedInShip:
                        sb.AppendFormat("\u001b[37m{0}", "X");
                        break;
                    case CellType.StartOfShip:
                        sb.AppendFormat("\u001b[37m{0}", "0");
                        break;
                    case CellType.Border:
                        sb.AppendFormat("\u001b[37m{0}", "#");
                        break;
                    case CellType.NumberedBorder:
                        if(cells[y, x].X == 0)
                        {
                            if(cells[y, x].Y > 1)
                            {
                                sb.AppendFormat("\u001b[37m{0}", cells[y,x].Y - 2);
                            }else
                            {
                                //sb.AppendFormat("\u001b[37m{0}", "#");

                            }
                        }
                        else if (cells[y,x].Y == 0)
                        {
                            if (cells[y,x].X > 1)
                            {
                                sb.AppendFormat("\u001b[37m{0}", Convert.ToChar('A' + cells[y, x].X-2  ));
                            }
                            else
                            {
                                //sb.AppendFormat("\u001b[37m{0}", "#");

                            }
                        }
                        
                        break;

                }
            }
            if(y != 0)
                sb.AppendFormat("\u001b[37m{0}", "#");
            sb.Append("\n");
        }
        for (int i = 0; i < width; i++)
        {
            sb.AppendFormat("\u001b[37m{0}", "#");
        }

        Console.WriteLine(sb);
    }
    public static bool NextNeighborIsFree(this Cell cell, Cell[,] cells, int x, int y, int height, int width, Direction direction)
    {

        switch (direction)
        {
            case Direction.Up:
                if (y > 0)
                {
                    if (cells[y - 1, x].type == CellType.Water)
                    {
                        return true;
                    }
                }
                break;
            case Direction.Down:
                if (y < height - 1)
                {
                    if (cells[y + 1, x].type == CellType.Water)
                    {
                        return true;
                    }
                }
                break;
            case Direction.Left:
                if (x > 0)
                {
                    if (cells[y, x - 1].type == CellType.Water)
                    {
                        return true;
                    }
                }
                break;
            case Direction.Right:
                if (x < width - 1)
                {
                    if (cells[y, x + 1].type == CellType.Water)
                    {
                        return true;
                    }
                }
                break;
        }
        return false;
    }

}
