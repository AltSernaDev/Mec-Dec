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

    public enum State
    {
        Morning, //6am
        Noon, //12pm
        Afternoon, //4pm
        Night //7pm
    }
    public State dayTimeState;                   // mi rey a este enlazas los evento que quieras

    [Header("Current Hour")]
    private DateTime currentHour;
    private int seconds;
    private int hours;
    private int minutes;

    [Header("Doors")]
    [SerializeField] GameObject day1Door;
    [SerializeField] GameObject day2Door;
    [SerializeField] GameObject day3Door;

    public TimeSpan DaysPassed { get => daysPassed; }
    public DateTime CurrentHour { get => currentHour; }
    public DateTime StartDay { get => startDay; }
    public int Hours { get => hours; }
    public int Minutes { get => minutes; }
    public int Seconds { get => seconds; }

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
        print("we start at " + startDay + " and, today is " + currentHour);

        print(daysPassed.Days + " days have passed");
        print("It's " + hours + ":" + minutes + ":" + seconds + ":");

        switch (daysPassed.Days + 1)
        {
            case 1:
                day1Door.SetActive(false);
                break;
            case 2:
                day2Door.SetActive(false);
                break;
            case 3:
                day3Door.SetActive(false);
                break;
            default:
                break;
        }
    }
    private void Update()
    {
        if (!manual)
            setCurrentHour();
        dayTimeState = DayTimeStateMachine(hours);
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
    State DayTimeStateMachine(int hour_)
    {
        State state_ = State.Night;
        switch (hour_)
        {
            case int i when hour_ >= 6:
                state_ = State.Morning;
                break;
            case int i when hour_ >= 12:
                state_ = State.Noon;
                break;
            case int i when hour_ >= 16:
                state_ = State.Afternoon;
                break;
            case int i when hour_ >= 19 || hour_ < 6:
                state_ = State.Night;
                break;
        }
        return state_;
    }
}
