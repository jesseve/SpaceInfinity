using UnityEngine;
using System.Collections;

public class PlayerHealth 
{
	private PlayerManager manager;

	private int health;
	public int Health { get { return health; } }
	public int maxHits = 3;

	public PlayerHealth (PlayerManager playerManager) 
	{
		this.manager = playerManager;
		Reset ();
	}

	public void Reset() {
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
