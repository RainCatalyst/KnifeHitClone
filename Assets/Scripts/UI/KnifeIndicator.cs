using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EventChannels;

public class KnifeIndicator : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] BoundIntEventChannelSO knifeCountEvent;

    [Header("Properties")]
    [SerializeField] KnifeIcon knifeIconPrefab;

    List<KnifeIcon> knifeIcons = new List<KnifeIcon>();

    void OnEnable()
    {
        knifeCountEvent.OnMaxValueUpdated += OnMaxKnivesUpdated;
        knifeCountEvent.OnValueUpdated += OnKnivesUpdated;
    }

    void OnDisable()
    {
        knifeCountEvent.OnMaxValueUpdated -= OnMaxKnivesUpdated;
        knifeCountEvent.OnValueUpdated -= OnKnivesUpdated;
    }

    void OnMaxKnivesUpdated(int number)
    {
        // Create extra knives if needed
        var currentKnifeCount = knifeIcons.Count;
        for (int i = 0; i < number - currentKnifeCount; i++)
        {
            var knife = Instantiate(knifeIconPrefab, transform);
            knifeIcons.Add(knife);
        }

        for (int i = 0; i < knifeIcons.Count; i++)
        {
            var knife = knifeIcons[i];
            if (i < number) {
                knife.gameObject.SetActive(true);
                knife.SetEnabled(true);
            }else{
                knife.gameObject.SetActive(false);
            }
        }
    }

    void OnKnivesUpdated(int number)
    {
        for (int i = 0; i < knifeIcons.Count; i++)
        {
            var knife = knifeIcons[i];
            if (i > number - 1)
                knife.SetEnabled(false);
            else
                knife.SetEnabled(true);
        }
    }
    
}
