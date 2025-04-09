using System;

public static class GameEvents {


    public static event Action OnGameOver;

    public static void TriggerGameOver() {
        OnGameOver?.Invoke();
    }

}
