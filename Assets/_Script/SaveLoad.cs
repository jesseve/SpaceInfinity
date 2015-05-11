using UnityEngine;
using System.Collections;

public class SaveLoad {

    public const string currentScore = "CurrentScore";
    public const string highScore = "HighScore";

    public static void SaveCurrentScore(float score) {
        PlayerPrefs.SetFloat(currentScore, score);
    }
    public static float LoadCurrentScore() {
        if (PlayerPrefs.HasKey(currentScore))
            return PlayerPrefs.GetFloat(currentScore);
        return 0;
    }
    public static void SaveHighScore(float score) {
        PlayerPrefs.SetFloat(highScore, score);
    }
    public static float LoadHighScore() {
        if (PlayerPrefs.HasKey(highScore))
            return PlayerPrefs.GetFloat(highScore);
        return 0;
    }
}
