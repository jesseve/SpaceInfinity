using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	private Movement movement;
	private PlayerHealth health;

	// Use this for initialization
	void Start () {
		movement = GetComponent<Movement>();
		health = GetComponent<PlayerHealth>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Move(int direction){
		movement.Move(direction);
	}

	public void HitObject(int damage) {

	}
}
