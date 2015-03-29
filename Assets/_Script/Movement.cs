using UnityEngine;
using System.Collections;
using System;

public interface IMovement
{
	bool ResetShipRotation();
	void RotateShip(Vector3 tapPosition);
}

public class Movement : IDisposable , IMovement
{

	private float speed = 0f;
	private float halfWidth = 0f;
	private Transform transform = null;

	public Movement(GameObject gameObject, float speed)
	{
		transform = gameObject.transform;
		this.speed = speed;
		halfWidth = Screen.width / 2f;
	}

	/// <summary>
	/// Resets the ship rotation. Returns true if ship rotation is fully done
	/// </summary>
	/// <returns><c>true</c>, if ship rotation is fully done, <c>false</c> otherwise.</returns>
	public bool ResetShipRotation()
	{
		float angle = transform.eulerAngles.z; 
		if(Mathf.Approximately(angle, 0f) == false)
		{
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f,0f,0f), Time.deltaTime *speed );
			return false;
		}
		return true;
	}

	public void RotateShip(Vector3 tapPosition)
	{
		float polarity = (tapPosition.x < halfWidth) ? 1f : -1f;
		transform.Rotate(Vector3.forward, polarity * Time.deltaTime * speed);
		float angle = transform.eulerAngles.z;
		transform.eulerAngles = new Vector3(0f, 0f, ClampAngle(angle, -45f,45f));
	}

	private float ClampAngle(float angle, float min, float max)
	{
		if(angle < 90f || angle > 270f)
		{
			if(angle > 180)
			{
				angle -= 360f;
			}
			if(max > 180)
			{
				max -= 360f;
			}
			if(min > 180)
			{
				min -=360f;
			}
		}
		angle = Mathf.Clamp (angle, min, max);
		if(angle < 0)
		{
			angle += 360f;
		}
		return angle;
	}

	#region IDisposable implementation

	public void Dispose ()
	{
		GC.SuppressFinalize(this);
	}

	#endregion
}
