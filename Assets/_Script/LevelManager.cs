using UnityEngine;
using System.Collections;
using System;

public class LevelManager : GameManager 
{
	
	public Vector3 upperRightCorner;
	public Vector3 bottomLeftCorner;

	public event Action OnStartGame = ()=>{};
	public event Action OnGameOver = ()=>{};

	// Use this for initialization
	protected override void Start () {
		base.Start();
		upperRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
		bottomLeftCorner = Camera.main.ScreenToWorldPoint(Vector3.zero);
        OnStartGame();
	}


	public void StartGame() 
	{
		OnStartGame();
	}

	public void Pause() 
	{
	}
	public void GameOver() {

		OnGameOver();
        ShowRetryScreen();
	}

    private void ShowRetryScreen() {
        Application.LoadLevel("RetryScreen");
    }
}
