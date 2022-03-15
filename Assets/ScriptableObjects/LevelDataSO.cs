using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "Level/Level Data")]
public class LevelDataSO : ScriptableObject
{
    [SerializeField] string levelName;
    [SerializeField] int hitCount;
    [SerializeField] LogDataSO logData;
    [SerializeField] KnifeDataSO knifeData;
    [SerializeField] float[] knifePositions;
    [SerializeField] float[] applePositions;
    [SerializeField] AnimationCurve rotationCurve;

    public string Name => levelName;
    public int HitCount => hitCount;

    public void Init(string levelName, int hitCount, LogDataSO logData, KnifeDataSO knifeData,
        float[] knifePositions, float[] applePositions, AnimationCurve rotationCurve)
    {
        this.levelName = levelName;
        this.hitCount = hitCount;
        this.logData = logData;
        this.knifeData = knifeData;
        this.knifePositions = knifePositions;
        this.applePositions = applePositions;
        this.rotationCurve = rotationCurve;
    }

    public Log CreateObject(Log logPrefab, Knife knifePrefab, Apple applePrefab)
    {
        var log = logData.CreateObject(logPrefab);
        log.SetRotationCurve(rotationCurve);
        // Setup log parameters (animation etc)
        
        foreach (float knifePosition in knifePositions)
        {
            var knife = knifeData.CreateObject(knifePrefab);
            // Add knives to the log
            log.AddKnife(knife, Mathf.Deg2Rad * knifePosition);
        }

        foreach (float applePosition in applePositions)
        {
            // Add apples to the log
            var apple = Instantiate(applePrefab);
            log.AddApple(apple, Mathf.Deg2Rad * applePosition);
        }

        return log;
    }

}