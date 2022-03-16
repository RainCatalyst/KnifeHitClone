using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EventChannels;

[RequireComponent(typeof(LevelSpawner))]
public class LevelManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] GameEventChannelSO gameEvents;
    [SerializeField] LevelEventChannelSO levelEvents;
    [SerializeField] BoundIntEventChannelSO knifeCountEvent;
    [SerializeField] IntEventChannelSO scoreEvent;
    [SerializeField] IntEventChannelSO maxScoreEvent;
    [SerializeField] IntEventChannelSO pointsEvent;

    [Header("Stage Properties")]
    [SerializeField] StageDataSO[] stages;

    LevelSpawner levelSpawner;

    StageDataSO currentStage;
    int stageIdx;
    int levelIdx;
    int currentKnives;

    void Awake()
    {
        levelSpawner = GetComponent<LevelSpawner>();
    }

    void OnEnable()
    {
        gameEvents.OnGameStarted += OnGameStarted;
        gameEvents.OnGameFinished += OnGameFinished;
        gameEvents.OnLevelStarted += OnLevelStarted;
        gameEvents.OnLevelFinished += OnLevelFinished;
        levelEvents.OnLogDestroyed += OnLogDestroyed;
        levelEvents.OnKnifeDeflected += OnKnifeDeflected;
        levelEvents.OnAppleDestroyed += OnAppleDestroyed;
    }

    void OnDisable()
    {
        gameEvents.OnGameStarted -= OnGameStarted;
        gameEvents.OnGameFinished -= OnGameFinished;
        gameEvents.OnLevelStarted -= OnLevelStarted;
        gameEvents.OnLevelFinished -= OnLevelFinished;
        levelEvents.OnLogDestroyed -= OnLogDestroyed;
        levelEvents.OnKnifeDeflected -= OnKnifeDeflected;
        levelEvents.OnAppleDestroyed -= OnAppleDestroyed;
    }

    void OnGameStarted()
    {
        // Happens before level start
        stageIdx = 0;
        levelIdx = 0;
        currentStage = stages[stageIdx];
    }

    void OnGameFinished()
    {
        // Happens after level end
        levelSpawner.HideLevel();
    }

    void OnLevelStarted()
    {
        // Get LevelData from Stage
        LevelDataSO levelData;
        if (levelIdx < currentStage.LevelCount)
            levelData = currentStage.CreateLevel();
        else
            levelData = currentStage.GetBossLevel();
        levelSpawner.SpawnLevel(levelData);
        knifeCountEvent.UpdateMaxValue(levelData.HitCount);
    }

    void OnLevelFinished(bool win)
    {
        if (!win)
            levelSpawner.StopLevel();
        else {
            levelIdx++;
            if (levelIdx > currentStage.LevelCount) {
                stageIdx = Mathf.Clamp(stageIdx + 1, 0, stages.Length - 1);
                currentStage = stages[stageIdx];
                levelIdx = 0;
            }
            scoreEvent.AddValue();
        }
            
    }

    void OnLogDestroyed()
    {
        gameEvents.FinishLevel(true);
    }

    void OnKnifeDeflected()
    {
        gameEvents.FinishLevel(false);
    }

    void OnAppleDestroyed()
    {
        pointsEvent.AddValue();
    }
}
