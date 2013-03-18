using UnityEngine;
using System.Collections;

public class Clock : MonoBehaviour
{
    private static Clock instance = null;
    public static Clock Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("instantiate");
                GameObject go = new GameObject();
                instance = go.AddComponent<Clock>();
                go.name = "Clock";
            }

            return instance;
        }
    }


    private float tiempoTranscurrido;
    private float tiempoMeta;
    public bool estaContando;
    public bool esGameOver;
    public int totalPersonasRecord = 5;

    void Awake()
    {
        tiempoTranscurrido = 0;
        estaContando = false;
        tiempoTranscurrido = 0;
        tiempoMeta = 0;
    }


    void Update()
    {
        if (estaContando)
        {
            tiempoTranscurrido += Time.deltaTime;
            if (checkGameOver())
            {
                gameOver();
            }
        }

    }


    void OnGUI()
    {
        if(estaContando)
        {
            float tiempoMostrar = tiempoMeta - tiempoTranscurrido;
            int minutos = (int)(tiempoMostrar / 60);
            float segundos = Mathf.Floor(tiempoMostrar % 60);

            string stiempoActual = string.Format("{0:00}:{1:00}", minutos, segundos);

            AdvancedLabel.Draw(new Rect((Screen.width/2)-60, 20, 100, 200), stiempoActual, new NewFontSize(40), new NewColor(Color.green));
        }

        if (esGameOver)
        {
            AdvancedLabel.Draw(new Rect(10, 20, 200, 200), "Mejores Records", new NewFontSize(20), new NewColor(Color.yellow));

            for (int i = 1; i <= totalPersonasRecord; i++)
            {
                string etiquetaJugador = PlayerPrefs.GetString("Nombre" + i, "Chofer") + " : " + PlayerPrefs.GetInt("Record" + i, 0) + " Pasajeros";
                AdvancedLabel.Draw(new Rect(10, 20+(i*20), 200, 200), etiquetaJugador, new NewFontSize(20), new NewColor(Color.yellow));
            }
        }
    }

    //Revisa si se pasó del límite de tiempo
    public bool checkGameOver()
    {
        return (tiempoTranscurrido > tiempoMeta);
    }

    private void gameOver()
    {
        Debug.Log("Se acabo el tiempo");
        estaContando = false;


        for(int i = 1; i <= totalPersonasRecord; i++)
            if (Player.timeScore > PlayerPrefs.GetFloat("Record" + i))
            {
                PlayerPrefs.SetString("Name" + i, PlayerPrefs.GetString("Nombre"));
                PlayerPrefs.SetFloat("Record" + i, Player.timeScore);
                break;
            }

        esGameOver = true;        
    }

    public void iniciaContador()
    {
        estaContando = true;
    }

}
