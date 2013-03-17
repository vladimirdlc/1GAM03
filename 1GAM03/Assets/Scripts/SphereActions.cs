using UnityEngine;
using System.Collections;

public class SphereActions : MonoBehaviour {
	
	
	public Color color;
	public float disminutionStep = 0.10f;
	public const int ballDisminutionTime = 1;
	
	float initialTime = 0;
	
	
	// Use this for initialization
	void Start () {
		gameObject.renderer.material.color = color;
	}
	
	// Update is called once per frame
	void Update () {
		if(initialTime == 0) initialTime = Time.time;
		float deltaTime ;
		deltaTime = Time.time - initialTime;
		Debug.Log (deltaTime);
		if (deltaTime >= ballDisminutionTime) {
			
			this.transform.localScale = new Vector3(this.transform.localScale.x - disminutionStep, 
				                                    this.transform.localScale.y - disminutionStep, 
				                                    this.transform.localScale.z - disminutionStep);

			/*	this.transform.localScale.x -= disminutionStep;	
			this.transform.localScale.y -= disminutionStep;
			this.transform.localScale.z -= disminutionStep;*/
			initialTime = 0;
		};
		
	}

    void OnGUI()
    {
        Vector3 currPos = Camera.main.WorldToScreenPoint(transform.position);
        AdvancedLabel.Draw(new Rect(currPos.x - 6, currPos.y - 20, 100, 100), "A", new NewFontSize(25));
    }
}
