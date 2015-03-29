using UnityEngine;
using System.Collections;

public class LevelManager : GameManager {
	
	public Vector3 upperRightCorner;
	public Vector3 bottomLeftCorner;

	private ObjectSpawner spawner;
	private PlayerManager player;
	private CanvasManager canvas;

	// Use this for initialization
	public override void Start () {
		base.Start();
		upperRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
		bottomLeftCorner = Camera.main.ScreenToWorldPoint(Vector3.zero);

		Init ();
	}

	private void Init() {
		spawner = Instances.scripts.spawner;
		player = Instances.scripts.player;
		canvas = Instances.scripts.canvas;

		player.Init();
		spawner.Init ();
		canvas.Init();
	}

	public void StartGame() {
		player.StartGame();
		spawner.StartGame();

		//SetState(State.Running);
	}

	public void Pause() {
		/*if(GetState() == State.Pause) {
			SetState(State.Running);
		} else if(GetState() == State.Running) {
			SetState(State.Pause);
		}*/
	}

	public void GameOver() {

		spawner.GameOver();

		//SetState(State.GameOver);
	}
}
