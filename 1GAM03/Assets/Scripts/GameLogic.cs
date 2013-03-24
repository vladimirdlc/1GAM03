using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class GameLogic : MonoBehaviour {
    public const int totalDivisions = 30;
    public const int totalRows = 5;
    public const int totalCollumms = 6;
    public float randomOffsetPercent = 0.01f;
    public static int currentLevel = 1;
    public int screenMargin = 100;
    public GameObject bongBase;
    public int mask = 1 << 8;

    public GameObject levelTimeGO;
    public GameObject playerTimeGO;

    private Timer levelTimer;
    private Timer playerTimer;

    private GUIStyle guiStyle;

    public static List<Vector2> points;

    public float levelTime = 20;
    private float currentTime;

    public const int lastLevel = 26;

    public Font GUIFont;

    private static GameLogic instance;



    public static GameLogic Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("GameLogic").AddComponent<GameLogic>();
            }

            return instance;
        }
    }

    public void OnApplicationQuit()
    {
        instance = null;
    }

	// Use this for initialization
	void Start () {
        if (instance == null) instance = gameObject.GetComponent<GameLogic>();

        playerTimer = playerTimeGO.GetComponent<Timer>();
        levelTimer = levelTimeGO.GetComponent<Timer>();
         
        resetLevel();
	}

    private void resetLevel()
    {
        currentTime = levelTime;
        points = new List<Vector2>();
        levelTimer.startTimer();
        int spacex = (Screen.width - screenMargin) / totalCollumms;
        int spacey = (Screen.height - screenMargin) / totalRows;
        int roffsetx = (int)(Screen.width * randomOffsetPercent);
        int roffsety = (int)(Screen.height * randomOffsetPercent);

        for (int i = 1; i <= totalRows; i++)
            for (int j = 1; j <= totalCollumms; j++)
            {
                int offsetx = Random.Range(-roffsetx, roffsetx);
                int offsety = Random.Range(-roffsety, roffsety);

                points.Add(new Vector2(Mathf.Min(i * spacex + offsetx, Screen.width - screenMargin),
                    Mathf.Min(j * spacey + offsety, Screen.height - screenMargin)));
            }

        string lettersShuffled = RandomLogic.GetLettersShuffled();

        int idx = 0;

        foreach (Vector2 point in points.OrderBy(n => System.Guid.NewGuid()).Take(currentLevel))
        {
            Ray ray = Camera.main.ScreenPointToRay(point);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, mask))
            {
                SphereActions bong = (Instantiate(bongBase, hit.point, transform.rotation) as GameObject).GetComponent<SphereActions>();
                bong.letter = lettersShuffled[idx].ToString();
                bong.targetPos = getRandomSpacePoint();
                idx++;
            }
        }
    }

    public Vector3 getRandomSpacePoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(points[Random.Range(0,points.Count)]);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, mask))
        {
            return hit.point;
        }
        else
        {
            Debug.LogError("Error, there is no hit");
            return Vector3.zero;
        }
    }

    void Update()
    {
        currentTime -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel(0);
        }

        if (Player.isGameOver)
        {
            levelTimer.pauseTimer();
            playerTimer.pauseTimer();
        }

        if (currentTime <= 0)
        {
            if (currentLevel != lastLevel)
            {
                if (!Player.isGameOver) levelUp();
            }
            else
            {
                levelTimer.pauseTimer();
            }
        }
    }

    void levelUp()
    {
        currentLevel++;
        foreach (GameObject bong in GameObject.FindGameObjectsWithTag("Bong"))
            Destroy(bong);

        resetLevel();
    }

    void OnGUI()
    {
        if (guiStyle == null)
        {
            guiStyle = new GUIStyle(GUI.skin.label);
            guiStyle.alignment = TextAnchor.MiddleCenter;
            guiStyle.fontSize = 20;
            guiStyle.font = GUIFont;
            guiStyle.fontStyle = FontStyle.Bold;
        }

        if (Player.isGameOver)
        {
            GUI.BeginGroup(new Rect(Screen.width/2 - 250, 50, 400, 300));
            GUI.Box(new Rect(50, 0, 400, 200), string.Empty);

            if(currentLevel == lastLevel)
                GUI.Label(new Rect(50, 25, 350, 50), "You are amazing. You are the Tentancle 26 Master!", guiStyle);
            else
                GUI.Label(new Rect(50, 25, 350, 50), "You got to Tentacle " + currentLevel + " !", guiStyle);

            GUI.Label(new Rect(50, 75, 350, 50), "Your were alive: " + playerTimer.getTimeLabel() + " s", guiStyle);

            if (GUI.Button(new Rect(125, 150, 200, 25), "PRESS TO RETURN"))
            {
                Application.LoadLevel(0);
            }
            GUI.EndGroup();
        }
    }

}
