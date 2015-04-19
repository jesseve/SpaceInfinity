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
		base.Spawn();

		rig.velocity = (Vector2.right * speed) - (Vector2.up * speed * 0.5f);

	}

    protected override void CalculateSpawnPoint()
    {
        spawnPoint.y = Random.Range(topY * 0.5f, topY);
    }

    protected override void IsOutOfBounds() {
		base.IsOutOfBounds();
		if(transform.position.x > maxX)
			ReturnToPool();
	}
}
