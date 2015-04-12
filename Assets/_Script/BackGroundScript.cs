using UnityEngine;
using System.Collections;

public class BackGroundScript : MonoBehaviour {

	private MeshRenderer rend;
	private Vector2 velocity;

	[Range(0, 1)]
	public float verticalSpeed;
	private float horizontalSpeed = 0;

	private Transform player;

	void Start() {
		rend = GetComponent<MeshRenderer>();
		player = GameObject.Find("Player").transform;
	}

	void Update() {

		if(!Mathf.Approximately(player.eulerAngles.z, 0)) {
			float angle = player.eulerAngles.z > 180 ? 360 - player.eulerAngles.z : player.eulerAngles.z;
			horizontalSpeed = player.eulerAngles.z > 180 ? 0.01f : -0.01f;
			velocity.x += horizontalSpeed * angle * Time.deltaTime;
			if(velocity.x > 1)
				velocity.x = 0;
			else if(velocity.x < 0)
				velocity.x = 1;
		}
		velocity.y = Mathf.Repeat(Time.time * verticalSpeed, 1);
		rend.sharedMaterial.SetTextureOffset("_MainTex", velocity);
	}
}
