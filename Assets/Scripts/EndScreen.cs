using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScore;
    Score scoreKeeper;
    void Awake()
    {
        scoreKeeper = FindObjectOfType<Score>();
    }

    public void showFinalScore(){
        finalScore.text = "Your Score is:" + scoreKeeper.calculateScore() + "%";
    }
}
