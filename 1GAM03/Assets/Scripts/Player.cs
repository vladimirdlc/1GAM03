using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public static float timeScore;
    public static string playerName;
    public static bool isGameOver;

    public GameObject explosion;
    private bool isExplosionStarted;

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
        else
        {
            if (!isExplosionStarted)
            {
                Instantiate(explosion);
                isExplosionStarted = true;
                audio.Play();
            }
        }
    }

    public void restart()
    {
        timeScore = 0;
        isGameOver = false;
        isExplosionStarted = false;
    }
}
