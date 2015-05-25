using UnityEngine;
using System.Collections;

public class ObjectMeteor : Enemy
{

	private Vector3 targetPoint;	//Coordinates of the point the meteor will aim to	

    protected override void CalculateSpawnPoint()
    {
        spawnPoint.x = Random.Range(minX * 2, maxX * 2);
    }    

    protected override Vector2 CalculateVelocity()
    {
        targetPoint = new Vector3(Random.Range(minX, maxX), -topY);
        float distance = Vector3.Distance(transform.position, targetPoint);
        speed = distance / travelTimeVertical;
        return (targetPoint - transform.position).normalized * speed;
    }
}
