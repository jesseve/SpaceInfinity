using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class DistanceManager : StateMachine 
{
    [SerializeField]
    private LevelManager levelManager = null;
	//checkpoints when the player reaches the next athmosphere
	public float[] checkpoints;
    private int currentCheckpoint;

	public Text distanceMeter;

	//The distance the player has travelled
	private float distance;
	//Getter for distance
	public float Distance { get { return distance; } }

	// The speed the player is travelling
	private float speed;
	//Getter for speed;
	public float Speed { get { return speed; } }


	private void Awake () 
	{
		speed = 5;
        if (levelManager == null) 
        {
            GameObject gameManager = GameObject.Find("GameManager");
            levelManager = gameManager.GetComponent<LevelManager>();
        }
		
		levelManager.OnEnterRunning += HandleOnEnterRunning;
        levelManager.OnGameOver += HandleOnGameOver;
		InitStateMachine(true);        
	}

    void HandleOnEnterRunning()
	{
		ChangeCurrentState (DistanceState.Running);
	}
    private void HandleOnGameOver() {
        ChangeCurrentState(DistanceState.GameOver);
        SaveLoad.SaveCurrentScore(distance);
    }
	private void UpdateRunning() 
	{
		distance += speed * Time.deltaTime;
		distanceMeter.text = "Distance:" + distance.ToString("n0");

        if (distance > checkpoints[Mathf.Clamp(currentCheckpoint, 0, checkpoints.Length - 1)]) {
            if (currentCheckpoint < checkpoints.Length) {
                EventManager.TriggerEvent("ChangeBackground");
                currentCheckpoint++;
            }
        }
	}
	protected void InitStateMachine(bool debug)
	{
		InitializeStateMachineWithTransitions(debug);
		CreateNewStateWithTransitions(DistanceState.StartGame, new Enum[] {DistanceState.Running});
        CreateNewStateWithTransitions(DistanceState.Running, new Enum[] { DistanceState.StartGame, DistanceState.GameOver });
        CreateNewStateWithTransitions(DistanceState.GameOver, new Enum[] { DistanceState.Running });
        ChangeCurrentState(DistanceState.StartGame);
	}

    public enum DistanceState 
    {
        StartGame,Running,GameOver
    }
}
