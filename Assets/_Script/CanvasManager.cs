using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Instead of using the LevelManager and check each frame if somethign changed
// Better to have the level Manager triggering an event that the CanvasManager is listening
// registration is done in start and OnDestroy to unregister
public class CanvasManager : MonoBehaviour {

	private LevelManager levelManager;

	[SerializeField] private GameObject startBtn = null;

	public Canvas menuCanvas;
	public Canvas pauseCanvas;
	public Canvas gameoverCanvas;
	public Canvas gameplayCanvas;

	// Use this for initialization
	private void Awake () {
		GameObject manager = GameObject.Find("GameManager");
		levelManager = manager.GetComponent<LevelManager>();
		levelManager.OnStartGame += HandleOnStartGame;

		if(startBtn == null)
		{
			startBtn = transform.Find ("StartButton").gameObject;
		}
	}

	private void OnDestroy()
	{
		if(levelManager != null)
		{
			levelManager.OnStartGame -= HandleOnStartGame;
			levelManager = null;
		}
	}

	private void HandleOnStartGame ()
	{
		startBtn.SetActive(false);
	}


	private void SetCanvas(Canvas canvas) {
		menuCanvas.enabled = false;
		pauseCanvas.enabled = false;
		gameoverCanvas.enabled = false;
		gameplayCanvas.enabled = false;

		canvas.enabled = true;
	}
}
