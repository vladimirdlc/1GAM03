using UnityEngine;
using System.Collections;

public class MainMenuLogic : MonoBehaviour {
    private string textAreaString;
    public int totalInScores = 5;
    //public GUIStyle guiStyle;

	// Use this for initialization
	void Start () {
        textAreaString = "Rogue";
        //guiStyle = new GUIStyle();
        //guiStyle.fontSize = 18;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        showInputPlayerName();
        showBestRecords();
    }

    private void showInputPlayerName()
    {
        GUI.BeginGroup(new Rect(Screen.width / 2 - 150, Screen.height/2 - 100, 300, 300));

        Rect textArea = new Rect(55, 150, 180, 20);
        textAreaString = GUI.TextArea(textArea, textAreaString, 256);

        if (GUI.Button(new Rect(55, 180, 180, 40), "Hit me!") || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Player.name = textAreaString;
            Application.LoadLevel("MainGame");
        }

        GUI.EndGroup();
    }

    private void showBestRecords()
    {
        AdvancedLabel.Draw(new Rect(10, 10, 200, 200), "<Best Records>", new NewFontSize(18), new NewColor(Color.white), new NewFontStyle(FontStyle.Italic));

        for (int i = 1; i <= totalInScores; i++)
        {
            string etiquetaJugador = ">" +PlayerPrefs.GetString("Name" + i, "Rogue") + ": " + PlayerPrefs.GetInt("Record" + i, 0) + " sg";
            AdvancedLabel.Draw(new Rect(10, 10 + (i * 20), 200, 200), etiquetaJugador, new NewFontSize(15), new NewColor(Color.white), new NewFontStyle(FontStyle.Italic));
        }
    }
}
