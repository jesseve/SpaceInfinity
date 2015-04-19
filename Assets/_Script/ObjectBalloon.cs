using UnityEngine;
using System.Collections;

public class ObjectBalloon : CollidableObject {


	public override void Init ()
	{
		base.Init ();
		spawnPoint = spawner.upperSpawnPoint;
		speed = (topY * 2) / time;
	}

    protected override void CalculateSpawnPoint()
    {
        spawnPoint.x = Random.Range(minX * 1.5f, maxX * 1.5f);
    }

    public override void Spawn ()
	{
		base.Spawn();

		rig.velocity = Vector2.up * -speed;
	}
}
