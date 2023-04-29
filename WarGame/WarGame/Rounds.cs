public enum Winner 
{
    You,
    Enemy,
}

public class Rounds
{
    public int maxRoundsToWin = 3;

    public int currentRound = 0;

    public int ownWonRoundsAmount = 0;
    public int enemyWonRoundsAmount = 0;

    public Action OnStartRound;
    public Action<Winner> OnEndMatch;


    public void StartRounds() => TryStartNextRound();

    public void TryStartNextRound()
    {
        if(ownWonRoundsAmount >= maxRoundsToWin)
        {
            OnEndMatch?.Invoke(Winner.You);
            return;
        }
        else if(enemyWonRoundsAmount >= maxRoundsToWin)
        {
            OnEndMatch?.Invoke(Winner.Enemy);
            return;
        }
        StartNextRound();
    }
    public void StartNextRound()
    {
        currentRound++;
        OnStartRound?.Invoke();
    }

    
}
