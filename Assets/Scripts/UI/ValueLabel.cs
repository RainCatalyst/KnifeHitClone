using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using EventChannels;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ValueLabel : MonoBehaviour
{
    [SerializeField] IntEventChannelSO valueEvent;
    TextMeshProUGUI textMesh;

    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        valueEvent.OnValueUpdated += OnValueUpdated;
    }

    void OnDisable()
    {
        valueEvent.OnValueUpdated -= OnValueUpdated;
    }

    void OnValueUpdated(int value) => textMesh.text = $"{value}";
}
