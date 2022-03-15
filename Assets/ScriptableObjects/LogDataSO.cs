using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLogData", menuName = "Level/Log Data")]
public class LogDataSO : ScriptableObject
{
    [SerializeField] string logName;
    [SerializeField] Sprite sprite;
    [SerializeField] float radius;
    [SerializeField] GameObject destroyEffect;
    [SerializeField] GameObject hitEffect;

    public string Name => logName;
    public Sprite Sprite => sprite;
    public float Radius => radius;
    public GameObject DestroyEffect => destroyEffect;
    public GameObject HitEffect => hitEffect;

    public Log CreateObject(Log logPrefab)
    {
        var log = Instantiate(logPrefab);
        log.AssignData(this);
        return log;
    }
}
