using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public static float timeScore;

	// Use this for initialization
	void Start () {
	
	}

    void Update()
    {
        timeScore += Time.deltaTime;
    }

    public void restart()
    {
        timeScore = 0;
    }
}
