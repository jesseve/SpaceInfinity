using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	private Movement movement;
	private PlayerHealth health;
	private InputManager input;
	private DistanceManager distance;

	private LevelManager levelManager;

	// Use this for initialization
	public void Init () {
		movement = GetComponent<Movement>();
		health = GetComponent<PlayerHealth>();
		input = GetComponent<InputManager>();
		distance = GetComponent<DistanceManager>();

		levelManager = Instances.scripts.levelmanager;

		health.Init();
		input.Init();
		movement.Init ();
		distance.Init ();
	}

	public int GetPlayerHealth() {
		return health.Health;
	}

	public void StartGame() {
		health.Reset();
	}

	public void Move(int direction){
		movement.Move(direction);
	}

	public void HitObject(int damage) {
		health.ApplyDamage(damage);
	}

	public void PlayerDie() {
		levelManager.GameOver();
	}
}
