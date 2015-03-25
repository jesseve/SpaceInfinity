using UnityEngine;
using System.Collections;

public class Instances : MonoBehaviour {

	public static Instances scripts;

	public PlayerManager player;
	//public PlayerHealth health;
	public LevelManager levelmanager;
	public ObjectSpawner spawner;
	public CanvasManager canvas;

	// Use this for initialization
	void Awake () {
		scripts = this;
	}
}
