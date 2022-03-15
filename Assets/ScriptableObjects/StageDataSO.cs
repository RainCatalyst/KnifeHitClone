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
    [SerializeField] MinMaxTuple appleCountRange;
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
        var applePositions = GenerateApples(slots);
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
        float slotStep = 360f / slotCount;
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

    float[] GenerateApples(List<float> slots)
    {
        var appleCount = appleCountRange.GetRandom();
        var applePositions = new float[appleCount];

        for (int i = 0; i < appleCount; i++)
        {
            var slotIdx = Random.Range(0, slots.Count);

            applePositions[i] = slots[slotIdx];
            slots.RemoveAt(slotIdx);
        }

        return applePositions;
    }
}