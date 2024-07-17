using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    quiz Q;
    EndScreen end;

    void Awake(){
        Q = FindObjectOfType<quiz>();
        end = FindObjectOfType<EndScreen>(); 
    }

    void Start()
    {
        Q.gameObject.SetActive(true);
        end.gameObject.SetActive(false);
    }
    
    void Update()
    {
        if(Q.isComplete){
            Q.gameObject.SetActive(false);
            end.gameObject.SetActive(true);
            end.showFinalScore();
        }
    }

    public void OnReplayLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
