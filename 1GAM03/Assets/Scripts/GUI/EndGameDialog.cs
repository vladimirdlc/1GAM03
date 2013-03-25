using UnityEngine;
using System.Collections;

public class EndGameDialog : MonoBehaviour {
    private GUIStyle guiStyle;
    public Font GUIFont;

    void OnGUI()
    {
        if (guiStyle == null)
        {
            guiStyle = new GUIStyle(GUI.skin.label);
            guiStyle.alignment = TextAnchor.MiddleCenter;
            guiStyle.fontSize = 20;
            guiStyle.font = GUIFont;
            guiStyle.fontStyle = FontStyle.Bold;
        }

        if (Player.isGameOver)
        {
            GUI.BeginGroup(new Rect(Screen.width / 2 - 250, 50, 400, 300));
            GUI.Box(new Rect(50, 0, 400, 200), string.Empty);

            if (GameLogic.currentLevel == GameLogic.finalLevel)
                GUI.Label(new Rect(50, 25, 350, 50), "You are the Tentancle 26 Master!", guiStyle);
            else
                GUI.Label(new Rect(50, 25, 350, 50), "You got to Tentacle " + GameLogic.currentLevel + " !", guiStyle);

            GUI.Label(new Rect(50, 75, 350, 50), "Your were alive: " + Player.timeScore.ToString().Replace(".", "'") + " s", guiStyle);

            if (GUI.Button(new Rect(125, 150, 200, 25), "PRESS TO RETURN"))
            {
                Application.LoadLevel(0);
            }
            GUI.EndGroup();
        }
    }
}
