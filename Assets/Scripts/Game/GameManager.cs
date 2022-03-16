using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EventChannels;

public class GameManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] GameEventChannelSO gameEvents;
    [SerializeField] BoolEventChannelSO toggleInputEvent;
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
        gameEvents.OnMenuOpened += OnMenuOpened;
        gameEvents.OnGameStarted += OnGameStarted;
        gameEvents.OnGameFinished += OnGameFinished;
        gameEvents.OnLevelStarted += OnLevelStarted;
        gameEvents.OnLevelFinished += OnLevelFinished;
        scoreEvent.OnValueAdded += OnScoreAdded;
        pointsEvent.OnValueAdded += OnPointAdded;
    }

    void OnDisable()
    {
        gameEvents.OnMenuOpened -= OnMenuOpened;
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
    public void FinishGame() => gameEvents.FinishGame();

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

    void OnMenuOpened()
    {
        animation.Play("OpenMenu");
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
            animation.Play("GameOver");
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
