using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EventChannels;

public class Player : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] GameEventChannelSO gameEvents;
    [SerializeField] LevelEventChannelSO levelEvents; 

    [Header("Knife Properties")]
    [SerializeField] Knife knifePrefab;
    [SerializeField] KnifeDataSO knifeData;
    [SerializeField] Transform knifePoint;

    [Header("Throw Properties")]
    [SerializeField] float throwVelocity = 5f;
    [SerializeField] float throwDelay = 0.1f;

    bool inputEnabled = false;
    bool throwQueued;
    float throwTimer;
    int currentKnifeCount;
    Knife currentKnife;

    void OnEnable()
    {
        gameEvents.OnInputToggled += OnInputToggled;
        gameEvents.OnLevelStarted += OnLevelStarted;
        gameEvents.OnLevelFinished += OnLevelFinished;
        // levelEvents.OnMaxKnivesUpdated += OnMaxKnivesUpdated;
        levelEvents.OnKnivesUpdated += OnKnivesUpdated;
    }

    void OnDisable()
    {
        gameEvents.OnInputToggled -= OnInputToggled;
        gameEvents.OnLevelStarted -= OnLevelStarted;
        gameEvents.OnLevelFinished -= OnLevelFinished;
        // levelEvents.OnMaxKnivesUpdated -= OnMaxKnivesUpdated;
        levelEvents.OnKnivesUpdated -= OnKnivesUpdated;
    }

    void Update()
    {
        if (!inputEnabled)
            return;

        throwTimer -= Time.deltaTime;
        if (throwTimer < 0f) {
            if (throwQueued) {
                ThrowKnife();
                throwQueued = false;
                throwTimer = throwDelay;
            }else{
                throwTimer = 0f;
            }
        }
        
        if (Input.GetMouseButtonDown(0)) {
            if (throwTimer == 0f) {
                ThrowKnife();
                throwTimer = throwDelay;
            }else{
                throwQueued = true;
            }
        }
    }

    void ReadyKnife()
    {
        currentKnife = knifeData.CreateObject(knifePrefab);
        currentKnife.transform.position = knifePoint.position;
        // Do knife animations
        currentKnife.Ready();
    }

    void HideKnife()
    {
        if (currentKnife != null)
            Destroy(currentKnife.gameObject);
    }

    void ThrowKnife()
    {
        if (currentKnife == null)
            return;
        currentKnife.Throw(throwVelocity);
        currentKnife = null;
        
        levelEvents.ThrowKnife();

        if (currentKnifeCount > 1)
            ReadyKnife();
    }

    void OnLevelStarted()
    {
        ReadyKnife();
    }

    void OnLevelFinished(bool win)
    {
        HideKnife();
    }

    void OnInputToggled(bool enable)
    {
        inputEnabled = enable;
    }

    void OnKnivesUpdated(int number)
    {
        currentKnifeCount = number;
    }
}
