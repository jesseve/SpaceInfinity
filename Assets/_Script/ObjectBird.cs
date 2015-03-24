using UnityEngine;
using System.Collections;

public class ObjectBird : CollidableObject {

	public override void Init ()
	{
		base.Init ();
		spawnPoint = spawner.sideSpawnPoint;
		speed = (maxX * 2) / time;
	}

	public override void Spawn ()
	{
		spawnPoint.y = Random.Range (topY * 0.5f, topY);
		base.Spawn();
		GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;

	}

	void Update() {

	}
}
