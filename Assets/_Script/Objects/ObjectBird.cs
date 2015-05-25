using UnityEngine;
using System.Collections;

public class ObjectBird : Enemy {

    public float horizotalTime;

    private float horizontalSpeed;

	public override void Init ()
	{
		base.Init ();
		spawnPoint = spawner.sideSpawnPoint;
        horizontalSpeed = (maxX * 2) / horizotalTime;
	}	

    protected override Vector2 CalculateVelocity()
    {
        return (Vector2.right * horizontalSpeed) - (Vector2.up * speed * 0.5f);
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
