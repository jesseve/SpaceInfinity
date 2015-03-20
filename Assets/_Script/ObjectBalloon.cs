using UnityEngine;
using System.Collections;

public class ObjectBalloon : CollidableObject {


	public override void Init ()
	{
		base.Init ();
		spawnPoint = spawner.upperSpawnPoint;
	}

	public override void Spawn ()
	{
		spawnPoint.x = Random.Range(minX, maxX);
		base.Spawn();
	}
}
