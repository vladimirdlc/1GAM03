using UnityEngine;
using System.Collections;

public class SphereActions : MonoBehaviour {
	public Color color;
	
	// Use this for initialization
	void Start () {
		gameObject.renderer.material.color = color;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI () {
		AdvancedLabel.Draw(new Rect(this.transform.localPosition.x, this.transform.localPosition.y, 100, 100),"a", new NewFontSize(10));
	}
}
