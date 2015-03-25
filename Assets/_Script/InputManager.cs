using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputManager : MonoBehaviour {

	private LevelManager manager;
	private PlayerManager player;
	
	private float halfScreenHeight;

	// Use this for initialization
	public void Init () {
		halfScreenHeight = Screen.height * .5f;
		manager = Instances.scripts.levelmanager;
		player = Instances.scripts.player;
	}
	
	// Update is called once per frame
	void Update () {
		
		//Decide what happens when player presses the back/escape button
		if (Input.GetKeyDown(KeyCode.Escape)) {
			//TODO
		}

		RunningInput();

		//Detect input only when the game is running
		if (manager.GetState() != State.Running) return;
		

	}

	private void RunningInput(){
		int direction = 0;
		
		//Decide whether the player moves to left or right        
		if (Input.GetMouseButton(0)) {
			if(Input.mousePosition.y <= halfScreenHeight)
				direction = Input.mousePosition.x >= Screen.width * 0.5f ? 1 : -1;            
		}
		player.Move(direction);
	}
}
