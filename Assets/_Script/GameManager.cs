 using UnityEngine;
using System.Collections;
using System;

[DisallowMultipleComponent]
public class GameManager : StateMachine
{
	public static readonly Vector2 screenScale = new Vector2(1920, 1080);

	private int screenWidth;
	private int screenHeight;
	
	protected virtual void Start()
	{        	
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		InitStateMachine (true);
	}
	
	protected virtual void Update()
	{
		// This one to be called or no update is run for the state machine
		StateUpdate();
	}
	protected override void InitStateMachine (bool debug)
	{
		// This is called to initialize the class
		// Debug indicates if we want to see transistion printed on console
		InitializeStateMachine(debug);
		// Here is where you define all states and their sub states
		// A substate is a state you can go to from the current state
		// Let's say you have Start, Pause, Running, GameOver
		// You should be able to go from
		// Start to Running
		// Running to Pause and GameOver
		// Pause to Running (there is no reason to lose on pause)
		// GameOver to Start
		// So you add like this
		AddStateWithTransitions(GameState.Start, new string[]{GameState.Running});
		AddStateWithTransitions(GameState.Running, new string[]{GameState.Pause, GameState.Running});
		AddStateWithTransitions(GameState.Pause, new string[]{GameState.Running});
		AddStateWithTransitions(GameState.GameOver, new string[]{GameState.Running});

		// All state a re declared now we give a starting state
		RequestState(GameState.Start);
	}
	// once you have states you can define if you need EnterState/ UpdateState / ExitState
	// Those are not compulsory to be defined
	private void EnterStart(string oldState)
	{
		// Place here things you want to happen when entering the state
		// This is actually done in the previous frame but well
		// The parameter is used by the state machine but you may need it as well
	}
	private void UpdateStart()
	{
		// Here whatever you want each frame for that state
	}
	private void ExitStart(string oldState)
	{
		// Here whatever should be called when leaving that state
	}



	// This will avoid typos and enable autocompletion
	class GameState{
		public static string Start = "Start";
		public static string Running = "Running";
		public static string Pause = "Pause";
		public static string GameOver = "GameOver";
	}
}