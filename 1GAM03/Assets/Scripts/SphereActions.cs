using UnityEngine;
using System.Collections;

public class SphereActions : MonoBehaviour {
	public Color color;
	public float disminutionStep = 0.10f;
	public float ballDisminutionTime = 0.1f;
    public string letter;
	public int offsetx = 5;
	public int offsety = 20;
	
	private Vector3 currPos;
	
	float initialTime = 0;
	
	void Start () {
		gameObject.renderer.material.color = color;
		currPos = Camera.main.WorldToScreenPoint(transform.position);
        letter = RandomLetter.GetUppercasetLetter().ToString();
	}
	
	void Update () {
		if(initialTime == 0) initialTime = Time.time;
		float deltaTime ;
		deltaTime = Time.time - initialTime;
		
		if (deltaTime >= ballDisminutionTime) {
			
			this.transform.localScale = new Vector3(this.transform.localScale.x - disminutionStep, 
				                                    this.transform.localScale.y - disminutionStep, 
				                                    this.transform.localScale.z - disminutionStep);

			initialTime = 0;
		};

        if (Input.GetKeyDown(letter.ToLower()))
        {
            Debug.Log("it works: "+letter);
        }
	}

    void OnGUI()
    {
        AdvancedLabel.Draw(new Rect(currPos.x - offsetx, currPos.y - offsety, 100, 100), letter, new NewFontSize(25));
    }
}
