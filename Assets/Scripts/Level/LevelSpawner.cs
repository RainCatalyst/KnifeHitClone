using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] Transform levelHolder;
    [SerializeField] Log logPrefab;
    [SerializeField] Knife knifePrefab;
    [SerializeField] Apple applePrefab;
    
    Log currentLevel;

    public void SpawnLevel(LevelDataSO levelData)
    {
        // Spawn level from data
        currentLevel = levelData.CreateObject(logPrefab, knifePrefab, applePrefab);
        // currentLevel.transform.position = levelHolder.position;
        currentLevel.transform.SetParent(levelHolder, false);
    }

    public void StopLevel()
    {
        currentLevel?.Stop();
    }

    public void HideLevel()
    {
        currentLevel?.Hide();
    }
}
