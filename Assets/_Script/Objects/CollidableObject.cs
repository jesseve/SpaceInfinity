using UnityEngine;
using System.Collections;

public class CollidableObject : MonoBehaviour {

	public int damage = 1;	//The amount of damage the object does to the player
	public int size;		//The size of the object's width. The size is percentage of the screen width
	public int time = 8;		//The time the object takes to travel across the screen

	protected PlayerManager player;		//Reference to the playermanager
	protected ObjectSpawner spawner;	//Reference to the spawner script

	//The edges of the screen in world point
	protected float topY;
	protected float minX;
	protected float maxX;
	protected float minY;

	protected float speed;	//The speed the object will travel with

	public Vector3 spawnPoint;

	protected Rigidbody2D rig = null;

	public virtual void OnTriggerEnter2D(Collider2D other) {
		if(other.tag.Equals("Player")) {
			HitPlayer();
		}
	}

	protected virtual void IsOutOfBounds() {
		if(transform.position.y < minY)
			ReturnToPool();
	}

	public virtual void Init() {

		player = GameObject.Find("Player").GetComponent<PlayerManager>();
		spawner = GameObject.Find("Spawner").GetComponent<ObjectSpawner>();

		minX = spawner.minX;
		maxX = spawner.maxX;

		//set the scale so that the object is certain percentage of the screen width
		transform.localScale = new Vector3(size * 0.02f * maxX, size * 0.02f * maxX);

		topY = spawner.topY;
		minY = spawner.minY - (transform.localScale.y * 0.5f);

		rig = GetComponent<Rigidbody2D>();

        spawnPoint = spawner.upperSpawnPoint;
        speed = (topY * 2) / time;
    }

	public virtual void HitPlayer(){
		player.HitObject(damage);
		ReturnToPool();
	}

	public virtual void Spawn() {
        CalculateSpawnPoint();

		transform.position = spawnPoint;
		InvokeRepeating("IsOutOfBounds", 1, 2);

        rig.velocity = Vector2.up * -speed;
    }

    protected virtual void CalculateSpawnPoint() {
        //Default position is a random point on the x-axis at the top of screen
        spawnPoint.x = Random.Range(minX * 1.5f, maxX * 1.5f);
    }

	public virtual void ReturnToPool() {
		if(IsInvoking("IsOutOfBounds"))
		   CancelInvoke("IsOutOfBounds");
		rig.velocity = Vector2.zero;
		transform.position = Vector3.up * 200;

		spawner.ReturnToPool(this.gameObject);
	}
}
