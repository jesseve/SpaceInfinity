using UnityEngine;
using System.Collections;

public class TouchControlHandler : 	MonoBehaviour
{
	private IInputManager inputManager = null;
	private ITouchInputEventHandler inputController = null;
	
	private void Awake () 
	{
		inputManager = (IInputManager)GetComponent(typeof(IInputManager));	
		inputController = (ITouchInputEventHandler)GetComponent(typeof(ITouchInputEventHandler));
		inputManager.RegisterEvent(inputController);
	}
	
	private void OnDestroy()
	{
		if(inputManager != null)
		{
			inputManager.UnregisterEvent(inputController);
		}
	}
}