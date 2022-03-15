using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EventChannels;

public class GameManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] GameEventChannelSO gameEvents;
    [SerializeField] ToggleEventChannelSO toggleInputEvent;
    [SerializeField] IntEventChannelSO scoreEvent;
    [SerializeField] IntEventChannelSO maxScoreEvent;
    [SerializeField] IntEventChannelSO pointsEvent;

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
        scoreEvent.OnValueAdded += OnScoreAdded;
        pointsEvent.OnValueAdded += OnPointAdded;
    }

    void OnDisable()
    {
        gameEvents.OnGameStarted -= OnGameStarted;
        gameEvents.OnGameFinished -= OnGameFinished;
        gameEvents.OnLevelStarted -= OnLevelStarted;
        gameEvents.OnLevelFinished -= OnLevelFinished;
        scoreEvent.OnValueAdded -= OnScoreAdded;
        pointsEvent.OnValueAdded -= OnPointAdded;
    }

    public void EnableInput() => toggleInputEvent.RaiseEvent(true);
    public void DisableInput() => toggleInputEvent.RaiseEvent(false);
    public void StartLevel() => gameEvents.StartLevel();

    void Start()
    {
        Vibration.Init();
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
        pointsEvent.UpdateValue(points);
        maxScoreEvent.UpdateValue(maxScore);
    }

    void OnGameStarted()
    {
        score = 0;
        scoreEvent.UpdateValue(score);
        animation.Play("GameStart");
    }

    void OnGameFinished()
    {
        if (score > maxScore)
        {
            maxScore = score;
            maxScoreEvent.UpdateValue(maxScore);
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
        scoreEvent.UpdateValue(score);
    }

    void OnPointAdded()
    {
        points++;
        pointsEvent.UpdateValue(points);
    }
}
