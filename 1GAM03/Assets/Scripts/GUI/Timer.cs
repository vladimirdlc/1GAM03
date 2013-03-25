using UnityEngine;
using System.Collections;
 
public class Timer : MonoBehaviour {

public int seconds;
public int fraction;

public int secondsGoal;
public int fractionGoal;
 
protected float currentTime;

[SerializeField]
protected bool isAscending = true;
	
[SerializeField]
protected bool isCounting = false;

public GUIStyle style;
public Rect rposition = new Rect(40, 40, 200, 200);

public bool isTimeOver;
 

	protected void Start() 
	{
        if (string.IsNullOrEmpty(style.name))
        {
            style.fontSize = 40;
            style.fontStyle = FontStyle.BoldAndItalic;
            resetGUI();
        }

        resetTime();
	}
 
 
	protected void Update() 
	{
        count();

        if (!isTimeOver && isOverGoalTime())
        {
            timeOverAction();
            isTimeOver = true;
        }
	}

    protected virtual void resetGUI()
    {
        style.normal.textColor = Color.gray;
    }

    protected virtual void timeOverAction()
    {
        style.normal.textColor = Color.red;
    }

    protected void count()
    {
        if (isCounting)
        {
            currentTime += (isAscending) ? Time.deltaTime : -Time.deltaTime;
            seconds = (int)(currentTime);
            fraction = (int)((currentTime * 10) % 10);
        }
    }

    void OnGUI()
    {
        GUI.Label(rposition, getTimeLabel(), style);
    }

    private string getTimeLabel()
    {
        return string.Format("{0}''{1}", seconds, fraction);
    }
 
	public void startTimer()
	{
		resetTime();
		isCounting = true;
	}
 	
	public void resumeTimer()
	{
		isCounting = true;
	}
	
	public void pauseTimer()
	{
		isCounting = false;
	}

    private bool isOverGoalTime()
    {
        if (isAscending)
        {
            return seconds > secondsGoal ||
                (seconds == secondsGoal && fraction >= fractionGoal);
        }
        else
        {
            return seconds <= 0 && fraction <= 0;
        }
    }

    public void resetTime()
    {
        resetGUI();
        if (!isAscending)
        {
            fraction = fractionGoal % 10;
            seconds = secondsGoal % 60;
            currentTime = secondsGoal + (fractionGoal / 10.0f);
        }
        else
        {
            fraction = fraction % 10;
            seconds = seconds % 60;
            currentTime = seconds + (fraction / 10.0f);
        }
    }

    public void setCountType(bool isAscending)
    {
        this.isAscending = isAscending;
    }

    public float getTime()
    {
        return currentTime;
    }
}