using UnityEngine;
using System.Collections;

public class SphereActions : MonoBehaviour {
	public float disminutionStep = 0.1f;
    public float augmentationStep = 1f;
    public float maxScale = 60f;
	public float ballDisminutionTime = 0.1f;
    public float minScale = 15.0f;
    
    public string letter;
	public int offsetx = 6;
	public int offsety = 20;

    public Vector3 targetPos;

	Vector3 currPos;
	
	float initialTime = 0;
	
	void Start () {
        gameObject.renderer.material.color = RandomLogic.GetColor();
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

            if (this.transform.localScale.x <= minScale)
                Player.isGameOver = true;

			initialTime = 0;
		};

        doMove();
	}

    void doMove()
    {
        if (Vector3.Distance(transform.position, targetPos) < 7.0f)
        {
            targetPos = GameLogic.Instance.getRandomSpacePoint();
            targetPos = new Vector3(targetPos.x, targetPos.y, transform.position.z);
        }
        else
        {
            Vector3 newPos = Vector3.Slerp(transform.position, targetPos, Time.deltaTime / 2);
            transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
        }

    }
    void OnGUI()
    {
        currPos = Camera.main.WorldToScreenPoint(transform.position);
        AdvancedLabel.Draw(new Rect(currPos.x + offsetx, Screen.height - currPos.y + offsety, 100, 100), letter, new NewFontSize(28), new NewColor(Color.black), new NewFontStyle(FontStyle.Bold));
        AdvancedLabel.Draw(new Rect(currPos.x + offsetx, Screen.height - currPos.y + offsety, 100, 100), letter, new NewFontSize(25));
    }
}
