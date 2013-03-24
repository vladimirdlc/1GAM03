using UnityEngine;
using System.Collections;

public class MainMenuLogic : MonoBehaviour {
    private static string textAreaString = "Rogue";
    public int totalInScores = 5;
    public Font GUIFont;
    private GUIStyle guiStyle;

	// Use this for initialization
	void Start () {
        //GUI.skin.box.fontSize = 100;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            startGame();
        }
	}

    void OnGUI()
    {
        if (guiStyle == null)
        {
            guiStyle = new GUIStyle(GUI.skin.textField);
            guiStyle.fontSize = 18;
        }
        showInputPlayerName();
        showBestRecords();
        AdvancedLabel.Draw(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 400, 200), "Tentacle 26", new NewFontSize(50), new NewColor(Color.black), new NewFont(GUIFont));
        AdvancedLabel.Draw(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 400, 200), "Tentacle 26", new NewFontSize(52), new NewColor(Color.white), new NewFont(GUIFont));

    }

    private void showInputPlayerName()
    {
        GUI.BeginGroup(new Rect(Screen.width / 2 - 100, Screen.height/2 - 100, 400, 300));

        Rect textArea = new Rect(105, 150, 150, 30);
        textAreaString = GUI.TextArea(textArea, textAreaString, 12, guiStyle);

        if (GUI.Button(new Rect(260, 150, 40, 30), "Go!"))
        {
            startGame();
        }

        GUI.EndGroup();
    }

    private void startGame()
    {
        Player.playerName = textAreaString;
        GameLogic.currentLevel = 1;
        Application.LoadLevel("MainGame");
    }

    private void showBestRecords()
    {
        GUI.Box(new Rect(0, 0, 200, 30 * totalInScores), string.Empty);
        AdvancedLabel.Draw(new Rect(10, 10, 200, 200), "<Best Records>", new NewFontSize(18), new NewColor(Color.white), new NewFontStyle(FontStyle.Italic));

        for (int i = 1; i <= totalInScores; i++)
        {
            float playerScore = PlayerPrefs.GetFloat("Record" + i, 0);

            string labelPlayer = "~" + PlayerPrefs.GetString("Name" + i, "Rogue") + ": " + playerScore + " sg";
            AdvancedLabel.Draw(new Rect(10, 10 + (i * 20), 200, 200), labelPlayer, new NewFontSize(15), new NewColor(Color.white), new NewFontStyle(FontStyle.Italic));
        }
    }
}
