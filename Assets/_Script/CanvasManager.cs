using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasManager : MonoBehaviour {

	private LevelManager levelManager;

	public Canvas menuCanvas;
	public Canvas pauseCanvas;
	public Canvas gameoverCanvas;
	public Canvas gameplayCanvas;

	// Use this for initialization
	public void Init () {
		levelManager = Instances.scripts.levelmanager;
//		levelManager.changeState += ChangeGameState;

		ChangeGameState();
	}

	private void ChangeGameState() {
		/*switch(levelManager.GetState()) {
		case State.Menu:
			SetCanvas(menuCanvas);
			break;
		case State.Pause:
			SetCanvas(pauseCanvas);
			break;
		case State.Running:
			SetCanvas(gameplayCanvas);
			break;
		case State.GameOver:
			SetCanvas(gameoverCanvas);
			break;
		}*/
	}

	private void SetCanvas(Canvas canvas) {
		menuCanvas.enabled = false;
		pauseCanvas.enabled = false;
		gameoverCanvas.enabled = false;
		gameplayCanvas.enabled = false;

		canvas.enabled = true;
	}
}
