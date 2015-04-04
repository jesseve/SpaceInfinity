﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour {

	public GameObject[] objectsToSpawn;	//Contains the objects to spawn. Balloon, Meteor and Bird
	
	public int amount = 4;

	public GameObject objectParent;

	private LevelManager manager;

	//All values in world space
	public float maxX;				//The x-coordinate of the right side of screen
	public float minX;				//The x-coordinate of the left side of screen
	public float topY;				//The y-coordinate of top of screen
	public float minY;				//The y-coordinate of the bottom of the screen

	public Vector3 upperSpawnPoint;
	public Vector3 sideSpawnPoint;

	private int maxObjects = 3;
	private int objectsUsed = 0;

	private string spawningMethod = "SpawningUpdate";

	// Use this for initialization
	public void Init () {
		manager = Instances.scripts.levelmanager;

		maxX = manager.upperRightCorner.x;
		minX = manager.bottomLeftCorner.x;
		topY = manager.upperRightCorner.y;
		minY = manager.bottomLeftCorner.y;

		upperSpawnPoint = new Vector3(0, topY);
		sideSpawnPoint = new Vector3(minX, 0);

		CreateObjectPool();

		StartGame();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			SpawnObject();
		}
	}

	public void StartGame() {
		Invoke (spawningMethod, 5);
	}

	public void GameOver() {
		ResetPool();
	}

	public void ResetPool() {

	}

	private void SpawningUpdate() {
		bool chance = Random.Range(0, 2) > 0;

		if(chance == true) {
			SpawnObject();
		}
		Invoke (spawningMethod, 1);
	}

	private void CreateObjectPool() {
		for(int i = 0; i < objectsToSpawn.Length; i++) {
			ObjectPool.Instance.AddToPool(objectsToSpawn[i], amount, objectParent.transform);

			for(int j = 0; j < amount; j++) {
				GameObject g = ObjectPool.Instance.PopFromPool(objectsToSpawn[i]);
				g.GetComponent<CollidableObject>().Init();

				g.name = g.name.Split('(')[0];

				ObjectPool.Instance.PushToPool(ref objectsToSpawn[i], ref g, objectParent.transform);
			}
		}
	}

	private void SpawnObject() {
		if(objectsUsed >= maxObjects) return;

		GameObject go = ObjectPool.Instance.PopFromPool(objectsToSpawn[Random.Range(0, objectsToSpawn.Length)]);

		if(go == null) return;

		CollidableObject co = go.GetComponent<CollidableObject>();
		//co.Init();
		co.Spawn();

		objectsUsed++;
	}

	public void ReturnToPool(GameObject go) {
		for(int i = 0; i < objectsToSpawn.Length; i++) {
			if(objectsToSpawn[i].name.Equals(go.name))
			{
				ObjectPool.Instance.PushToPool(ref objectsToSpawn[i], ref go, objectParent.transform);

				objectsUsed--;
			}
		}
	}
}