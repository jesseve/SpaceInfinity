using UnityEngine;
using System.Collections;

public class ObjectManager : MonoBehaviour 
{
	private Transform playerTr = null;

	void Start() 
	{
		GameObject player = GameObject.Find("Player");
		playerTr = player.transform;
	}

	private void Update() 
	{
		if(Mathf.Approximately(playerTr.eulerAngles.z, 0)) 
		{
			return;
		}
		float polarity = playerTr.eulerAngles.z;

		if(polarity > 50)
		{
			polarity = (360 - polarity) * -1;
		}
		polarity /= 45;

		float speed = polarity * 2f;
		transform.Translate(Vector3.right * speed * Time.deltaTime);
	}
}
