using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public static float timeScore;
    public static string playerName;
    public static bool isGameOver;

	// Use this for initialization
	void Start () {
        restart();
	}

    void Update()
    {
        if (!isGameOver)
        {
            timeScore += Time.deltaTime;
        }
    }

    public void restart()
    {
        timeScore = 0;
        isGameOver = false;
    }
}
