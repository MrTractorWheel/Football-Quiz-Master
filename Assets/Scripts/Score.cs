using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    int correct_answers = 0;
    int question_seen = 0;
    
    public int getCorrectAnwers(){
        return correct_answers;
    }

    public void incrementCorrectAnswers(){
        correct_answers++;
    }
    public int getQSAnwers(){
        return question_seen;
    }

    public void incrementQS(){
        question_seen++;
    }

    public int  calculateScore(){
        return Mathf.RoundToInt(correct_answers /  (float) question_seen * 100);
    }
}
