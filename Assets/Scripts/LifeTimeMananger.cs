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
        Afternoon, //12pm
        Night //6pm
    }
    public State dayTimeState;                   // mi rey a este enlazas los evento que quieras

    [Header("Current Hour")]
    private DateTime currentHour;  // count the day 0
    [SerializeField] private int seconds, hours, minutes;

    [Header("Doors")]
    [SerializeField] GameObject day1Door;
    [SerializeField] GameObject day1DoorBlock;
    [SerializeField] GameObject day2Door;
    [SerializeField] GameObject day2DoorBlock;
    [SerializeField] GameObject day3Door;
    [SerializeField] GameObject day3DoorBlock;

    [Header("Objs")]
    [SerializeField] GameObject day1Objs;
    [SerializeField] GameObject day2Objs;
    [SerializeField] GameObject day3Objs;
    [SerializeField] List<GameObject> MorningObj;
    [SerializeField] List<GameObject> AfterNoonObj;
    [SerializeField] List<GameObject> NightObj;

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
                day1Door.SetActive(false); //por aqui rotas las puertas 
                day1DoorBlock.SetActive(false); //TEMP (temp si lo quieres cambiar xd) machete ante el sueño 
                Destroy(day2Objs);
                Destroy(day3Objs);
                break;
            case 2:
                day2Door.SetActive(false);
                day2DoorBlock.SetActive(false);
                Destroy(day1Objs);
                Destroy(day3Objs);
                break;
            case 3:
                day3Door.SetActive(false);
                day3DoorBlock.SetActive(false);
                Destroy(day1Objs);
                Destroy(day2Objs);
                break;
        }
        CheckObjs();
        //Invoke("CheckObjs", 1);
    }
    private void Update()
    {
        if (!manual)
            setCurrentHour();
        ObjStateManager(DayTimeStateMachine(hours));
    }
    
    void CheckObjs()
    {
        StartCoroutine(CheckList(MorningObj));
        StartCoroutine(CheckList(AfterNoonObj));
        StartCoroutine(CheckList(NightObj));
    }
    IEnumerator CheckList(List<GameObject> list)
    {
        yield return null;

        int count = 0;
        while (count < list.Count)
        {
            print(list[count]);
            if (list[count] == null)
                list.RemoveAt(count);
            else
                count++;
        }
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
        /*startDay = startDay.AddDays(0);
        PlayerPrefs.SetString("startDay", startDay.ToBinary().ToString());
        print(startDay);*/
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
                state_ = State.Afternoon;
                break;
            case int i when hour_ >= 18 || hour_ < 6:
                state_ = State.Night;
                break;
        }
        return state_;
    }

    void ObjStateManager(State state_)
    {
        dayTimeState = state_;

        switch (state_)
        {
            case State.Morning:
                foreach (GameObject item in MorningObj)
                    item.SetActive(true);
                foreach (GameObject item in NightObj)
                    item.SetActive(false);
                foreach (GameObject item in AfterNoonObj)
                    item.SetActive(false);

                break;
            case State.Afternoon:
                foreach (GameObject item in MorningObj)
                    item.SetActive(false);
                foreach (GameObject item in NightObj)
                    item.SetActive(false);
                foreach (GameObject item in AfterNoonObj)
                    item.SetActive(true);

                break;
            case State.Night:
                foreach (GameObject item in MorningObj)
                    item.SetActive(false);
                foreach (GameObject item in NightObj)
                    item.SetActive(true);
                foreach (GameObject item in AfterNoonObj)
                    item.SetActive(false);

                break;
        }
    }
}
