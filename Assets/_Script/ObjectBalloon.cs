using UnityEngine;
using System.Collections;

public class ObjectBalloon : CollidableObject {


	public override void Init ()
	{
		base.Init ();
		spawnPoint = spawner.upperSpawnPoint;
		speed = (topY * 2) / time;
	}

	public override void Spawn ()
	{
		spawnPoint.x = Random.Range(minX, maxX);
		base.Spawn();
		GetComponent<Rigidbody2D>().velocity = Vector2.up * -speed;
	}
}
