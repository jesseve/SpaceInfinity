﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour {

	public GameObject[] enemiesToSpawn;	//Contains the objects to spawn. Balloon, Meteor and Bird
    public GameObject[] powerUpsToSpawn;
	
	public int enemyAmount = 4;
    public int powerUpAmount = 2;

	public GameObject objectParent;

	private LevelManager levelManager = null;

	//All values in world space
	public float maxX;				//The x-coordinate of the right side of screen
	public float minX;				//The x-coordinate of the left side of screen
	public float topY;				//The y-coordinate of top of screen
	public float minY;				//The y-coordinate of the bottom of the screen

	public Vector3 upperSpawnPoint;
	public Vector3 sideSpawnPoint;

	private int maxEnemies = 3;
	private int enemiesUsed = 0;

    private bool powerUpOnScreen = false;

	private string enemySpawningMethod = "EnemySpawningUpdate";
    private string powerUpSpawningMethod = "PowerUpSpawningUpdate";

	// Use this for initialization
	private void Start () {
		GameObject gameManager = GameObject.Find ("GameManager");
		levelManager = gameManager.GetComponent<LevelManager>();
		levelManager.OnStartGame += StartGame;
		levelManager.OnGameOver += GameOver;

		maxX = levelManager.upperRightCorner.x;
		minX = levelManager.bottomLeftCorner.x;
		topY = levelManager.upperRightCorner.y;
		minY = levelManager.bottomLeftCorner.y;

		upperSpawnPoint = new Vector3(0, topY);
		sideSpawnPoint = new Vector3(minX, 0);

		CreateObjectPool();

        //StartGame();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			SpawnEnemy();
		}
	}

	void OnDestroy()
	{
		if(levelManager!= null)
		{
			levelManager.OnStartGame -= StartGame;
			levelManager.OnGameOver -= GameOver;
			levelManager = null;
		}
	}

	private void StartGame() 
	{
		Invoke (enemySpawningMethod, 5);
	}

	private void GameOver() 
	{
		ResetPool();
	}

	private void ResetPool() 
	{
        ObjectPool.Instance.Reset();
    }

	private void EnemySpawningUpdate() 
	{
		bool chance = Random.Range(0, 2) > 0;

		if(chance == true) {
			SpawnEnemy();
		}
		Invoke (enemySpawningMethod, 1);
	}

	private void CreateObjectPool() {

        //Add enemies to pool
        for (int i = 0; i < enemiesToSpawn.Length; i++) {
			ObjectPool.Instance.AddToPool(enemiesToSpawn[i], enemyAmount, objectParent.transform);

			for(int j = 0; j < enemyAmount; j++) {
				GameObject g = ObjectPool.Instance.PopFromPool(enemiesToSpawn[i]);
				g.GetComponent<CollidableObject>().Init();

                g.name = g.name.Split('(')[0];  //Remove the '(Clone)' part of the name

				ObjectPool.Instance.PushToPool(ref enemiesToSpawn[i], ref g, objectParent.transform);
			}
		}

        //Add power ups to pool
        for (int i = 0; i < powerUpsToSpawn.Length; i++)
        {
            ObjectPool.Instance.AddToPool(powerUpsToSpawn[i], powerUpAmount, objectParent.transform);

            for (int j = 0; j < powerUpAmount; j++)
            {
                GameObject g = ObjectPool.Instance.PopFromPool(powerUpsToSpawn[i]);
                g.GetComponent<CollidableObject>().Init();

                g.name = g.name.Split('(')[0];  //Remove the '(Clone)' part of the name

                ObjectPool.Instance.PushToPool(ref powerUpsToSpawn[i], ref g, objectParent.transform);
            }
        }
    }

	private void SpawnEnemy() {
		if(enemiesUsed >= maxEnemies) return;

		GameObject go = ObjectPool.Instance.PopFromPool(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)]);

		if(go == null) return;

		CollidableObject co = go.GetComponent<CollidableObject>();
		co.Spawn();

		enemiesUsed++;
	}

    private void SpawnPowerUp() {
        if (powerUpOnScreen == true) return;    //Allow only one power up on screen at one time

        GameObject go = ObjectPool.Instance.PopFromPool(powerUpsToSpawn[Random.Range(0, powerUpsToSpawn.Length)]);

        if (go == null) return;

        CollidableObject co = go.GetComponent<CollidableObject>();
        co.Spawn();
    }

	public void ReturnToPool(GameObject go) {
		for(int i = 0; i < enemiesToSpawn.Length; i++) {
			if(enemiesToSpawn[i].name.Equals(go.name))
			{
				ObjectPool.Instance.PushToPool(ref enemiesToSpawn[i], ref go, objectParent.transform);

				enemiesUsed--;
                return;
			}
		}

        for (int i = 0; i < powerUpsToSpawn.Length; i++)
        {
            if (powerUpsToSpawn[i].name.Equals(go.name))
            {
                ObjectPool.Instance.PushToPool(ref powerUpsToSpawn[i], ref go, objectParent.transform);

                powerUpOnScreen = false;
                return;
            }
        }
    }
}