using UnityEngine;
using System.Collections;
using System;

[DisallowMultipleComponent]
public class GameManager : StateMachine
{
	public static readonly Vector2 screenScale = new Vector2(1920, 1080);
	public delegate void ResolutionChanged();
	public static event ResolutionChanged resolutionChanged = delegate { };

	private int screenWidth;
	private int screenHeight;
	
	public virtual void Start()
	{        	
		screenWidth = Screen.width;
		screenHeight = Screen.height;
	}
	
	protected virtual void Update()
	{
		if (Screen.width != screenWidth || Screen.height != screenHeight)
		{
			screenWidth = Screen.width;
			screenHeight = Screen.height;
			if(resolutionChanged != null)
				resolutionChanged();
		}
	}
	protected override void InitStateMachine (bool debug)
	{

	}
}