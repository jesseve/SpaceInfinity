 using UnityEngine;
using System.Collections;
using System;


public class GameManager : StateMachine
{
	public static readonly Vector2 screenScale = new Vector2(1920, 1080);

    public event Action<Action> OnEnterStart = (action) => { };
    public event Action OnEnterRunning = () => {  };

	protected virtual void Start()
	{        	
		InitStateMachine (true);
	}
	
	protected void InitStateMachine (bool debug)
	{
		// This is called to initialize the class
		// Debug indicates if we want to see transistion printed on console
		InitializeStateMachineWithTransitions(debug);
		// Here is where you define all states and their sub states
		// A substate is a state you can go to from the current state
		// Let's say you have Start, Pause, Running, GameOver
		// You should be able to go from
		// Start to Running
		// Running to Pause and GameOver
		// Pause to Running (there is no reason to lose on pause)
		// GameOver to Start
		// So you add like this
		CreateNewStateWithTransitions(GameState.Start, new Enum[]{GameState.Running});
        CreateNewStateWithTransitions(GameState.Running, new Enum[] { GameState.Pause, GameState.Running });
        CreateNewStateWithTransitions(GameState.Pause, new Enum[] { GameState.Running });
        CreateNewStateWithTransitions(GameState.GameOver, new Enum[] { GameState.Running });

		// All state a re declared now we give a starting state
		ChangeCurrentState(GameState.Start);
	}
	// once you have states you can define if you need EnterState/ UpdateState / ExitState
	// Those are not compulsory to be defined
	private void EnterStart(Enum oldState)
	{
		// Place here things you want to happen when entering the state
		// This is actually done in the previous frame but well
		// The parameter is used by the state machine but you may need it as well
        Action action = () =>
        {
            Debug.Log("Call");
            ChangeCurrentState(GameState.Running);
        };
        OnEnterStart(action);
	}
    private void EnterRunning(Enum oldState)
	{
        OnEnterRunning();
	}



	// This will avoid typos and enable autocompletion
	public enum GameState{
		Start,Running,Pause,GameOver
	}
}