using System;

public class Rounds
{
    public int maxRounds = 5;
    public int currentRound = 0;

    public Action OnStartRound;
    public Action OnEndMatch;

    public void StartRounds() => TryStartNextRound();

    public void TryStartNextRound()
    {
        
        currentRound++;
        if(currentRound >= maxRounds)
        {
            OnEndMatch?.Invoke();
        }
        else
        {
            StartNextRound();
        }
    }
    public void StartNextRound() => OnStartRound?.Invoke();
    
}
