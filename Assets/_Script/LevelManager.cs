using UnityEngine;
using System.Collections;

public class LevelManager : GameManager {
	
	public Vector3 upperRightCorner;
	public Vector3 bottomLeftCorner;

	// Use this for initialization
	public override void Awake () {
		base.Awake();
		upperRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
		Debug.Log(upperRightCorner);
		bottomLeftCorner = Camera.main.ScreenToWorldPoint(Vector3.zero);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	protected override void SetupManager()
	{
	
	}

	public void StartGame() {

	}

	public void Pause() {

	}

	public void GameOver() {

	}
}
