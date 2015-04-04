using UnityEngine;
using System.Collections;

public class ObjectManager : MonoBehaviour {

	private float halfWidth = 0;

	private Transform player;

	void Start() {
		player = Instances.scripts.player.transform;
		halfWidth = Screen.width * 0.5f;
	}

	private void Update() {
		if(Mathf.Approximately(player.eulerAngles.z, 0)) {
			return;
		}
		float polarity = player.eulerAngles.z;

		if(polarity > 50)
			polarity = (360 - polarity) * -1;
		polarity /= 45;

		float speed = polarity * 2f;


		transform.Translate(Vector3.right * speed * Time.deltaTime);
	}
}
