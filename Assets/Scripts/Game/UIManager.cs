using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

using EventChannels;
using UI;

public class UIManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] GameEventChannelSO gameEvents;
    [SerializeField] BoolEventChannelSO toggleInputEvent;

    [Header("UI")]
    [SerializeField] EventSystem eventSystem;
    [SerializeField] UIPanel menuPanel;
    [SerializeField] UIPanel levelPanel;
    [SerializeField] UIPanel gameOverPanel;

    void OnEnable()
    {
        toggleInputEvent.OnEventRaised += OnInputToggled;
    }

    void OnDisable()
    {
        toggleInputEvent.OnEventRaised -= OnInputToggled;
    }

    public void OpenMenuPanel() => menuPanel.Open();
    public void CloseMenuPanel() => menuPanel.Close();
    public void OpenLevelPanel() => levelPanel.Open();
    public void CloseLevelPanel() => levelPanel.Close();
    public void OpenGameOverPanel() => gameOverPanel.Open();
    public void CloseGameOverPanel() => gameOverPanel.Close();

    void OnInputToggled(bool enable) => eventSystem.enabled = enable;
}
