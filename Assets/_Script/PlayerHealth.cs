using UnityEngine;
using System.Collections;
using System;

public interface IHealth
{
	event Action OnDeath;
	event Action<int> OnHit;
	void ApplyDamage(int damage);
}

public class PlayerHealth : MonoBehaviour 
{
#region Variables


	private int health;
	public int maxHits = 3;
	private int maxDamage = 10; 			// this is to ensure we are not applying a large damage when not expected 

#endregion

#region Unity lifecycle

	private void Start () 
	{
		health = maxHits;
	}

#endregion

#region IHealthinterface
	public event Action OnDeath = ()=>{};
	public event Action<int> OnHit = (i)=>{};
	public void ApplyDamage(int damage) 
	{
		if(damage <= 0){
			Debug.Log ("Damage is 0");
			return;
		}
		if(damage > maxDamage)
		{
			Debug.Log ("Damage is too big");
			return;
		}
		health -= damage;
		if(health <= 0)
		{
			OnDeath();				// Simply report death to any listeners
		}
	}

#endregion

}

