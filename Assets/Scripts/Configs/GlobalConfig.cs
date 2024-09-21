using UnityEngine;

public static class GlobalConfig
{
    private static float speedMultiplier = 1;
    private static float speed = 2;
    private static float initialSpeed = 1;
    private static bool onPause = false;
    private static bool gameOver = false;

    static GlobalConfig()
    {
        PlayerPrefs.SetInt(Constants.skinsVariableKey + "Monster", (int)CharacterSkinState.Purchased);
    }


    public static bool GetOnPause()
    {
        return onPause;
    }

    public static void SetOnPause(bool onPause)
    {
        GlobalConfig.onPause = onPause;
    }
    public static bool GetGameOver()
    {
        return gameOver;
    }

    public static void SetGameOver(bool gameOver)
    {
        GlobalConfig.gameOver = gameOver;
    }

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

    public static void IncreaseSpeed(float valueToAdd) {
        speed += valueToAdd;
    }

    public static int GetTotalCoins()
    {
        return PlayerPrefs.GetInt(Constants.coinsVariableKey, 0);
    }


    public static void UpdateTotalCoins(int valueToAdd)
    {
        PlayerPrefs.SetInt(Constants.coinsVariableKey, PlayerPrefs.GetInt(Constants.coinsVariableKey, 0) + valueToAdd);
    }

    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt(Constants.highScoreVariableKey, 0);
    }

    public static void UpdateHighScore(int currentScore)
    {
        int highScore = PlayerPrefs.GetInt(Constants.highScoreVariableKey, 0);
        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt(Constants.highScoreVariableKey, currentScore);
        }
    }

    public static int GetSkinState(string name)
    {
        return PlayerPrefs.GetInt(Constants.skinsVariableKey + name, 0);
    }


    public static void SetSkinState(string name, CharacterSkinState state)
    {
        PlayerPrefs.SetInt(Constants.skinsVariableKey + name, (int)state);
    }


    public static string GetSelectedSkin()
    {
        return PlayerPrefs.GetString(Constants.selectedSkinVariableKey, "Monster");
    }


    public static void SetSelectedSkin(string skinName)
    {
        PlayerPrefs.SetString(Constants.selectedSkinVariableKey, skinName);
    }
}
