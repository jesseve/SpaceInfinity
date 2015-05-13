using UnityEngine;
using System;
using System.Collections;

public class PlayerManager : MonoBehaviour, ITouchInputEventListener {

    public float speed = 5.0f;

	private PlayerHealth health = null;
	private Movement movement = null;
	private LevelManager levelManager = null;

#region Unity lifecycle

	private void Start () {
		health = new PlayerHealth();
		GameObject gameManager = GameObject.Find ("GameManager");
		levelManager = gameManager.GetComponent<LevelManager>();
		levelManager.OnStartGame += StartGame;

		if(movement == null) 
		{
			movement = new Movement(this.gameObject, speed);
		}
	}

	void OnDestroy()
	{
		if(levelManager!= null)
		{
			levelManager.OnStartGame -= StartGame;
			levelManager = null;
		}
	}

#endregion

	public int GetPlayerHealth() {
		return health.Health;
	}

	public void StartGame() {
		health.Reset();
	}

	public void HitObject(int damage) {
        if (health.ApplyDamage(damage))  //If true, player has taken too many hits
            levelManager.GameOver();
	}

	private void RotateShip(Vector3 tapPosition) {
		movement.Reset = false;
		movement.RotateShip(tapPosition);
	}
	private void ResetShip(Vector3 tapPosition) {
		movement.Reset = true;
		StartCoroutine(ResetShipRotation());
	}
	private IEnumerator ResetShipRotation() {
		while(!movement.ResetShipRotation() && movement.Reset) {
			yield return null;
		}
	}


	#region ITouchInputEventListener implementation

	public void Register (ITouchInputEventHandler handler)
	{
		if(movement == null) movement = new Movement(this.gameObject, 25.0f);
		handler.OnTap += RotateShip;
		handler.OnRemove += ResetShip;

	}

	public void Unregister (ITouchInputEventHandler handler)
	{
		handler.OnTap -= RotateShip;
	}

	#endregion
}
