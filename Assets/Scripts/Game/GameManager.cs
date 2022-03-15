using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EventChannels;

public class GameManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] GameEventChannelSO gameEvents;

    Animation animation;

    int score;
    int maxScore;
    int points;

    void Awake()
    {
        animation = GetComponent<Animation>();
    }

    void OnEnable()
    {
        gameEvents.OnGameStarted += OnGameStarted;
        gameEvents.OnGameFinished += OnGameFinished;
        gameEvents.OnLevelStarted += OnLevelStarted;
        gameEvents.OnLevelFinished += OnLevelFinished;
        gameEvents.OnScoreAdded += OnScoreAdded;
        gameEvents.OnPointAdded += OnPointAdded;
    }

    void OnDisable()
    {
        gameEvents.OnGameStarted -= OnGameStarted;
        gameEvents.OnGameFinished -= OnGameFinished;
        gameEvents.OnLevelStarted -= OnLevelStarted;
        gameEvents.OnLevelFinished -= OnLevelFinished;
        gameEvents.OnScoreAdded -= OnScoreAdded;
        gameEvents.OnPointAdded -= OnPointAdded;
    }

    public void EnableInput() => gameEvents.ToggleInput(true);
    public void DisableInput() => gameEvents.ToggleInput(false);
    public void StartLevel() => gameEvents.StartLevel();

    void Start()
    {
        LoadPlayerData();
    }

    void SavePlayerData()
    {
        PlayerPrefs.SetInt("MaxScore", maxScore);
        PlayerPrefs.SetInt("Points", points);
    }

    void LoadPlayerData()
    {
        maxScore = PlayerPrefs.GetInt("MaxScore", 0);
        points = PlayerPrefs.GetInt("Points", 0);
        gameEvents.UpdatePoints(points);
        gameEvents.UpdateHighscore(maxScore);
    }

    void OnGameStarted()
    {
        score = 0;
        gameEvents.UpdateScore(score);
        animation.Play("GameStart");
    }

    void OnGameFinished()
    {
        if (score > maxScore)
        {
            maxScore = score;
            gameEvents.UpdateHighscore(maxScore);
        }
        animation.Play("GameOver");
        SavePlayerData();
    }

    void OnLevelStarted()
    {
        // animator.Play("LevelStart");
    }

    void OnLevelFinished(bool win)
    {
        if (win)
            animation.Play("LevelTransition");
        else
            gameEvents.FinishGame();
    }

    void OnScoreAdded()
    {
        score++;
        gameEvents.UpdateScore(score);
    }

    void OnPointAdded()
    {
        points++;
        gameEvents.UpdatePoints(points);
    }
}
