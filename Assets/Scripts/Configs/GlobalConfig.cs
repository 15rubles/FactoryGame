using UnityEngine;

public static class GlobalConfig
{
    private static float speedMultiplier = 1;
    private static float speed = 2;
    private static float initialSpeed = 1;
    private static int coins = 0;

    public static float GetSpeedMultiplied()
    {
        return speed * speedMultiplier;
    }

    public static float GetSpeed()
    {
        return speed * speedMultiplier;
    }

    public static void SetSpeed(float speed) {
        GlobalConfig.speed = speed;
    }

    public static float GetSpeedMultiplier()
    {
        return speedMultiplier;
    }

    public static void SetSpeedMultiplier(float speedMultiplier)
    {
        GlobalConfig.speedMultiplier = speedMultiplier;
    }


    public static float GetInitialSpeed()
    {
        return initialSpeed;
    }

    public static void SetInitialSpeed(float initialSpeed)
    {
        GlobalConfig.initialSpeed = initialSpeed;
    }


    public static float GetSpeedIncreasePercent()
    {
        return speed / initialSpeed;
    }

    public static int GetCoins()
    {
        return coins;
    }

    public static void incrementCoins()
    {
        coins++;
    }

    public static void IncreaseSpeed(float valueToAdd) {
        speed += valueToAdd;
    }

    public static int getCoins()
    {
        return PlayerPrefs.GetInt(Constants.coinsVariableKey, 0);
    }


    public static void updateCoins(int valueToAdd)
    {
        PlayerPrefs.SetInt(Constants.coinsVariableKey, PlayerPrefs.GetInt(Constants.coinsVariableKey, 0) + valueToAdd);
    }

    public static int getHighScore()
    {
        return PlayerPrefs.GetInt(Constants.highScoreVariableKey, 0);
    }


    public static void updateHighScore(int currentScore)
    {
        int highScore = PlayerPrefs.GetInt(Constants.highScoreVariableKey, 0);
        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt(Constants.highScoreVariableKey, currentScore);
        }
    }
}
