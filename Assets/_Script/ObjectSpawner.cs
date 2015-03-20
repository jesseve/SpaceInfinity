using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour {

	public GameObject[] objectsToSpawn;	//Contains the objects to spawn. Balloon, Meteor and Bird
	private List<GameObject> objects;
	private List<CollidableObject> objectScripts;
	public int uniquePoolAmount = 4;

	private LevelManager manager;

	public float maxX;				//The x-coordinate of the right side of screen
	public float minX;				//The x-coordinate of the left side of screen
	public float topY;				//The y-coordinate of top of screen

	public Vector3 upperSpawnPoint;
	public Vector3 sideSpawnPoint;

	private int maxObjects = 3;

	// Use this for initialization
	void Awake () {
		manager = Instances.scripts.levelmanager;
		objects = new List<GameObject>();
		objectScripts = new List<CollidableObject>();

		maxX = manager.upperRightCorner.x;
		minX = manager.bottomLeftCorner.x;
		topY = manager.upperRightCorner.y;

		upperSpawnPoint = new Vector3(0, topY);
		sideSpawnPoint = new Vector3(minX, 0);

		CreateObjectPool();

		//Debug.Log("Min X: " + minX + " MaxX: " + maxX + " TopY: " + topY + " Upper: " + upperSpawnPoint + " Side: " + sideSpawnPoint);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			SpawnObject();
		}
	}

	private void CreateObjectPool() {
		for(int i = 0; i < objectsToSpawn.Length; i++) {
			for(int j = 0; j < uniquePoolAmount; j++) {
				GameObject go = Instantiate(objectsToSpawn[i], Vector3.up * 200f, Quaternion.identity) as GameObject;
				objects.Add(go);
				CollidableObject co = go.GetComponent<CollidableObject>();
				objectScripts.Add(co);
				co.Init();
			}
		}
	}

	public void GameStarted() {

	}

	private void SpawnObject() {
		objectScripts[Random.Range(0, objectScripts.Count)].Spawn();
	}
}