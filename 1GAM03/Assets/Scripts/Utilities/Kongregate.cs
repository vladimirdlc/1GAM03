using UnityEngine;
using System.Collections;

public class Kongregate : MonoBehaviour {
#if UNITY_WEBPLAYER
    private static bool isKongregateReady;

    void Start()
    {
       // Try to connect to Kongregate.
       // The gameObject.name parameter is used so SendMessage
       // will look for the OnKongregateAPILoaded method
       // on this same MonoBehaviour
        if (!isKongregateReady)
        {
            Application.ExternalEval(
            "if(typeof(kongregateUnitySupport) != 'undefined'){" +
            " kongregateUnitySupport.initAPI('" + gameObject.name + "','OnKongregateAPILoaded');" +
            "};"
            );
        }
    }

    void OnKongregateAPILoaded(string userInfoString)
    {
       // Here I set a static variable which I can
       // check to know if Kongregate connection is ready
       isKongregateReady = true;
       // Kongregate returns a char delimited string
       // composed of userId|username|gameAuthToken
       // Here I just store them for easier access
       string[] parms = userInfoString.Split("|"[0]);
       int userId = System.Convert.ToInt32(parms[0]); // int
       string username = parms[1]; // string
       string gameAuthToken = parms[2]; // string
    }
#endif
}
