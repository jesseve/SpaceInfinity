﻿using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour 
{

	class State {
		public string name;
		public List<string> transitions;
		public bool allowAnyTransition;
		public Action updateMethod = ()=>{};
		public Action<string> enterMethod = ( oldState) => { };
		public Action<string> exitMethod = ( oldState) => { };
		
		public State(string name) {
			this.name = name;
			this.allowAnyTransition = false;
			this.transitions = new List<string>();
		}
	}
	
	private Dictionary<string, State> states;
	
	private State currentState = null;
	private State transitionSource = null;
	private State transitionTarget = null;
	private bool inTransition = false;
	
	private bool initialized = false;
	private bool debugTransitions;
	
	private Action OnUpdate = () => { };
	
	public string CurrentState { get { return currentState.name; } }

	protected abstract void InitStateMachine(bool debug);

	protected void InitializeStateMachine( bool debug ) 
	{
		if (initialized) 
		{
			Debug.LogWarning( GetType().ToString() + " is trying to initialize statefulness multiple times." );
			return;
		}
		
		this.states = new Dictionary<string, State>();
		
		this.AddState( "InitialState" );
		State initial = states["InitialState"];
		initial.allowAnyTransition = true;
		
		currentState = initial;
		inTransition = false;
		transitionSource = null;
		transitionTarget = null;
		initialized = true;
		debugTransitions = debug;
	}
	
	protected bool IsLegalTransition(string fromstate, string tostate) 
	{
		if (states.ContainsKey(fromstate) && states.ContainsKey(tostate)) 
		{
			if (states[fromstate].allowAnyTransition || states[fromstate].transitions.Contains(tostate)) 
			{
				return true;
			}
		}
		return false;
	}
	
	private void TransitionTo(string newstate) 
	{
		transitionSource = currentState;
		transitionTarget = states[newstate];
		inTransition = true;
		
		currentState.exitMethod(transitionTarget.name);
		transitionTarget.enterMethod(currentState.name);
		currentState = transitionTarget;
	}
	
	private void FinalizeCurrentTransition() 
	{
		if (transitionTarget == null || transitionSource == null)
		{
			Debug.LogError(this.GetType().ToString() + " cannot finalize transition; source or target state is null!");
			return;
		}
		
		inTransition = false;
		transitionSource = null;
		transitionTarget = null;
	}
	
	protected bool RequestState(string newstate) 
	{
		if (!initialized) 
		{
			Debug.LogError( this.GetType().ToString()+ " requests transition to state " + newstate + " but statefulness is not initialized!");
			return false;            
		}
		
		if (inTransition) 
		{
			if (debugTransitions)
			{
				Debug.Log(this.GetType().ToString() + " requests transition to state " + newstate +
				          " when still transitioning to state " + transitionTarget.name);
			}
			return false;
		}
		
		if( IsLegalTransition( currentState.name, newstate )) 
		{
			if (debugTransitions) 
			{
				Debug.Log( this.GetType().ToString() + " transition: " + currentState.name + " => " + newstate );
			}
			
			TransitionTo(newstate);
			FinalizeCurrentTransition();
		} 
		else 
		{
			Debug.LogError( this.GetType().ToString() + " requests transition: " + currentState.name + " => " + newstate + " but it is not a legal transition!" );
			return false;
		}
		OnUpdate = null;
		OnUpdate = currentState.updateMethod;
		return true;
	}
	
	protected void AddState(string newstate) 
	{
		State s = new State( newstate );
		System.Type ourType = this.GetType(); 
		
		MethodInfo update = ourType.GetMethod("Update" + newstate, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
		MethodInfo enter = ourType.GetMethod("Enter" + newstate, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
		MethodInfo exit = ourType.GetMethod("Exit" + newstate, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
		
		if (update != null)
		{
			s.updateMethod = (Action)Delegate.CreateDelegate(typeof(Action), this, update);
		}
		if (enter != null)
		{
			s.enterMethod = (Action<string>)Delegate.CreateDelegate(typeof(Action<string>), this,enter);
		}
		if (exit != null)
		{
			s.exitMethod = (Action<string>)Delegate.CreateDelegate(typeof(Action<string>), this, exit);
		}
		this.states.Add( newstate, s );
	}
	
	protected void AddStateWithTransitions(string newstate, string[] transitions) 
	{
		AddState(newstate);
		State s = states[newstate];
		
		foreach (string t in transitions) 
		{
			s.transitions.Add( t );
		}
	}
	
	protected virtual void StateUpdate() 
	{
		OnUpdate();
	}
}