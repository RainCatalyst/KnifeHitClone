using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MinMaxTuple
{
    public int min;
    public int max;

    public int GetRandom() => Random.Range(min, max + 1);
}

[CreateAssetMenu(fileName = "NewStageData", menuName = "Level/Stage Data")]
public class StageDataSO : ScriptableObject
{
    [SerializeField] string stageName;
    [SerializeField] int levelCount;
    [SerializeField] LogDataSO logData;
    [SerializeField] KnifeDataSO knifeData;
    [SerializeField] float appleChance;
    [SerializeField] MinMaxTuple knifeCountRange;
    [SerializeField] MinMaxTuple hitCountRange;
    [SerializeField] AnimationCurve[] animationCurves;
    [SerializeField] LevelDataSO[] bosses;

    public string Name => stageName;
    public int LevelCount => levelCount;

    readonly int slotCount = 10;

    public LevelDataSO CreateLevel()
    {
        var slots = CreateSlots();
        var knifePositions = GenerateKnives(slots);
        var applePositions = new float[0];
        if (Random.value < appleChance)
            applePositions = GenerateApple(slots);
        
        int hitCount = hitCountRange.GetRandom();
        var rotationCurve = animationCurves[Random.Range(0, animationCurves.Length)];

        var level = ScriptableObject.CreateInstance<LevelDataSO>();
        level.Init("RandomLevel", hitCount, logData, knifeData, knifePositions, applePositions, rotationCurve);

        return level;
    }

    public LevelDataSO GetBossLevel()
    {
        return bosses[Random.Range(0, bosses.Length)];
    }

    List<float> CreateSlots()
    {
        var slots = new List<float>();
        float slotStep = 360f / (slotCount);
        for (int i = 0; i < slotCount; i++)
        {
            slots.Add(i * slotStep);
        }

        return slots;
    }

    float[] GenerateKnives(List<float> slots)
    {
        var knifeCount = knifeCountRange.GetRandom();
        var knifePositions = new float[knifeCount];

        for (int i = 0; i < knifeCount; i++)
        {
            // TODO: Improve slots (adjacency etc)
            var slotIdx = Random.Range(0, slots.Count);

            knifePositions[i] = slots[slotIdx];
            slots.RemoveAt(slotIdx);
        }

        return knifePositions;
    }

    float[] GenerateApple(List<float> slots)
    {
        var appleCount = 1;
        var applePositions = new float[appleCount];

        var slotIdx = Random.Range(0, slots.Count);
        applePositions[0] = slots[slotIdx];
        slots.RemoveAt(slotIdx);

        return applePositions;
    }
}