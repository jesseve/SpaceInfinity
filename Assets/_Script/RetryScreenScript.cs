using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RetryScreenScript : MonoBehaviour {

    private float score;

    public Text scoreText;

	// Use this for initialization
	void Awake () {
        score = SaveLoad.LoadCurrentScore();

        scoreText.text = "Your score was: " + score.ToString("n0");
	}

    public void PressRestart() {
        Application.LoadLevel(1);
    }

    public void PressHighScores() { }
}
