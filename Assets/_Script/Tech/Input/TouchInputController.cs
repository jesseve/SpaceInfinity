using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;

/// <summary>
/// Touch input controller. Listens to the Input manager and dispatches the information to any listeners 
/// </summary>
[DisallowMultipleComponent]
public class TouchInputController : MonoBehaviour, 
ITouchInputEventHandler
{	
	
#region Variables
	
	private ITouchInputEventListener [] listeners = null;							
	
#endregion
	
#region Unity lifecycle
	
	private void Awake()
	{
		#if !UNITY_EDITOR
		//fingerID = 0;
		#endif
		
		GameObject [] objs = GameObject.FindObjectsOfType<GameObject>(); 
		List<ITouchInputEventListener>list = new List<ITouchInputEventListener>();
		foreach(GameObject obj in objs)
		{
			ITouchInputEventListener listener = (ITouchInputEventListener)obj.GetComponent(typeof(ITouchInputEventListener));
			if(listener != null)
			{
				listener.Register(this);
				list.Add(listener);
			}
		}
		listeners = list.ToArray();
	}
	
	private void OnDestroy()
	{
		foreach(ITouchInputEventListener listener in listeners)
		{
			listener.Unregister(this);
		}
	}
	
#endregion
	
#region ITouchInputEventHandler implementation
	
	public event Action<Vector3> OnTap = (pos) => { };
	public event Action<Vector3> OnRemove = (pos) => {};
	public event Action OnGUIAction = ()=>{};

#endregion

#region ITouchInputEventListener

	public void OnTouchDown (Vector3 position)
	{	
		OnTap(position);
	}

	public void OnTouchDrag (Vector3 position)
	{
		OnTap(position);
	}
	
	// The user removed the touch
	public void OnTouchEnd (Vector3 position)
	{
		OnRemove(position);
	}
	
	public void OnTouchStationary(Vector3 position)
	{
		OnTap(position);
	}
	
	#endregion
	
	#region Methods

	
	#endregion
	
}

public interface ITouchInputEventListener
{
	void Register(ITouchInputEventHandler handler);
	void Unregister(ITouchInputEventHandler handler);
}

// Implemented in TouchInputController
// The method that gets registered to the IInputManager
public interface ITouchInputEventHandler
{
	void OnTouchDown(Vector3 position);
	void OnTouchDrag(Vector3 position);
	void OnTouchEnd (Vector3 position);
	void OnTouchStationary(Vector3 position);
	event Action<Vector3> OnTap;
	event Action <Vector3>OnRemove;
	event Action OnGUIAction;
}