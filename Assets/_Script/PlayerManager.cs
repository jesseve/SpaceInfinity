using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	private Movement movement;
	private PlayerHealth health;
	private LevelManager levelManager;

	// Use this for initialization
	void Start () {
		movement = GetComponent<Movement>();
		health = GetComponent<PlayerHealth>();
		levelManager = Instances.scripts.levelmanager;
	}
	
	// Update is called once per frame
	void Update () {
	
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
