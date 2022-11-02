using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LifeTimeMananger : MonoBehaviour
{
    public static LifeTimeMananger instance;
    public bool manual = false;

    bool dead = false;
    DateTime startDay = DateTime.Now;
    private TimeSpan daysPassed;

    [Header("Current Hour")]
    private DateTime currentHour;
    [SerializeField] int hours;
    [SerializeField] int minutes;
    [SerializeField] int seconds;

    public TimeSpan DaysPassed { get => daysPassed; }
    public DateTime CurrentHour { get => currentHour; }

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }
    private void Start()
    {
        lookForSaves();
        setCurrentHour();

        daysPassed = currentHour - startDay;

        print(daysPassed.Days + " days have passed");
        print("It's " + hours + ":" + minutes + ":" + seconds + ":");
    }
    private void Update()
    {
        if (!manual)
            setCurrentHour();
    }

    void lookForSaves() 
    {
        if (PlayerPrefs.HasKey("startDay"))
        {
            long temp = (long)Convert.ToInt64(PlayerPrefs.GetString("startDay"));
            startDay = DateTime.FromBinary(temp);
            print(startDay);
        }
        else
        {
            startDay = DateTime.Now;
            PlayerPrefs.SetString("startDay", startDay.ToBinary().ToString());
            print(startDay);
        }
    }
    void setCurrentHour()
    {
        currentHour = DateTime.Now;
        hours = currentHour.Hour;
        minutes = currentHour.Minute;
        seconds = currentHour.Second;
    }
    void setCurrentHour(int hours_, int minutes_, int secods_)
    {
        hours = hours_;
        minutes = minutes_; 
        seconds = secods_;
    }
}
