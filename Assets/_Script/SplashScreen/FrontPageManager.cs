using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FrontPageManager : MonoBehaviour 
{
	[SerializeField]
	private Image image;
	[SerializeField]
	private Sprite sprite;
	
	void Awake() 
	{
		AudioSource audioObject = FindObjectOfType<AudioSource>() as AudioSource;
		DontDestroyOnLoad(audioObject.gameObject);
		SetBackgroundTexture();
	}
	
	public void LoadNextScene() 
	{
		Application.LoadLevel("LevelSelection");
	}
	private void SetBackgroundTexture() 
	{
		Sprite backgroundSprite = CheckForSeasonalSprite();
		image.sprite = (backgroundSprite != null) ? backgroundSprite : SetOriginalSprite(); 
	}
	
	private Sprite CheckForSeasonalSprite() 
	{
		GameObject persistentObject = GameObject.Find("TextureObject");
		if (persistentObject == null)
		{
			return null;
		}
		Sprite sprite = persistentObject.GetComponent<SpriteRenderer>().sprite;
		return sprite;
	}
	
	private Sprite SetOriginalSprite() 
	{
		// Returns a texture based on device
		return sprite;
	}
}