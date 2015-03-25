﻿using UnityEngine;
using System.Collections;

public class ObjectMeteor : CollidableObject {

	private Vector3 targetPoint;	//Coordinates of the point the meteor will aim to

	public override void Init ()
	{
		base.Init ();
		spawnPoint = spawner.upperSpawnPoint;
	}
	
	public override void Spawn ()
	{
		spawnPoint.x = Random.Range(minX, maxX);
		base.Spawn();
		targetPoint = new Vector3(Random.Range(minX, maxX), -topY);
		float distance = Vector3.Distance(transform.position, targetPoint);
		speed = distance / time;
		GetComponent<Rigidbody2D>().velocity = (targetPoint - transform.position).normalized * speed;
	}
}