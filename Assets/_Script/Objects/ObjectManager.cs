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
		float angle = playerTr.eulerAngles.z;

		if(angle > 50)
		{
			angle = (360 - angle) * -1;
		}
		angle /= 45;

		float speed = angle * 2f;
		transform.Translate(Vector3.right * speed * Time.deltaTime);
	}

    public void EnableSlow() {
        Rigidbody2D[] rbs = transform.GetComponentsInChildren<Rigidbody2D>();
        foreach (Rigidbody2D r in rbs) {
            r.velocity *= 0.5f;
        }
    }

    public void DisableSlow() {
        Rigidbody2D[] rbs = transform.GetComponentsInChildren<Rigidbody2D>();
        foreach (Rigidbody2D r in rbs)
        {
            r.velocity *= 2;
        }
    }
}
