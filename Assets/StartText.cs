using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class StartText : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager = null;
    [SerializeField]
    private Text text = null;

    private string[] texts = { "Ready", "Set", "GO!!!" };

	// Use this for initialization
	void Awake () 
    {
        gameManager.OnEnterStart += OnEnterStart;
        text.text = texts[0];
	}
	
	private void OnEnterStart (Action action) 
    {
        Debug.Log("Enter");
        StartCoroutine(UpdateCoroutine(action));
	}

    private IEnumerator UpdateCoroutine(Action action) 
    {
        float timer = 0f;
        int index = 0;
        int lastIndex = index;
        while (true) 
        {
            timer += Time.deltaTime;
            index = (int)timer;
            if (index != lastIndex) 
            {
                lastIndex = index;
                if (lastIndex == texts.Length) 
                {
                    break;
                }
                text.text = texts[lastIndex];
            }
            yield return null;
        }
        action();
        text.enabled = false;
        this.enabled = false;
    }
}
