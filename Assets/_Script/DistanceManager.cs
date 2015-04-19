using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DistanceManager : StateMachine 
{
	//checkpoints when the player reaches the next athmosphere
	public float[] checkpoints;

	public Text distanceMeter;

	//The distance the player has travelled
	private float distance;
	//Getter for distance
	public float Distance { get { return distance; } }

	// The speed the player is travelling
	private float speed;
	//Getter for speed;
	public float Speed { get { return speed; } }
	private LevelManager levelManager = null;

	private void Start () 
	{
		speed = 5;
		GameObject gameManager = GameObject.Find ("GameManager");
		levelManager = gameManager.GetComponent<LevelManager>();
		levelManager.OnStartGame += HandleOnStartGame;
		InitStateMachine(true);        
	}

	private void Update()
	{
		StateUpdate();
	}
	void HandleOnStartGame ()
	{
		RequestState ("Running");
	}

	private void UpdateRunning() 
	{
		distance += speed * Time.deltaTime;
		distanceMeter.text = "Distance:" + distance.ToString("n0");
	}
	protected override void InitStateMachine(bool debug)
	{
		InitializeStateMachine(debug);
		AddStateWithTransitions("StartGame", new string[]{"Running"});
		AddStateWithTransitions("Running", new string[]{"StartGame"});
		RequestState ("StartGame");
	}
}
