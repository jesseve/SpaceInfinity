using UnityEngine;
using System.Collections;

public class BackGroundScript : MonoBehaviour {

	private Vector2 velocity;

	[Range(0, 3)]
	public float verticalSpeed;

    [Range(0, 10)]
	public float horizontalSpeed = 0;

	private Transform player;
    private Rigidbody2D rb;

    public GameObject backgroundPrefab;

    public BackgroundTransition[] transitions;
    private int currentTransition;

    private Vector2 respawnPosition;

    private SpriteRenderer[][] bgSprites; //This is to access the sprites and change them when necessary

    void OnEnable() {
        EventManager.StartListening("ChangeBackground", StartTransition);
    }
    void OnDisable() {
        EventManager.StopListening("ChangeBackground", StartTransition);
    }

	void Start() {
        respawnPosition = new Vector2();

        CreateBackgrounds();
		player = GameObject.Find("Player").transform;        

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -verticalSpeed);
        velocity = rb.velocity;
	}

	void Update() {

        if (transform.position.y < -respawnPosition.y) {
            transform.position = new Vector3(transform.position.x, respawnPosition.y, 0);
        }
        if (transform.position.x < -respawnPosition.x) {
            transform.position = new Vector3(respawnPosition.x, transform.position.y, 0);
        }
        else if (transform.position.x > respawnPosition.x) {
            transform.position = new Vector3(-respawnPosition.x, transform.position.y, 0);
        }        

        velocity.x = 0;

        if (!Mathf.Approximately(player.eulerAngles.z, 0))
        {
            float angle = player.eulerAngles.z > 180 ? 360 - player.eulerAngles.z : player.eulerAngles.z;
            int direction = player.eulerAngles.z > 180 ? -1 : 1;
            velocity.x = (angle / 45) * horizontalSpeed * direction;
        }        
         rb.velocity = velocity;
	}

    void SetSpriteForCamera(GameObject go) 
    {        
        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector3 vec = Vector3.one;
        vec.x = worldScreenWidth;
        vec.y =  worldScreenHeight;
        go.transform.localScale = vec;
    }

    private void CreateBackgrounds() {
        bgSprites = new SpriteRenderer[3][];

        int index = 0;
        GameObject go = null;

        for (int i = -1; i < 2; i++) {
            
            bgSprites[i + 1] = new SpriteRenderer[3];
            
            for (int j = -1; j < 2; j++) {
                
                go = Instantiate(backgroundPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                
                SetSpriteForCamera(go);
                Vector3 scale = go.transform.localScale;

                go.name = "BG" + index.ToString();
                go.transform.position = new Vector3(j * scale.x, i * scale.y, 0);
                go.transform.SetParent(transform);
                
                bgSprites[i+1][j+1] = go.GetComponent<SpriteRenderer>();                

                index++;
            }
        }
        respawnPosition = go.transform.localScale;
    }

    private void StartTransition() {
        BackgroundTransition bgt = transitions[currentTransition];
        StartCoroutine(TransitToSprite(bgt.transition, bgt.next));
        
        if(currentTransition < transitions.Length - 1)
            currentTransition++;
    }

    private IEnumerator TransitToSprite(Sprite transitionBg, Sprite nextBg) {
        if (transform.position.y > 0)
            while (transform.position.y > 0)
                yield return null;

        while (transform.position.y <= 0)
            yield return null;

        ChangeRowSprite(transitionBg, 1);
        ChangeRowSprite(nextBg, 2);

        while (transform.position.y > 0)
            yield return null;

        ChangeRowSprite(nextBg, 0);

        while (transform.position.y <= 0)
            yield return null;

        ChangeRowSprite(nextBg, 1);
    }

    private void ChangeRowSprite(Sprite s, int row) {
        row = Mathf.Clamp(row, 0, 2);
        foreach (SpriteRenderer sr in bgSprites[row]) {
            sr.sprite = s;
        }
    }
}
