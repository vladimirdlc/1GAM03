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

    List<Vector2> points;

	// Use this for initialization
	void Start () {
        points = new List<Vector2>();
        int spacex = (Screen.width-screenMargin)/totalCollumms;
        int spacey = (Screen.height - screenMargin)/totalRows;
        int roffsetx = (int)(Screen.width * randomOffsetPercent);
        int roffsety = (int)(Screen.height * randomOffsetPercent);


        for (int i = 1; i <= totalRows; i++)
            for (int j = 1; j <= totalCollumms; j++)
            {
                int offsetx = Random.Range(-roffsetx, roffsetx);
                int offsety = Random.Range(-roffsety, roffsety);

                points.Add(new Vector2(Mathf.Min(i * spacex + offsetx, Screen.width-screenMargin),
                    Mathf.Min(j * spacey + offsety, Screen.height-screenMargin)));
            }

        string lettersShuffled = RandomLogic.GetLettersShuffled();

        int idx = 0;
      
        foreach (Vector2 point in points.OrderBy(n => System.Guid.NewGuid()).Take(currentLevel))
        {
            Ray ray = Camera.main.ScreenPointToRay(point);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                SphereActions bong = (Instantiate(bongBase, hit.point, transform.rotation) as GameObject).GetComponent<SphereActions>();
                bong.letter = lettersShuffled[idx].ToString();
                idx++;
            }
        }
	}

}
