using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DistanceManager : MonoBehaviour {

	//Reference to player manager
	private PlayerManager manager;
	//Reference to levelmanager
	private LevelManager levelManager;

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

	// Use this for initialization
	public void Init () {
		speed = 5;

		manager = GetComponent<PlayerManager>();
		levelManager = Instances.scripts.levelmanager;
	}

	void Update() {
		distance += speed * Time.deltaTime;
		distanceMeter.text = "Distance:" + distance.ToString("n0");
	}

}
