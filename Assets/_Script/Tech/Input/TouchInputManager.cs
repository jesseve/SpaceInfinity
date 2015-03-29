using UnityEngine;
using System.Collections;
using System;

public class TouchInputManager : MonoBehaviour , IInputManager, IInputManagerEvent
{
	private Action<Vector3> OnTouchDown = (vec)=>{};
	private Action<Vector3> OnTouchDrag = (vec)=>{};
	private Action<Vector3> OnTouchEnd  = (vec)=>{};
	private Action<Vector3> OnTouchStationary =  (vec)=>{};
	
	private IInput input = null;
	
	public DeviceOrientation CurrentDeviceOrientation{get{return Input.deviceOrientation;}}
	
	private Action UpdateMethod = () => {};
	
	private void Awake()
	{
		RegisterUpdateMethod();
	}
	
	private void OnDestroy()
	{
		ResetEvent();
	}
	private void Update()
	{
		UpdateMethod();
	}
	
	public void RegisterEvent(ITouchInputEventHandler listener)
	{
		if(listener == null)
		{
			return;
		}
		this.OnTouchDown += listener.OnTouchDown;
		this.OnTouchDrag += listener.OnTouchDrag;
		this.OnTouchEnd  += listener.OnTouchEnd;
		this.OnTouchStationary += listener.OnTouchStationary;
	}
	
	public void UnregisterEvent(ITouchInputEventHandler listener)
	{
		if(listener == null)
		{
			return;
		}
		this.OnTouchDown -= listener.OnTouchDown;
		this.OnTouchDrag -= listener.OnTouchDrag;
		this.OnTouchEnd  -= listener.OnTouchEnd;
		this.OnTouchStationary -= listener.OnTouchStationary;
	}
	
	private void ResetEvent()
	{
		this.OnTouchDown = null;
		this.OnTouchDrag = null;
		this.OnTouchEnd  = null;
		this.OnTouchStationary = null;
		
		this.OnTouchDown = (position) => { };
		this.OnTouchDrag = (position) => { };
		this.OnTouchDown = (position) => { };
		this.OnTouchStationary = (position) => { };
	}
	
	private void RegisterUpdateMethod()
	{
		#if UNITY_EDITOR
		input = new EditorInput(this as IInputManagerEvent);
		#else
		input = new MobileInput(this as IInputManagerEvent);
		#endif
		UpdateMethod = input.Update;
	}
	
	private void UnregisterUpdateMethod()
	{
		input = null;
		UpdateMethod = ()=> { };
	}
	
	
	#region IInputManagerEvent implementation
	
	public void TouchDown (Vector3 vec)
	{
		OnTouchDown(vec);
	}
	
	public void TouchDrag (Vector3 vec)
	{
		OnTouchDrag(vec);
	}
	
	public void TouchEnd (Vector3 vec)
	{
		OnTouchEnd(vec);
	}
	
	public void TouchStationary (Vector3 vec)
	{
		OnTouchStationary(vec);
	}
	
	#endregion
}

// Implemented in the TouchInputManager and used in the TouchInputController
// TouchInputController uses it to listen to the input event
public interface IInputManager
{
	void RegisterEvent(ITouchInputEventHandler listener);
	void UnregisterEvent(ITouchInputEventHandler listener);
	DeviceOrientation CurrentDeviceOrientation {get;}
}

public interface IInputManagerEvent
{
	void TouchDown(Vector3 vec);
	void TouchDrag(Vector3 vec);
	void TouchEnd (Vector3 vec);
	void TouchStationary(Vector3 vec);
}