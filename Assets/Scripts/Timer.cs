using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float question_time = 20f;
    [SerializeField] float display_time = 10f;

    public bool isQuestion = false;
    public float fill_frac;
    public bool loadNext = false;
    float timer_val;
    void Update()
    {
        updateTimer();
    }

    public void resetTimer(){
        timer_val = 0;
    }

    private void updateTimer(){
        timer_val -= Time.deltaTime;
        if (timer_val <= 0){
            if(isQuestion){
                timer_val = display_time;
                isQuestion = false;
            }
            else{
                loadNext = true;
                timer_val = question_time;
                isQuestion = true;
            }
        }
        else{
            if(isQuestion){
                fill_frac = timer_val / question_time;
            }
            else{
                fill_frac = timer_val / display_time;
            }
        }
    }
}
