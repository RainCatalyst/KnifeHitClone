using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewKnifeData", menuName = "Knife/Knife Data")]
public class KnifeDataSO : ScriptableObject
{
    [SerializeField] string knifeName;
    [SerializeField] Sprite sprite;
    [SerializeField] GameObject hitEffect;

    public string Name => knifeName;
    public Sprite Sprite => sprite;
    public GameObject Effect => hitEffect;

    public Knife CreateObject(Knife knifePrefab)
    {
        var knife = Instantiate(knifePrefab);
        knife.AssignData(this);
        return knife;
    }
}
