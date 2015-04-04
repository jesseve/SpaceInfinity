using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour, ITouchInputEventListener {
	
	private PlayerHealth health;
	private DistanceManager distance;

	private Movement movement;
	
	private LevelManager levelManager;

	// Use this for initialization
	public void Init () {
		health = GetComponent<PlayerHealth>();
		distance = GetComponent<DistanceManager>();

		levelManager = Instances.scripts.levelmanager;

		if(movement == null) movement = new Movement(this.gameObject, 5.0f);

		health.Init();
		distance.Init ();
	}

	public int GetPlayerHealth() {
		return health.Health;
	}

	public void StartGame() {
		health.Reset();
	}

	public void HitObject(int damage) {
		health.ApplyDamage(damage);
	}

	public void PlayerDie() {
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
