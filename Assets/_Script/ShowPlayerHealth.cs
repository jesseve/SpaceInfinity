using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowPlayerHealth : MonoBehaviour {

	private PlayerManager player;

	private Text text;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").GetComponent<PlayerManager>();
		text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "Health: " + player.GetPlayerHealth().ToString();
	}
}
