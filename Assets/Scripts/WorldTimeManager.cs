using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTimeManager : MonoBehaviour
{
    [SerializeField] UIManager uiManager;

    public int hour, day, month, year;
    //public int hoursElapsed;
    private float timer = 0;

    [SerializeField] public float secondsPerHour; //60
    [SerializeField] public float hoursPerDay;  //24
    [SerializeField] public float daysPerMonth;  //10
    [SerializeField] public float monthsPerYear;  //4

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        uiManager.ShowTime(hour, day, month, year);
    }

    // Update is called once per frame
    void Update()
    {
        Timer();


        //Debug.Log(timer);
    }

    private void Timer()
    {
        timer += Time.deltaTime;
        if (timer >= secondsPerHour)
        {
            hour++;
            timer = 0;

            if (hour == hoursPerDay)
            {
                hour = 0;
                day++;

                if (day == daysPerMonth)
                {
                    day = 1;
                    month++;

                    if (month == monthsPerYear)
                    {
                        month = 1;
                        year++;
                    }
                }
            }
            uiManager.ShowTime(hour, day, month, year);
        }
    }

}
