using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Logic : MonoBehaviour
{
    [SerializeField] private Text happyCustomers;
    [SerializeField] private Text angryCustomers;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Text highscoreText;
    [SerializeField] private Text currentGameTimeText;

    private float currentGameTime = 0;

    public int countHappyCustomers = 0;
    public int countAngryCustomers = 0;
    public bool isGameOver = false;

    private const string HighScoreKey = "Highscore";

    private void Start() {
        InitializeHighScore();

    }
    private void Update() {
        if (!isGameOver) { 
            currentGameTime += Time.deltaTime;
        }
        happyCustomers.text = countHappyCustomers.ToString();
        angryCustomers.text = countAngryCustomers.ToString();

        //on game over
        if (countAngryCustomers == 3) {
            isGameOver = true;
            UpdateHighScore();
            currentGameTimeText.text = Mathf.FloorToInt(currentGameTime).ToString() + " seconds";
            highscoreText.text = GetHighScore().ToString() + " seconds";
            gameOverScreen.SetActive(true);
        }
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    private void InitializeHighScore() {
        if (!PlayerPrefs.HasKey(HighScoreKey)) {
            PlayerPrefs.SetInt(HighScoreKey, 0);
        }
    }


    private void UpdateHighScore() {
        if (currentGameTime > GetHighScore()) {
            SetHighScore(Mathf.FloorToInt(currentGameTime));
        }
    }

    private int GetHighScore() {
        return PlayerPrefs.GetInt(HighScoreKey);
    }

    private void SetHighScore(int score) {
        PlayerPrefs.SetInt(HighScoreKey, score);
        PlayerPrefs.Save(); 
    }
}
