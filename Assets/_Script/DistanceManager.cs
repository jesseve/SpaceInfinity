using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DistanceManager : StateMachine 
{
    [SerializeField]
    private LevelManager levelManager = null;
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


	private void Awake () 
	{
		speed = 5;
        if (levelManager == null) 
        {
            GameObject gameManager = GameObject.Find("GameManager");
            levelManager = gameManager.GetComponent<LevelManager>();
        }
		
		levelManager.OnEnterRunning += HandleOnEnterRunning;
		InitStateMachine(true);        
	}

	private void Update()
	{
		StateUpdate();
	}
    void HandleOnEnterRunning()
	{
		RequestState (DistanceState.Running);
	}
	private void UpdateRunning() 
	{
		distance += speed * Time.deltaTime;
		distanceMeter.text = "Distance:" + distance.ToString("n0");
	}
	protected override void InitStateMachine(bool debug)
	{
		InitializeStateMachine(debug);
		AddStateWithTransitions(DistanceState.StartGame, new string[]{DistanceState.Running});
		AddStateWithTransitions(DistanceState.Running, new string[]{DistanceState.StartGame});
        RequestState(DistanceState.StartGame);
	}

    class DistanceState 
    {
        public const string StartGame = "StartGame";
        public const string Running = "Running";

    }
}
