using UnityEngine;
using System.Collections;

public interface IInput
{
	void Update();
}

public abstract class InputBase
{
	protected IInputManagerEvent inputManager = null;
	
	protected InputBase(IInputManagerEvent inputManager)
	{
		this.inputManager = inputManager;
	}
	
	protected void OneTouch(Touch touch)
	{
		switch(touch.phase)
		{
		case TouchPhase.Began:
			inputManager.TouchDown(touch.position);
			break;
		case TouchPhase.Moved:
			inputManager.TouchDrag(touch.position);
			break;
		case TouchPhase.Stationary:
			inputManager.TouchStationary(touch.position);
			break;
		case TouchPhase.Ended:
			inputManager.TouchEnd(touch.position);
			break;
		}
	}
}

public class EditorInput :InputBase,IInput
{
	private Vector3 previousPosition = Vector3.zero;
	
	public EditorInput(IInputManagerEvent inputManager) : base(inputManager)
	{
		
	}
	
	public void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			inputManager.TouchDown(Input.mousePosition);
			previousPosition = Input.mousePosition;
			return;
		}
		else if(Input.GetMouseButtonUp(0))
		{
			inputManager.TouchEnd(Input.mousePosition);
			return;
		}
		else if(Input.GetMouseButton(0))
		{
			
			Vector3 tempPosition = Input.mousePosition;
			if(previousPosition != tempPosition)
			{
				inputManager.TouchDrag(tempPosition);
			}
			else
			{
				inputManager.TouchStationary(tempPosition);
			}
			previousPosition = tempPosition;
			return;
		}
	}
}

public class MobileInput :InputBase,IInput
{
	public MobileInput(IInputManagerEvent inputManager):base(inputManager){}
	
	public void Update()
	{
		if(Input.touchCount > 0)
		{
			if(Input.touchCount == 1)
			{
				OneTouch(Input.touches[0]);
			}
		}
	}
}