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

    public static List<Vector2> points;

    public float levelTime = 20;
    private float currentTime;

    public const int finalLevel = 26;
    public int storeNScores = 5;

    private bool isWaitingForAction;

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

        if (Player.isGameOver && !isWaitingForAction)
        {
            levelTimer.pauseTimer();
            playerTimer.pauseTimer();
            Player.timeScore = Mathf.Floor(playerTimer.getTime() * 10)/10;

            submitScore();
            isWaitingForAction = true;
        }

        if (currentTime <= 0)
        {
            if (currentLevel != finalLevel)
            {
                if (!Player.isGameOver) levelUp();
            }
            else
            {
                levelTimer.pauseTimer();
            }
        }
    }

    private void submitScore()
    {
        for (int i = 1; i <= storeNScores; i++)
        {
            if(Player.timeScore > PlayerPrefs.GetFloat("Record" + i))
            {
                PlayerPrefs.SetFloat("Record" + i, Player.timeScore);
                PlayerPrefs.SetString("Name" + i, Player.playerName);
                break;
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

}
