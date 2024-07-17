using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class quiz : MonoBehaviour
{
    [Header("Questions")]
    QuestionSO cur_question;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    [SerializeField] TextMeshProUGUI question_text;
    [Header("Answers")]
    [SerializeField] GameObject[] answer_buttons;
    bool hasAnsweredEarly = true;
    [SerializeField] int correctAnswerIndex;
    [Header("Buttons")]
    [SerializeField] Sprite default_answer_sprite;
    [SerializeField] Sprite correct_answer_sprite;
    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;
    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    Score scoreKeeper;
    [Header("Slider")]
    [SerializeField] Slider progressBar;
    public bool isComplete = false;

    private void Awake(){
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<Score>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }
    private void Update() {
        timerImage.fillAmount = timer.fill_frac;
        if(timer.loadNext){
            if(progressBar.value == progressBar.maxValue){
                isComplete = true;
                return;
            }
            hasAnsweredEarly = false;
            timer.loadNext = false;
            getNextQuestion();
        }
        else if(!hasAnsweredEarly && !timer.isQuestion){
            displayAnswer(-1);
            setButtonState(false);
        }
    }

    private void displayQuestion(){
        question_text.text = cur_question.GetQuestion();
        for(int i = 0; i<4; i++){
            TextMeshProUGUI button_text = answer_buttons[i].GetComponentInChildren<TextMeshProUGUI>();
            button_text.text = cur_question.GetAnswer(i);
        }
    }

    private void getNextQuestion(){
        if(questions.Count > 0){
            setButtonState(true); 
            setDefaultSprites();
            getRandomQuestion();
            displayQuestion();
            progressBar.value++;
            scoreKeeper.incrementQS();
        }
    }

    private void getRandomQuestion(){
        int index = Random.Range(0, questions.Count);
        cur_question = questions[index];
        if(questions.Contains(cur_question)){
            questions.Remove(cur_question);
        }
    }

    private void setDefaultSprites(){
        for(int i = 0; i<4; i++){
            Image button_image = answer_buttons[i].GetComponent<Image>();
            button_image.sprite = default_answer_sprite;
        }
    }

    public void OnAnswerSelected(int index){
        hasAnsweredEarly = true;
        setButtonState(false);
        displayAnswer(index);
        timer.resetTimer();
        scoreText.text = "Score: " + scoreKeeper.calculateScore() + "%";
    }

    private void displayAnswer(int index){
        Image button_image;
        if(index == cur_question.GetCorrectAnswerIndex()){
            question_text.text = "Correct Answer!";
            button_image = answer_buttons[index].GetComponent<Image>();
            button_image.sprite = correct_answer_sprite;
            scoreKeeper.incrementCorrectAnswers();
        }
        else{
            correctAnswerIndex = cur_question.GetCorrectAnswerIndex();
            string correct_answer = cur_question.GetAnswer(correctAnswerIndex);
            question_text.text = "Wrong Answer! Correct one is: " + correct_answer;
            button_image = answer_buttons[correctAnswerIndex].GetComponent<Image>();
            button_image.sprite = correct_answer_sprite;
        }
    }

    private void setButtonState(bool state){
        for(int i=0; i<4; i++){
            Button button = answer_buttons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }
}
