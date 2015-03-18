using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float speed;
	//public float accelerateSpeed = 0.02f;
	private Vector2 velocity;
	private float gameAreaWidth;
	private Rigidbody2D rb;
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		velocity = new Vector3(speed, 0);
		//Calculates the area player can move to
		gameAreaWidth = (Camera.main.ScreenToWorldPoint(Vector3.right * Screen.width).x - Camera.main.ScreenToWorldPoint(Vector3.zero).x) * 0.5f;
		gameAreaWidth -= transform.localScale.x * .5f;
	}


	public void Move(int direction){
		Vector3 position = transform.position;
		float x = position.x;
		position.x = Mathf.Clamp(position.x, -gameAreaWidth, gameAreaWidth);
		if (position.x == x || CheckDirection(direction))
		{
			//acceleration = acceleration < 1 ? acceleration + accelerateSpeed : 1;
			rb.velocity = velocity * direction;
		}
		else
		{
			transform.position = new Vector3(gameAreaWidth * Mathf.Sign(position.x), position.y);
			rb.velocity = Vector2.zero;
		}        
	}
	private bool CheckDirection(int direction) 
	{
		if (Mathf.Sign(direction) != Mathf.Sign(transform.position.x) && direction != 0)
			return true;
		return false;
	}

	private float CalculatePlayerWidth() {
		SpriteRenderer s = GetComponent<SpriteRenderer>();
		float center = s.bounds.center.x;
		float side = s.bounds.max.x;
		Debug.Log("return: " + (center - side));
		Debug.Log("Side: " + side);
		Debug.Log("Center: " + center);
		return center - side;
	}
}
