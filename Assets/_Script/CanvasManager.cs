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
	}


	private void SetCanvas(Canvas canvas) {
		menuCanvas.enabled = false;
		pauseCanvas.enabled = false;
		gameoverCanvas.enabled = false;
		gameplayCanvas.enabled = false;

		canvas.enabled = true;
	}
}
