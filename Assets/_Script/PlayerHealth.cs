using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	private PlayerManager manager;

	private int health;
	public int maxHits = 3;

	// Use this for initialization
	public void Init () {
		manager = GetComponent<PlayerManager>();
		health = maxHits;
	}

	public void ApplyDamage(int damage) {
		health -= damage;
		if(health <= 0)
			Die();
	}

	public void Die() {
		manager.PlayerDie();
	}
}
