using UnityEngine;
using System.Collections;

public class CollidableObject : MonoBehaviour {

	public int damage = 1;	//The amount of damage the object does to the player
	public int size;		//The size of the object's width. The size is percentage of the screen width

	private PlayerManager player;

	protected virtual void Start() {
		player = Instances.scripts.player;
	}

	public virtual void HitPlayer(){
		player.HitObject(damage);
	}
}
