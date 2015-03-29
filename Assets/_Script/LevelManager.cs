using UnityEngine;
using System.Collections;

public class LevelManager : GameManager {
	
	public Vector3 upperRightCorner;
	public Vector3 bottomLeftCorner;

	private ObjectSpawner spawner;
	private PlayerManager player;
	private CanvasManager canvas;
	
	protected override void Start () 
	{
		base.Start();
		upperRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
		bottomLeftCorner = Camera.main.ScreenToWorldPoint(Vector3.zero);

		InitStateMachine(true);
	}
	protected override void InitStateMachine(bool debug)
	{
		InitializeStateMachine(debug);
		AddStateWithTransitions(State.Idle, new string[]{State.Running});
		AddStateWithTransitions(State.Running, new string[]{State.Pause, State.GameOver});
		AddStateWithTransitions(State.Pause, new string[]{State.Running});
		AddStateWithTransitions(State.GameOver, new string[]{State.Idle});
		RequestState(State.Idle);
		// Seems like not much happening here, mostly the state machine will prevent wrong going between states
		// Will see what should be happening
	}
	class State
	{
		public static string Idle = "Idle";
		public static string Running = "Running";
		public static string Pause = "Pause";
		public static string GameOver = "GameOver";
	}
}
