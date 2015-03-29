using UnityEngine;
using System.Collections;

public class PlayerManager : StateMachine 
{

#region Variables

	[SerializeField] private float speedRotation = 100;
	[SerializeField] private GameObject inputGameObject = null;
	private IMovement movement = null;
	private ITouchInputEventHandler inputEvent = null;
	private bool isTapping = false;
	private Vector3 position = Vector3.zero;

#endregion

#region Unity lifecycle

	private void Awake () 
	{
		movement = new Movement(this.gameObject,speedRotation) as IMovement;

		if(inputGameObject == null)
		{
			inputGameObject = GameObject.Find ("InputSystem");
		}
		inputEvent = (ITouchInputEventHandler)inputGameObject.GetComponent(typeof(ITouchInputEventHandler));
		if(inputEvent != null)
		{
			inputEvent.OnTap += HandleOnTap;
			inputEvent.OnRemove += HandlOnRemove;
		}
		InitStateMachine(true);
	}

	private void Update()
	{
		StateUpdate();
	}

#endregion

#region StateMachine

	private void UpdateIdle()
	{
		if(isTapping)
		{
			RequestState(State.Rotate);
		}
	}
	private void EnterReset(string oldState)
	{
		isTapping = false;
	}
	private void UpdateReset()
	{
		if(isTapping)
		{
			RequestState("Rotate");
			return;
		}
		if(movement.ResetShipRotation() == true)
		{
			RequestState(State.Idle);
		}
	}
	private void EnterRotate(string oldState)
	{
		isTapping = false;
	}
	private void UpdateRotate()
	{
		movement.RotateShip(position);
	}
	
	private void HandleOnTap (Vector3 position)
	{
		this.position = position;
		isTapping = true;
	}
	private void HandlOnRemove(Vector3 position)
	{
		RequestState(State.Reset);
	}

	protected override void InitStateMachine(bool debug)
	{
		InitializeStateMachine(debug);
		AddStateWithTransitions(State.Idle, new string[]{State.Rotate});
		AddStateWithTransitions(State.Rotate, new string[]{State.Reset});
		AddStateWithTransitions(State.Reset, new string[]{State.Rotate, State.Idle});
		RequestState(State.Idle);
	}
	class State{
		public static string Reset = "Reset";
		public static string Rotate = "Rotate";
		public static string Idle = "Idle";
	}

#endregion

}
