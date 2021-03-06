﻿using UnityEngine;
using System.Collections;

public class PlayerHealth 
{
	private int health;
	public int Health { get { return health; } }
	public int maxHits = 3;

	public PlayerHealth () 
	{
		Reset ();
	}

    public void Reset() {
		health = maxHits;
	}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="damage"></param>
    /// <returns>True if the player dies</returns>
	public bool ApplyDamage(int damage) {
		health -= damage;
        if (health <= 0)
            return true;
        if (health > maxHits)
            health = maxHits;
        return false;
	}
}
