using UnityEngine;
using System.Collections;

public class CollidableObject : MonoBehaviour {

	public int damage = 1;	//The amount of damage the object does to the player
	public int size;		//The size of the object's width. The size is percentage of the screen width
	public int time;		//The time the object takes to travel across the screen

	protected PlayerManager player;		//Reference to the playermanager
	protected ObjectSpawner spawner;	//Reference to the spawner script

	//The edges of the screen in world point
	protected float topY;
	protected float minX;
	protected float maxX;

	protected float speed;	//The speed the object will travel with
	
	public bool Used {get {return isUsed;}}
	protected bool isUsed;

	public Vector3 spawnPoint;

	public virtual void OnTriggerEnter2D(Collider2D other) {
		if(other.tag.Equals("Player")) {
			HitPlayer();
		}
	}

	public virtual void Init() {

		//player = Instances.scripts.player;
		spawner = Instances.scripts.spawner;
		isUsed = false;

		topY = spawner.topY;
		minX = spawner.minX;
		maxX = spawner.maxX;

		//set the scale so that the object is certain percentage of the screen width
		transform.localScale = new Vector3(size * 0.02f * maxX, size * 0.02f * maxX);
	}

	public virtual void HitPlayer(){
		//player.HitObject(damage);
		ReturnToPool();
	}

	public virtual void Spawn() {
		transform.position = spawnPoint;
		isUsed = true;
		//Debug.Log("Spawnpoint: " + spawnPoint + " Position" + transform.position);
	}

	public virtual void ReturnToPool() {
		isUsed = false;
	}
}
