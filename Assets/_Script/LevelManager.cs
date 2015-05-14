using UnityEngine;
using System.Collections;
using System;

public class LevelManager : GameManager 
{
	
	public event Action OnStartGame = ()=>{};
	public event Action OnGameOver = ()=>{};

	// Use this for initialization
	protected override void Start () {
		base.Start();
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
