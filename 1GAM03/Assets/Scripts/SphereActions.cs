using UnityEngine;
using System.Collections;

public class SphereActions : MonoBehaviour {
	public float disminutionStep = 0.1f;
    public float augmentationStep = 1f;
    public float maxScale = 60f;
	public float ballDisminutionTime = 0.1f;
    public string letter;
	public int offsetx = 6;
	public int offsety = 20;
	
	private Vector3 currPos;
	
	float initialTime = 0;
	
	void Start () {
        gameObject.renderer.material.color = RandomLogic.GetColor();
		currPos = Camera.main.WorldToScreenPoint(transform.position);
        letter = RandomLogic.GetUppercasetLetter().ToString();
	}
	
	void Update () {
		if(initialTime == 0) initialTime = Time.time;

		float deltaTime;
		deltaTime = Time.time - initialTime;


        if (Input.GetKeyDown(letter.ToLower()))
        {
            if (this.transform.localScale.x < maxScale)
            {
                this.transform.localScale = new Vector3(this.transform.localScale.x + augmentationStep,
                                                        this.transform.localScale.y + augmentationStep,
                                                        this.transform.localScale.z + augmentationStep);
            }
        }
        else if (deltaTime >= ballDisminutionTime) {
			
			this.transform.localScale = new Vector3(this.transform.localScale.x - disminutionStep, 
				                                    this.transform.localScale.y - disminutionStep, 
				                                    this.transform.localScale.z - disminutionStep);

			initialTime = 0;
		};
	}

    void OnGUI()
    {
        AdvancedLabel.Draw(new Rect(currPos.x - offsetx, currPos.y - offsety, 100, 100), letter, new NewFontSize(25));
    }
}
