using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Collections;
using JSON;

/// <summary>
/// SplashScreen takes care of displaying the company logo
/// It also searches for a specific texture to be displayed on the Front page
/// This allows for seasonal front page.
/// If one is found then the normal one will go
/// </summary>
public class SplashScreen : MonoBehaviour 
{
	class Data 
	{
		public string Error { get; set; }
		public bool IsSeasonal { get; set;}
		public string Url { get; set; }
	}
	[SerializeField]
	private Image image;
	
	private Texture2D seasonalTexture = null;
	private string cacheUrl = null;
	private bool isRunning = false;
	private bool waitForTexture = false;
	
	void Awake() 
	{
		
		Color color = image.color;
		color.a = 0f;
		image.color = color;
		
		cacheUrl = Application.dataPath + "/../Seasonal.jpg";
		StartCoroutine(Init());
		StartCoroutine(FadeInCoroutine(1f,
		                               () => { isRunning = false; }));
	}
	
	private IEnumerator Init() 
	{
		waitForTexture = true;
		Data data = new Data();
		
		yield return StartCoroutine(GetInfoFileFromServer("users.metropolia.fi/~lucasc/info.json", data));
		
		if (data.Error != null) // could not connect, check the cache
		{
			Debug.Log("No connection");
			seasonalTexture = CheckCacheForSeasonalTexture(cacheUrl);
		}
		
		if(data.Error == null)
		{
			if (data.IsSeasonal == false)  // Could connect and no seasonal, remove from cache
			{
				Debug.Log("Clear");
				ClearSeasonalTextureFromCache(cacheUrl);
			}
			else 
			{
				// Could connect and should fnd seasonal
				// Look in cache
				seasonalTexture = CheckCacheForSeasonalTexture(cacheUrl);
				if (seasonalTexture == null) // Could not find in the cache, check online
				{
					yield return StartCoroutine(CheckServerForSeasonalTexture(data.Url));
				}
			}     
		}
		if (seasonalTexture != null)  // found a texture
		{
			SaveTextureToCache(seasonalTexture, cacheUrl);  // place in cache
			SaveTextureToPersistentObject(seasonalTexture); // Create a persistent object
		}
		waitForTexture = false;
	}
	
	private IEnumerator GetInfoFileFromServer(string url, Data data) 
	{
		WWW www = new WWW(url);
		yield return www;
		if (www.error != null) 
		{
			data.Error = www.error;
			yield break;
		}
		
		string info = www.text;
		JSONObject json = JSONObject.Parse(info);
		data.IsSeasonal = json.GetBoolean("Seasonal");
		data.Url = json.GetString("url");
	}
	
	
	
	private IEnumerator FadeInCoroutine(float target, System.Action action) 
	{
		isRunning = true;
		Color color = image.color;
		while (color.a != target)
		{
			color.a = Mathf.MoveTowards(color.a, target, 1f * Time.deltaTime); ;
			image.color = color;
			yield return null;
		}
		action();
	}
	
	private IEnumerator CheckServerForSeasonalTexture(string url) 
	{
		WWW www = new WWW(url);
		yield return www;
		if (www.error != null)
		{
			ClearSeasonalTextureFromCache(url);
			yield break;
		}
		seasonalTexture = www.texture;
	}
	
	private void ClearSeasonalTextureFromCache(string url)
	{
		if (File.Exists(url) == true) { File.Delete(url); }     
	}
	
	private Texture2D CheckCacheForSeasonalTexture(string url) 
	{
		if (File.Exists(url) == false)
		{
			return null;
		}
		byte[] file = File.ReadAllBytes(url);
		if (file == null)
		{
			return null;
		}
		Debug.Log("Found Image");
		Texture2D texture = new Texture2D(4,4);
		texture.LoadImage(file);
		return texture;
	}
	
	private void SaveTextureToCache(Texture2D texture, string url)
	{
		ClearSeasonalTextureFromCache(url);
		var bytes = texture.EncodeToJPG();
		File.WriteAllBytes(url, bytes);
	}
	
	private void SaveTextureToPersistentObject(Texture2D texture) 
	{
		GameObject obj = new GameObject("TextureObject");
		Object.DontDestroyOnLoad(obj);
		SpriteRenderer sr = obj.AddComponent<SpriteRenderer>();
		Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
		sr.sprite = sprite;
	}
	
	public void LaunchFrontScene()
	{
		if (isRunning == true || waitForTexture == true)
		{
			return;
		}
		StartCoroutine(FadeInCoroutine(0f,
		                               () => Application.LoadLevel("Front")));
	}
}