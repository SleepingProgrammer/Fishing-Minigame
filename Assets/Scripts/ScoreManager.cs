using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    int _score = 0;
    int _highScore = 0;
    public bool isHighScore = false;
    public static ScoreManager instance;
    public GameObject gameOverUI;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI endgameScoreText;
    public GameObject highscoreIndicator;

 

    private void Awake() {

        if(instance != null) {
            Destroy(gameObject);
            return;
        }


        instance = this;



        LoadHighScore();
    }

    private void Start() {
        FishingManager.instance.onGameEnd.AddListener(EndGame);
        FishingManager.instance.onGameStart.AddListener(StartGame);
    }

    private void OnDestroy() {
        FishingManager.instance.onGameEnd.RemoveListener(EndGame);
        FishingManager.instance.onGameStart.RemoveListener(StartGame);
    }

    public int score {
        get {
            return _score;
        }
        set {
            _score = value;

            if(_score > _highScore) {
                _highScore = _score;
                isHighScore = true;
            }

            scoreText.text =  _score.ToString(); 
        }
    }

    public void AddScore(int scoreToAdd) {
        score += scoreToAdd;
    }

    public void ResetScore() {
        score = 0;
    }

    public void SubtractScore(int scoreToSubtract) {
        score -= scoreToSubtract;
    }

    public void SaveHighScore() {
        PlayerPrefs.SetInt("Score", score);
    }

    public void LoadHighScore() {
        _highScore = PlayerPrefs.GetInt("Score");
    }

    public void ClearScore() {
        PlayerPrefs.DeleteKey("Score");
    }

    public void AddScoreFromFish(Fish fish) {
        score += fish.score;
    }

    public bool CheckHighScore() {
        if(score > _highScore) {
            _highScore = score;
            isHighScore = true;
            return true;
        }
        return false;
    } 

    public void EndGame() { 
        if(isHighScore) {
            SaveHighScore();
            highscoreIndicator.SetActive(true);
        }


        gameOverUI.SetActive(true);
        highScoreText.text = "HIGH SCORE: " + _highScore.ToString();
        endgameScoreText.text = "SCORE: " + _score.ToString();

        CurrencyManager.instance.AddCoins(score);
        
        ResetScore();
    }

    public void StartGame() {
        highscoreIndicator.SetActive(false);
        gameOverUI.SetActive(false);
    }
}
